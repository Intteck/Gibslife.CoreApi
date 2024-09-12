using System.Text.Json;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace Gibs.Api.Controllers
{
    [Route("api/metadata")]
    public class MetadataController(ControllerServices services, IWebHostEnvironment host)
        : SecureControllerBase(services)
    {
        [HttpGet("vehicles")]
        public async Task<IEnumerable<VehicleResponse>> ListVehicles()
        {
            var options = new JsonSerializerOptions
            {
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            var vehicles = await GetFromJsonAsync<VehicleResponse[]>(
                @"\Data\config\vehicles.json", options) ?? [];

            return vehicles;
        }

        [HttpGet("industries")]
        public async Task<IEnumerable<string>> ListIndustries()
        {
            var industries = await GetFromJsonAsync<string[]>(
                @"\Data\config\industries.json") ?? [];

            return industries;
        }

        [HttpGet("countries")]
        public async Task<IEnumerable<CountryResponse>> ListCountries()
        {
            var ngStates = await GetFromJsonAsync<StateResponse[]>(
                @"\Data\config\states.json") ?? [];

            return [new CountryResponse { CountryID = "NG", CountryName = "Nigeria", States = ngStates }];
        }

        private async Task<T?> GetFromJsonAsync<T>(string path, JsonSerializerOptions? options = null)
        {
            using var stream = System.IO.File.OpenRead(host.WebRootPath + path);
            return await JsonSerializer.DeserializeAsync<T>(stream, options);
        }

        public class VehicleResponse
        {
            [JsonPropertyName("brand")]
            public string Brand { get; set; } = string.Empty;

            [JsonPropertyName("models")]
            public string[] Models { get; set; } = [];
        }

        public class CountryResponse
        {
            public string CountryID { get; set; } = string.Empty;
            public string CountryName { get; set; } = string.Empty;
            public StateResponse[] States { get; set; } = [];
        }

        public class StateResponse
        {
            [JsonPropertyName("state_id")]
            public string StateID { get; set; } = string.Empty;
            [JsonPropertyName("state_name")]
            public string StateName { get; set; } = string.Empty;
            [JsonPropertyName("lgas")]
            public LgaResponse[] LGAs { get; set; } = [];
        }

        public class LgaResponse
        {
            [JsonPropertyName("lga_id")]
            public string LgaID { get; set; } = string.Empty;
            [JsonPropertyName("lga_name")]
            public string LgaName { get; set; } = string.Empty;
        }

    }
}
