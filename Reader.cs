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
	public partial class Reader : Form
	{
		public const int MAX_SIZE_PSWD = 10;
		public const int MAX_SIZE_HOMEADR = 80;
		public const int MAX_SIZE_FIO = 50;

		private static Book book;
		private static Register register;

		private DataGridView _copyData = null;

		public Reader()
		{
			InitializeComponent();

			register = new Register();
			book = new Book();

			Register.setReader(this);
			Register.setBook(book);

			Book.setReader(this);
			Book.setRegister(register);

			_copyData = new DataGridView();
			_copyData.Columns.Add("Column1", "Password_Data");
			_copyData.Columns.Add("Column2", "Home_Address");
			_copyData.Columns.Add("Column3", "FIO");

			DataGridView d = readDataFromDB();
			dataGridView1.Rows.Clear();
			_copyData.Rows.Clear();

			if (d.Rows.Count > 1)
			{
				for (int i = 0; i < (d.Rows.Count - 1); i++)
				{
					_copyData.Rows.Add(CloneWithValues(d.Rows[i]));
				}

				for (int i = 0; i < (d.Rows.Count - 1); i++)
				{
					dataGridView1.Rows.Add(CloneWithValues(d.Rows[i]));
				}
			}

			dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
		}

		private void dbPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			int index = e.RowIndex;
			string indexStr = (index + 1).ToString();
			object header = this.dataGridView1.Rows[index].HeaderCell.Value;
			if ((header == null) || (!header.Equals(indexStr)))
				this.dataGridView1.Rows[index].HeaderCell.Value = indexStr;
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
						|| (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim(' ').Length != MAX_SIZE_PSWD))
					{
						flag = false;
					}

					try
					{
						long.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
					}
					catch (Exception) { flag = false; }
				}
			}

			return flag;
		}

		private bool CheckOrientedData(String pass, String home, String fio)
		{
			if ((home.Length == 0)
				|| (fio.Length == 0)
				|| (home.Trim(' ').Length == 0)
				|| (fio.Trim(' ').Length == 0)
				|| (home.Length > MAX_SIZE_HOMEADR)
				|| (fio.Length > MAX_SIZE_FIO)
				|| (!passwordDataValidate(pass)))
				return false;
			try
			{
				long.Parse(pass);
			}
			catch (Exception) { return false; }

			return true;
		}

		public static bool CheckTextBoxes(List<TextBox> txb)
		{
			if (txb.Count == 0)
				return false;

			foreach (var i in txb)
			{
				if (i.Text.Length == 0)
					return false;
			}

			return true;
		}

		public static bool passwordDataValidate(String psw)
		{
			if (psw.Length != MAX_SIZE_PSWD)
				return false;
			bool flag = true;
			foreach (var i in psw)
				if (!Char.IsDigit(i))
				{
					flag = false;
					break;
				}

			return flag;
		}

		private void _btnInput_Click(object sender, EventArgs e)
		{
			if (!CheckTextBoxes(new List<TextBox> { _txtPasswData, _txtHomeAddress, _txtFIO}))
			{
				MessageBox.Show("Ошибка: не все текстовые поля заполнены!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!passwordDataValidate(_txtPasswData.Text))
			{
				MessageBox.Show("Ошибка: не корректные паспортные данные! Паспортные даные" +
					" могут состоять только из набора цифр, длина которых строго равна " + MAX_SIZE_PSWD.ToString(), "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			_txtPasswData.Text = _txtPasswData.Text.Trim(new char[] { ' ' });
			_txtHomeAddress.Text = _txtHomeAddress.Text.Trim(new char[] { ' ' });
			_txtFIO.Text = _txtFIO.Text.Trim(new char[] { ' ' });

			if(addDataToDataBase(_txtPasswData.Text, _txtHomeAddress.Text, _txtFIO.Text))
			{
				_copyData.Rows.Add(_txtPasswData.Text, _txtHomeAddress.Text, _txtFIO.Text);
				dataGridView1.Rows.Add(_txtPasswData.Text, _txtHomeAddress.Text, _txtFIO.Text);
			}
		}

		private bool addDataToDataBase(String pwd, String home, String fname)
		{
			if(!CheckOrientedData(pwd, home, fname))
			{
				MessageBox.Show("Ошибка: добавление записи невозможно! Запись содержит не корректные данные!",
					"Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCMD = new SqlCommand("dbo.WriteReaderData", sqlConnection))
				{
					sqlCMD.CommandType = System.Data.CommandType.StoredProcedure;

					SqlParameter valueReturn = new SqlParameter("valueReturn", SqlDbType.Int);
					valueReturn.Direction = ParameterDirection.ReturnValue;

					sqlCMD.Parameters.Add(valueReturn);
					sqlCMD.Parameters.AddWithValue("@pwd", long.Parse(pwd));
					sqlCMD.Parameters.AddWithValue("@home", home);
					sqlCMD.Parameters.AddWithValue("@fname", fname);
					sqlCMD.ExecuteScalar();

					int result = Convert.ToInt32(valueReturn.Value);

					if (result < 0)
					{
						MessageBox.Show("Ошибка: невозможно добавить запись с паспортными данными " +
						pwd + ", поскольку данные паспортные данные"
						+ " уже присутствуют в базе даных!", "Ошибка!",
						MessageBoxButtons.OK, MessageBoxIcon.Error);
						sqlConnection.Close();
						return false;
					}
				}

				sqlConnection.Close();
			}

			return true;
		}

		private void _txtPasswData_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtPasswData.Text + number).Length > MAX_SIZE_PSWD)
				e.Handled = true;
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

				using (SqlCommand sqlCMD = new SqlCommand("dbo.DeleteReaderData", sqlConnection))
				{
					sqlCMD.CommandType = System.Data.CommandType.StoredProcedure;

					SqlParameter valueReturn = new SqlParameter("valueReturn", SqlDbType.Int);
					valueReturn.Direction = ParameterDirection.ReturnValue;

					sqlCMD.Parameters.Add(valueReturn);
					sqlCMD.Parameters.AddWithValue("@pwd", long.Parse(pwd));
					sqlCMD.ExecuteScalar();

					int result = Convert.ToInt32(valueReturn.Value);

					if (result < 0)
					{
						MessageBox.Show("Ошибка: невозможно удалить данную запись!" +
							" Данные паспортные данные зарегистрированы! Чтобы удалить данную" +
							" запись необходимо удалить запись с данными паспортными данными!", "Ошибка!",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
						sqlConnection.Close();
						return;
					}
				}
				sqlConnection.Close();
			}
			_copyData.Rows.RemoveAt(index);
			dataGridView1.Rows.RemoveAt(index);
		}

		private void readerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			book.Show();
			this.Hide();
		}

		private void bookToolStripMenuItem_Click(object sender, EventArgs e)
		{
			register.Show();
			this.Hide();
		}

		public static DataGridViewRow CloneWithValues(DataGridViewRow row)
		{
			DataGridViewRow clonedRow = (DataGridViewRow)row.Clone();
			for (Int32 index = 0; index < row.Cells.Count; index++)
			{
				clonedRow.Cells[index].Value = row.Cells[index].Value;
			}
			return clonedRow;
		}

		public static DataGridView readDataFromDB()
		{
			DataGridView data = new DataGridView();
			data.Columns.Add("Column1", "Password_Data");
			data.Columns.Add("Column2", "Home_Address");
			data.Columns.Add("Column3", "FIO");
			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand command = new SqlCommand("dbo.ReadReaderData", sqlConnection))
				{
					command.CommandType = System.Data.CommandType.StoredProcedure;
					SqlDataReader sqlReader = command.ExecuteReader();
					if (sqlReader.HasRows)
					{
						while (sqlReader.Read())
						{
							String pwd = sqlReader.GetValue(0).ToString();
							while (pwd.Length != 10)
								pwd = ("0" + pwd);
							data.Rows.Add(pwd,
							sqlReader.GetValue(1).ToString(),
							sqlReader.GetValue(2).ToString());
						}
					}
					sqlReader.Close();
				}

				sqlConnection.Close();
			}

			return data;
		}

		public static bool checkRow(DataGridViewRow row)
		{
			for (int i = 0; i < row.Cells.Count; i++)
				if (row.Cells[i].Value == null)
					return false;
			return true;
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

			if (flag)
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

			if ((!checkRow(dataGridView1.Rows[index])) || (!CheckOrientedData(
				dataGridView1.Rows[index].Cells[0].Value.ToString(),
				dataGridView1.Rows[index].Cells[1].Value.ToString(),
				dataGridView1.Rows[index].Cells[2].Value.ToString())))
			{
				reloadDatabase(true);
				MessageBox.Show("Ошибка: введены не корректные параметры! Изменение данных не возможно осуществить!", "Ошибка!",
							MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCMD = new SqlCommand("dbo.UpdateReaderData", sqlConnection))
				{
					sqlCMD.CommandType = System.Data.CommandType.StoredProcedure;
					sqlCMD.Parameters.AddWithValue("@pwd", long.Parse(dataGridView1.Rows[index].Cells[0].Value.ToString()));
					sqlCMD.Parameters.AddWithValue("@newHome", dataGridView1.Rows[index].Cells[1].Value.ToString());
					sqlCMD.Parameters.AddWithValue("@newFName", dataGridView1.Rows[index].Cells[2].Value.ToString());
					sqlCMD.ExecuteNonQuery();
				}

				sqlConnection.Close();
			}

			dataGridView1.Refresh();
			DataGridViewRow row = Reader.CloneWithValues(dataGridView1.Rows[index]);
			for (int i = 0; i < row.Cells.Count; i++)
				_copyData.Rows[index].Cells[i].Value = row.Cells[i].Clone();
		}
	}
}
