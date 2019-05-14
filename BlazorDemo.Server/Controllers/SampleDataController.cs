using BlazorDemo.Server.Services;
using BlazorDemo.Shared;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorDemo.Server.Controllers
{
    [Route("api/[controller]/[action]")]
    public class SampleDataController : Controller
    {

        [HttpGet]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
            var rng = new Random();
            var summaries = new[]
            {
                "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
            };

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = summaries[rng.Next(summaries.Length)]
            });
        }

        [HttpGet]
        public MainLotListing FilterLots([FromServices] LotService lotService)
        {
            var filter = new LotFilter
            {
                isAllowedIndividual = true,
                isAllowedJuridic = true,
                from = 0,
                to = 2
            };

            return lotService.Filter(filter);
        }

        [HttpGet]
        public IEnumerable<MainLotItem> CardLots([FromQuery]int page,[FromQuery] int company, [FromServices] LotService lotService)
        {
            var filter = new LotFilter
            {
                isAllowedIndividual = true,
                isAllowedJuridic = true,
                from = (page - 1) * 2,
                to = (page) * 2,
                companyId=company
            };

            return lotService.CardItems(filter);
        }

    }
}
