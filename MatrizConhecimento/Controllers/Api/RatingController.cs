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
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class RatingController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RatingController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Rating
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRatings()
        {
            List<Rating> result = await _context.Ratings.Where(p => p.UserId == int.Parse(User.Identity.Name)).ToListAsync();
            return _mapper.Map<List<RatingDTO>>(result);
        }

        // GET: api/Rating/5
        [HttpGet("matter/{id}")]
        public async Task<ActionResult<RatingDTO>> GetRating(int id)
        {
            var rating = await _context.Ratings.FirstOrDefaultAsync(p => p.MatterId == id && p.UserId == int.Parse(User.Identity.Name));

            if (rating == null)
            {
                return NotFound();
            }

            return _mapper.Map<RatingDTO>(rating);
        }

        [HttpGet("topic/{id}")]
        public async Task<ActionResult<IEnumerable<RatingDTO>>> GetRatingTopic(int id)
        {
            var rating = await _context.Ratings.Where(p => p.TopicId == id && p.UserId == int.Parse(User.Identity.Name)).ToListAsync();

            if (rating == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<RatingDTO>>(rating);
        }

        private bool RatingExists(int id)
        {
            return _context.Ratings.Any(e => e.TopicId == id);
        }
    }
}
