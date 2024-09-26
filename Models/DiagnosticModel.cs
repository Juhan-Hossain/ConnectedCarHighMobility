namespace Demo_APP.Models;

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BatteryVoltage
    {
        public Data data { get; set; }
        public DateTime timestamp { get; set; }
        public object failure { get; set; }
    }

    public class Data
    {
        public string unit { get; set; }
        public double value { get; set; }
        public string location { get; set; }
        public Pressure pressure { get; set; }
        public string id { get; set; }
        public string status { get; set; }
        public string system { get; set; }
        public string ecu_id { get; set; }
        public int occurrences { get; set; }
    }

    public class Diagnostics
    {
        public BatteryVoltage battery_voltage { get; set; }
        public EngineCoolantTemperature engine_coolant_temperature { get; set; }
        public FuelLevel fuel_level { get; set; }
        public Odometer odometer { get; set; }
        public List<TirePressure> tire_pressures { get; set; }
        public List<TroubleCode> trouble_codes { get; set; }
    }

    public class EngineCoolantTemperature
    {
        public Data data { get; set; }
        public DateTime timestamp { get; set; }
        public object failure { get; set; }
    }

    public class FuelLevel
    {
        public double data { get; set; }
        public DateTime timestamp { get; set; }
        public object failure { get; set; }
    }

    public class Odometer
    {
        public Data data { get; set; }
        public DateTime timestamp { get; set; }
        public object failure { get; set; }
    }

    public class Pressure
    {
        public string unit { get; set; }
        public double value { get; set; }
    }

    public class Root
    {
        public Diagnostics diagnostics { get; set; }
    }

    public class TirePressure
    {
        public Data data { get; set; }
        public DateTime timestamp { get; set; }
        public object failure { get; set; }
    }

    public class TroubleCode
    {
        public Data data { get; set; }
        public DateTime timestamp { get; set; }
        public object failure { get; set; }
    }



