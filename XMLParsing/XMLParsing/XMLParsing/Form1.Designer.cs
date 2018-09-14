namespace XMLParsing
{
    partial class XMLParser
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.OpenXML = new System.Windows.Forms.Button();
            this.gridView = new System.Windows.Forms.DataGridView();
            this.saveXML = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // OpenXML
            // 
            this.OpenXML.Location = new System.Drawing.Point(12, 309);
            this.OpenXML.Name = "OpenXML";
            this.OpenXML.Size = new System.Drawing.Size(189, 20);
            this.OpenXML.TabIndex = 1;
            this.OpenXML.Text = "Open XML";
            this.OpenXML.UseVisualStyleBackColor = true;
            this.OpenXML.Click += new System.EventHandler(this.UploadXML_Click);
            // 
            // gridView
            // 
            this.gridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.gridView.Location = new System.Drawing.Point(12, 38);
            this.gridView.Name = "gridView";
            this.gridView.Size = new System.Drawing.Size(671, 265);
            this.gridView.TabIndex = 2;
            this.gridView.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.gridView_CellEndEdit);
            this.gridView.CellValidating += new System.Windows.Forms.DataGridViewCellValidatingEventHandler(this.gridView_CellValidating);
            // 
            // saveXML
            // 
            this.saveXML.Location = new System.Drawing.Point(494, 309);
            this.saveXML.Name = "saveXML";
            this.saveXML.Size = new System.Drawing.Size(189, 20);
            this.saveXML.TabIndex = 3;
            this.saveXML.Text = "Save XML";
            this.saveXML.UseVisualStyleBackColor = true;
            this.saveXML.Click += new System.EventHandler(this.SaveXML_Click);
            // 
            // XMLParser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 420);
            this.Controls.Add(this.saveXML);
            this.Controls.Add(this.gridView);
            this.Controls.Add(this.OpenXML);
            this.Name = "XMLParser";
            this.Text = "XMLParser";
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button OpenXML;
        private System.Windows.Forms.DataGridView gridView;
        private System.Windows.Forms.Button saveXML;
    }
}

