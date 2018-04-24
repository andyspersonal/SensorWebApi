using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SensorWebApi.Models;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SensorWebApi.Controllers
{
    [Route("api/[controller]")]
    public class TemperatureReadingController : Controller
    {
        private readonly TemperatureReadingContext _context;


        public TemperatureReadingController(TemperatureReadingContext context)
        {
            
            _context = context;
        }

        [HttpGet]
        public IEnumerable<TemperatureReading> GetAll()
        {
            return _context.TemperatureReadings.ToList();
        }

        [HttpGet("GetLatest")]
        public IActionResult GetLatest()
        {
            var item = _context.TemperatureReadings.OrderByDescending(t => t.Id).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

      [HttpGet("GetMax")]
      public IActionResult GetMax(DateTime dateForMax)
      {
         DateTime start = dateForMax.Date;
         DateTime end = start.AddDays(1);
         var item = _context.TemperatureReadings.Where(t => t.TakenDateTime > start && t.TakenDateTime < end).OrderByDescending(t => t.Temperature).FirstOrDefault();
         if (item == null)
         {
            return NotFound();
         }
         return new ObjectResult(item);

      }

      [HttpGet("GetMin")]
      public IActionResult GetMin(DateTime dateForMin)
      {
         DateTime start = dateForMin.Date;
         DateTime end = start.AddDays(1);
         var item = _context.TemperatureReadings.Where(t => t.TakenDateTime > start && t.TakenDateTime < end).OrderBy(t => t.Temperature).FirstOrDefault();
         if (item == null)
         {
            return NotFound();
         }
         return new ObjectResult(item);

      }

      [HttpGet("GetTop")]
      public IEnumerable<TemperatureReading> GetTop(int count)
      {
         var items = _context.TemperatureReadings.OrderByDescending(s => s.Id).Take(count);
         return items;
      }
      

      [HttpGet("Search", Name = "GetTemperatureByDateRange")]
      public IEnumerable<TemperatureReading> Search(DateTime startdate, DateTime enddate)
      {
         var items = _context.TemperatureReadings.Where(s => s.TakenDateTime > startdate && s.TakenDateTime < enddate).ToList();
         return items;        
      }
        [HttpGet("{id}", Name = "GetTemperatureReading")]
        public IActionResult GetById(long id)
        {
            var item = _context.TemperatureReadings.FirstOrDefault(t => t.Id == id);
            if(item == null)
            {
                return NotFound();
            }
            return new ObjectResult(item);
        }

        [HttpPost]  
        public IActionResult Create([FromBody] TemperatureReading reading)
        {
            if(reading == null)
            {
                return BadRequest();
            }
            _context.TemperatureReadings.Add(reading);
            _context.SaveChanges();
            return CreatedAtRoute("GetTemperatureReading", new { id = reading.Id }, reading);

        }




        // GET: api/values
        /*
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }
        */
        /*
        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        */
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
