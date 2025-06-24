using ErrorOr;

namespace Syllabus.Application.Programs;

public static class ProgramErrors
{
    public static Error ProgramNotFound => Error.NotFound(
        code: "Program.NotFound",
        description: "Program not found");

    public static Error ProgramNameAlreadyExists => Error.Conflict(
        code: "Program.NameAlreadyExists",
        description: "A program with this name already exists");

    public static Error ProgramHasSyllabuses => Error.Conflict(
        code: "Program.HasSyllabuses",
        description: "Cannot delete program that has syllabuses");

    public static Error DepartmentNotFound => Error.NotFound(
        code: "Program.DepartmentNotFound",
        description: "Department not found");
} 