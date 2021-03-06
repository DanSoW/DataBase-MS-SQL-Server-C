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
	public partial class Book : Form
	{
		public const int MAX_SIZE_REGNUM = 10;
		public const int MAX_SIZE_PAGE = 6;
		public const int MAX_SIZE_YEAR = 4;
		public const int MAX_SIZE_SECTION = 150;

		private static Reader reader;
		private static Register register;

		private bool _editor = true;
		private bool _view = false;
		private DataGridView _copyData = null;
		public Book()
		{
			InitializeComponent();

			_copyData = new DataGridView();
			_copyData.Columns.Add("Column1", "Register_Number");
			_copyData.Columns.Add("Column2", "Count_Pages");
			_copyData.Columns.Add("Column3", "Year_Publishing");
			_copyData.Columns.Add("Column4", "Section");

			this.FormClosing += Book_FormClosing;
		}

		private void Book_FormClosing(object sender, FormClosingEventArgs e)
		{
			Application.Exit();
		}

		public static void setReader(Reader read)
		{
			reader = read;
		}

		public static void setRegister(Register reg)
		{
			register = reg;
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
						|| (dataGridView1.Rows[i].Cells[0].Value.ToString().Trim(' ').Length != MAX_SIZE_REGNUM))
					{
						flag = false;
					}

					try
					{
						long.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString());
						short.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
						short.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
					}
					catch (Exception) { flag = false; }
				}
			}

			return flag;
		}

		public static bool registerNumberValidate(String psw)
		{
			if (psw.Length != MAX_SIZE_REGNUM)
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

		public static DataGridView readDataFromDB(bool del = false)
		{
			DataGridView data = new DataGridView();
			data.Columns.Add("Column1", "Register_Number");
			data.Columns.Add("Column2", "Count_Pages");
			data.Columns.Add("Column3", "Year_Publish");
			data.Columns.Add("Column4", "Section");

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();
				using (SqlCommand cmd = new SqlCommand("USE DataBaseSQL", sqlConnection))
				{
					cmd.ExecuteNonQuery();
				}

				using (SqlCommand command = new SqlCommand("SELECT * FROM dbo.Book", sqlConnection))
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
							sqlReader.GetValue(2).ToString(),
							sqlReader.GetValue(3).ToString());
						}
					}
					sqlReader.Close();
				}

				if (del)
				{
					using (SqlCommand cmd = new SqlCommand("DELETE FROM dbo.Book;" +
					" DBCC CHECKIDENT ('dbo.Book', RESEED, 0)", sqlConnection))
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

		private void _btnInput_Click(object sender, EventArgs e)
		{
			if (_view)
			{
				MessageBox.Show("Ошибка: в режиме просмотра не возможно добавлять данные в таблицу!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!Reader.CheckTextBoxes(new List<TextBox> { _txtPasswData, _txtCountPage, _txtDataPublish }))
			{
				MessageBox.Show("Ошибка: не все текстовые поля заполнены!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			if (!registerNumberValidate(_txtPasswData.Text))
			{
				MessageBox.Show("Ошибка: не корректный регистрационный номер!" +
					" Номер должен состоять из числового набора длиной равной " + MAX_SIZE_REGNUM.ToString(), "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return;
			}

			_txtPasswData.Text = _txtPasswData.Text.Trim(new char[] { ' ' });
			_txtCountPage.Text = _txtCountPage.Text.Trim(new char[] { ' ' });
			_txtDataPublish.Text = _txtDataPublish.Text.Trim(new char[] { ' ' });
			_txtSection.Text = _txtSection.Text.Trim(new char[] { ' ' });

			for (int i = 0; i < dataGridView1.Rows.Count; i++)
			{
				if (dataGridView1.Rows[i].Cells[0].Value == null)
					break;
				if (dataGridView1.Rows[i].Cells[0].Value.ToString().Equals(_txtPasswData.Text))
				{
					MessageBox.Show("Ошибка: введённый регистрационный номер уже присутствует в таблице!", "Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

			dataGridView1.Rows.Add(_txtPasswData.Text, _txtCountPage.Text, _txtDataPublish.Text, _txtSection.Text);
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
			if ((number != 8) && ((_txtPasswData.Text + number).Length > MAX_SIZE_REGNUM))
				e.Handled = true;
		}

		private void _txtCountPage_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}

			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtCountPage.Text + number).Length > MAX_SIZE_PAGE)
				e.Handled = true;
		}

		private void _txtDataPublish_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}

			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtDataPublish.Text + number).Length > MAX_SIZE_YEAR)
				e.Handled = true;
		}

		private void _txtSection_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (_view)
			{
				e.Handled = true;
				return;
			}

			char number = e.KeyChar;

			if ((number != 8) && (_txtSection.Text + number).Length > MAX_SIZE_SECTION)
				e.Handled = true;
		}

		private void readerToolStripMenuItem_Click(object sender, EventArgs e)
		{
			reader.Show();
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

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();
				using (SqlCommand cmd = new SqlCommand("USE DataBaseSQL", sqlConnection))
				{
					cmd.ExecuteNonQuery();
				}

				for (int i = 0; i < (dataGridView1.Rows.Count - 1); i++)
				{
					using (SqlCommand sqlCMD = new SqlCommand("SET IDENTITY_INSERT dbo.Book ON; INSERT INTO dbo.Book (Register_Number, Count_Pages, Year_Publishing, Section) VALUES (@reg, @pages, @year, @section);" +
						"SET IDENTITY_INSERT dbo.Reader OFF;", sqlConnection))
					{
						try
						{
							sqlCMD.Parameters.AddWithValue("@reg", long.Parse(dataGridView1.Rows[i].Cells[0].Value.ToString()));
							sqlCMD.Parameters.AddWithValue("@pages", long.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString()));
							sqlCMD.Parameters.AddWithValue("@year", short.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString()));
							sqlCMD.Parameters.AddWithValue("@section", dataGridView1.Rows[i].Cells[3].Value.ToString());
							sqlCMD.ExecuteNonQuery();
						}
						catch (Exception)
						{
							/*MessageBox.Show("Ошибка: невозможно добавить запись с регистрационным номером " +
							dataGridView1.Rows[i].Cells[0].Value.ToString() + ", поскольку данный регистрационный номер"
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
	}
}
