using OfficeOpenXml;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace MvcMovie.Models.Process
{
    public class ExcelProcess
    {
        public DataTable ReadExcelFile(Stream fileStream)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using (var package = new ExcelPackage(fileStream))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
                DataTable table = new DataTable();

                // Đọc header
                for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                {
                    string columnName = worksheet.Cells[1, col].Text;
                    if (string.IsNullOrWhiteSpace(columnName))
                        columnName = "Column" + col;
                    table.Columns.Add(columnName);
                }

                // Đọc từng dòng dữ liệu
                for (int row = worksheet.Dimension.Start.Row + 1; row <= worksheet.Dimension.End.Row; row++)
                {
                    DataRow dataRow = table.NewRow();
                    for (int col = worksheet.Dimension.Start.Column; col <= worksheet.Dimension.End.Column; col++)
                    {
                        dataRow[col - 1] = worksheet.Cells[row, col].Text;
                    }
                    table.Rows.Add(dataRow);
                }

                return table;
            }
        }
    }
}
