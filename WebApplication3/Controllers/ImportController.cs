using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using WebApplication3.Models;
using WebApplication3.Data;   // Asigură-te că e namespace-ul corect

public class ImportController : Controller
{
    private readonly AppDbContext _context;

    public ImportController(AppDbContext context)
    {
        _context = context;
    }

    public IActionResult ImportDistances()
    {
        string filePath = "C:\\Users\\diana\\source\\repos\\WebApplication3\\Traseu 3 - SB 99 ULB.xlsx"; // <-- Calea corectă

        using (var workbook = new XLWorkbook(filePath))
        {
            var worksheet = workbook.Worksheet("Matrix"); // <-- citim sheet-ul Matrix
            int numberOfPoints = worksheet.LastRowUsed().RowNumber(); // de ex. 99 puncte

            // Citim header-ul (coloana A) ca să avem numele punctelor
            List<string> pointNames = new List<string>();

            for (int col = 2; col <= numberOfPoints + 1; col++) // +1 ca să sari de A1
            {
                var pointName = worksheet.Cell(1, col).GetString();
                pointNames.Add(pointName);
            }

            for (int row = 2; row <= numberOfPoints + 1; row++) // +1 pentru ca rândul 1 e header
            {
                for (int col = 2; col <= numberOfPoints + 1; col++)
                {
                    var fromPointName = worksheet.Cell(row, 1).GetString();
                    var toPointName = worksheet.Cell(1, col).GetString();

                    double distanceValue = 0;

                    var cellValue = worksheet.Cell(row, col).Value;

                    if (double.TryParse(cellValue.ToString(), out double parsedValue))
                    {
                        distanceValue = parsedValue;
                    }
                    else
                    {
                        distanceValue = 0; // Celula invalidă sau goală -> 0
                    }

                    if (row - 2 == col - 2) continue; // Dacă e pe diagonală (de la un punct la el însuși), sărim

                    var distance = new Distance
                    {
                        FromPointId = row - 1, // Point_1 -> ID 1, Point_2 -> ID 2 etc.
                        ToPointId = col - 1,
                        DistanceMeters = distanceValue
                    };

                    _context.Distances.Add(distance);
                }
            }

            _context.SaveChanges();
        }

        return Content("Import terminat cu succes!");
    }
}
