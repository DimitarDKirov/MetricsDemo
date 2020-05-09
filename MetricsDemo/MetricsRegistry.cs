using App.Metrics;
using App.Metrics.Counter;
using App.Metrics.Gauge;
using App.Metrics.Meter;
using App.Metrics.Timer;

namespace MetricsDemo
{
    public static class MetricsRegistry
    {
        public static CounterOptions CounterOptions = new CounterOptions
        {
            Name = "requests",
            MeasurementUnit = Unit.Calls,
            ResetOnReporting = true
        };

        public static MeterOptions RequestMeter = new MeterOptions
        {
            Name = "requests_throughput",
            MeasurementUnit = Unit.Calls,
        };

        public static TimerOptions RequestTimer = new TimerOptions
        {
            Name = "requests_timer",
            MeasurementUnit = Unit.Requests,
            DurationUnit = TimeUnit.Milliseconds,
            RateUnit = TimeUnit.Milliseconds,
        };

        public static readonly GaugeOptions PhysicalMemory = new GaugeOptions
        {
            Name = "physical_memory",
            MeasurementUnit = Unit.Bytes
        };

        public static readonly MeterOptions Exceptions = new MeterOptions
        {
            Name = "exceptions_thrown",
            MeasurementUnit = Unit.Errors,
        };
    }
}