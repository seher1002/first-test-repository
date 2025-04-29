using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System;

namespace WebApplication3.Services
{
    public class MapboxService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = "AIzaSyB8dW-jrXJP1CfAGDP6SjrnqdM5zEnCsz4"; // ← înlocuiește cu tokenul tău

        public MapboxService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<double[,]> GetDistanceMatrixAsync(List<(double lng, double lat)> coordinates)
        {
            string baseUrl = "https://api.mapbox.com/directions-matrix/v1/mapbox/driving";

            // Formatăm locațiile
            var coordString = string.Join(";", coordinates.ConvertAll(c => $"{c.lng},{c.lat}"));
            string url = $"{baseUrl}/{coordString}?annotations=distance&access_token={_apiKey}";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            using var doc = JsonDocument.Parse(content);

            var matrix = doc.RootElement.GetProperty("distances");

            int n = matrix.GetArrayLength();
            var result = new double[n, n];

            for (int i = 0; i < n; i++)
            {
                var row = matrix[i].EnumerateArray();
                int j = 0;
                foreach (var val in row)
                {
                    result[i, j++] = val.GetDouble();
                }
            }

            return result;
        }
    }
}
