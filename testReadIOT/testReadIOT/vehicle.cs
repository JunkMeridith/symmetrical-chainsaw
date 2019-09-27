using System;

namespace testReadIOT
{
    public class Latitude
    {
        public DateTime lastUpdated { get; set; }
    }

    public class Vin
    {
        public DateTime lastUpdated { get; set; }
    }

    public class Speed
    {
        public DateTime lastUpdated { get; set; }
    }

    public class Longitude
    {
        public DateTime lastUpdated { get; set; }
    }

    public class EngineRpm
    {
        public DateTime lastUpdated { get; set; }
    }

    public class EngineRunTime
    {
        public DateTime lastUpdated { get; set; }
    }

    public class FuelLevel
    {
        public DateTime lastUpdated { get; set; }
    }

    public class AmbientAirTemperature
    {
        public DateTime lastUpdated { get; set; }
    }

    public class DistanceWithMalfunctionLight
    {
        public DateTime lastUpdated { get; set; }
    }

    public class Metadata
    {
        public DateTime lastUpdated { get; set; }
        public Latitude latitude { get; set; }
        public Vin vin { get; set; }
        public Speed speed { get; set; }
        public Longitude longitude { get; set; }
        public EngineRpm engineRpm { get; set; }
        public EngineRunTime engineRunTime { get; set; }
        public FuelLevel fuelLevel { get; set; }
        public AmbientAirTemperature ambientAirTemperature { get; set; }
        public DistanceWithMalfunctionLight distanceWithMalfunctionLight { get; set; }
    }
    public class Vehicle
    {
        public string latitude { get; set; }
        public string vin { get; set; }
        public float speed { get; set; }
        public string longitude { get; set; }
        public double engineRpm { get; set; }
        public int engineRunTime { get; set; }
        public string fuelLevel { get; set; }
        public string ambientAirTemperature { get; set; }
        public int distanceWithMalfunctionLight { get; set; }
        public Metadata metadata { get; set; }
        public int version { get; set; }
    }
}




