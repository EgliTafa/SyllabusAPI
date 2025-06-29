using ErrorOr;

namespace Syllabus.Application.Departments;

public static class DepartmentErrors
{
    public static Error DepartmentNotFound => Error.NotFound(
        code: "Department.NotFound",
        description: "Department not found");

    public static Error DepartmentNameAlreadyExists => Error.Conflict(
        code: "Department.NameAlreadyExists",
        description: "A department with this name already exists");

    public static Error DepartmentHasPrograms => Error.Conflict(
        code: "Department.HasPrograms",
        description: "Cannot delete department that has programs");
} 