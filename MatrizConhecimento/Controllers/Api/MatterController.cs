using AutoMapper;
using MatrizConhecimento.Context;
using MatrizConhecimento.DTO;
using MatrizConhecimento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatrizConhecimento.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class MatterController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MatterController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Matter
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MatterDTO>>> GetMatters()
        {
            List<Matter> result = await _context.Matters.Where(p => p.UserId == int.Parse(User.Identity.Name) && p.Active == true).ToListAsync(); ;
            return _mapper.Map<List<MatterDTO>>(result);
        }

        // GET: api/Matter/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MatterDTO>> GetMatter(int id)
        {
            var matter = await _context.Matters.FirstOrDefaultAsync(p => p.Id == id && p.UserId == int.Parse(User.Identity.Name) && p.Active == true); ;

            if (matter == null)
            {
                return NotFound();
            }

            return _mapper.Map<MatterDTO>(matter);
        }

        // PUT: api/Matter/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatter(int id, MatterDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            Matter matter = _mapper.Map<Matter>(dto);
            matter.UserId = int.Parse(User.Identity.Name);
            _context.Entry(matter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatterExists(id))
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

        // POST: api/Matter
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Matter>> PostMatter(MatterDTO dto)
        {
            try
            {
                Matter matter = _mapper.Map<Matter>(dto);
                matter.UserId = int.Parse(User.Identity.Name);
                _context.Matters.Add(matter);
                _ = await _context.SaveChangesAsync();

                Rating rating = new Rating()
                {
                    MatterId = matter.Id,
                    TopicId = matter.TopicId,
                    UserId = matter.UserId,
                    RatingDate = null,
                    RatingHistoryId = null,
                    Score = null
                };

                _context.Ratings.Add(rating);

                _ = await _context.SaveChangesAsync();

                return CreatedAtAction("GetMatter", new { id = matter.Id }, _mapper.Map<MatterDTO>(matter));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        // DELETE: api/Matter/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Matter>> DeleteMatter(int id)
        {
            var matter = await _context.Matters.FindAsync(id);
            if (matter == null)
            {
                return NotFound();
            }

            matter.Active = false;
            _context.Entry(matter).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return matter;
        }

        private bool MatterExists(int id)
        {
            return _context.Matters.Any(e => e.Id == id);
        }
    }
}
