using Syllabus.ApiContracts.Courses;

namespace SyllabusAPI.RequestExamples.Courses
{
    /// <summary>
    /// Example request for adding course details.
    /// </summary>
    public class AddCourseDetailRequestExample
    {
        /// <summary>
        /// Gets an example request for adding course details.
        /// </summary>
        public static CourseDetailRequestApiDTO Example => new()
        {
            AcademicProgram = "Master Shkencor \"Information Systems Engineering\"",
            AcademicYear = "2023-2025",
            Language = "Anglisht",
            CourseTypeLabel = "C / Me Zgjedhje",
            EthicsCode = "Referenca në Kodin e Etikës së Universitetit",
            ExamMethod = "Provim",
            TeachingFormat = "Me Shkrim",
            Credits = 8,
            TeachingPlan = new()
            {
                LectureHours = 28,
                LabHours = 28,
                PracticeHours = 0,
                ExerciseHours = 0,
                WeeklyHours = 4,
                IndividualStudyHours = 144
            },
            EvaluationBreakdown = new()
            {
                ParticipationPercent = 10,
                Test1Percent = 15,
                Test2Percent = 15,
                Test3Percent = 20,
                FinalExamPercent = 40
            },
            Objective = "The objective of this course is to provide students with a comprehensive understanding of networking fundamentals...",
            KeyConcepts = "Networking, TCP/IP, Routing, Switching, Network Security",
            Prerequisites = "Basic understanding of computer systems and operating systems",
            SkillsAcquired = "Network configuration, Troubleshooting, Security implementation",
            CourseResponsible = "Dr. John Smith",
            Topics = new()
            {
                new()
                {
                    Title = "CCNA 1 introduces the field of computer networks",
                    Hours = 4,
                    Reference = "Cisco Networking Academy"
                },
                new()
                {
                    Title = "Cisco IOS",
                    Hours = 2,
                    Reference = "Cisco Documentation"
                },
                new()
                {
                    Title = "Addressing schemes for IPv4 and IPv6",
                    Hours = 2,
                    Reference = "RFC 791, RFC 2460"
                }
            }
        };
    }
} 