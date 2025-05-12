using System.Data;
using System.IO;
using OfficeOpenXml;

namespace MvcMovie.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ExcelToDataTable(string path)
        {
            var dt = new DataTable();
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                if (worksheet != null)
                {
                    for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                    {
                        dt.Columns.Add(worksheet.Cells[1, col].Text);
                    }
                    for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                    {
                        DataRow dr = dt.NewRow();
                        for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                        {
                            dr[col - 1] = worksheet.Cells[row, col].Text;
                        }
                        dt.Rows.Add(dr);
                    }
                }
            }
            return dt;
        }
    }
}