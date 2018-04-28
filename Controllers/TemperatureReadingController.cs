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
            LogIpAddress();
            return _context.TemperatureReadings.ToList();
            //Dummy chage on mac;
        }

        [HttpGet("GetLatest")]
        public IActionResult GetLatest()
        {
            LogIpAddress();
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
         LogIpAddress();
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
         LogIpAddress();
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
         LogIpAddress();
         var items = _context.TemperatureReadings.OrderByDescending(s => s.Id).Take(count);
         return items;
      }
      

      [HttpGet("Search", Name = "GetTemperatureByDateRange")]
      public IEnumerable<TemperatureReading> Search(DateTime startdate, DateTime enddate)
      {
         LogIpAddress();
         var items = _context.TemperatureReadings.Where(s => s.TakenDateTime > startdate && s.TakenDateTime < enddate).ToList();
         return items;        
      }
        [HttpGet("{id}", Name = "GetTemperatureReading")]
        public IActionResult GetById(long id)
        {
            LogIpAddress();
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
            LogIpAddress();
            if(!this.Request.Host.ToString().ToLower().Contains("localhost"))
            {
               return this.Unauthorized();
            }
            
            var tst = this.Request;
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
        

        

      private void LogIpAddress()
      {
         if(this.HttpContext != null && this.HttpContext.Connection != null && this.HttpContext.Connection.RemoteIpAddress != null)
         {

            string message = DateTime.Now.ToString() + ": ";
            message += "Host: " + this.Request.Host;
            message += ", Path: " + this.Request.Path;
            message += ", Remote IP: " + this.HttpContext.Connection.RemoteIpAddress;
            Console.WriteLine(message);
         }
         
      }
    }
}
