namespace MemoryCacheExample
{
    public class WeatherForecast
    {
        public DateTime ResponseDateCache { get; set; }
        public DateTime ResponseDateReal { get; set; }
        public DateOnly Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string? Summary { get; set; }
    }
}
