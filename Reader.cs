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

		private bool _editor = true;
		private bool _view = false;
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
			if (_view)
			{
				MessageBox.Show("Ошибка: в режиме просмотра не возможно добавлять данные в таблицу!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

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

			for(int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				if (dataGridView1.Rows[i].Cells[0].Value == null)
					break;
				if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals(_txtPasswData.Text))
				{
					MessageBox.Show("Ошибка: введённые паспортные данные уже присутствуют в таблице!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

			dataGridView1.Rows.Add(_txtPasswData.Text, _txtHomeAddress.Text, _txtFIO.Text);
		}

		private void _txtPasswData_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}

			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtPasswData.Text + number).Length > MAX_SIZE_PSWD)
				e.Handled = true;
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
			catch (Exception) {
				MessageBox.Show("Ошибка: удаление данной строки невозможно!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
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

			using(SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();
				using(SqlCommand cmd = new SqlCommand("USE DataBaseSQL", sqlConnection))
				{
					cmd.ExecuteNonQuery();
				}

				for (int i = 0; i < (dataGridView1.Rows.Count-1); i++)
				{
					using (SqlCommand sqlCMD = new SqlCommand("SET IDENTITY_INSERT dbo.Reader ON; INSERT INTO dbo.Reader (Password_Data, Home_Address, Full_Name) VALUES (@pwd, @home, @fname);" +
						"SET IDENTITY_INSERT dbo.Reader OFF;", sqlConnection))
					{
						try
						{
							sqlCMD.Parameters.AddWithValue("@pwd", long.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()));
							sqlCMD.Parameters.AddWithValue("@home", dataGridView1.Rows[i].Cells[1].Value.ToString());
							sqlCMD.Parameters.AddWithValue("@fname", dataGridView1.Rows[i].Cells[2].Value.ToString());
							sqlCMD.ExecuteNonQuery();
						}
						catch (Exception)
						{
							/*MessageBox.Show("Ошибка: невозможно добавить запись с паспортными данными " +
							dataGridView1.Rows[i].Cells[0].Value.ToString() + ", поскольку данные паспортные данные"
							+ " уже присутствуют в базе даных!", "Ошибка!",
								MessageBoxButtons.OK, MessageBoxIcon.Error);*/
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
			if((d.Rows.Count-1) <= 0)
			{
				MessageBox.Show("Ошибка: в базе данных нет записей!", "Ошибка!",
					   MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			dataGridView1.Rows.Clear();
			for (int i = 0; i < (d.Rows.Count - 1); i++)
				dataGridView1.Rows.Add(CloneWithValues(d.Rows[i]));

			MessageBox.Show("Все данные считаны!", "Информация",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
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
			for (int i = 0; i < (_copyData.Rows.Count-1); i++)
				dataGridView1.Rows.Add(CloneWithValues(_copyData.Rows[i]));
			_copyData.Rows.Clear();
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
			for (int i = 0; i < (dataGridView1.Rows.Count-1); i++)
				_copyData.Rows.Add(CloneWithValues(dataGridView1.Rows[i]));

			DataGridView d = readDataFromDB();
			if ((d.Rows.Count - 1) <= 0)
			{
				MessageBox.Show("Ошибка: в базе данных нет записей!", "Ошибка!",
					   MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			dataGridView1.Rows.Clear();
			for (int i = 0; i < (d.Rows.Count - 1); i++)
				dataGridView1.Rows.Add(CloneWithValues(d.Rows[i]));

			MessageBox.Show("Данные для режима просмотра считаны!", "Информация",
					MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void _txtFIO_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}
		}

		private void _txtHomeAddress_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}
		}

		public static DataGridView readDataFromDB(bool del = false)
		{
			DataGridView data = new DataGridView();
			data.Columns.Add("Column1", "Password_Data");
			data.Columns.Add("Column2", "Home_Address");
			data.Columns.Add("Column3", "FIO");
			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();
				using (SqlCommand cmd = new SqlCommand("USE DataBaseSQL", sqlConnection))
				{
					cmd.ExecuteNonQuery();
				}

				using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Reader", sqlConnection))
				{
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

				if (del)
				{
					using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Reader;" +
					" DBCC CHECKIDENT ('dbo.Reader', RESEED, 0)", sqlConnection))
					{
						try
						{
							cmd.ExecuteNonQuery();
						}
						catch (Exception) { }
					}
				}
			}

			return data;
		}
	}
}
