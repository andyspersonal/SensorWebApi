using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SensorWebApi.Models
{
    public class TemperatureReadingContext : DbContext
    {
        public DbSet<TemperatureReading> TemperatureReadings { get; set;  }
        public TemperatureReadingContext(DbContextOptions<TemperatureReadingContext> options)
            : base(options)
        {

        }
    }
}
