using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
        public async Task<string> TestGetWithRandomDelay()
        {
            var delay = Random.Next(1000, 10000);
            using (_metrics.Measure.Timer.Time(MetricsRegistry.RequestTimer, new MetricTags("req", "RandomDelay")))
            {
                await Task.Delay(delay);
            }

            return delay.ToString();
        }

        [HttpGet("withDelay")]
        public async Task<string> Get(int delay)
        {
            using (_metrics.Measure.Timer.Time(MetricsRegistry.RequestTimer, new MetricTags("req", "ParamDelay")))
            {
                await Task.Delay(delay);
            }

            return "OK";
        }

        [HttpPost]
        public async Task<string> Create(string tag)
        {
            var tags = new MetricTags("ctag", string.IsNullOrEmpty(tag) ? "Unknown" : tag);
            _metrics.Measure.Counter.Increment(MetricsRegistry.CounterOptions, tags);
            return "OK";
        }

        [HttpDelete]
        public async Task<string> Remove(string tag)
        {
            var tags = new MetricTags("ctag", string.IsNullOrEmpty(tag) ? "Unknown" : tag);
            _metrics.Measure.Counter.Decrement(MetricsRegistry.CounterOptions, tags);
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