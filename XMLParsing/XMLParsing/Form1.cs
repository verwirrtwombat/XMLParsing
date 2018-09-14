using System;
using System.Net;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Configuration;
using log4net;
using System.Text.RegularExpressions;


namespace XMLParsing
{
    public partial class XMLParser : Form
    {
        DataSet ds = new DataSet();
        bool canSave = true; //value to determine if it can be saved to a new XML, functionality disabled so always saves.

        private static readonly ILog Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public XMLParser()
        {
            InitializeComponent();
            Logger.Info("Program Started");
        }

        private void UploadXML_Click(object sender, EventArgs e)
        {
            OpenFileDialog odialog = new OpenFileDialog
            {
                Title = "Open XML file",
                Filter = "XML Files (*.xml)|*.xml",
                InitialDirectory = ConfigurationManager.AppSettings["FileLocation"].ToString(),
                FilterIndex = 0,
                DefaultExt = "xml"
            };
            if (odialog.ShowDialog() == DialogResult.OK)
            {
                if (!String.Equals(Path.GetExtension(odialog.FileName), ".xml", StringComparison.OrdinalIgnoreCase))
                {
                    MessageBox.Show("The type of file is not an *.XML.  Please select an *.XML type file.",
                        "Invalid File Type",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    Logger.Warn("Unable to load *.XML " + odialog.FileName);
                }
                else
                {
                    try
                    {
                        XmlReader xmlFile = XmlReader.Create(odialog.FileName, new XmlReaderSettings());
                        ds.ReadXml(xmlFile);
                        gridView.DataSource = ds.Tables[1];
                        Logger.Info(odialog.FileName + " has been parsed.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("The *.XML file could not be read. " + ex, "Invalid XML file",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                        Logger.Error(odialog.FileName + " could not be read: " + ex);
                    }
                }
            }
        }

        private void SaveXML_Click(object sender, EventArgs e)
        {
            SaveFileDialog sdialog = new SaveFileDialog
            {
                Title = "Save XML file",
                Filter = "XML Files (*.xml)|*.xml",
                InitialDirectory = ConfigurationManager.AppSettings["FileLocation"].ToString(),
                FilterIndex = 0,
                DefaultExt = "xml"
            };
            if (sdialog.ShowDialog() == DialogResult.OK)
            {
                //if else statement about canSave boolean value to determine if the datagridview is complete and can be saved to an XML.  Data cannot be saved if it is missing values or values are wrong.  Currently disabled so can save with errors.
                if (canSave)
                {
                    try
                    {
                        XmlWriterSettings settings = new XmlWriterSettings();
                        settings.Indent = true;

                        using (XmlWriter writer = XmlWriter.Create(sdialog.FileName, settings))
                        {
                            int count = gridView.Rows.Count;
                            writer.WriteStartDocument();
                            writer.WriteStartElement("ExampleObjectContainer");
                            writer.WriteAttributeString("xmlns", "xsi", null, "http://www.w3.org/2001/XMLSchema-instance");
                            writer.WriteAttributeString("xmlns", "xsd", null, "http://www.w3.org/2001/XMLSchema");
                            writer.WriteStartElement("Objects");
                            foreach (DataGridViewRow row in gridView.Rows)
                            {
                                if (!row.IsNewRow)
                                {
                                    writer.WriteStartElement("ExampleObject");
                                    writer.WriteAttributeString("Name", row.Cells["Name"].Value.ToString());
                                    writer.WriteAttributeString("DOB", row.Cells["DOB"].Value.ToString());
                                    writer.WriteAttributeString("NumberOfWidgets", row.Cells["NumberOfWidgets"].Value.ToString());
                                    writer.WriteEndElement();
                                }
                            }
                            writer.WriteEndElement();
                            writer.WriteEndElement();
                            writer.WriteEndDocument();
                        }
                        //ds.WriteXml(sdialog.FileName);

                        MessageBox.Show("The *.XML file was successfully saved as " + sdialog.FileName);
                        Logger.Info("New XML saved as " + sdialog.FileName);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("The *.XML file could not be written. " + ex, "Unable to Save File",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                        Logger.Error(sdialog.FileName + " could not be written: " + ex);
                    }
                }
                else
                {
                    MessageBox.Show("The *.XML file could not be written because the data has error/s.");
                    Logger.Warn(sdialog.FileName + " could not be written because the data has error/s.");
                }
            }
        }

        private void gridView_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            //canSave = false;

            string columnName = gridView.Columns[e.ColumnIndex].Name;

            if (columnName.Equals("Name"))
            {
                //Is this empty?
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    gridView.Rows[e.RowIndex].ErrorText = "Name cannot be empty.";
                    e.Cancel = true;
                }
                else
                {
                    //Is this a string?
                    if (e.FormattedValue.ToString() is string)
                    {
                        //canSave = true;
                    }
                    else
                    {
                        gridView.Rows[e.RowIndex].ErrorText = "Name must be a string.";
                        e.Cancel = true;
                    }
                }
            }
            else if (columnName.Equals("DOB"))
            {
                //Is this empty?
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    gridView.Rows[e.RowIndex].ErrorText = "DOB cannot be empty.";
                    e.Cancel = true;
                }
                else
                {
                    //Is this a properly formated UTC date without timezone corrections?
                    //1970-01-02T00:00:00 <--example
                    Regex datePattern = new Regex("^(-?(?:[1-9][0-9]*)?[0-9]{4})-(1[0-2]|0[1-9])-(3[01]|0[1-9]|[12][0-9])T(2[0-3]|[01][0-9]):([0-5][0-9]):([0-5][0-9])(\\.[0-9]+)?$");
                    if (!datePattern.IsMatch(e.FormattedValue.ToString()))
                    {
                        gridView.Rows[e.RowIndex].ErrorText = "DOB must be in UTC format.";
                        e.Cancel = true;
                    }
                    else
                    {
                        //canSave = true;
                    }
                }
            }
            else if (columnName.Equals("NumberOfWidgets"))
            {
                //Is this empty?
                if (string.IsNullOrEmpty(e.FormattedValue.ToString()))
                {
                    gridView.Rows[e.RowIndex].ErrorText = "NumberOfWidgets cannot be empty.";
                    e.Cancel = true;
                }
                else
                {
                    //Is this an integer with a value 0 or more?
                    if (!int.TryParse(e.FormattedValue.ToString(), out int newInteger) || newInteger <= 0)
                    {
                        gridView.Rows[e.RowIndex].ErrorText = "NumberOfWidgets must be a positive integer.";
                        e.Cancel = true;
                    }
                    else
                    {
                        //canSave = true;
                    }
                }
            }
        }

        private void gridView_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            gridView.Rows[e.RowIndex].ErrorText = string.Empty;
        }
    }
}
