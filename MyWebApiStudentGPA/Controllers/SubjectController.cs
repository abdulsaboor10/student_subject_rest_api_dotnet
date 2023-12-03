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
        public async Task<ActionResult<SubjectDbDto>> getStudent(int id)
        {
            var s = await _db.subjectDbDto.FindAsync(id);
            if (s == null) {
                return NotFound();
            }
            return Ok(s);
        }
        [HttpGet("")]
        public async Task<ActionResult<List<SubjectDbDto>>> getAllStudents()
        {
            var s = await _db.subjectDbDto.ToListAsync();
            return Ok(s);
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
        public async Task<ActionResult> deleteStudent(int id)
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