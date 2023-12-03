using Core.Models.RequestModels;
using Core.Models.ResponseModels;
using DL.DbModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MyWebApiStudentGPA.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {

        private StudentDbContext _db;

        public StudentController(StudentDbContext _dbContext)
        {
            this._db = _dbContext;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> getStudent(int id)
        {
            var st = await _db.studentDbDto.FindAsync(id);

            return Ok(new StudentResponseDto  {
                Id = st.Id,
                Name = st.Name , 
                RollNo = st.RollNumber,
                PhoneNumber = st.PhoneNumber,
                GPA = st.GPA ?? 0
            });
        }
        [HttpGet("")]
        public async Task<ActionResult> getAllStudents()
        {
            var st = await _db.studentDbDto.Select(st => new StudentResponseDto  {
                Id = st.Id,
                Name = st.Name , 
                RollNo = st.RollNumber,
                PhoneNumber = st.PhoneNumber,
                GPA = st.GPA ?? 0
            }).ToListAsync();
            return Ok(st);
        }

        [HttpGet("{id}/subjects")]
        public async Task<ActionResult> GetSubjects(int id)
        {
            var result = await _db.studentDbDto
                .Include(st => st.StudentSubjects)
                .ThenInclude(st_sb => st_sb.SubjectDbDto)
                .FirstOrDefaultAsync(st => st.Id == id);

            if (result == null)
            {
                return NotFound();
            }

            var subjects = result.StudentSubjects.Select(st_sub => new SubjectResponseDto {
                Id = st_sub.Id,
                Name = st_sub.SubjectDbDto.Name
            }).ToList();

            return Ok(subjects);
        }

        [HttpGet("{student_id}/subjects/{subject_id}/marks")]
        public async Task<ActionResult> GetSubjectMarks(int student_id , int subject_id)
        {
            var studentSubject = await _db.studentSubjectDbDto.FirstAsync(st_sub => st_sub.StudentId == student_id && st_sub.SubjectId  == subject_id);

            if (studentSubject == null)
            {
                return NotFound();
            }

            return Ok(studentSubject.Marks);

        }

        [HttpGet("{student_id}/marks")]
        public async Task<ActionResult> GetAllSubjectMarks(int student_id)
        {
            var marks = await _db.studentSubjectDbDto.Include(st_sub => st_sub.SubjectDbDto).Where(st_sub => st_sub.StudentId == student_id).Select(st_sub => new SubjectMarksResponseDto{
                Id = st_sub.Id,
                Name = st_sub.SubjectDbDto.Name,
                Marks = st_sub.Marks
            }).ToListAsync();
            
            return Ok(marks);

        }


        [HttpPost]
        public async Task<ActionResult> createStudent(StudentRequestDto _Student)
        {

            var student = await _db.studentDbDto.AddAsync(new StudentDbDto
            {
                Name = _Student.Name,
                RollNumber = _Student.RollNo,
                PhoneNumber = _Student.PhoneNumber,
                GPA = _Student.GPA != null ? _Student.GPA : null
            });
            await _db.SaveChangesAsync();

            return Ok(_Student);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> updateStudent(int id, StudentRequestDto _Student)
        {
            var student = await _db.studentDbDto.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            student.Name = _Student.Name;
            student.PhoneNumber = _Student.PhoneNumber;
            student.RollNumber = _Student.RollNo;
            student.GPA = _Student.GPA != null ? _Student.GPA : student.GPA;

            await _db.SaveChangesAsync();

            return Ok(student);
        }

        [HttpPatch("{id}/gpa")]
        public async Task<ActionResult> addGPA(int id, double GPA)
        {
            var student = await _db.studentDbDto.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            student.GPA = GPA;

            await _db.SaveChangesAsync();

            return Ok(student);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> deleteStudent(int id)
        {
            var student = await _db.studentDbDto.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            _db.studentDbDto.Remove(student);
            await _db.SaveChangesAsync();

            return Ok("Deleted Successfully");
        }

    }
}