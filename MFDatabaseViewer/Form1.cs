using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MFDatabase;
using MySql.Data.MySqlClient;

namespace MFDatabaseViewer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MFDatabaseFactory dbFactory = new MFDatabaseFactory();
            dbFactory.Server = "localhost";
            dbFactory.Port = 3306;
            dbFactory.Database = "MeltFinder";
            dbFactory.UserID = "root";
            dbFactory.Password = "Wargames99!";

            dbFactory.ConnectAndGetData();

            dataGridView1.DataSource = dbFactory.Materials;

            dbFactory.Close();
        }
    }
}
