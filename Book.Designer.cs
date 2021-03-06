
namespace DataBase
{
	partial class Book
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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.readerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.bookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.dataGridView1 = new System.Windows.Forms.DataGridView();
			this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this._btnDelete = new System.Windows.Forms.Button();
			this._btnInput = new System.Windows.Forms.Button();
			this._txtDataPublish = new System.Windows.Forms.TextBox();
			this._txtCountPage = new System.Windows.Forms.TextBox();
			this._txtPasswData = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._txtSection = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this._btnAddToBD = new System.Windows.Forms.Button();
			this._btnView = new System.Windows.Forms.Button();
			this._btnRedactor = new System.Windows.Forms.Button();
			this._btnReadFromDB = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readerToolStripMenuItem,
            this.bookToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(1303, 28);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// readerToolStripMenuItem
			// 
			this.readerToolStripMenuItem.Name = "readerToolStripMenuItem";
			this.readerToolStripMenuItem.Size = new System.Drawing.Size(70, 24);
			this.readerToolStripMenuItem.Text = "Reader";
			this.readerToolStripMenuItem.Click += new System.EventHandler(this.readerToolStripMenuItem_Click);
			// 
			// bookToolStripMenuItem
			// 
			this.bookToolStripMenuItem.Name = "bookToolStripMenuItem";
			this.bookToolStripMenuItem.Size = new System.Drawing.Size(77, 24);
			this.bookToolStripMenuItem.Text = "Register";
			this.bookToolStripMenuItem.Click += new System.EventHandler(this.bookToolStripMenuItem_Click);
			// 
			// dataGridView1
			// 
			this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3,
            this.Column4});
			this.dataGridView1.Location = new System.Drawing.Point(531, 49);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(748, 293);
			this.dataGridView1.TabIndex = 1;
			this.dataGridView1.RowPrePaint += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dbPrePaint);
			// 
			// Column1
			// 
			this.Column1.HeaderText = "Регистрационный номер";
			this.Column1.MinimumWidth = 6;
			this.Column1.Name = "Column1";
			this.Column1.Width = 125;
			// 
			// Column2
			// 
			this.Column2.HeaderText = "Количество страниц";
			this.Column2.MinimumWidth = 6;
			this.Column2.Name = "Column2";
			this.Column2.Width = 125;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Год издания";
			this.Column3.MinimumWidth = 6;
			this.Column3.Name = "Column3";
			this.Column3.Width = 125;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Раздел";
			this.Column4.MinimumWidth = 6;
			this.Column4.Name = "Column4";
			this.Column4.Width = 125;
			// 
			// _btnDelete
			// 
			this._btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnDelete.Location = new System.Drawing.Point(17, 224);
			this._btnDelete.Name = "_btnDelete";
			this._btnDelete.Size = new System.Drawing.Size(151, 35);
			this._btnDelete.TabIndex = 17;
			this._btnDelete.Text = "Удалить";
			this._btnDelete.UseVisualStyleBackColor = true;
			this._btnDelete.Click += new System.EventHandler(this._btnDelete_Click);
			// 
			// _btnInput
			// 
			this._btnInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnInput.Location = new System.Drawing.Point(271, 224);
			this._btnInput.Name = "_btnInput";
			this._btnInput.Size = new System.Drawing.Size(202, 35);
			this._btnInput.TabIndex = 16;
			this._btnInput.Text = "Добавить";
			this._btnInput.UseVisualStyleBackColor = true;
			this._btnInput.Click += new System.EventHandler(this._btnInput_Click);
			// 
			// _txtDataPublish
			// 
			this._txtDataPublish.Location = new System.Drawing.Point(271, 127);
			this._txtDataPublish.Name = "_txtDataPublish";
			this._txtDataPublish.Size = new System.Drawing.Size(202, 22);
			this._txtDataPublish.TabIndex = 15;
			this._txtDataPublish.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this._txtDataPublish.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtDataPublish_KeyPress);
			// 
			// _txtCountPage
			// 
			this._txtCountPage.Location = new System.Drawing.Point(271, 88);
			this._txtCountPage.Name = "_txtCountPage";
			this._txtCountPage.Size = new System.Drawing.Size(202, 22);
			this._txtCountPage.TabIndex = 14;
			this._txtCountPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this._txtCountPage.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtCountPage_KeyPress);
			// 
			// _txtPasswData
			// 
			this._txtPasswData.Location = new System.Drawing.Point(271, 49);
			this._txtPasswData.Name = "_txtPasswData";
			this._txtPasswData.Size = new System.Drawing.Size(202, 22);
			this._txtPasswData.TabIndex = 13;
			this._txtPasswData.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this._txtPasswData.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtPasswData_KeyPress);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label3.Location = new System.Drawing.Point(13, 129);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(122, 20);
			this.label3.TabIndex = 12;
			this.label3.Text = "Год издания:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label2.Location = new System.Drawing.Point(12, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(188, 20);
			this.label2.TabIndex = 11;
			this.label2.Text = "Количество страниц:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label1.Location = new System.Drawing.Point(12, 49);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(221, 20);
			this.label1.TabIndex = 10;
			this.label1.Text = "Регистрационный номер:";
			// 
			// _txtSection
			// 
			this._txtSection.Location = new System.Drawing.Point(271, 167);
			this._txtSection.Name = "_txtSection";
			this._txtSection.Size = new System.Drawing.Size(202, 22);
			this._txtSection.TabIndex = 19;
			this._txtSection.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this._txtSection.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtSection_KeyPress);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label4.Location = new System.Drawing.Point(13, 169);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(76, 20);
			this.label4.TabIndex = 18;
			this.label4.Text = "Раздел:";
			// 
			// _btnAddToBD
			// 
			this._btnAddToBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnAddToBD.Location = new System.Drawing.Point(17, 291);
			this._btnAddToBD.Name = "_btnAddToBD";
			this._btnAddToBD.Size = new System.Drawing.Size(456, 51);
			this._btnAddToBD.TabIndex = 20;
			this._btnAddToBD.Text = "Добавить в базу данных";
			this._btnAddToBD.UseVisualStyleBackColor = true;
			this._btnAddToBD.Click += new System.EventHandler(this._btnAddToBD_Click);
			// 
			// _btnView
			// 
			this._btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnView.Location = new System.Drawing.Point(1053, 367);
			this._btnView.Name = "_btnView";
			this._btnView.Size = new System.Drawing.Size(226, 39);
			this._btnView.TabIndex = 22;
			this._btnView.Text = "Режим просмотра";
			this._btnView.UseVisualStyleBackColor = true;
			this._btnView.Click += new System.EventHandler(this._btnView_Click);
			// 
			// _btnRedactor
			// 
			this._btnRedactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnRedactor.Location = new System.Drawing.Point(531, 367);
			this._btnRedactor.Name = "_btnRedactor";
			this._btnRedactor.Size = new System.Drawing.Size(254, 39);
			this._btnRedactor.TabIndex = 21;
			this._btnRedactor.Text = "Режим редактирования";
			this._btnRedactor.UseVisualStyleBackColor = true;
			this._btnRedactor.Click += new System.EventHandler(this._btnRedactor_Click);
			// 
			// _btnReadFromDB
			// 
			this._btnReadFromDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnReadFromDB.Location = new System.Drawing.Point(17, 358);
			this._btnReadFromDB.Name = "_btnReadFromDB";
			this._btnReadFromDB.Size = new System.Drawing.Size(456, 48);
			this._btnReadFromDB.TabIndex = 23;
			this._btnReadFromDB.Text = "Считать из базы данных";
			this._btnReadFromDB.UseVisualStyleBackColor = true;
			this._btnReadFromDB.Click += new System.EventHandler(this._btnReadFromDB_Click);
			// 
			// Book
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1303, 458);
			this.Controls.Add(this._btnReadFromDB);
			this.Controls.Add(this._btnView);
			this.Controls.Add(this._btnRedactor);
			this.Controls.Add(this._btnAddToBD);
			this.Controls.Add(this._txtSection);
			this.Controls.Add(this.label4);
			this.Controls.Add(this._btnDelete);
			this.Controls.Add(this._btnInput);
			this.Controls.Add(this._txtDataPublish);
			this.Controls.Add(this._txtCountPage);
			this.Controls.Add(this._txtPasswData);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Book";
			this.Text = "Book";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem readerToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem bookToolStripMenuItem;
		private System.Windows.Forms.DataGridView dataGridView1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
		private System.Windows.Forms.Button _btnDelete;
		private System.Windows.Forms.Button _btnInput;
		private System.Windows.Forms.TextBox _txtDataPublish;
		private System.Windows.Forms.TextBox _txtCountPage;
		private System.Windows.Forms.TextBox _txtPasswData;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox _txtSection;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button _btnAddToBD;
		private System.Windows.Forms.Button _btnView;
		private System.Windows.Forms.Button _btnRedactor;
		private System.Windows.Forms.Button _btnReadFromDB;
	}
}