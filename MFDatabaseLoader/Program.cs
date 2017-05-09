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
using MFDatabase;

namespace MFDatabaseLoader
{
    class Program
    {
        static void Main(string[] args)
        {
            MFDatabaseFactory dbFactory=new MFDatabaseFactory();
            dbFactory.Server = "localhost";
            dbFactory.Port = 3306;
            dbFactory.Database = "MeltFinder";
            dbFactory.UserID = "root";
            dbFactory.Password = "Wargames99!";

            dbFactory.ConnectAndGetData();

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
                dbFactory.Materials.Rows.Add(data.ToArray());
            }

            book.Close();
            stream.Close();
            dbFactory.Close();
        }
    }
}
