using OfficeOpenXml;
using WebApplication3.Models;
using WebApplication3.Data;

namespace WebApplication3.Services
{
    public class MatrixImporter
    {
        private readonly AppDbContext _context;

        public MatrixImporter(AppDbContext context)
        {
            _context = context;
        }

        public void ImportFromExcel(string filePath)
        {
           // ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(filePath));
            var sheet = package.Workbook.Worksheets["Matrix"];
            int size = sheet.Dimension.Rows - 1;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    var cell = sheet.Cells[i + 2, j + 2].Text;
                    if (double.TryParse(cell, out double dist))
                    {
                        _context.DistanceMatrix.Add(new DistanceMatrixEntry
                        {
                            FromIndex = i,
                            ToIndex = j,
                            Distance = dist
                        });
                    }
                }
            }

            _context.SaveChanges();
        }
    }
}
