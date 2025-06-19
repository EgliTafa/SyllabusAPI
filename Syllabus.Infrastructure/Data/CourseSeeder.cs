using Syllabus.Domain.Sylabusses;

namespace Syllabus.Infrastructure.Data;

public static class CourseSeeder
{
    public static async Task SeedCoursesAsync(SyllabusDbContext context)
    {
        if (context.Courses.Any())
        {
            return; // Courses already seeded
        }

        var courses = new List<Course>
        {
            // VITI I - Semestri I
            new Course
            {
                Title = "Bazat e informatikës",
                Code = "INF101",
                Year = 1,
                Semester = 1,
                LectureHours = 30,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 10,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Mandatory,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2024-2025",
                    Language = "Shqip",
                    CourseTypeLabel = "B - Bazë",
                    EthicsCode = "INF101-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim",
                    Credits = 10,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 4,
                        IndividualStudyHours = 190
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 20,
                        Test2Percent = 20,
                        Test3Percent = 20,
                        FinalExamPercent = 30
                    },
                    Objective = "Të kuptuarit e koncepteve themelore të informatikës",
                    KeyConcepts = "Sistemet kompjuterike, Algoritmet bazë, Logjika kompjuterike",
                    Prerequisites = "Nuk ka",
                    SkillsAcquired = "Njohuri bazë të informatikës dhe algoritmeve",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Hyrje në informatikë", Hours = 12, Reference = "Kapitulli 1" },
                        new Topic { Title = "Sistemet kompjuterike", Hours = 12, Reference = "Kapitulli 2" },
                        new Topic { Title = "Algoritmet bazë", Hours = 12, Reference = "Kapitulli 3" },
                        new Topic { Title = "Logjika kompjuterike", Hours = 12, Reference = "Kapitulli 4" },
                        new Topic { Title = "Sistemet numerike", Hours = 12, Reference = "Kapitulli 5" }
                    }
                }
            },
            new Course
            {
                Title = "Fizikë",
                Code = "FIZ101",
                Year = 1,
                Semester = 1,
                LectureHours = 30,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 10,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Mandatory,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2024-2025",
                    Language = "Shqip",
                    CourseTypeLabel = "A - Avancuar",
                    EthicsCode = "FIZ101-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim dhe Laborator",
                    Credits = 10,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 4,
                        IndividualStudyHours = 190
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 25,
                        Test2Percent = 25,
                        Test3Percent = 0,
                        FinalExamPercent = 40
                    },
                    Objective = "Të kuptuarit e koncepteve bazë të fizikës dhe aplikimi i tyre në informatikë",
                    KeyConcepts = "Mekanika, Elektriciteti, Magnetizmi, Optika",
                    Prerequisites = "Nuk ka",
                    SkillsAcquired = "Aftësi në zgjidhjen e problemeve fizike dhe aplikimin e tyre në informatikë",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Mekanika", Hours = 15, Reference = "Kapitulli 1" },
                        new Topic { Title = "Elektriciteti", Hours = 15, Reference = "Kapitulli 2" },
                        new Topic { Title = "Magnetizmi", Hours = 15, Reference = "Kapitulli 3" },
                        new Topic { Title = "Optika", Hours = 15, Reference = "Kapitulli 4" }
                    }
                }
            },
            new Course
            {
                Title = "Matematikë I",
                Code = "MAT101",
                Year = 1,
                Semester = 1,
                LectureHours = 30,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 10,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Mandatory,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2024-2025",
                    Language = "Shqip",
                    CourseTypeLabel = "A - Avancuar",
                    EthicsCode = "MAT101-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim",
                    Credits = 10,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 4,
                        IndividualStudyHours = 190
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 25,
                        Test2Percent = 25,
                        Test3Percent = 0,
                        FinalExamPercent = 40
                    },
                    Objective = "Të kuptuarit e koncepteve themelore të matematikës",
                    KeyConcepts = "Analiza matematike, Algjebra lineare, Gjeometria analitike",
                    Prerequisites = "Nuk ka",
                    SkillsAcquired = "Aftësi në zgjidhjen e problemeve matematikore",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Funksionet dhe limitet", Hours = 15, Reference = "Kapitulli 1" },
                        new Topic { Title = "Derivatet", Hours = 15, Reference = "Kapitulli 2" },
                        new Topic { Title = "Integralet", Hours = 15, Reference = "Kapitulli 3" },
                        new Topic { Title = "Gjeometria analitike", Hours = 15, Reference = "Kapitulli 4" }
                    }
                }
            },
            new Course
            {
                Title = "Algjebër",
                Code = "ALG101",
                Year = 1,
                Semester = 1,
                LectureHours = 30,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 5,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Elective,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2024-2025",
                    Language = "Shqip",
                    CourseTypeLabel = "A - Avancuar",
                    EthicsCode = "ALG101-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim",
                    Credits = 5,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 4,
                        IndividualStudyHours = 90
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 25,
                        Test2Percent = 25,
                        Test3Percent = 0,
                        FinalExamPercent = 40
                    },
                    Objective = "Të kuptuarit e koncepteve themelore të algjebres",
                    KeyConcepts = "Strukturat algjebrike, Grupet, Unazat, Fushat",
                    Prerequisites = "Nuk ka",
                    SkillsAcquired = "Aftësi në zgjidhjen e problemeve algjebrike",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Strukturat algjebrike", Hours = 15, Reference = "Kapitulli 1" },
                        new Topic { Title = "Grupet", Hours = 15, Reference = "Kapitulli 2" },
                        new Topic { Title = "Unazat", Hours = 15, Reference = "Kapitulli 3" },
                        new Topic { Title = "Fushat", Hours = 15, Reference = "Kapitulli 4" }
                    }
                }
            },
            new Course
            {
                Title = "Struktura të dhënash në C",
                Code = "SDH101",
                Year = 1,
                Semester = 1,
                LectureHours = 30,
                SeminarHours = 15,
                LabHours = 15,
                PracticeHours = 0,
                Credits = 11,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Mandatory,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2024-2025",
                    Language = "Shqip",
                    CourseTypeLabel = "B - Bazë",
                    EthicsCode = "SDH101-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim dhe Laborator",
                    Credits = 11,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 15,
                        PracticeHours = 0,
                        ExerciseHours = 15,
                        WeeklyHours = 4,
                        IndividualStudyHours = 215
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 20,
                        Test2Percent = 20,
                        Test3Percent = 20,
                        FinalExamPercent = 30
                    },
                    Objective = "Të kuptuarit dhe implementimi i strukturave të të dhënave në C",
                    KeyConcepts = "Arrays, Linked Lists, Stacks, Queues, Trees, Graphs",
                    Prerequisites = "Bazat e programimit në C",
                    SkillsAcquired = "Implementimi i strukturave të të dhënave dhe algoritmeve në C",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Arrays dhe Strings", Hours = 12, Reference = "Kapitulli 1" },
                        new Topic { Title = "Linked Lists", Hours = 12, Reference = "Kapitulli 2" },
                        new Topic { Title = "Stacks dhe Queues", Hours = 12, Reference = "Kapitulli 3" },
                        new Topic { Title = "Trees", Hours = 12, Reference = "Kapitulli 4" },
                        new Topic { Title = "Graphs", Hours = 12, Reference = "Kapitulli 5" }
                    }
                }
            },
            new Course
            {
                Title = "Sisteme operative",
                Code = "SOP101",
                Year = 1,
                Semester = 1,
                LectureHours = 30,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 9,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Mandatory,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2024-2025",
                    Language = "Shqip",
                    CourseTypeLabel = "B - Bazë",
                    EthicsCode = "SOP101-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim dhe Laborator",
                    Credits = 9,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 4,
                        IndividualStudyHours = 165
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 20,
                        Test2Percent = 20,
                        Test3Percent = 20,
                        FinalExamPercent = 30
                    },
                    Objective = "Të kuptuarit e sistemeve operative dhe funksionimit të tyre",
                    KeyConcepts = "Proceset, Threads, Memoria, Sistemi i file-ve, Siguria",
                    Prerequisites = "Bazat e informatikës",
                    SkillsAcquired = "Administrimi i sistemeve operative dhe zgjidhja e problemeve",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Hyrje në sisteme operative", Hours = 12, Reference = "Kapitulli 1" },
                        new Topic { Title = "Proceset dhe threads", Hours = 12, Reference = "Kapitulli 2" },
                        new Topic { Title = "Menaxhimi i memories", Hours = 12, Reference = "Kapitulli 3" },
                        new Topic { Title = "Sistemi i file-ve", Hours = 12, Reference = "Kapitulli 4" },
                        new Topic { Title = "Siguria", Hours = 12, Reference = "Kapitulli 5" }
                    }
                }
            },

            // VITI I - Semestri II
            new Course
            {
                Title = "Anglisht",
                Code = "ENG101",
                Year = 1,
                Semester = 2,
                LectureHours = 15,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 5,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Elective,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2024-2025",
                    Language = "Anglisht",
                    CourseTypeLabel = "D - Plotësues",
                    EthicsCode = "ENG101-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim dhe Me Gojë",
                    Credits = 5,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 15,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 3,
                        IndividualStudyHours = 80
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 20,
                        Test1Percent = 20,
                        Test2Percent = 20,
                        Test3Percent = 0,
                        FinalExamPercent = 40
                    },
                    Objective = "Përmirësimi i aftësive gjuhësore në anglisht",
                    KeyConcepts = "Grammar, Vocabulary, Speaking, Writing",
                    Prerequisites = "Nuk ka",
                    SkillsAcquired = "Komunikimi profesional në anglisht",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Grammar Review", Hours = 10, Reference = "Unit 1" },
                        new Topic { Title = "Technical Vocabulary", Hours = 10, Reference = "Unit 2" },
                        new Topic { Title = "Professional Writing", Hours = 10, Reference = "Unit 3" },
                        new Topic { Title = "Presentation Skills", Hours = 15, Reference = "Unit 4" }
                    }
                }
            },

            // VITI I - Semestri II (continuing)
            new Course
            {
                Title = "Edukim fizik",
                Code = "EDF101",
                Year = 1,
                Semester = 2,
                LectureHours = 0,
                SeminarHours = 15,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 1,
                Evaluation = EvaluationMethod.ContinuousAssessment,
                Type = CourseType.Elective,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2024-2025",
                    Language = "Shqip",
                    CourseTypeLabel = "D - Plotësues",
                    EthicsCode = "EDF101-ETH",
                    ExamMethod = "Vlerësim i vazhduar",
                    TeachingFormat = "Praktik",
                    Credits = 1,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 0,
                        LabHours = 0,
                        PracticeHours = 15,
                        ExerciseHours = 0,
                        WeeklyHours = 1,
                        IndividualStudyHours = 10
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 50,
                        Test1Percent = 25,
                        Test2Percent = 25,
                        Test3Percent = 0,
                        FinalExamPercent = 0
                    },
                    Objective = "Zhvillimi i aftësive fizike dhe shëndetësore",
                    KeyConcepts = "Aktiviteti fizik, Sporti, Shëndeti",
                    Prerequisites = "Nuk ka",
                    SkillsAcquired = "Aftësi fizike dhe sportive",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Ushtrime fizike", Hours = 5, Reference = "Moduli 1" },
                        new Topic { Title = "Sporte ekipore", Hours = 5, Reference = "Moduli 2" },
                        new Topic { Title = "Fitness", Hours = 5, Reference = "Moduli 3" }
                    }
                }
            },

            // VITI II - Semestri III
            new Course
            {
                Title = "Programim i orientuar nga objektet",
                Code = "POO201",
                Year = 2,
                Semester = 3,
                LectureHours = 30,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 12,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Mandatory,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2025-2026",
                    Language = "Shqip",
                    CourseTypeLabel = "B - Bazë",
                    EthicsCode = "POO201-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim dhe Laborator",
                    Credits = 12,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 4,
                        IndividualStudyHours = 240
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 20,
                        Test2Percent = 20,
                        Test3Percent = 20,
                        FinalExamPercent = 30
                    },
                    Objective = "Të kuptuarit dhe aplikimi i programimit të orientuar nga objektet",
                    KeyConcepts = "Klasat, Objektet, Trashëgimia, Polimorfizmi",
                    Prerequisites = "Bazat e programimit",
                    SkillsAcquired = "Zhvillimi i aplikacioneve duke përdorur OOP",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Hyrje në OOP", Hours = 15, Reference = "Kapitulli 1" },
                        new Topic { Title = "Klasat dhe Objektet", Hours = 15, Reference = "Kapitulli 2" },
                        new Topic { Title = "Trashëgimia", Hours = 15, Reference = "Kapitulli 3" },
                        new Topic { Title = "Polimorfizmi", Hours = 15, Reference = "Kapitulli 4" }
                    }
                }
            },

            // VITI II - Semestri III
            new Course
            {
                Title = "Statistikë e aplikuar",
                Code = "STA201",
                Year = 2,
                Semester = 3,
                LectureHours = 30,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 6,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Elective,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2025-2026",
                    Language = "Shqip",
                    CourseTypeLabel = "C - Karakterizues",
                    EthicsCode = "STA201-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim",
                    Credits = 6,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 4,
                        IndividualStudyHours = 90
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 25,
                        Test2Percent = 25,
                        Test3Percent = 0,
                        FinalExamPercent = 40
                    },
                    Objective = "Të kuptuarit dhe aplikimi i metodave statistikore",
                    KeyConcepts = "Probabiliteti, Shpërndarja, Korrelacioni, Regresioni",
                    Prerequisites = "Matematikë I",
                    SkillsAcquired = "Analiza statistikore dhe interpretimi i të dhënave",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Statistika përshkruese", Hours = 15, Reference = "Kapitulli 1" },
                        new Topic { Title = "Probabiliteti", Hours = 15, Reference = "Kapitulli 2" },
                        new Topic { Title = "Shpërndarja", Hours = 15, Reference = "Kapitulli 3" },
                        new Topic { Title = "Korrelacioni dhe regresioni", Hours = 15, Reference = "Kapitulli 4" }
                    }
                }
            },
            new Course
            {
                Title = "Matematikë e aplikuar",
                Code = "MAT201",
                Year = 2,
                Semester = 3,
                LectureHours = 30,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 11,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Mandatory,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2025-2026",
                    Language = "Shqip",
                    CourseTypeLabel = "C - Karakterizues",
                    EthicsCode = "MAT201-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim",
                    Credits = 11,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 4,
                        IndividualStudyHours = 215
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 25,
                        Test2Percent = 25,
                        Test3Percent = 0,
                        FinalExamPercent = 40
                    },
                    Objective = "Të kuptuarit dhe aplikimi i koncepteve të avancuara matematikore",
                    KeyConcepts = "Ekuacionet diferenciale, Seritë, Transformimet",
                    Prerequisites = "Matematikë I",
                    SkillsAcquired = "Zgjidhja e problemeve komplekse matematikore",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Ekuacionet diferenciale", Hours = 20, Reference = "Kapitulli 1" },
                        new Topic { Title = "Seritë numerike", Hours = 20, Reference = "Kapitulli 2" },
                        new Topic { Title = "Transformimet", Hours = 20, Reference = "Kapitulli 3" }
                    }
                }
            },

            // VITI III - Semestri V
            new Course
            {
                Title = "Inxhinieri Softuerike",
                Code = "INS301",
                Year = 3,
                Semester = 5,
                LectureHours = 30,
                SeminarHours = 30,
                LabHours = 0,
                PracticeHours = 0,
                Credits = 5,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Mandatory,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2026-2027",
                    Language = "Shqip",
                    CourseTypeLabel = "B - Bazë",
                    EthicsCode = "INS301-ETH",
                    ExamMethod = "Provim",
                    TeachingFormat = "Me Shkrim dhe Projekt",
                    Credits = 5,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 30,
                        LabHours = 0,
                        PracticeHours = 0,
                        ExerciseHours = 30,
                        WeeklyHours = 4,
                        IndividualStudyHours = 90
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 10,
                        Test1Percent = 20,
                        Test2Percent = 20,
                        Test3Percent = 20,
                        FinalExamPercent = 30
                    },
                    Objective = "Të kuptuarit e procesit të zhvillimit të softuerit",
                    KeyConcepts = "SDLC, Agile, Testing, Project Management",
                    Prerequisites = "Programim i orientuar nga objektet",
                    SkillsAcquired = "Menaxhimi i projekteve softuerike",
                    CourseResponsible = "Prof. Dr. Example Name",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Software Development Life Cycle", Hours = 15, Reference = "Kapitulli 1" },
                        new Topic { Title = "Agile Methodology", Hours = 15, Reference = "Kapitulli 2" },
                        new Topic { Title = "Software Testing", Hours = 15, Reference = "Kapitulli 3" },
                        new Topic { Title = "Project Management", Hours = 15, Reference = "Kapitulli 4" }
                    }
                }
            },

            // VITI III - Semestri VI
            new Course
            {
                Title = "Provim Diplome",
                Code = "DIP301",
                Year = 3,
                Semester = 6,
                LectureHours = 0,
                SeminarHours = 0,
                LabHours = 0,
                PracticeHours = 45,
                Credits = 8,
                Evaluation = EvaluationMethod.Exam,
                Type = CourseType.Mandatory,
                Detail = new CourseDetail
                {
                    AcademicProgram = "Bachelor në Informatikë",
                    AcademicYear = "2026-2027",
                    Language = "Shqip",
                    CourseTypeLabel = "E - Diplomë",
                    EthicsCode = "DIP301-ETH",
                    ExamMethod = "Mbrojtje Diplome",
                    TeachingFormat = "Projekt dhe Prezantim",
                    Credits = 8,
                    TeachingPlan = new TeachingPlan
                    {
                        LectureHours = 0,
                        LabHours = 0,
                        PracticeHours = 45,
                        ExerciseHours = 0,
                        WeeklyHours = 3,
                        IndividualStudyHours = 155
                    },
                    EvaluationBreakdown = new EvaluationBreakdown
                    {
                        ParticipationPercent = 0,
                        Test1Percent = 0,
                        Test2Percent = 0,
                        Test3Percent = 0,
                        FinalExamPercent = 100
                    },
                    Objective = "Përgatitja dhe mbrojtja e temës së diplomës",
                    KeyConcepts = "Hulumtim, Zhvillim, Dokumentim, Prezantim",
                    Prerequisites = "Të gjitha lëndët e detyrueshme",
                    SkillsAcquired = "Hulumtim i pavarur dhe prezantim profesional",
                    CourseResponsible = "Komisioni i Diplomimit",
                    Topics = new List<Topic>
                    {
                        new Topic { Title = "Hulumtimi dhe literatura", Hours = 15, Reference = "Faza 1" },
                        new Topic { Title = "Zhvillimi i projektit", Hours = 15, Reference = "Faza 2" },
                        new Topic { Title = "Përgatitja e dokumentacionit", Hours = 15, Reference = "Faza 3" }
                    }
                }
            }
        };

        await context.Courses.AddRangeAsync(courses);
        await context.SaveChangesAsync();
    }
}