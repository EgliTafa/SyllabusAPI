# 🎓 Syllabus Management System

A full-stack academic syllabus and subject management system designed for professors and academic staff to create, update, and export university syllabuses and subject details (including DOCX/PDF templates). The project is based on the **Bachelor in Informatics 2024–2027** syllabus from the Faculty of Natural Sciences, University of Tirana.

---

## 🚀 Tech Stack

| Layer             | Technology                       |
|------------------|----------------------------------|
| Frontend (planned) | React (TypeScript)              |
| Backend           | .NET 9 / C#              |
| Authentication    | ASP.NET Identity + JWT           |
| ORM               | Entity Framework Core (SQL Server) |
| Database          | Microsoft SQL Server             |
| OpenAPI UI        | Swagger                          |
| DevOps            | Docker + Docker Compose          |
| Architecture      | Domain-Driven Design + Repository Pattern |
| Extras            | File Upload, PDF/DOCX export (planned) |

---

## 📁 Project Structure

```
SyllabusAPI/
├── Syllabus.API/              → ASP.NET Core Web API
├── Syllabus.Application/      → (planned) CQRS, MediatR commands/queries
├── Syllabus.Domain/           → Entities, Enums, Repositories
├── Syllabus.Infrastructure/   → EF Core, DbContext, Repositories
├── Syllabus.ApiContracts/     → DTOs (if used externally)
├── Syllabus.Util/             → Configuration, Helpers
├── docker-compose.yml
├── README.md
```

---

## 📦 Features

- ✅ Manage academic **syllabuses** (e.g., Bachelor in Informatics)
- ✅ Manage **courses**, **course details**, and **lecture topics**
- ✅ Store **evaluation structure**, **teaching plan**, and **prerequisites**
- ✅ Automatic **Swagger UI** for API browsing and testing
- ✅ Authentication ready via **IdentityUser**
- ✅ Dockerized API + SQL Server for easy development

---

## 🛠️ Setup Instructions

### ✅ Prerequisites

- [.NET SDK 9](https://dotnet.microsoft.com/)
- [Docker + Docker Compose](https://docs.docker.com/)
- Optional: Visual Studio / VS Code

---

### 🐳 Run via Docker Compose (RECOMMENDED)

```bash
docker-compose up --build
```

Then open your browser:

```
http://localhost:5000/       ← Swagger UI (OpenAPI GUI)
http://localhost:5000/swagger/v1/swagger.json
```

---

### 🧪 Local Dev (no Docker)

#### 1. Setup database connection

In `appsettings.json` or `launchSettings.json`, ensure:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SyllabusDb;Trusted_Connection=True;TrustServerCertificate=True"
}
```

#### 2. Run migrations (optional)

```bash
dotnet ef database update --project Syllabus.Infrastructure --startup-project Syllabus.API
```

#### 3. Start API locally

```bash
dotnet run --project Syllabus.API
```

Navigate to:

```
https://localhost:7276/ ← Swagger UI
```

---

## 🔐 Authentication

The app uses ASP.NET Identity and supports JWT tokens for secured endpoints.

- You can register and login users via `/api/auth/register` and `/api/auth/login` (if implemented).
- Roles like `Professor`, `Admin` can be managed via custom `UserEntity`.

---

## ✏️ Usage via Swagger UI

1. Start the app.
2. Open Swagger: [http://localhost:5000/](http://localhost:5000/)
3. Test endpoints like:
   - `GET /api/syllabuses`
   - `GET /api/courses`
   - `POST /api/courses/{id}/details`
   - `POST /api/courses/{id}/topics`

All response models and sample request bodies are auto-documented.

---

## 📄 Data Model Overview

- `Syllabus` → has many `Course`
- `Course` → has one `CourseDetail`
- `CourseDetail` → has many `Topic`
- `CourseDetail` → includes `TeachingPlan` and `EvaluationBreakdown` as owned types

---

## 📤 Coming Soon

- 📄 DOCX and PDF generation using OpenXML + DinkToPDF
- 🧾 File upload support (e.g., syllabus attachments)
- 🧠 MediatR + CQRS for cleaner application layer
- 🖼️ React-based frontend

---

## 👥 Contributing

This is a university-driven academic tool. Contributions for syllabus formats from other faculties or institutions are welcome.

---

## 🧑‍💻 Author

**E. T.** – Software Engineer & Academic Tools Enthusiast

---

## 📝 License

MIT License
