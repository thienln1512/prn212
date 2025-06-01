namespace HW3
{
    using System;
    using System.Collections.Generic;

    namespace DesignPatterns.Homework
    {
        public interface IWeatherStation
        {
            void RegisterObserver(IWeatherObserver observer);
            void RemoveObserver(IWeatherObserver observer);
            void NotifyObservers();

            float Temperature { get; }
            float Humidity { get; }
            float Pressure { get; }
        }

        public interface IWeatherObserver
        {
            void Update(float temperature, float humidity, float pressure);
        }

        public class WeatherStation : IWeatherStation
        {
            private readonly List<IWeatherObserver> _observers;
            private float _temperature;
            private float _humidity;
            private float _pressure;

            public WeatherStation()
            {
                _observers = new List<IWeatherObserver>();
            }

            public void RegisterObserver(IWeatherObserver observer)
            {
                if (observer == null) return;
                _observers.Add(observer);
                Console.WriteLine("WeatherStation: Registered an observer.");
            }

            public void RemoveObserver(IWeatherObserver observer)
            {
                if (observer == null) return;
                bool removed = _observers.Remove(observer);
                if (removed)
                    Console.WriteLine("WeatherStation: Removed an observer.");
            }

            public void NotifyObservers()
            {
                Console.WriteLine("WeatherStation: Notifying observers...");
                foreach (var observer in _observers)
                {
                    observer.Update(_temperature, _humidity, _pressure);
                }
            }

            public float Temperature => _temperature;
            public float Humidity => _humidity;
            public float Pressure => _pressure;

            public void SetMeasurements(float temperature, float humidity, float pressure)
            {
                Console.WriteLine("\n--- Weather Station: Weather measurements updated ---");
                _temperature = temperature;
                _humidity = humidity;
                _pressure = pressure;

                Console.WriteLine($"Temperature: {_temperature}°C");
                Console.WriteLine($"Humidity: {_humidity}%");
                Console.WriteLine($"Pressure: {_pressure} hPa");

                NotifyObservers();
            }
        }

        public class CurrentConditionsDisplay : IWeatherObserver
        {
            private float _temperature;
            private float _humidity;
            private float _pressure;
            private readonly IWeatherStation _weatherStation;

            public CurrentConditionsDisplay(IWeatherStation weatherStation)
            {
                _weatherStation = weatherStation;
                _weatherStation.RegisterObserver(this);
            }

            public void Update(float temperature, float humidity, float pressure)
            {
                _temperature = temperature;
                _humidity = humidity;
                _pressure = pressure;
            }

            public void Display()
            {
                Console.WriteLine("CurrentConditionsDisplay:");
                Console.WriteLine($"  Temperature: {_temperature}°C");
                Console.WriteLine($"  Humidity: {_humidity}%");
                Console.WriteLine($"  Pressure: {_pressure} hPa");
            }
        }

        public class StatisticsDisplay : IWeatherObserver
        {
            private float _maxTemp = float.MinValue;
            private float _minTemp = float.MaxValue;
            private float _tempSum = 0f;
            private int _numReadings = 0;
            private readonly IWeatherStation _weatherStation;

            public StatisticsDisplay(IWeatherStation weatherStation)
            {
                _weatherStation = weatherStation;
                _weatherStation.RegisterObserver(this);
            }

            public void Update(float temperature, float humidity, float pressure)
            {
                _numReadings++;
                _tempSum += temperature;
                if (temperature > _maxTemp) _maxTemp = temperature;
                if (temperature < _minTemp) _minTemp = temperature;
            }

            public void Display()
            {
                float avgTemp = _numReadings > 0 ? (_tempSum / _numReadings) : 0f;
                Console.WriteLine("StatisticsDisplay:");
                Console.WriteLine($"  Avg Temperature: {avgTemp:F1}°C");
                Console.WriteLine($"  Max Temperature: {_maxTemp:F1}°C");
                Console.WriteLine($"  Min Temperature: {_minTemp:F1}°C");
            }
        }

        public class ForecastDisplay : IWeatherObserver
        {
            private float _lastPressure;
            private float _currentPressure;
            private readonly IWeatherStation _weatherStation;
            private bool _firstUpdate = true;

            public ForecastDisplay(IWeatherStation weatherStation)
            {
                _weatherStation = weatherStation;
                _weatherStation.RegisterObserver(this);
            }

            public void Update(float temperature, float humidity, float pressure)
            {
                if (_firstUpdate)
                {
                    _currentPressure = pressure;
                    _firstUpdate = false;
                }
                else
                {
                    _lastPressure = _currentPressure;
                    _currentPressure = pressure;
                }
            }

            public void Display()
            {
                Console.WriteLine("ForecastDisplay:");
                if (_firstUpdate)
                {
                    Console.WriteLine("  No forecast data available yet.");
                }
                else if (_currentPressure > _lastPressure)
                {
                    Console.WriteLine("  Forecast: Improving weather on the way!");
                }
                else if (_currentPressure < _lastPressure)
                {
                    Console.WriteLine("  Forecast: Cooler, rainy weather expected.");
                }
                else
                {
                    Console.WriteLine("  Forecast: More of the same.");
                }
            }
        }

        class Program
        {
            static void Main(string[] args)
            {
                Console.WriteLine("Observer Pattern Homework - Weather Station\n");

                try
                {
                    WeatherStation weatherStation = new WeatherStation();

                    Console.WriteLine("Creating display devices...");
                    CurrentConditionsDisplay currentDisplay = new CurrentConditionsDisplay(weatherStation);
                    StatisticsDisplay statisticsDisplay = new StatisticsDisplay(weatherStation);
                    ForecastDisplay forecastDisplay = new ForecastDisplay(weatherStation);

                    Console.WriteLine("\nSimulating weather changes...");

                    weatherStation.SetMeasurements(25.2f, 65.3f, 1013.1f);

                    Console.WriteLine("\n--- Displaying Information ---");
                    currentDisplay.Display();
                    statisticsDisplay.Display();
                    forecastDisplay.Display();

                    weatherStation.SetMeasurements(28.5f, 70.2f, 1012.5f);

                    Console.WriteLine("\n--- Displaying Updated Information ---");
                    currentDisplay.Display();
                    statisticsDisplay.Display();
                    forecastDisplay.Display();

                    weatherStation.SetMeasurements(22.1f, 90.7f, 1009.2f);

                    Console.WriteLine("\n--- Displaying Final Information ---");
                    currentDisplay.Display();
                    statisticsDisplay.Display();
                    forecastDisplay.Display();

                    Console.WriteLine("\nRemoving CurrentConditionsDisplay...");
                    weatherStation.RemoveObserver(currentDisplay);

                    weatherStation.SetMeasurements(24.5f, 80.1f, 1010.3f);

                    Console.WriteLine("\n--- Displaying Information After Removal ---");
                    statisticsDisplay.Display();
                    forecastDisplay.Display();

                    Console.WriteLine("\nObserver Pattern demonstration complete.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred: {ex.Message}");
                }

                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
