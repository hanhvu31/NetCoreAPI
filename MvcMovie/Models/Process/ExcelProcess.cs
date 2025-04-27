using System.Data;
using OfficeOpenXml;
using System.IO;

namespace MvcMovie.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string path)
        {
            FileInfo fileInfo = new FileInfo(path);
            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                DataTable dt = new DataTable();

                foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
                {
                    dt.Columns.Add(firstRowCell.Text);
                }

                for (int rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
                {
                    var wsRow = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
                    DataRow row = dt.NewRow();
                    int i = 0;
                    foreach (var cell in wsRow)
                    {
                        row[i++] = cell.Text;
                    }
                    dt.Rows.Add(row);
                }

                return dt;
            }
        }
    }
}