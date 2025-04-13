# User Shifts API Service

This service is the **Backend mediator** of the Keep On Time Shifts project.  
It serves as the bridge between the frontend and the scheduling engine, handling business logic and  
coordinating shift data between services. This service interacts with the PostgreSQL database,  
processes employee shift data, and communicates with the Python-based scheduling engine for optimized shift results.

---

## 🚀 Tech Stack

- **C# / .NET 8**
- **ASP.NET Core Web API** – Lightweight API framework for building RESTful services
- **Entity Framework Core** – ORM for database interaction
- **PostgreSQL** (via Docker) – Database used for storing employee data and scheduling information
- **NSwag** – OpenAPI & Swagger tools
- **Ngrok** – Local tunneling for Auth0 development

---

## 🧠 Role in the Architecture

This service acts as the **bridge between the frontend and the scheduling engine**.  
It receives employee data from the frontend, saves it to the DB.  
Upon request, forwards employee's data to the Python scheduling service (via the NuGet client),  
and returns optimized shift results to the frontend.

---

## ✨ Features
- **Saving Users Data to DB**: Using Entity Framework and Auth0 integration to save new users into the database upon registration.
- **Vacation / Date Range Request Management**: Handles authorized requests to store employee vacation or preferred date ranges.
- **DB migrations**: Making sure that the DB structure is up to date.
---

## 🐳 Dockerized Database
This service includes a Dockerized PostgreSQL database, which simplifies development setup and ensures reproducibility.

To start the database, use the following:
```bash
docker-compose up -d
```

---

## Send Vacation Request Example Using cURL
You can use `cURL` to send a vacation request. Here’s how:
```bash
curl -X POST /user-schedule-preferences-request/date-range-preference-request \
-H "Authorization: Bearer <your-jwt-token>" \
-d '{
  "start_date": "2025-04-15T00:00:00",
  "end_date": "2025-04-20T23:59:59",
  "request_type": "Vacation"
}'
```
Make sure to replace <your-jwt-token> with your actual JWT token obtained through Auth0 authentication.

--- 

## ⚙️ Installation

```bash
# Clone the repository
git clone https://github.com/noadudai/UserShiftsApiService
cd UserShiftsApiService

# Restore packages
dotnet restore

# Run the project
dotnet run
```

---

## GitHub Actions
This project uses GitHub Actions to automate the process of:
- Generate the OpenAPI spec
- Create the NPM client and publish the library.

The CI workflow ensures the system is always up-to-date with the latest scheduling logic.  
Check out the workflow configuration in the `.github/workflows` directory for more details.

---

## 👤 About the Author

*Noa Dudai*  
Full Stack Developer & Backend Engineer  
Based in Israel 🇮🇱

### 🌐 Connect with me

<a href="https://github.com/noadudai" target="_blank">
  <img src="https://img.icons8.com/?size=100&id=62856&format=png&color=FFFFFF" alt="GitHub" width="30" height="30">
</a>
&nbsp;&nbsp;
<a href="https://www.linkedin.com/in/noadudai" target="_blank">
  <img src="https://cdn.jsdelivr.net/gh/devicons/devicon/icons/linkedin/linkedin-original.svg" alt="LinkedIn" width="30" height="30">
</a>