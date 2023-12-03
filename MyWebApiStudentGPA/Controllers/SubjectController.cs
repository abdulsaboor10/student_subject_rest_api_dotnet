using Core.Models.RequestModels;
using Core.Models.ResponseModels;
using DL.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyWebApiStudentGPA.Controllers
{
    [Route("api/subject")]
    [ApiController]
    public class SubjectController : ControllerBase
    {

        private StudentDbContext _db;

        public SubjectController(StudentDbContext _dbContext)
        {
            this._db = _dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getSubject(int id)
        {
            var s = await _db.subjectDbDto.FindAsync(id);
            if (s == null) {
                return NotFound();
            }
            return Ok(new SubjectResponseDto{
                Id = s.id,
                Name = s.Name
            });
        }
        [HttpGet("")]
        public async Task<ActionResult<List<SubjectDbDto>>> getAllSubjects()
        {
            var subjects = await _db.subjectDbDto.Select(s => new SubjectResponseDto{
                Id = s.id,
                Name = s.Name
            }).ToListAsync();
            return Ok(subjects);
        }
        [HttpPost]
        public async Task<ActionResult<SubjectResponseDto>> createSubject(SubjectRequestDto _Subject)
        {

            var subject = await _db.subjectDbDto.AddAsync(new SubjectDbDto
            {
                Name =  _Subject.Name
            });
            await _db.SaveChangesAsync();

            return Ok(_Subject);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<SubjectDbDto>> updateSubject(int id, SubjectRequestDto _Subject)
        {
            var subject = await _db.subjectDbDto.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            subject.Name = _Subject.Name;
            await _db.SaveChangesAsync();

            return Ok(_Subject);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteSubject(int id)
        {
            var subject = await _db.subjectDbDto.FindAsync(id);
            if (subject == null)
            {
                return NotFound();
            }
            _db.subjectDbDto.Remove(subject);
            await _db.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }

    }
}