namespace Zephyr.Backend.Contracts.Dtos.OpenWeather;

public record MainInfo
{
    public double Temp { get; init; }
    public int Humidity { get; init; }
    public int Pressure { get; init; }
}