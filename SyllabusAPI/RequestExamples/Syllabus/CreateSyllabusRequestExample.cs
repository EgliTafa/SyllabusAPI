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
                Name = "Master Shkencor në Inxhinieri e Sistemeve të Informacionit, Viti Akademik 2023-2025",
                Courses = new List<CreateCourseRequestApiDTO>
                {
                    CreateNetworkingFundamentalsCourse(1),
                    CreateNetworkingFundamentalsCourse(2),
                    CreateNetworkingFundamentalsCourse(3),
                    CreateNetworkingFundamentalsCourse(4),
                    CreateNetworkingFundamentalsCourse(5)
                }
            };
        }

        private static CreateCourseRequestApiDTO CreateNetworkingFundamentalsCourse(int syllabusId)
        {
            return new CreateCourseRequestApiDTO
            {
                Title = "Networking Fundamentals Cisco (I-II)",
                Code = "NFC101",
                Semester = 1,
                LectureHours = 28,
                SeminarHours = 0,
                LabHours = 28,
                Credits = 8,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Elective,
                SyllabusId = syllabusId,
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
                        new TopicRequestApiDTO { Title = "CCNA 1 prezanton fushën e rrjeteve kompjuterike: LAN, WAN, Rrjete", Hours = 4, Reference = null },
                        new TopicRequestApiDTO { Title = "Cisco IOS", Hours = 2, Reference = null },
                        new TopicRequestApiDTO { Title = "Adresimi për IPv4 dhe IPv6, hyrje në protokollet, subnetting", Hours = 2, Reference = null },
                        new TopicRequestApiDTO { Title = "Modeli OSI: Physical, Data Link, Network, Transport, Application", Hours = 2, Reference = null },
                        new TopicRequestApiDTO { Title = "Kabllizimi dhe vizualizimi i rrjetit (GNS3 / Packet Tracer)", Hours = 2, Reference = null },
                        new TopicRequestApiDTO { Title = "Parimet e menaxhimit dhe sigurisë për rrjetet e konverguara", Hours = 2, Reference = null },
                        new TopicRequestApiDTO { Title = "Implementimi i teknologjive të rutimit dhe switching në një mjedis LAN", Hours = 2, Reference = null }
                    }
                }
            };
        }
    }
}
