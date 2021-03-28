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

			DataGridView d = readDataFromDB();
			dataGridView1.Rows.Clear();
			_copyData.Rows.Clear();

			if (d.Rows.Count > 1)
			{
				for (int i = 0; i < (d.Rows.Count - 1); i++)
					_copyData.Rows.Add(Reader.CloneWithValues(d.Rows[i]));

				for (int i = 0; i < (d.Rows.Count-1); i++)
					dataGridView1.Rows.Add(Reader.CloneWithValues(d.Rows[i]));
			}
			
			dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
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
			if (dataGridView1.CurrentRow == null)
			{
				MessageBox.Show("Ошибка: не выбрана строка для удаления", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			int index = dataGridView1.CurrentRow.Index;

			String pwd = dataGridView1.Rows[index].Cells[0].Value.ToString();
			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCMD = new SqlCommand("dbo.DeleteRegisterData", sqlConnection))
				{
					sqlCMD.CommandType = System.Data.CommandType.StoredProcedure;

					sqlCMD.Parameters.AddWithValue("@id", index);
					sqlCMD.ExecuteNonQuery();
				}
				sqlConnection.Close();
			}
			_copyData.Rows.RemoveAt(index);
			dataGridView1.Rows.RemoveAt(index);
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

		private bool CheckOrientedData(String reg, String pwd, String dateIssue, String dateReturn)
		{
			if ((!Reader.passwordDataValidate(pwd))
				|| (!Book.registerNumberValidate(reg))
				|| (!checkDateEntry(dateIssue))
				|| (!checkDateEntry(dateReturn)))
				return false;
			try
			{
				long.Parse(reg);
				long.Parse(pwd);
			}
			catch (Exception) { return false; }

			return true;
		}

		private bool checkDateEntry(String date)
		{
			if (date.Length != MAX_SIZE_DATA)
				return false;

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

			if (addDataToDataBase(_txtPasswData1.Text, _txtPasswData2.Text, _txtDataIssue.Text, _txtDataReturn.Text))
			{
				_copyData.Rows.Add(_txtPasswData1.Text, _txtPasswData2.Text, _txtDataIssue.Text, _txtDataReturn.Text);
				dataGridView1.Rows.Add(_txtPasswData1.Text, _txtPasswData2.Text, _txtDataIssue.Text, _txtDataReturn.Text);
			}
		}

		private bool addDataToDataBase(String reg, String pwd, String dateIssue, String dateReturn)
		{
			if (!CheckOrientedData(reg, pwd, dateIssue, dateReturn))
			{
				MessageBox.Show("Ошибка: добавление записи невозможно! Запись содержит не корректные данные!",
					"Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				string[] dateString = dateIssue.Split(new char[] { '.' });
				DateTime date1 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
							int.Parse(dateString[0]));

				dateString = dateReturn.Split(new char[] { '.' });
				DateTime date2 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
							int.Parse(dateString[0]));
				using (SqlCommand sqlCMD = new SqlCommand("dbo.WriteRegisterData", sqlConnection))
				{
					sqlCMD.CommandType = System.Data.CommandType.StoredProcedure;

					SqlParameter valueReturn = new SqlParameter("valueReturn", SqlDbType.Int);
					valueReturn.Direction = ParameterDirection.ReturnValue;

					sqlCMD.Parameters.Add(valueReturn);
					sqlCMD.Parameters.AddWithValue("@id", dataGridView1.Rows.Count);
					sqlCMD.Parameters.AddWithValue("@reg", long.Parse(reg));
					sqlCMD.Parameters.AddWithValue("@pwd", long.Parse(pwd));
					sqlCMD.Parameters.AddWithValue("@dateIssue", date1);
					sqlCMD.Parameters.AddWithValue("@dateReturn", date2);
					sqlCMD.ExecuteScalar();

					int result = Convert.ToInt32(valueReturn.Value);

					string[] names = { "регистрационные данные", "паспортные данные" };

					if((result < 0) && (result > (-3)))
					{
						MessageBox.Show("Ошибка: в записи содержаться не существующие "
							+ names[(result == (-2))? 0 : 1] + "!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
						sqlConnection.Close();
						return false;
					}else if(result == (-3))
					{
						MessageBox.Show("Ошибка: дата возврата не может быть раньше даты получения!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
						sqlConnection.Close();
						return false;
					}
				}

				sqlConnection.Close();
			}

			return true;
		}

		private void _txtPasswData1_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtPasswData1.Text + number).Length > Book.MAX_SIZE_REGNUM)
				e.Handled = true;
		}

		private void _txtPasswData2_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtPasswData2.Text + number).Length > Reader.MAX_SIZE_PSWD)
				e.Handled = true;
		}

		public DataGridView readDataFromDB()
		{
			DataGridView data = new DataGridView();
			data.Columns.Add("Column1", "Register_Number");
			data.Columns.Add("Column2", "Password_Data");
			data.Columns.Add("Column3", "Data_Issue");
			data.Columns.Add("Column4", "Data_Return");

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand command = new SqlCommand("dbo.ReadRegisterData", sqlConnection))
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;

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

				sqlConnection.Close();
			}

			return data;
		}

		private void _txtDataIssue_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != '.') && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtDataIssue.Text + number).Length > MAX_SIZE_DATA)
				e.Handled = true;
		}

		private void _txtDataReturn_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != '.') && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtDataReturn.Text + number).Length > MAX_SIZE_DATA)
				e.Handled = true;
		}

		private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
		{
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
			dataGridView2.Rows.Clear();

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand command = new SqlCommand("dbo.TaskOneT", sqlConnection))
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					
					command.Parameters.AddWithValue("@monthValue", int.Parse(textBox1.Text));
					SqlDataReader sqlReader = command.ExecuteReader();
					if (sqlReader.HasRows)
					{
						while (sqlReader.Read())
						{
							String fullName = sqlReader.GetValue(0).ToString();
							String pwd = sqlReader.GetValue(1).ToString();
							String reg = sqlReader.GetValue(2).ToString();

							while (pwd.Length != 10)
								pwd = ("0" + pwd);

							while (reg.Length != 10)
								reg = ("0" + reg);

							dataGridView2.Rows.Add(fullName, pwd, reg,
								sqlReader.GetValue(3).ToString().Split(new char[] { ' ' })[0],
								sqlReader.GetValue(4).ToString().Split(new char[] { ' ' })[0],
								"0");
						}
					}
					sqlReader.Close();
				}

				for (int i = 0; i < (dataGridView2.Rows.Count-1); i++)
				{
					using (SqlCommand command2 = new SqlCommand("dbo.DefineBookCounter", sqlConnection))
					{
						command2.CommandType = System.Data.CommandType.StoredProcedure;

						SqlParameter valueReturn = new SqlParameter("valueReturn", SqlDbType.Int);
						valueReturn.Direction = ParameterDirection.ReturnValue;

						command2.Parameters.Add(valueReturn);
						command2.Parameters.AddWithValue("@pwd", long.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString()));
						command2.ExecuteScalar();

						dataGridView2.Rows[i].Cells[dataGridView2.Rows[i].Cells.Count - 1].Value = Convert.ToString(valueReturn.Value);
					}
				}

				sqlConnection.Close();
			}
		}

		private void _txtInputE2_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtInputE2.Text + number).Length > Reader.MAX_SIZE_PSWD)
				e.Handled = true;
		}

		private void _btnExecute2_Click(object sender, EventArgs e)
		{
			if(_txtInputE2.Text.Length != Reader.MAX_SIZE_PSWD)
			{
				MessageBox.Show("Ошибка: не введены паспортные данные!", "Ошибка!",
					   MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}
			richTextBox1.Clear();

			List<DataElement> elements = new List<DataElement>();

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand command = new SqlCommand("dbo.TaskTwot", sqlConnection))
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					command.Parameters.AddWithValue("@pwd", long.Parse(_txtInputE2.Text));
					SqlDataReader sqlReader = command.ExecuteReader();
					if (sqlReader.HasRows)
					{
						while (sqlReader.Read())
						{
							String reg = sqlReader.GetValue(0).ToString();

							while (reg.Length != 10)
								reg = ("0" + reg);

							elements.Add(new DataElement(reg,
								sqlReader.GetValue(1).ToString().Split(new char[] { ' ' })[0],
							int.Parse(sqlReader.GetValue(2).ToString()),
							sqlReader.GetValue(3).ToString()));
						}
					}
					sqlReader.Close();
				}

				sqlConnection.Close();
			}

			elements.Sort(new Comparison<DataElement>(compare));
			for (int i = 0; i < elements.Count; i++)
			{
				richTextBox1.Text = richTextBox1.Text + elements[i].ToString() + "\n";
			}
		}

		int compare(DataElement o1, DataElement o2)
		{
			string[] dateString = o1.dataIssue.Split(new char[] { '.' });
			DateTime date1 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
				int.Parse(dateString[0]));

			dateString = o2.dataIssue.Split(new char[] { '.' });
			DateTime date2 = new DateTime(int.Parse(dateString[2]), int.Parse(dateString[1]),
				int.Parse(dateString[0]));

			return date1.CompareTo(date2);
		}

		private void reloadDatabase(bool flag = false)
		{
			DataGridView d = readDataFromDB();
			if (d.Rows.Count > 1)
			{
				dataGridView1.Rows.Clear();
				_copyData.Rows.Clear();
				for (int i = 0; i < (d.Rows.Count - 1); i++)
				{
					_copyData.Rows.Add(Reader.CloneWithValues(d.Rows[i]));
					dataGridView1.Rows.Add(Reader.CloneWithValues(d.Rows[i]));
				}
			}

			if(flag)
				dataGridView1.Refresh();
		}

		private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
		{
			if (dataGridView1.Rows.Count <= 1)
				return;
			int index = e.RowIndex;
			if (index == dataGridView1.Rows.Count)
				return;
			bool flag = true;
			for (int i = 0; (i < dataGridView1.Rows[index].Cells.Count) && (flag); i++)
				if (!(dataGridView1.Rows[index].Cells[i].Value.ToString().Equals(
					_copyData.Rows[index].Cells[i].Value.ToString())))
					flag = false;
			if (flag)
				return;

			if ((!Reader.checkRow(dataGridView1.Rows[index])) || (!CheckOrientedData(
				dataGridView1.Rows[index].Cells[0].Value.ToString(),
				dataGridView1.Rows[index].Cells[1].Value.ToString(),
				dataGridView1.Rows[index].Cells[2].Value.ToString(),
				dataGridView1.Rows[index].Cells[3].Value.ToString())))
			{
				reloadDatabase(true);
				MessageBox.Show("Ошибка: введены не корректные параметры! Изменение данных не возможно осуществить!", "Ошибка!",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCMD = new SqlCommand("dbo.UpdateRegisterData", sqlConnection))
				{
					sqlCMD.CommandType = System.Data.CommandType.StoredProcedure;

					string[] dateString = dataGridView1.Rows[index].Cells[2].Value.ToString().Split(new char[] { '.' });
					DateTime date1 = new DateTime(int.Parse(dateString[2]),
						int.Parse(dateString[1]),
						int.Parse(dateString[0]));
					dateString = dataGridView1.Rows[index].Cells[3].Value.ToString().Split(new char[] { '.' });

					DateTime date2 = new DateTime(int.Parse(dateString[2]),
						int.Parse(dateString[1]),
						int.Parse(dateString[0]));

					SqlParameter valueReturn = new SqlParameter("valueReturn", SqlDbType.Int);
					valueReturn.Direction = ParameterDirection.ReturnValue;

					sqlCMD.Parameters.Add(valueReturn);
					sqlCMD.Parameters.AddWithValue("@id", index);
					sqlCMD.Parameters.AddWithValue("@reg", long.Parse(dataGridView1.Rows[index].Cells[0].Value.ToString()));
					sqlCMD.Parameters.AddWithValue("@pwd", long.Parse(dataGridView1.Rows[index].Cells[1].Value.ToString()));
					sqlCMD.Parameters.AddWithValue("@dateIssue", date1);
					sqlCMD.Parameters.AddWithValue("@dateReturn", date2);
					sqlCMD.ExecuteScalar();

					int result = Convert.ToInt32(valueReturn.Value);

					string[] names = { "регистрационные данные", "паспортные данные" };
					if ((result < 0) && (result > (-3)))
					{
						MessageBox.Show("Ошибка: в записи содержаться не существующие "
							+ names[(result == (-2)) ? 0 : 1] + "!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
						sqlConnection.Close();
						reloadDatabase(true);
						return;
					}
					else if (result == (-3))
					{
						MessageBox.Show("Ошибка: дата возврата не может быть раньше даты получения!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
						sqlConnection.Close();
						reloadDatabase(true);
						return;
					}
				}

				sqlConnection.Close();
			}
			dataGridView1.Refresh();
			DataGridViewRow row = Reader.CloneWithValues(dataGridView1.Rows[index]);
			for (int i = 0; i < row.Cells.Count; i++)
				_copyData.Rows[index].Cells[i].Value = row.Cells[i].Clone();
		}
	}

	struct DataElement
	{
		public string register;
		public string dataIssue;
		public int pages;
		public string section;

		public DataElement(string reg, string data, int pages, string section)
		{
			this.register = reg;
			this.dataIssue = data;
			this.pages = pages;
			this.section = section;
		}

		public override string ToString()
		{
			return register + ", " + dataIssue + ", " + pages.ToString()
				+ ", " + section;
		}
	}
}
