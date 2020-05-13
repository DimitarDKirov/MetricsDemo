using System;
using System.Threading.Tasks;
using App.Metrics;
using App.Metrics.Counter;
using Microsoft.AspNetCore.Mvc;

namespace MetricsDemo.Controllers
{
    [Route("super/useful/api")]
    public class DemoController : Controller
    {
        private readonly IMetrics _metrics;
        private static readonly Random Random = new Random();

        public DemoController(IMetrics metrics)
        {
            _metrics = metrics ?? throw new ArgumentNullException(nameof(metrics));
        }

        [HttpGet]
        public async Task<string> Delay()
        {
            var delay = Random.Next(1000, 5000);
            await Task.Delay(delay);
            return delay.ToString();
        }

        [HttpPost]
        public async Task<string> Create()
        {
            _metrics.Measure.Counter.Increment(new CounterOptions { Name = "requests_count", MeasurementUnit = Unit.Calls });
            return "OK";
        }

        [HttpDelete]
        public async Task<string> Remove()
        {
            _metrics.Measure.Counter.Increment(new CounterOptions { Name = "requests_count", MeasurementUnit = Unit.Calls });
            return "OK";
        }

        [HttpGet("boom")]
        public async Task<string> Boom()
        {
            var num = Random.Next(1, 5);
            switch (num)
            {
                case 1:
                    throw new OutOfMemoryException();
                case 2:
                    throw new NullReferenceException();
                case 3:
                    throw new Exception("Booooom!");
                case 4:
                    throw new IndexOutOfRangeException();
                case 5:
                    throw new ArgumentNullException();
                default:
                    throw new NullReferenceException();
            }
        }
    }
}