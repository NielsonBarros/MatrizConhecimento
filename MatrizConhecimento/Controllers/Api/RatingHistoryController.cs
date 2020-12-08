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
    public class RatingHistoryController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public RatingHistoryController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/RatingHistory
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RatingHistoryDTO>>> GetRatingHistories()
        {
            var result = await _context.RatingHistories.ToListAsync();
            return _mapper.Map<List<RatingHistoryDTO>>(result);
        }

        // GET: api/RatingHistory/5
        [HttpGet("matter/{id}")]
        public async Task<ActionResult<IEnumerable<RatingHistoryDTO>>> GetRatingHistory(int id)
        {
            var ratingHistory = await _context.RatingHistories.Where(p => p.MatterId == id && p.UserId == int.Parse(User.Identity.Name)).ToListAsync();

            if (ratingHistory == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<RatingHistoryDTO>>(ratingHistory);
        }

        [HttpGet("topic/{id}")]
        public async Task<ActionResult<IEnumerable<RatingHistoryDTO>>> GetRatingTopicHistory(int id)
        {
            var ratingHistory = await _context.RatingHistories.Where(p => p.TopicId == id && p.UserId == int.Parse(User.Identity.Name)).ToListAsync();

            if (ratingHistory == null)
            {
                return NotFound();
            }

            return _mapper.Map<List<RatingHistoryDTO>>(ratingHistory);
        }

        [HttpPost]
        public async Task<ActionResult<RatingHistoryDTO>> PostRatingHistory(RatingHistoryDTO dto)
        {
            try
            {
                RatingHistory ratingHistory = _mapper.Map<RatingHistory>(dto);
                ratingHistory.UserId = int.Parse(User.Identity.Name);
                _context.RatingHistories.Add(ratingHistory);
                await _context.SaveChangesAsync();

                Rating rating = new Rating()
                {
                    UserId = ratingHistory.UserId,
                    MatterId = ratingHistory.MatterId,
                    TopicId = ratingHistory.TopicId,
                    RatingDate = ratingHistory.RatingDate,
                    RatingHistoryId = ratingHistory.Id,
                    Score = ratingHistory.Score
                };
                _context.Entry(rating).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetRatingHistory", new { id = ratingHistory.Id }, _mapper.Map<RatingHistoryDTO>(ratingHistory) );
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
