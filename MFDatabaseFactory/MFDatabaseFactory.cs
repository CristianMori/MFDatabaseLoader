using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MFDatabase
{
    public class MFDatabaseFactory
    {
        
        public String Server { get; set; }
        public UInt32 Port { get; set; }
        public String Database { get; set; }
        public String UserID { get; set; }
        public String Password { get; set; }

        private DataSet dataSet = new DataSet();
        private MySqlConnection connection = null;
        private MySqlDataAdapter material = null;

        public void Connect()
        {
            try
            {
                connection = new MySqlConnection(CreateConnectionString());
                connection.Open();
            }
            catch
            {
                throw new Exception("Cannot Connect to MFDatabase");
            }
            PostConenctionOps();            
        }

        private String CreateConnectionString()
        {
            MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();
            stringBuilder.Server = Server;
            stringBuilder.Port = Port;
            stringBuilder.Database = Database;
            stringBuilder.UserID = UserID;
            stringBuilder.Password = Password;
            return stringBuilder.ConnectionString;
        }

        private void PostConenctionOps()
        {
            material = new MySqlDataAdapter("SELECT * FROM Materials", connection);
            material.TableMappings.Add("Table", "Materials");
            MySqlCommandBuilder builder = new MySqlCommandBuilder(material);
        }

        public void ConnectAndGetData()
        {
            Connect();
            RetrieveData();
        } 

        public DataTable Materials 
        {
            get
            {
                return dataSet.Tables["Materials"];                
            }
        }

        public void RetrieveData()
        {
            material.Fill(dataSet);
        }

        public void UpdateData()
        {
            material.Update(dataSet);
        }

        public void Close()
        {
            try
            {
                connection.Close();
                connection.Dispose();
                material.Dispose();
                dataSet.Dispose();
            }
            catch
            {
            }
            connection = null;
            material = null;
            dataSet = null;
        }

  
    }
}
