using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using NPOI.SS.UserModel;

namespace MFDatabaseLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            DataSet dataSet=new DataSet();
            MySqlConnectionStringBuilder stringBuilder=new MySqlConnectionStringBuilder();
            stringBuilder.Server = "localhost";
            stringBuilder.Port = 3306;
            stringBuilder.Database = "MeltFinder";
            stringBuilder.UserID = "root";
            stringBuilder.Password = "Wargames99!";
            MySqlConnection connection = new MySqlConnection(stringBuilder.ConnectionString);
            connection.Open();
            
            MySqlDataAdapter Material = new MySqlDataAdapter("SELECT * FROM Materials",connection);
            Material.TableMappings.Add("Table", "Materials");
            MySqlCommandBuilder builder = new MySqlCommandBuilder(Material);
            Material.Fill(dataSet);
            
            FileStream stream = new FileStream("f:\\MeltFinderT.xls",FileMode.Open);

            IWorkbook book = WorkbookFactory.Create(stream);
            ISheet sheet = book.GetSheet("MeltFinderT");
            for (Int32 ii = 1; ii < sheet.LastRowNum; ii++)
            {
                IRow currentRow = sheet.GetRow(ii);

                if (currentRow==null || currentRow.GetCell(0) == null)
                    continue;

                List<object> data = new List<object>();
                data.Add(ii + 5497);
                for (Int32 ij=0; ij<29; ij++)
                    data.Add(currentRow.GetCell(ij).ToString());
                dataSet.Tables["Materials"].Rows.Add(data.ToArray());
            }

            book.Close();
            stream.Close();
            Material.Update(dataSet);
            Material.Dispose();
            connection.Clone();
            connection.Dispose();
        }
    }
}
