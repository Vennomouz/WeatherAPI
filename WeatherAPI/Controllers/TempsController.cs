using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherAPI.Classes;
using WeatherAPI.Models;


namespace WeatherAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempsController : ControllerBase
    {
        private readonly WeatherContext _context;
        Metodos getOpenWeather = new Metodos();
        public TempsController(WeatherContext context)
        {
            _context = context;

            //_context.Temps
            //    .Where(t => t.name == "Guarulhos")
            //    .FirstOrDefault();
            
        }

        // GET: api/Temps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Temp>>> GetTemps()
        {
            return await _context.Temps.ToListAsync();
        }

        // GET: api/Temps/5
        [HttpGet("{name}")]
        public async Task<ActionResult<Temp>> GetTemp(string name)//long id
        {
            //var temp = await _context.Temps.FindAsync(id); //long id
            var temp = _context.Temps.Where(t => t.name == name).FirstOrDefault();

            if (temp == null)
            {
                //return NotFound();
                temp = new Temp();
                dynamic dados = getOpenWeather.metodoGetAsync(name).Result;

                temp.name = dados.name;
                temp.tempMax = dados.main.temp_max;
                temp.tempMin = dados.main.temp_min;
                temp.tempAtual = dados.main.temp;

                await PostTemp(temp);
                temp = await _context.Temps.FindAsync(temp.id);
                //var temp = _context.Temps.Single(t => t.name == name);

                return temp;
            }
            else if (DateTime.Now.AddMinutes(-20) >= temp.date)
            {
                dynamic dados = getOpenWeather.metodoGetAsync(name).Result;

                temp.name = dados.name;
                temp.tempMax = dados.main.temp_max;
                temp.tempMin = dados.main.temp_min;
                temp.tempAtual = dados.main.temp;
                temp.date = DateTime.Now;
                await PutTemp(temp.id, temp);
                temp = await _context.Temps.FindAsync(temp.id);
                //var temp = _context.Temps.Single(t => t.name == name);

                return temp;
            }
            

            return temp;
        }

        // PUT: api/Temps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemp(long id, Temp temp)
        {
            if (id != temp.id)
            {
                return BadRequest();
            }

            _context.Entry(temp).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TempExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Temps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Temp>> PostTemp(Temp temp)
        {
            _context.Temps.Add(temp);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTemp), new { id = temp.id }, temp);
        }

        // DELETE: api/Temps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemp(long id)
        {
            var temp = await _context.Temps.FindAsync(id);
            //var temp = _context.Temps.Single(t => t.name == name);
            if (temp == null)
            {
                return NotFound();
            }

            _context.Temps.Remove(temp);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TempExists(long id)
        {
            return _context.Temps.Any(e => e.id == id);
        }
    }
}
