


using System.Data;
using System.Text;
using WebApplication3.Models;
using ExcelDataReader;
using ExcelDataReader;


namespace WebApplication3.Data
{
    public static class ExcelReader
    {
        public static List<LocationPoint> ReadRoute(string filePath)
        {
            var points = new List<LocationPoint>();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            using var stream = File.Open(filePath, FileMode.Open, FileAccess.Read);
            using var reader = ExcelReaderFactory.CreateReader(stream);

            // Adaugă configurația pentru AsDataSet
            var conf = new ExcelDataSetConfiguration
            {
                ConfigureDataTable = _ => new ExcelDataTableConfiguration
                {
                    UseHeaderRow = true
                }
            };

            var result = reader.AsDataSet(conf);
            var table = result.Tables[0];

            for (int i = 0; i < table.Rows.Count; i++)
            {
                var row = table.Rows[i];
                if (double.TryParse(row[4]?.ToString(), out double lat) &&
                    double.TryParse(row[5]?.ToString(), out double lng))
                {
                    points.Add(new LocationPoint
                    {
                        Lat = lat,
                        Lng = lng,
                        Timestamp = DateTime.TryParse(row[2]?.ToString(), out var ts) ? ts : DateTime.MinValue
                    });
                }
            }

            return points;
        }
    }
}
