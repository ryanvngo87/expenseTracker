# Expense Tracker

## 1. Introduction

Expense Tracker is a full-stack web application that allows users to manage their personal expenses. It consists of a RESTful API built with ASP.NET Core 8 and a React frontend served as static files. Users can create, view, update, and delete expense records. Data is persisted to a SQL Server database via Entity Framework Core.

## 2. System Architecture

The software uses an **MVC (Model-View-Controller)** architecture with a React SPA frontend:

- **Model** — Entity and DTO classes representing expense data (`Expense`, `AddExpenseDto`, `UpdateExpenseDto`)
- **View** — React SPA (served from `wwwroot`) for browsing expenses; Swagger UI for API exploration and testing (available in Development mode)
- **Controller** — `ExpenseController` handles all HTTP requests and delegates to the data layer

## 3. Data Design

### Expense Entity

| Field | Type | Required | Notes |
|-------|------|----------|-------|
| `Id` | int | Auto | Primary key, auto-incremented |
| `Name` | string | Yes | Name of the expense |
| `Description` | string | No | Optional description |
| `Type` | string | Yes | Category/type of expense |
| `Date` | DateTime | No | Defaults to current date/time |

Database migrations are managed with EF Core and stored in the `Migrations/` folder.

## 4. Interface Design

### API Endpoints

Base route: `/api/expense`

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/expense` | Retrieve all expenses |
| GET | `/api/expense/{id}` | Retrieve an expense by ID |
| POST | `/api/expense` | Add a new expense |
| PUT | `/api/expense/{id}` | Update an existing expense |
| DELETE | `/api/expense/{id}` | Delete an expense |

## 5. Component Design

```
ExpenseTracker/
├── Controllers/
│   └── ExpenseController.cs   # CRUD API endpoints
├── Data/
│   └── ApplicationDbContext.cs
├── Migrations/                # EF Core database migrations
├── Models/
│   ├── Entities/
│   │   └── Expense.cs         # Core expense entity
│   ├── AddExpenseDto.cs       # DTO for creating expenses
│   └── UpdateExpenseDto.cs    # DTO for updating expenses
├── src/                       # React frontend source
│   ├── components/
│   │   ├── ExpenseGrid.tsx    # Expense table component
│   │   └── ExpenseGrid.css
│   ├── types/
│   │   └── expense.ts         # TypeScript interface for Expense
│   ├── App.tsx
│   ├── main.tsx
│   ├── index.html
│   └── index.css
├── wwwroot/                   # Built React SPA (output of Vite build)
└── Program.cs                 # App configuration and startup

ExpenseTracker.Tests/
└── TestExpenseController.cs   # Unit tests for the controller

vite.config.ts                 # Vite build config (proxies /api to ASP.NET)
```

## 6. User Interface Design

The application includes a **React SPA** served at the root (`/`) that displays all expenses in a table. It fetches data from `GET /api/expense` and renders it with loading and error states.

The API is also documented and testable via **Swagger UI**, accessible at `/swagger` when running in Development mode. All endpoints accept and return JSON.

## 7. Assumptions and Dependencies

- **Runtime:** .NET 8
- **Database:** SQL Server (local or remote)
- **ORM:** Entity Framework Core 8
- **API Docs:** Swashbuckle / Swagger (dev only)
- **Testing:** xUnit
- **Frontend:** React 19, TypeScript, Vite

### Setup

1. Configure the database connection string in `appsettings.json`:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=YOUR_SERVER;Database=ExpenseTracker;Trusted_Connection=True;"
     }
   }
   ```
2. Apply migrations:
   ```bash
   dotnet ef database update
   ```
3. Build the React frontend:
   ```bash
   npm install
   npm run build
   ```
4. Run the application:
   ```bash
   dotnet run
   ```

For frontend development with hot reload, run `npm run dev` alongside `dotnet run`. The Vite dev server proxies `/api` requests to `https://localhost:7240`.

## 8. Glossary of Terms

| Term | Definition |
|------|------------|
| **Expense** | A record representing a financial expenditure, including its name, type, and date |
| **DTO** | Data Transfer Object — a simplified model used to receive data from API requests |
| **EF Core** | Entity Framework Core — the ORM used to interact with the SQL Server database |
| **MVC** | Model-View-Controller — an architectural pattern separating data, logic, and presentation |
| **Migration** | A versioned change to the database schema managed by EF Core |
