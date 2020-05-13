using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace MetricsDemo.Controllers
{
    [Route("super/useful/api")]
    public class DemoController : Controller
    {
        private static readonly Random Random = new Random();

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
            return "OK";
        }

        [HttpDelete]
        public async Task<string> Remove()
        {
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