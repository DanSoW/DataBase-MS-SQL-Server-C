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

			DataGridView d = readDataFromDB();
			dataGridView1.Rows.Clear();
			_copyData.Rows.Clear();

			if (d.Rows.Count > 1)
			{
				for (int i = 0; i < (d.Rows.Count - 1); i++)
				{
					_copyData.Rows.Add(Reader.CloneWithValues(d.Rows[i]));
				}

				for (int i = 0; i < (d.Rows.Count - 1); i++)
				{
					dataGridView1.Rows.Add(Reader.CloneWithValues(d.Rows[i]));
				}
			}

			dataGridView1.CellValueChanged += dataGridView1_CellValueChanged;
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

		private bool CheckOrientedData(String reg, String pages, String year, String section)
		{
			if ((pages.Length == 0)
				|| (year.Trim(' ').Length == 0)
				|| (section.Trim(' ').Length == 0)
				|| (pages.Length > MAX_SIZE_PAGE)
				|| (year.Length > MAX_SIZE_YEAR)
				|| (section.Length > MAX_SIZE_SECTION)
				|| (!registerNumberValidate(reg)))
				return false;
			try
			{
				long.Parse(reg);
				short.Parse(pages);
				short.Parse(year);
			}
			catch (Exception) { return false; }

			return true;
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

		private bool addDataToDataBase(String reg, String pages, String year, String section)
		{
			if (!CheckOrientedData(reg, pages, year, section))
			{
				MessageBox.Show("Ошибка: добавление записи невозможно! Запись содержит не корректные данные!",
					"Ошибка!",
					MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCMD = new SqlCommand("dbo.WriteBookData", sqlConnection))
				{
					sqlCMD.CommandType = System.Data.CommandType.StoredProcedure;

					SqlParameter valueReturn = new SqlParameter("valueReturn", SqlDbType.Int);
					valueReturn.Direction = ParameterDirection.ReturnValue;

					sqlCMD.Parameters.Add(valueReturn);
					sqlCMD.Parameters.AddWithValue("@reg", long.Parse(reg));
					sqlCMD.Parameters.AddWithValue("@pages", short.Parse(pages));
					sqlCMD.Parameters.AddWithValue("@year", short.Parse(year));
					sqlCMD.Parameters.AddWithValue("@section", section);
					sqlCMD.ExecuteScalar();

					int result = Convert.ToInt32(valueReturn.Value);
					if (result < 0)
					{
						MessageBox.Show("Ошибка: невозможно добавить запись с регистрационным номером " +
						reg + ", поскольку данный регистрационный номер"
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

		public static DataGridView readDataFromDB()
		{
			DataGridView data = new DataGridView();
			data.Columns.Add("Column1", "Register_Number");
			data.Columns.Add("Column2", "Count_Pages");
			data.Columns.Add("Column3", "Year_Publish");
			data.Columns.Add("Column4", "Section");

			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand command = new SqlCommand("dbo.ReadBookData", sqlConnection))
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
							sqlReader.GetValue(2).ToString(),
							sqlReader.GetValue(3).ToString());
						}
					}
					sqlReader.Close();
				}

				sqlConnection.Close();
			}

			return data;
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

			String reg = dataGridView1.Rows[index].Cells[0].Value.ToString();
			using (SqlConnection sqlConnection = new SqlConnection("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseSQL;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"))
			{
				sqlConnection.Open();

				using (SqlCommand sqlCMD = new SqlCommand("dbo.DeleteBookData", sqlConnection))
				{
					sqlCMD.CommandType = System.Data.CommandType.StoredProcedure;

					SqlParameter valueReturn = new SqlParameter("valueReturn", SqlDbType.Int);
					valueReturn.Direction = ParameterDirection.ReturnValue;

					sqlCMD.Parameters.Add(valueReturn);
					sqlCMD.Parameters.AddWithValue("@reg", long.Parse(reg));
					sqlCMD.ExecuteScalar();

					int result = Convert.ToInt32(valueReturn.Value);

					if (result < 0)
					{
						MessageBox.Show("Ошибка: невозможно удалить данную запись!" +
							" Данный регистрационный номер зарегистрирован! Чтобы удалить данную" +
							" запись необходимо удалить запись с данным регистрационным номером!", "Ошибка!",
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

		private void _btnInput_Click(object sender, EventArgs e)
		{
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

			if (addDataToDataBase(_txtPasswData.Text, _txtCountPage.Text, _txtDataPublish.Text, _txtSection.Text))
			{
				_copyData.Rows.Add(_txtPasswData.Text, _txtCountPage.Text, _txtDataPublish.Text, _txtSection.Text);
				dataGridView1.Rows.Add(_txtPasswData.Text, _txtCountPage.Text, _txtDataPublish.Text, _txtSection.Text);
			}
		}

		private void _txtPasswData_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && ((_txtPasswData.Text + number).Length > MAX_SIZE_REGNUM))
				e.Handled = true;
		}

		private void _txtCountPage_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtCountPage.Text + number).Length > MAX_SIZE_PAGE)
				e.Handled = true;
		}

		private void _txtDataPublish_KeyPress(object sender, KeyPressEventArgs e)
		{
			char number = e.KeyChar;

			if (!Char.IsDigit(number) && (number != 8))
				e.Handled = true;
			if ((number != 8) && (_txtDataPublish.Text + number).Length > MAX_SIZE_YEAR)
				e.Handled = true;
		}

		private void _txtSection_KeyPress(object sender, KeyPressEventArgs e)
		{
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

				using (SqlCommand sqlCMD = new SqlCommand("dbo.UpdateBookData", sqlConnection))
				{
					sqlCMD.CommandType = System.Data.CommandType.StoredProcedure;

					sqlCMD.Parameters.AddWithValue("@reg", long.Parse(dataGridView1.Rows[index].Cells[0].Value.ToString()));
					sqlCMD.Parameters.AddWithValue("@newPages", short.Parse(dataGridView1.Rows[index].Cells[1].Value.ToString()));
					sqlCMD.Parameters.AddWithValue("@newYear", short.Parse(dataGridView1.Rows[index].Cells[2].Value.ToString()));
					sqlCMD.Parameters.AddWithValue("@newSection", dataGridView1.Rows[index].Cells[3].Value.ToString());
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
