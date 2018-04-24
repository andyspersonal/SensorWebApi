using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SensorWebApi.Models
{
    public class TemperatureReading
    {
        
        public long Id { get; set; }
        public double Temperature { get; set; }
        public DateTime TakenDateTime { get; set; }
        public string SensorName { get; set; }


    }
}
