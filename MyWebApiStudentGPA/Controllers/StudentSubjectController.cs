using System.Runtime.Intrinsics.X86;
using Core.Models;
using Core.Models.RequestModels;
using Core.Models.ResponseModels;
using DL.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyWebApiStudentGPA.Controllers
{
    [Route("api/student-subjects")]
    [ApiController]
    public class StudentSubjectController : ControllerBase
    {

        private StudentDbContext _db;

        public StudentSubjectController(StudentDbContext _dbContext)
        {
            this._db = _dbContext;
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<StudentSubjectDbDto>> getStudentSubject(int id)
        // {
        //     var s = await _db.subjectDbDto.FindAsync(id);
        //     if (s == null) {
        //         return NotFound();
        //     }
        //     return Ok(s);
        // }
        // [HttpGet("")]
        // public async Task<ActionResult<List<SubjectDbDto>>> getAllStudents()
        // {
        //     var s = await _db.subjectDbDto.ToListAsync();
        //     return Ok(s);
        // }
        [HttpPost]
        public async Task<ActionResult> addStudentSubject(int StudentId , int SubjectId , double?  Marks)
        {
            var student = await _db.studentDbDto.FindAsync(StudentId);
            var subject = await _db.subjectDbDto.FindAsync(SubjectId);

            if (student == null || subject == null){
                return NotFound();
            }
            await _db.studentSubjectDbDto.AddAsync(new StudentSubjectDbDto{
                StudentId = StudentId,
                SubjectId = SubjectId,
                Marks = Marks ?? 0
            });
            await _db.SaveChangesAsync();

            return Ok("Added Subject to Student Successfully");
        }
        [HttpPost("marks")]
        public async Task<ActionResult> addStudentSubjectMarks(int StudentId , int SubjectId , int marks)
        {
 
            var studentSubject = await _db.studentSubjectDbDto.FirstAsync(s => s.StudentId == StudentId && s.SubjectId == StudentId);
            if (studentSubject == null){
                return NotFound();
            }
            studentSubject.Marks = marks;
            await _db.SaveChangesAsync();

            return Ok("Added marks successfully");
        }


        // [HttpPut("{id}")]
        // public async Task<ActionResult<StudentSubjectMarksDto>> updateSubject(int id, SubjectRequestDto _Subject)
        // {
        //     var subject = await _db.subjectDbDto.FindAsync(id);
        //     if (subject == null)
        //     {
        //         return NotFound();
        //     }
        //     subject.Name = _Subject.Name;
        //     await _db.SaveChangesAsync();

        //     return Ok(_Subject);
        // }

        [HttpDelete("{id}")]
        public async Task<ActionResult> removeStudentSubject(int id)
        {
            var studentSubject = await _db.studentSubjectDbDto.FindAsync(id);
            if (studentSubject == null)
            {
                return NotFound();
            }
            _db.studentSubjectDbDto.Remove(studentSubject);
            await _db.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }

    }
}