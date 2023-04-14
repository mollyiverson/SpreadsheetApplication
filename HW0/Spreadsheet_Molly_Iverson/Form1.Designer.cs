namespace Spreadsheet_Molly_Iverson
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileOption = new System.Windows.Forms.ToolStripMenuItem();
            this.saveSpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadSpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSpreadsheetToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editOption = new System.Windows.Forms.ToolStripMenuItem();
            this.undoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cellOption = new System.Windows.Forms.ToolStripMenuItem();
            this.changeBackgroundColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 36);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 62;
            this.dataGridView1.RowTemplate.Height = 33;
            this.dataGridView1.Size = new System.Drawing.Size(1180, 631);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellBeginEdit += new System.Windows.Forms.DataGridViewCellCancelEventHandler(this.DataGridView1_CellBeginEdit);
            this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.DataGridView1_CellEndEdit);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(3, 673);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(1180, 54);
            this.button1.TabIndex = 1;
            this.button1.Tag = "DemoButton";
            this.button1.Text = "Demo Button";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.DemoButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileOption,
            this.editOption,
            this.cellOption});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1188, 33);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileOption
            // 
            this.fileOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveSpreadsheetToolStripMenuItem,
            this.loadSpreadsheetToolStripMenuItem,
            this.clearSpreadsheetToolStripMenuItem});
            this.fileOption.Name = "fileOption";
            this.fileOption.Size = new System.Drawing.Size(54, 29);
            this.fileOption.Text = "File";
            // 
            // saveSpreadsheetToolStripMenuItem
            // 
            this.saveSpreadsheetToolStripMenuItem.Name = "saveSpreadsheetToolStripMenuItem";
            this.saveSpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.saveSpreadsheetToolStripMenuItem.Text = "Save spreadsheet";
            this.saveSpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.SaveSpreadsheet_Click);
            // 
            // loadSpreadsheetToolStripMenuItem
            // 
            this.loadSpreadsheetToolStripMenuItem.Name = "loadSpreadsheetToolStripMenuItem";
            this.loadSpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.loadSpreadsheetToolStripMenuItem.Text = "Load spreadsheet";
            this.loadSpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.LoadSpreadsheet_Click);
            // 
            // clearSpreadsheetToolStripMenuItem
            // 
            this.clearSpreadsheetToolStripMenuItem.Name = "clearSpreadsheetToolStripMenuItem";
            this.clearSpreadsheetToolStripMenuItem.Size = new System.Drawing.Size(270, 34);
            this.clearSpreadsheetToolStripMenuItem.Text = "Clear spreadsheet";
            this.clearSpreadsheetToolStripMenuItem.Click += new System.EventHandler(this.ClearSpreadsheetToolStripMenuItem_Click);
            // 
            // editOption
            // 
            this.editOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoMenuItem,
            this.redoMenuItem});
            this.editOption.Name = "editOption";
            this.editOption.Size = new System.Drawing.Size(58, 29);
            this.editOption.Text = "Edit";
            // 
            // undoMenuItem
            // 
            this.undoMenuItem.Name = "undoMenuItem";
            this.undoMenuItem.Size = new System.Drawing.Size(158, 34);
            this.undoMenuItem.Text = "Undo";
            this.undoMenuItem.Click += new System.EventHandler(this.UndoTextChangesToolStripMenuItem_Click);
            // 
            // redoMenuItem
            // 
            this.redoMenuItem.Name = "redoMenuItem";
            this.redoMenuItem.Size = new System.Drawing.Size(158, 34);
            this.redoMenuItem.Text = "Redo";
            this.redoMenuItem.Click += new System.EventHandler(this.RedoToolStripMenuItem_Click);
            // 
            // cellOption
            // 
            this.cellOption.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.changeBackgroundColorToolStripMenuItem});
            this.cellOption.Name = "cellOption";
            this.cellOption.Size = new System.Drawing.Size(56, 29);
            this.cellOption.Text = "Cell";
            // 
            // changeBackgroundColorToolStripMenuItem
            // 
            this.changeBackgroundColorToolStripMenuItem.Name = "changeBackgroundColorToolStripMenuItem";
            this.changeBackgroundColorToolStripMenuItem.Size = new System.Drawing.Size(320, 34);
            this.changeBackgroundColorToolStripMenuItem.Text = "Change background color";
            this.changeBackgroundColorToolStripMenuItem.Click += new System.EventHandler(this.ChangeBackgroundColorToolStripMenuItem_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Filter = "XML Files (*.xml)|*.xml";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Filter = "XML Files (*.xml)|*.xml";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1188, 728);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DataGridView dataGridView1;
        private Button button1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileOption;
        private ToolStripMenuItem editOption;
        private ToolStripMenuItem cellOption;
        private ToolStripMenuItem undoMenuItem;
        private ToolStripMenuItem redoMenuItem;
        private ToolStripMenuItem changeBackgroundColorToolStripMenuItem;
        private ColorDialog colorDialog1;
        private ToolStripMenuItem saveSpreadsheetToolStripMenuItem;
        private ToolStripMenuItem loadSpreadsheetToolStripMenuItem;
        private OpenFileDialog openFileDialog1;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem clearSpreadsheetToolStripMenuItem;
    }
}