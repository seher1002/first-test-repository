


//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using WebApplication3.Data;
//using WebApplication3.Models;
//using System.Linq;

//public class MapController : Controller
//{
//    private readonly AppDbContext _context;

//    public MapController(AppDbContext context)
//    {
//        _context = context;
//    }

//    public async Task<IActionResult> Index(string traseu)
//    {
//        var puncte = await _context.LocationPoints.OrderBy(p => p.Id).ToListAsync();
//        var distante = await _context.Distances.ToListAsync();

//        int n = puncte.Count;
//        double[,] matriceDistantelor = new double[n, n];

//        // Creez mapping de la Id real la index
//        var idToIndex = puncte
//            .Select((p, index) => new { p.Id, index })
//            .ToDictionary(x => x.Id, x => x.index);

//        // Completez matricea folosind indexurile corecte
//        foreach (var d in distante)
//        {
//            if (idToIndex.ContainsKey(d.FromPointId) && idToIndex.ContainsKey(d.ToPointId))
//            {
//                int fromIndex = idToIndex[d.FromPointId];
//                int toIndex = idToIndex[d.ToPointId];

//                matriceDistantelor[fromIndex, toIndex] = d.DistanceMeters;
//            }
//        }

//        // Optimizăm traseul
//        var traseuOptim = CalculeazaRutaOptima(matriceDistantelor);

//        // Refacem lista de puncte în ordinea optimizată
//        var puncteOptimizate = traseuOptim.Select(i => puncte[i]).ToList();

//        // Pregătim modelul pentru View
//        var model = new HartaViewModel
//        {
//            PuncteInitiale = puncte,
//            PuncteOptimizate = puncteOptimizate
//        };

//        // Trimitem informația despre ce traseu să selectăm
//        ViewBag.TraseuSelectat = traseu;

//        return View(model);


//        // Calculează distanța pentru traseul inițial
//        double distantaInitiala = 0;
//        for (int i = 0; i < puncte.Count - 1; i++)
//        {
//            int fromIndex = idToIndex[puncte[i].Id];
//            int toIndex = idToIndex[puncte[i + 1].Id];
//            distantaInitiala += matriceDistantelor[fromIndex, toIndex];
//        }

//        // Calculează distanța pentru traseul optimizat
//        double distantaOptimizata = 0;
//        for (int i = 0; i < puncteOptimizate.Count - 1; i++)
//        {
//            int fromIndex = idToIndex[puncteOptimizate[i].Id];
//            int toIndex = idToIndex[puncteOptimizate[i + 1].Id];
//            distantaOptimizata += matriceDistantelor[fromIndex, toIndex];
//        }

//        // Diferența
//        double diferenta = distantaInitiala - distantaOptimizata;

//        // Trimitem și în View pentru afișare
//        ViewBag.DistantaInitiala = distantaInitiala;
//        ViewBag.DistantaOptimizata = distantaOptimizata;
//        ViewBag.DiferentaDistantelor = diferenta;

//    }

//    // Algoritm simplu Nearest Neighbor pentru optimizare
//    private List<int> CalculeazaRutaOptima(double[,] matrice)
//    {
//        int n = matrice.GetLength(0);
//        var vizitat = new bool[n];
//        var traseu = new List<int>();

//        int current = 0;
//        traseu.Add(current);
//        vizitat[current] = true;

//        for (int i = 1; i < n; i++)
//        {
//            double minDist = double.MaxValue;
//            int next = -1;

//            for (int j = 0; j < n; j++)
//            {
//                if (!vizitat[j] && matrice[current, j] < minDist && matrice[current, j] > 0)
//                {
//                    minDist = matrice[current, j];
//                    next = j;
//                }
//            }

//            if (next != -1)
//            {
//                traseu.Add(next);
//                vizitat[next] = true;
//                current = next;
//            }
//            else
//            {
//                // Dacă nu mai găsim vecin, ieșim
//                break;
//            }
//        }

//        return traseu;
//    }

//}


using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication3.Data;
using WebApplication3.Models;
using System.Linq;

public class MapController : Controller
{
    private readonly AppDbContext _context;

    public MapController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string traseu)
    {
        var puncte = await _context.LocationPoints.OrderBy(p => p.Id).ToListAsync();
        var distante = await _context.Distances.ToListAsync();

        int n = puncte.Count;
        double[,] matriceDistantelor = new double[n, n];

        // Creez mapping de la Id real la index
        var idToIndex = puncte
            .Select((p, index) => new { p.Id, index })
            .ToDictionary(x => x.Id, x => x.index);

        // Completez matricea folosind indexurile corecte
        foreach (var d in distante)
        {
            if (idToIndex.ContainsKey(d.FromPointId) && idToIndex.ContainsKey(d.ToPointId))
            {
                int fromIndex = idToIndex[d.FromPointId];
                int toIndex = idToIndex[d.ToPointId];

                matriceDistantelor[fromIndex, toIndex] = d.DistanceMeters;
            }
        }

        // Optimizăm traseul
        var traseuOptim = CalculeazaRutaOptima(matriceDistantelor);

        // Refacem lista de puncte în ordinea optimizată
        var puncteOptimizate = traseuOptim.Select(i => puncte[i]).ToList();

        // Calculează distanța pentru traseul inițial
        double distantaInitiala = 0;
        for (int i = 0; i < puncte.Count - 1; i++)
        {
            int fromIndex = idToIndex[puncte[i].Id];
            int toIndex = idToIndex[puncte[i + 1].Id];
            distantaInitiala += matriceDistantelor[fromIndex, toIndex];
        }

        // Calculează distanța pentru traseul optimizat
        double distantaOptimizata = 0;
        for (int i = 0; i < puncteOptimizate.Count - 1; i++)
        {
            int fromIndex = idToIndex[puncteOptimizate[i].Id];
            int toIndex = idToIndex[puncteOptimizate[i + 1].Id];
            distantaOptimizata += matriceDistantelor[fromIndex, toIndex];
        }

        // Diferența
        double diferenta = distantaInitiala - distantaOptimizata;

        // Pregătim modelul pentru View
        var model = new HartaViewModel
        {
            PuncteInitiale = puncte,
            PuncteOptimizate = puncteOptimizate
        };

        // Trimitem și informațiile suplimentare către View
        ViewBag.TraseuSelectat = traseu;
        ViewBag.DistantaInitiala = distantaInitiala;
        ViewBag.DistantaOptimizata = distantaOptimizata;
        ViewBag.DiferentaDistantelor = diferenta;

        return View(model);
    }

    // Algoritm simplu Nearest Neighbor pentru optimizare
    private List<int> CalculeazaRutaOptima(double[,] matrice)
    {
        int n = matrice.GetLength(0);
        var vizitat = new bool[n];
        var traseu = new List<int>();

        int current = 0;
        traseu.Add(current);
        vizitat[current] = true;

        for (int i = 1; i < n; i++)
        {
            double minDist = double.MaxValue;
            int next = -1;

            for (int j = 0; j < n; j++)
            {
                if (!vizitat[j] && matrice[current, j] < minDist && matrice[current, j] > 0)
                {
                    minDist = matrice[current, j];
                    next = j;
                }
            }

            if (next != -1)
            {
                traseu.Add(next);
                vizitat[next] = true;
                current = next;
            }
            else
            {
                break;
            }
        }

        return traseu;
    }
}

