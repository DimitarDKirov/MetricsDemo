using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Metrics;
using Microsoft.Extensions.Hosting;

namespace MetricsDemo
{
    public class ProcessMemory : IHostedService
    {
        private static readonly TimeSpan TimerInterval = TimeSpan.FromSeconds(5);
        private readonly IMetrics _metrics;
        private readonly Process _process;
        private readonly Timer _timer;

        public ProcessMemory(IMetrics metrics)
        {
            _metrics = metrics;
            _process = Process.GetCurrentProcess();
            _timer = new Timer(OnTimer);
        }

        private void OnTimer(object state)
        {
            _metrics.Measure.Gauge.SetValue(MetricsRegistry.PhysicalMemory, _process.WorkingSet64);
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer.Change(TimerInterval, TimerInterval);
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
