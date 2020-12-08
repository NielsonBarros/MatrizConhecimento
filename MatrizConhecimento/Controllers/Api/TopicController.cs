using AutoMapper;
using MatrizConhecimento.Context;
using MatrizConhecimento.DTO;
using MatrizConhecimento.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MatrizConhecimento.Controllers.Api
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/[controller]")]
    [ApiController]
    public class TopicController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public TopicController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Topic
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDTO>>> GetTopics()
        {
            List<Topic> list =  await _context.Topics.Where(p => p.UserId == int.Parse(User.Identity.Name) && p.Active == true).ToListAsync();
            return _mapper.Map<List<TopicDTO>>(list);
        }

        // GET: api/Topic/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TopicDTO>> GetTopic(int id)
        {
            var topic = await _context.Topics.FirstOrDefaultAsync(p => p.Id ==  id && p.UserId == int.Parse(User.Identity.Name) && p.Active == true);

            if (topic == null)
            {
                return NotFound();
            }

            return _mapper.Map<TopicDTO>(topic);
        }

        // PUT: api/Topic/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTopic(int id, TopicDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            Topic topic = _mapper.Map<Topic>(dto);
            topic.UserId = int.Parse(User.Identity.Name);
            _context.Entry(topic).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TopicExists(id))
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

        // POST: api/Topic
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TopicDTO>> PostTopic(TopicDTO dto)
        {

            Topic topic = _mapper.Map<Topic>(dto);
            topic.UserId = int.Parse(User.Identity.Name);
            _context.Topics.Add(topic);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTopic", new { id = topic.Id }, _mapper.Map<TopicDTO>(topic));
        }

        // DELETE: api/Topic/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Topic>> DeleteTopic(int id)
        {
            var topic = await _context.Topics.FirstOrDefaultAsync(p => p.Id ==id && p.UserId == int.Parse(User.Identity.Name)) ;
            if (topic == null)
            {
                return NotFound();
            }

            topic.Active = false;
            _context.Entry(topic).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return topic;
        }

        private bool TopicExists(int id)
        {
            return _context.Topics.Any(e => e.Id == id);
        }
    }
}
