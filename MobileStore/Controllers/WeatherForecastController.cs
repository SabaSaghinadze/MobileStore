using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace MobileStore.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<JObject>> GetWeatherData()
        {
            var client = new HttpClient();
            var addressApiUrl = "http://ip-api.com/json/";
            var weatherApiUrl = "http://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=4f26db369b482d7708fd86a6156be2b4";

            var responseFromAddressApi = await client.GetAsync(addressApiUrl);

            if (!responseFromAddressApi.IsSuccessStatusCode)
            {
                return StatusCode(500);
            }

            var addressData = await responseFromAddressApi.Content.ReadAsStringAsync();

            dynamic addressDataObj = JObject.Parse(addressData);
            dynamic latitude = addressDataObj["lat"];
            dynamic longitude = addressDataObj["lon"];

            weatherApiUrl = weatherApiUrl.Replace("{lat}", latitude.ToString()).Replace("{lon}", longitude.ToString());

            var responseFromWeatherApi = await client.GetAsync(weatherApiUrl);

            if (!responseFromWeatherApi.IsSuccessStatusCode)
            {
                return StatusCode(500);
            }

            var weatherData = await responseFromWeatherApi.Content.ReadAsStringAsync();
            JObject weatherDataObj = JObject.Parse(weatherData);

            return weatherDataObj;
        }
    }
}
