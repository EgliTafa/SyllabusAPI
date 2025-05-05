using Swashbuckle.AspNetCore.Filters;
using Syllabus.ApiContracts.Courses;
using Syllabus.Domain.Sylabusses;

namespace SyllabusAPI.Example.Courses
{
    public class CreateCourseRequestExample : IExamplesProvider<CreateCourseRequestApiDTO>
    {
        public CreateCourseRequestApiDTO GetExamples()
        {
            return new CreateCourseRequestApiDTO
            {
                Title = "Rrjetat Kompjuterike Cisco (I-II)",
                Code = "NFC101",
                Semester = 1,
                LectureHours = 28,
                SeminarHours = 0,
                LabHours = 28,
                Credits = 8,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Elective,
                SyllabusId = 1,
                Detail = new CourseDetailRequestApiDTO
                {
                    AcademicProgram = "Master Shkencor 'Information Systems Engineering'",
                    AcademicYear = "2023-2025",
                    Language = "Anglisht",
                    CourseTypeLabel = "C / Me Zgjedhje",
                    EthicsCode = "Referuar Kodit të etikës së UT, miratuar me Vendim Nr. 12, datë 18.04.2011, studentët kanë për detyrë të respektojnë dispozitat e Kodit të Etikës, Universiteti i Tiranës: a. Të zbatojnë orarin e mësimit dhe t'u përmbahen rregullave të sanksionuara në Statutin dhe në Rregulloren e U.T. b. Të paraqiten në mënyrë serioze dhe dinjitoze në ambientet e institucionit, që nënkupton një veshje të përshtatshme, joekstravagante, si dhe përdorimin e një fjalori të përshtatshëm sipas normave të etikës, moralit dhe të mirësjelljes. c. Të respektojnë pedagogët, shokët dhe rregullat e mësimit. ... https://unitir.edu.al/images/dokumenta/Legjislacion/KodiEtikes.pdf",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim",
                    Credits = 8,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 28,
                        LabHours = 28,
                        PracticeHours = 0,
                        ExerciseHours = 0,
                        WeeklyHours = 4,
                        IndividualStudyHours = 144
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 10,
                        Test2Percent = 20,
                        Test3Percent = 0,
                        FinalExamPercent = 60
                    },
                    Objective = "Qëllimi i lëndës është të përgatisë studentët për të instaluar, konfiguruar dhe menaxhuar rrjete të thjeshta lokale dhe të gjera me referencë të veçantë për protokollet e rutimit.",
                    KeyConcepts = "CCNA, Cisco IOS, NetWorking",
                    Prerequisites = "Njohuri bazë të rrjeteve kompjuterike, arkitekturës së kompjuterëve dhe bazat e informatikës",
                    SkillsAcquired = "Në fund të kursit studentët do të jenë në gjendje të instalojnë, konfigurojnë dhe menaxhojnë rrjete të thjeshta lokale dhe të gjera me referencë të veçantë për protokollet e rutimit.",
                    CourseResponsible = "Dr. Julian Fezaj, MSc. Orion Lici",
                    Topics = new List<TopicRequestApiDTO>
                    {
                        new TopicRequestApiDTO
                        {
                            Title = "Hyrje në Rrjetat Kompjuterike",
                            Hours = 4,
                            Reference = "CCNA 1, Kapitulli 1"
                        },
                        new TopicRequestApiDTO
                        {
                            Title = "Protokollet e Rrjetit",
                            Hours = 6,
                            Reference = "CCNA 1, Kapitulli 2"
                        },
                        new TopicRequestApiDTO
                        {
                            Title = "Adresimi IP",
                            Hours = 8,
                            Reference = "CCNA 1, Kapitulli 3"
                        }
                    }
                }
            };
        }
    }
}
