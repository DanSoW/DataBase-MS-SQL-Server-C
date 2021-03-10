using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataBase
{
	public partial class Register : Form
	{
		public const int MAX_SIZE_DATA = 10;

		private static Reader reader;
		private static Book book;

		private bool _editor = true;
		private bool _view = false;
		private DataGridView _copyData = null;

		public Register()
		{
			InitializeComponent();

			_copyData = new DataGridView();
			_copyData.Columns.Add("Column1", "Register_Number");
			_copyData.Columns.Add("Column2", "Password_Data");
			_copyData.Columns.Add("Column3", "Data_Issue");
			_copyData.Columns.Add("Column4", "Data_Return");

			this.FormClosing += Register_FormClosing;
		}

		private void Register_FormClosing(object sender, FormClosingEventArgs e)
		{
			Application.Exit();
		}

		public static void setReader(Reader read)
		{
			reader = read;
		}

		public static void setBook(Book bk)
		{
			book = bk;
		}

		private void dbPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			int index = e.RowIndex;
			string indexStr = (index + 1).ToString();
			object header = this.dataGridView1.Rows[index].HeaderCell.Value;
			if ((header == null) || (!header.Equals(indexStr)))
				this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
		}

		private void readerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			reader.Show();
			this.Hide();
		}

		private void bookToolStripMenuItem_Click(object sender, EventArgs e)
		{
			book.Show();
			this.Hide();
		}

		private void _btnDelete_Click(object sender, EventArgs e)
		{
			if (_view)
			{
				MessageBox.Show("Ошибка: в режиме просмотра не возможно удалять данные из таблице!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (dataGridView1.CurrentRow == null)
			{
				MessageBox.Show("Ошибка: не выбрана строка для удаления", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			try
			{
				dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
			}
			catch (Exception)
			{
				MessageBox.Show("Ошибка: удаление данной строки невозможно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private bool CheckData()
		{
			if (dataGridView1 == null)
				return false;

			bool flag = true;

			for (int i = 0; (i < (dataGridView1.Rows.Count - 1)) && (flag == true); i++)
			{
				for (int j = 0; (j < dataGridView1.Rows[i].Cells.Count) && (flag == true); j++)
				{
					if ((dataGridView1.Rows[i].Cells[j].Value == null)
						|| (dataGridView1.Rows[i].Cells[j].Value.ToString() == "")
						|| (dataGridView1.Rows[i].Cells[j].Value.ToString().Trim(' ') == "")
						|| (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim(' ').Length != Book.MAX_SIZE_REGNUM)
						|| (dataGridView1.Rows[i].Cells[1].Value.ToString().Trim(' ').Length != Reader.MAX_SIZE_PSWD)
						|| (!checkDateEntry(dataGridView1.Rows[i].Cells[2].Value.ToString()))
						|| (!checkDateEntry(dataGridView1.Rows[i].Cells[3].Value.ToString())))
					{
						flag = false;
					}

					try
					{
						long.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
						long.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
					}
					catch (Exception) { flag = false; }
				}
			}

			return flag;
		}

		private bool checkDateEntry(String date)
		{
			if(date.Count((char i) => (i == '.')) != 2)
			{
				return false;
			}

			foreach (var i in date)
				if ((!Char.IsDigit(i)) && (i != '.'))
					return false;

			string[] dateSplit = date.Split(new char[] { '.' });
			if ((dateSplit[2].Length != 4) || (dateSplit[1].Length != 2) || (dateSplit[0].Length != 2))
				return false;

			try
			{
				int value = int.Parse(dateSplit[0]);
				if ((value <= 0) || (value > 31))
					return false;
				value = int.Parse(dateSplit[1]);
				if ((value <= 0) || (value > 12))
					return false;
				value = int.Parse(dateSplit[2]);
				if ((value <= 0) || (value > 9999))
					return false;
			}
			catch (Exception)
			{
				return false;
			}

			return true;
		}

		private void _btnInput_Click(object sender, EventArgs e)
		{
			if (_view)
			{
				MessageBox.Show("Ошибка: в режиме просмотра не возможно добавлять данные в таблицу!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!Reader.CheckTextBoxes(new List<TextBox> { _txtPasswData1, _txtPasswData2, _txtDataIssue, _txtDataReturn }))
			{
				MessageBox.Show("Ошибка: не все текстовые поля заполнены!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!Book.registerNumberValidate(_txtPasswData1.Text))
			{
				MessageBox.Show("Ошибка: не корректный регистрационный номер!" +
					" Номер должен состоять из числового набора длиной равной " + Book.MAX_SIZE_REGNUM.ToString(), "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!Reader.passwordDataValidate(_txtPasswData2.Text))
			{
				MessageBox.Show("Ошибка: не корректные паспортные данные! Паспортные даные" +
					" могут состоять только из набора цифр, длина которых строго равна " + Reader.MAX_SIZE_PSWD.ToString(), "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			_txtPasswData1.Text = _txtPasswData1.Text.Trim(new char[] { ' ' });
			_txtPasswData2.Text = _txtPasswData2.Text.Trim(new char[] { ' ' });
			_txtDataIssue.Text = _txtDataIssue.Text.Trim(new char[] { ' ' });
			_txtDataReturn.Text = _txtDataReturn.Text.Trim(new char[] { ' ' });

			if((!checkDateEntry(_txtDataIssue.Text)) || (!checkDateEntry(_txtDataReturn.Text))){
				MessageBox.Show("Ошибка: неверный формат даты! Дата выдачи и возврата должна быть" +
					" в формате ДД.ММ.ГГГГ", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			string[] dateString = _txtDataIssue.Text.Split(new char[] { '.' });
			DateTime date1 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
				int.Parse(dateString[0]));
			dateString = _txtDataReturn.Text.Split(new char[] { '.' });
			DateTime date2 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
				int.Parse(dateString[0]));

			if(DateTime.Compare(date1, date2) > 0)
			{
				MessageBox.Show("Ошибка: дата выдачи не может быть позже даты возврата!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			dataGridView1.Rows.Add(_txtPasswData1.Text, _txtPasswData2.Text, _txtDataIssue.Text, _txtDataReturn.Text);
		}

		private void _txtPasswData1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}

			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtPasswData1.Text + number).Length > Book.MAX_SIZE_REGNUM)
				e.Handled = true;
		}

		private void _txtPasswData2_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}

			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtPasswData2.Text + number).Length > Reader.MAX_SIZE_PSWD)
				e.Handled = true;
		}

		public DataGridView readDataFromDB(bool del = false)
		{
			DataGridView data = new DataGridView();
			data.Columns.Add("Column1", "Register_Number");
			data.Columns.Add("Column2", "Password_Data");
			data.Columns.Add("Column3", "Data_Issue");
			data.Columns.Add("Column4", "Data_Return");

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();
				using (SqlCommand cmd = new SqlCommand("USE DataBaseSQL", sqlConnection))
				{
					cmd.ExecuteNonQuery();
				}

				using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.RecordRegistration", sqlConnection))
				{
					SqlDataReader sqlReader = command.ExecuteReader();
					if (sqlReader.HasRows)
					{
						while (sqlReader.Read())
						{
							String reg = sqlReader.GetValue(1).ToString(),
								pwd = sqlReader.GetValue(2).ToString();

							while (reg.Length != 10)
								reg = ("0" + reg);

							while (pwd.Length != 10)
								pwd = ("0" + pwd);

							data.Rows.Add(reg, pwd,
							sqlReader.GetValue(3).ToString().Split(new char[] { ' ' })[0],
							sqlReader.GetValue(4).ToString().Split(new char[] { ' ' })[0]);
						}
					}
					sqlReader.Close();
				}

				if (del)
				{
					using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.RecordRegistration;" +
					" DBCC CHECKIDENT ('dbo.RecordRegistration', RESEED, 0)", sqlConnection))
					{
						cmd.ExecuteNonQuery();
					}
				}
			}

			return data;
		}

		private void _btnAddToBD_Click(object sender, EventArgs e)
		{
			if (_view)
			{
				MessageBox.Show("Ошибка: в режиме просмотра не возможно добавлять данные в таблицу!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!CheckData())
			{
				MessageBox.Show("Ошибка: добавление строк невозможно, так как в таблице содержаться" +
					" не корректные данные", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			DataGridView dataBook = Book.readDataFromDB();

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();
				using (SqlCommand cmd = new SqlCommand("USE DataBaseSQL", sqlConnection))
				{
					cmd.ExecuteNonQuery();
				}

				for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
				{
					string[] dateString = dataGridView1.Rows[i].Cells[2].Value.ToString().Split(new char[] { '.' });
					DateTime date1 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
								int.Parse(dateString[0]));

					dateString = dataGridView1.Rows[i].Cells[3].Value.ToString().Split(new char[] { '.' });
					DateTime date2 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
								int.Parse(dateString[0]));
					bool flag = true;
					if (dataBook.Rows[i].Cells[0].Value.ToString().Equals(dataGridView1.Rows[i].Cells[0].Value.ToString()))
					{
						int yearPublish = int.Parse(dataBook.Rows[i].Cells[2].Value.ToString());

						if (date1.Year < yearPublish)
						{
							flag = false;
							MessageBox.Show("Ошибка: не возможно добавить запись с регистрационным номером "
							+ dataGridView1.Rows[i].Cells[0].Value.ToString() + " и с паспортными данными "
							+ dataGridView1.Rows[i].Cells[1].Value.ToString() + " , так как" +
							" дата выдачи книги не может быть позже даты её выдачи ", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
						}
					}

					if (flag)
					{
						using (SqlCommand sqlCMD = new SqlCommand("INSERT INTO dbo.RecordRegistration (Book_Register_Number, Reader_Password_Data, Date_Issue, Date_Return) VALUES (@reg, @pwd, @dissue, @dreturn);", sqlConnection))
						{
							try
							{
								sqlCMD.Parameters.AddWithValue("@reg", long.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()));
								sqlCMD.Parameters.AddWithValue("@pwd", long.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
								sqlCMD.Parameters.AddWithValue("@dissue", date1);
								sqlCMD.Parameters.AddWithValue("@dreturn", date2);
								sqlCMD.ExecuteNonQuery();
							}
							catch (System.Data.SqlClient.SqlException)
							{
								MessageBox.Show("Ошибка: не возможно добавить запись с регистрационным номером "
									+ dataGridView1.Rows[i].Cells[0].Value.ToString() + " и с паспортными данными "
									+ dataGridView1.Rows[i].Cells[1].Value.ToString() + " , так как отсутствуют либо" +
									" введённые паспортные данные или регистрационный номер книги в соответствующих " +
									" таблицах (Читатель и Книга)", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
							}
						}
					}
				}
			}

			dataGridView1.Rows.Clear();
			MessageBox.Show("Записи добавлены!", "Информация",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void _btnReadFromDB_Click(object sender, EventArgs e)
		{
			if (_view)
			{
				MessageBox.Show("Ошибка: в режиме просмотра не возможно считать данные из таблицы!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			DataGridView d = readDataFromDB(true);
			if ((d.Rows.Count - 1) <= 0)
			{
				MessageBox.Show("Ошибка: в базе данных нет записей!", "Ошибка!",
					   MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			dataGridView1.Rows.Clear();
			for (int i = 0; i < (d.Rows.Count - 1); i++)
				dataGridView1.Rows.Add(Reader.CloneWithValues(d.Rows[i]));

			MessageBox.Show("Все данные считаны!", "Информация",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void _txtDataIssue_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}

			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != '.') && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtDataIssue.Text + number).Length > MAX_SIZE_DATA)
				e.Handled = true;
		}

		private void _txtDataReturn_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}

			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != '.') && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtDataReturn.Text + number).Length > MAX_SIZE_DATA)
				e.Handled = true;
		}

		private void _btnRedactor_Click(object sender, EventArgs e)
		{
			if (_editor)
			{
				MessageBox.Show("Вы уже находитесь в режиме редактора", "Информация",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			_editor = true;
			_view = false;

			dataGridView1.Rows.Clear();
			for (int i = 0; i < (_copyData.Rows.Count - 1); i++)
				dataGridView1.Rows.Add(Reader.CloneWithValues(_copyData.Rows[i]));
			_copyData.Rows.Clear();
		}

		private void _btnView_Click(object sender, EventArgs e)
		{
			if (_view)
			{
				MessageBox.Show("Вы уже находитесь в режиме просмотра", "Информация",
						MessageBoxButtons.OK, MessageBoxIcon.Information);
				return;
			}
			_editor = false;
			_view = true;
			_copyData.Rows.Clear();
			for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
				_copyData.Rows.Add(Reader.CloneWithValues(dataGridView1.Rows[i]));

			DataGridView d = readDataFromDB();
			if ((d.Rows.Count - 1) <= 0)
			{
				MessageBox.Show("Ошибка: в базе данных нет записей!", "Ошибка!",
					   MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			dataGridView1.Rows.Clear();
			for (int i = 0; i < (d.Rows.Count - 1); i++)
				dataGridView1.Rows.Add(Reader.CloneWithValues(d.Rows[i]));

			MessageBox.Show("Данные для режима просмотра считаны!", "Информация",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}

			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
		}

		private int numberBorrowedBooks(string passwordNumber)
		{
			DataGridView data = readDataFromDB();
			if ((data.Rows.Count - 1) <= 0)
			{
				throw new Exception("Ошибка: в базе данных нет записей!");
			}

			int count = 0;
			for(int i = 0; i < (data.Rows.Count-1); i++)
			{
				if (data.Rows[i].Cells[1].Value.ToString().Equals(passwordNumber))
					count++;
			}

			return count;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if(textBox1.Text.Length == 0)
			{
				MessageBox.Show("Ошибка: не введено число месяц(-а/-ев)!", "Ошибка!",
					   MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			DataGridView data = readDataFromDB(),
				readers = Reader.readDataFromDB();
			if ((data.Rows.Count - 1) <= 0)
			{
				MessageBox.Show("Ошибка: в базе данных нет записей!", "Ошибка!",
					   MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			dataGridView2.Rows.Clear();

			for (int i = 0; i < (data.Rows.Count-1); i++)
			{
				String dataIssue = data.Rows[i].Cells[2].Value.ToString(),
					dataReturn = data.Rows[i].Cells[3].Value.ToString();

				if ((int.Parse(dataReturn.Split(new char[] { '.' })[1]) - int.Parse(dataIssue.Split(new char[] { '.' })[1]))
					> int.Parse(textBox1.Text))
				{
					for(int j = 0; j < (readers.Rows.Count-1); j++)
					{
						if(readers.Rows[j].Cells[0].Value.ToString().Equals(
							data.Rows[i].Cells[0].Value.ToString()))
						{
							dataGridView2.Rows.Add(
								readers.Rows[j].Cells[readers.Rows[j].Cells.Count - 1].Value.ToString(),
								readers.Rows[j].Cells[0].Value.ToString(),
								data.Rows[i].Cells[0].Value.ToString(),
								dataIssue,
								dataReturn,
								numberBorrowedBooks(readers.Rows[j].Cells[0].Value.ToString()).ToString()
								);
						}
					}
				}
				else
				{
					int year = (int.Parse(dataReturn.Split(new char[] { '.' })[2]) - int.Parse(dataIssue.Split(new char[] { '.' })[2]));
					int month = int.Parse(textBox1.Text);
					int yearsBook = 0;
					while (month >= 12)
					{
						month -= 12;
						yearsBook++;
					}

					if(year > yearsBook)
					{
						for (int j = 0; j < (readers.Rows.Count - 1); j++)
						{
							if (readers.Rows[j].Cells[0].Value.ToString().Equals(
								data.Rows[i].Cells[0].Value.ToString()))
							{
								dataGridView2.Rows.Add(
									readers.Rows[j].Cells[readers.Rows[j].Cells.Count - 1].Value.ToString(),
									readers.Rows[j].Cells[0].Value.ToString(),
									data.Rows[i].Cells[0].Value.ToString(),
									dataIssue,
									dataReturn,
									numberBorrowedBooks(readers.Rows[j].Cells[0].Value.ToString()).ToString()
									);
							}
						}
					}
				}
			}

		}
	}
}
