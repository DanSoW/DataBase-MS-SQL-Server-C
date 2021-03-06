﻿
namespace DataBase
{
	partial class Register
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
			this._btnAddToBD = new System.Windows.Forms.Button();
			this._txtDataReturn = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this._btnDelete = new System.Windows.Forms.Button();
			this._btnInput = new System.Windows.Forms.Button();
			this._txtDataIssue = new System.Windows.Forms.TextBox();
			this._txtPasswData2 = new System.Windows.Forms.TextBox();
			this._txtPasswData1 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this._btnReadFromDB = new System.Windows.Forms.Button();
			this._btnView = new System.Windows.Forms.Button();
			this._btnRedactor = new System.Windows.Forms.Button();
			this.dataGridView2 = new System.Windows.Forms.DataGridView();
			this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column9 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Column10 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.label5 = new System.Windows.Forms.Label();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
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
			this.menuStrip1.Size = new System.Drawing.Size(1265, 30);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// readerToolStripMenuItem
			// 
			this.readerToolStripMenuItem.Name = "readerToolStripMenuItem";
			this.readerToolStripMenuItem.Size = new System.Drawing.Size(70, 26);
			this.readerToolStripMenuItem.Text = "Reader";
			this.readerToolStripMenuItem.Click += new System.EventHandler(this.readerToolStripMenuItem_Click);
			// 
			// bookToolStripMenuItem
			// 
			this.bookToolStripMenuItem.Name = "bookToolStripMenuItem";
			this.bookToolStripMenuItem.Size = new System.Drawing.Size(57, 26);
			this.bookToolStripMenuItem.Text = "Book";
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
			this.dataGridView1.Location = new System.Drawing.Point(519, 65);
			this.dataGridView1.Name = "dataGridView1";
			this.dataGridView1.RowHeadersWidth = 51;
			this.dataGridView1.RowTemplate.Height = 24;
			this.dataGridView1.Size = new System.Drawing.Size(734, 292);
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
			this.Column2.HeaderText = "Паспортные данные";
			this.Column2.MinimumWidth = 6;
			this.Column2.Name = "Column2";
			this.Column2.Width = 125;
			// 
			// Column3
			// 
			this.Column3.HeaderText = "Дата выдачи";
			this.Column3.MinimumWidth = 6;
			this.Column3.Name = "Column3";
			this.Column3.Width = 125;
			// 
			// Column4
			// 
			this.Column4.HeaderText = "Дата возврата";
			this.Column4.MinimumWidth = 6;
			this.Column4.Name = "Column4";
			this.Column4.Width = 125;
			// 
			// _btnAddToBD
			// 
			this._btnAddToBD.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnAddToBD.Location = new System.Drawing.Point(17, 292);
			this._btnAddToBD.Name = "_btnAddToBD";
			this._btnAddToBD.Size = new System.Drawing.Size(424, 51);
			this._btnAddToBD.TabIndex = 31;
			this._btnAddToBD.Text = "Добавить в базу данных";
			this._btnAddToBD.UseVisualStyleBackColor = true;
			this._btnAddToBD.Click += new System.EventHandler(this._btnAddToBD_Click);
			// 
			// _txtDataReturn
			// 
			this._txtDataReturn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._txtDataReturn.Location = new System.Drawing.Point(239, 183);
			this._txtDataReturn.Name = "_txtDataReturn";
			this._txtDataReturn.Size = new System.Drawing.Size(202, 26);
			this._txtDataReturn.TabIndex = 30;
			this._txtDataReturn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this._txtDataReturn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtDataReturn_KeyPress);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label4.Location = new System.Drawing.Point(13, 185);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(141, 20);
			this.label4.TabIndex = 29;
			this.label4.Text = "Дата возврата:";
			// 
			// _btnDelete
			// 
			this._btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnDelete.Location = new System.Drawing.Point(17, 225);
			this._btnDelete.Name = "_btnDelete";
			this._btnDelete.Size = new System.Drawing.Size(151, 35);
			this._btnDelete.TabIndex = 28;
			this._btnDelete.Text = "Удалить";
			this._btnDelete.UseVisualStyleBackColor = true;
			this._btnDelete.Click += new System.EventHandler(this._btnDelete_Click);
			// 
			// _btnInput
			// 
			this._btnInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnInput.Location = new System.Drawing.Point(239, 225);
			this._btnInput.Name = "_btnInput";
			this._btnInput.Size = new System.Drawing.Size(202, 35);
			this._btnInput.TabIndex = 27;
			this._btnInput.Text = "Добавить";
			this._btnInput.UseVisualStyleBackColor = true;
			this._btnInput.Click += new System.EventHandler(this._btnInput_Click);
			// 
			// _txtDataIssue
			// 
			this._txtDataIssue.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._txtDataIssue.Location = new System.Drawing.Point(239, 143);
			this._txtDataIssue.Name = "_txtDataIssue";
			this._txtDataIssue.Size = new System.Drawing.Size(202, 26);
			this._txtDataIssue.TabIndex = 26;
			this._txtDataIssue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this._txtDataIssue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtDataIssue_KeyPress);
			// 
			// _txtPasswData2
			// 
			this._txtPasswData2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._txtPasswData2.Location = new System.Drawing.Point(239, 104);
			this._txtPasswData2.Name = "_txtPasswData2";
			this._txtPasswData2.Size = new System.Drawing.Size(202, 26);
			this._txtPasswData2.TabIndex = 25;
			this._txtPasswData2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this._txtPasswData2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtPasswData2_KeyPress);
			// 
			// _txtPasswData1
			// 
			this._txtPasswData1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._txtPasswData1.Location = new System.Drawing.Point(239, 65);
			this._txtPasswData1.Name = "_txtPasswData1";
			this._txtPasswData1.Size = new System.Drawing.Size(202, 26);
			this._txtPasswData1.TabIndex = 24;
			this._txtPasswData1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this._txtPasswData1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this._txtPasswData1_KeyPress);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label3.Location = new System.Drawing.Point(13, 145);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(126, 20);
			this.label3.TabIndex = 23;
			this.label3.Text = "Дата выдачи:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label2.Location = new System.Drawing.Point(12, 104);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(187, 20);
			this.label2.TabIndex = 22;
			this.label2.Text = "Паспортные данные:";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label1.Location = new System.Drawing.Point(12, 65);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(221, 20);
			this.label1.TabIndex = 21;
			this.label1.Text = "Регистрационный номер:";
			// 
			// _btnReadFromDB
			// 
			this._btnReadFromDB.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnReadFromDB.Location = new System.Drawing.Point(17, 366);
			this._btnReadFromDB.Name = "_btnReadFromDB";
			this._btnReadFromDB.Size = new System.Drawing.Size(424, 48);
			this._btnReadFromDB.TabIndex = 32;
			this._btnReadFromDB.Text = "Считать из базы данных";
			this._btnReadFromDB.UseVisualStyleBackColor = true;
			this._btnReadFromDB.Click += new System.EventHandler(this._btnReadFromDB_Click);
			// 
			// _btnView
			// 
			this._btnView.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnView.Location = new System.Drawing.Point(1027, 375);
			this._btnView.Name = "_btnView";
			this._btnView.Size = new System.Drawing.Size(226, 39);
			this._btnView.TabIndex = 34;
			this._btnView.Text = "Режим просмотра";
			this._btnView.UseVisualStyleBackColor = true;
			this._btnView.Click += new System.EventHandler(this._btnView_Click);
			// 
			// _btnRedactor
			// 
			this._btnRedactor.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this._btnRedactor.Location = new System.Drawing.Point(519, 375);
			this._btnRedactor.Name = "_btnRedactor";
			this._btnRedactor.Size = new System.Drawing.Size(258, 39);
			this._btnRedactor.TabIndex = 33;
			this._btnRedactor.Text = "Режим редактирования";
			this._btnRedactor.UseVisualStyleBackColor = true;
			this._btnRedactor.Click += new System.EventHandler(this._btnRedactor_Click);
			// 
			// dataGridView2
			// 
			this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.dataGridView2.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column5,
            this.Column6,
            this.Column7,
            this.Column8,
            this.Column9,
            this.Column10});
			this.dataGridView2.Location = new System.Drawing.Point(17, 510);
			this.dataGridView2.Name = "dataGridView2";
			this.dataGridView2.RowHeadersWidth = 51;
			this.dataGridView2.RowTemplate.Height = 24;
			this.dataGridView2.Size = new System.Drawing.Size(918, 272);
			this.dataGridView2.TabIndex = 35;
			// 
			// Column5
			// 
			this.Column5.HeaderText = "ФИО";
			this.Column5.MinimumWidth = 6;
			this.Column5.Name = "Column5";
			this.Column5.Width = 125;
			// 
			// Column6
			// 
			this.Column6.HeaderText = "Паспортные данные";
			this.Column6.MinimumWidth = 6;
			this.Column6.Name = "Column6";
			this.Column6.Width = 125;
			// 
			// Column7
			// 
			this.Column7.HeaderText = "Регистрационный номер книги";
			this.Column7.MinimumWidth = 6;
			this.Column7.Name = "Column7";
			this.Column7.Width = 125;
			// 
			// Column8
			// 
			this.Column8.HeaderText = "Дата выдачи";
			this.Column8.MinimumWidth = 6;
			this.Column8.Name = "Column8";
			this.Column8.Width = 125;
			// 
			// Column9
			// 
			this.Column9.HeaderText = "Дата возврата";
			this.Column9.MinimumWidth = 6;
			this.Column9.Name = "Column9";
			this.Column9.Width = 125;
			// 
			// Column10
			// 
			this.Column10.HeaderText = "Количество взятых книг";
			this.Column10.MinimumWidth = 6;
			this.Column10.Name = "Column10";
			this.Column10.Width = 125;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label5.Location = new System.Drawing.Point(17, 464);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(425, 20);
			this.label5.TabIndex = 36;
			this.label5.Text = "Список читателей, которые держат книги более ";
			// 
			// textBox1
			// 
			this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.textBox1.Location = new System.Drawing.Point(449, 464);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(34, 26);
			this.textBox1.TabIndex = 37;
			this.textBox1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox1_KeyPress);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.label6.Location = new System.Drawing.Point(489, 464);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(119, 20);
			this.label6.TabIndex = 38;
			this.label6.Text = "месяц(-а/-ев)";
			// 
			// button1
			// 
			this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
			this.button1.Location = new System.Drawing.Point(614, 453);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(177, 42);
			this.button1.TabIndex = 39;
			this.button1.Text = "Сформировать";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// Register
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1265, 927);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.label6);
			this.Controls.Add(this.textBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.dataGridView2);
			this.Controls.Add(this._btnView);
			this.Controls.Add(this._btnRedactor);
			this.Controls.Add(this._btnReadFromDB);
			this.Controls.Add(this._btnAddToBD);
			this.Controls.Add(this._txtDataReturn);
			this.Controls.Add(this.label4);
			this.Controls.Add(this._btnDelete);
			this.Controls.Add(this._btnInput);
			this.Controls.Add(this._txtDataIssue);
			this.Controls.Add(this._txtPasswData2);
			this.Controls.Add(this._txtPasswData1);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.dataGridView1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Register";
			this.Text = "Register";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
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
		private System.Windows.Forms.Button _btnAddToBD;
		private System.Windows.Forms.TextBox _txtDataReturn;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button _btnDelete;
		private System.Windows.Forms.Button _btnInput;
		private System.Windows.Forms.TextBox _txtDataIssue;
		private System.Windows.Forms.TextBox _txtPasswData2;
		private System.Windows.Forms.TextBox _txtPasswData1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button _btnReadFromDB;
		private System.Windows.Forms.Button _btnView;
		private System.Windows.Forms.Button _btnRedactor;
		private System.Windows.Forms.DataGridView dataGridView2;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column6;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column9;
		private System.Windows.Forms.DataGridViewTextBoxColumn Column10;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button button1;
	}
}