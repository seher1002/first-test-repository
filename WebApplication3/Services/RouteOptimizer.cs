using System;
using System.Collections.Generic;

namespace WebApplication3.Services
{
    public class RouteOptimizer
    {
        public List<int> OptimizeRoute(double[,] matrix)
        {
            int n = matrix.GetLength(0);
            var visited = new bool[n];
            var route = new List<int>();

            int current = 0;
            visited[current] = true;
            route.Add(current);

            for (int step = 1; step < n; step++)
            {
                double minDist = double.MaxValue;
                int next = -1;

                for (int j = 0; j < n; j++)
                {
                    if (!visited[j] && matrix[current, j] < minDist)
                    {
                        minDist = matrix[current, j];
                        next = j;
                    }
                }

                if (next != -1)
                {
                    visited[next] = true;
                    route.Add(next);
                    current = next;
                }
            }

            return route;
        }
    }
}
