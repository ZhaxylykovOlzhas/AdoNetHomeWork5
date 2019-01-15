using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace AdoNetHomeWork5
{
	public partial class Form1 : Form
	{
		SqlConnection connect = new SqlConnection();
		SqlCommand command = new SqlCommand();
		SqlDataReader reader;
		string connection = null;

		public Form1()
		{
			connection = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
			InitializeComponent();

			connect = new SqlConnection(connection);
			string Select = "select table_name from information_schema.tables where table_type= 'base table' and table_name not like 'sys%';";

			try
			{
				connect.Open();
				command = new SqlCommand(Select, connect);
				reader = command.ExecuteReader();
				while (reader.Read())
				{
					comboBox1.Items.Add(reader["table_name"]);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			connect?.Close();
			command?.Clone();

		}



		private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
		{
			SqlDataAdapter sqlDataAdapter = new SqlDataAdapter("Select * from " + comboBox1.Text, connect);
			DataSet data = new DataSet();
			sqlDataAdapter.Fill(data);
			dataGridView1.DataSource = data.Tables[0];
		}

	}
}
