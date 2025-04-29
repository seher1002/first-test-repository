using OfficeOpenXml;
using System.ComponentModel;

namespace WebApplication3.Services
{
    public class ExcelMatrixService
    {
        private readonly string _filePath;

        public ExcelMatrixService(string filePath)
        {
            _filePath = filePath;
        }

        public double[,] LoadMatrix(out List<string> pointOrder)
        {
            OfficeOpenXml.ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            using var package = new ExcelPackage(new FileInfo(_filePath));
            var sheet = package.Workbook.Worksheets["Matrix"];
            int n = sheet.Dimension.Rows - 1;

            pointOrder = new List<string>();
            for (int i = 2; i <= n + 1; i++)
                pointOrder.Add(sheet.Cells[i, 1].Text);

            double[,] matrix = new double[n, n];

            for (int i = 0; i < n; i++)
                for (int j = 0; j < n; j++)
                {
                    var cell = sheet.Cells[i + 2, j + 2].Text;
                    double.TryParse(cell, out matrix[i, j]);
                }

            return matrix;
        }

    }
}
