using api.Data;
using api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionaireController(QuestionaireDbContext context) : ControllerBase
    {
        private readonly QuestionaireDbContext _context = context;


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var questionItems = await _context.Questions.ToListAsync();

            var result = questionItems.Select(i => new QuestionaireResponse
            {
                Id = i.Id,
                Text = i.Text,
                Options = _context.Options.Where(o => o.QuestionId == i.Id).ToList(),
                AnswerId = null
            });

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post(QuestionaireRequest reqObject)
        {
            if (reqObject == null || string.IsNullOrEmpty(reqObject.Fullname) || !reqObject.Answer.Any()) return BadRequest();

            var questionIds = reqObject.Answer.Select(i => i.Id).Distinct();

            var items = await _context.Questions.Where(i => questionIds.Contains(i.Id)).ToListAsync();

            if (items == null) return NotFound();

            var score = reqObject.Answer.Count(i => items.Any(q => q.Id == i.Id && q.AnswerId == i.AnswerId));

            _context.Respondents.Add(new Respondent { Fullname = reqObject.Fullname, Score = score });
            await _context.SaveChangesAsync();

            return Ok(score);
        }
    }
}
