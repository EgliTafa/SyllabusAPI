using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Syllabus;
using Syllabus.ApiContracts.Courses;
using System.Collections.Generic;
using Syllabus.Domain.Sylabusses;

namespace SyllabusAPI.Example.Syllabus
{
    public class CreateSyllabusRequestExample : IExamplesProvider<CreateSyllabusRequestApiDTO>
    {
        public CreateSyllabusRequestApiDTO GetExamples()
        {
            return new CreateSyllabusRequestApiDTO
            {
                Name = "Computer Science Year 1",
                Courses = new List<CreateCourseRequestApiDTO>
                {
                    new CreateCourseRequestApiDTO
                    {
                        Title = "Introduction to Programming",
                        Code = "CS101",
                        Semester = 1,
                        LectureHours = 40,
                        SeminarHours = 15,
                        LabHours = 10,
                        Credits = 6,
                        Evaluation = EvaluationMethod.Exam,
                        Type = CourseType.Mandatory,
                        SyllabusId = 1
                    },
                    new CreateCourseRequestApiDTO
                    {
                        Title = "Database Systems",
                        Code = "CS102",
                        Semester = 2,
                        LectureHours = 35,
                        SeminarHours = 10,
                        LabHours = 15,
                        Credits = 5,
                        Evaluation = EvaluationMethod.Exam,
                        Type = CourseType.Mandatory,
                        SyllabusId = 1
                    }
                }
            };
        }
    }
}
