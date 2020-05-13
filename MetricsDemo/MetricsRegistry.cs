using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Timer;

namespace MetricsDemo
{
    public static class MetricsRegistry
    {
        public static CounterOptions CounterOptions = new CounterOptions
        {
            Name = "requests_count",
            MeasurementUnit = Unit.Calls
        };

        public static TimerOptions RequestTimer = new TimerOptions
        {
            Name = "requests_timer",
            MeasurementUnit = Unit.Requests,
            DurationUnit = TimeUnit.Milliseconds,
        };
    }
}