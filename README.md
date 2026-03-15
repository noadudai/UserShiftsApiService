# Scheduling App

A shift scheduling application for small businesses. Employees submit vacation requests; managers generate optimized weekly schedules using constraint-solving.

```
User Browser
  └─► frontend (React, :5173)
        └─► backend-csharp (ASP.NET Core, :5029)
              ├─► backend-python (FastAPI + OR-Tools, :8000)  — schedule generation
              └─► PostgreSQL (:5432)                          — data persistence

Auth0  ──[Post User Registration action]──► ngrok ──► backend-csharp
```

---

## Quick Start

```bash
cp .env.example .env      # fill in Auth0 values and HMAC_SECRET (see External Setup below)
docker compose up --build
```

| Service | URL |
|---|---|
| Frontend | http://localhost:5173 |
| C# API (Swagger) | http://localhost:5029/swagger |
| Python API (docs) | http://localhost:8000/docs |
| PostgreSQL | localhost:5432 |

---

## Running in Cursor

Open the Command Palette (`Cmd+Shift+P`) → **Tasks: Run Task** to access all tasks defined in `.vscode/tasks.json`:

| Task | What it does |
|---|---|
| **Start all (local dev)** | Starts postgres in Docker + all 3 services locally in parallel |
| **Start all (docker compose)** | Builds and runs everything in Docker |
| **Start postgres only** | `docker compose up postgres` |
| **Start C# API (local)** | `dotnet run` in `backend-csharp/UserShiftsApiService` |
| **Start Python API (local)** | `uvicorn` in `backend-python` |
| **Start frontend (local)** | `npm run dev` in `frontend/front` (auto-regenerates API client) |
| **Run DB migrations** | `dotnet ef database update` |
| **Force regenerate API client** | Rebuilds the TypeScript client from the current openapi.json |
| **Stop all** | `docker compose down` |

---

## Services

### `frontend/` — React SPA

**Tech:** React 18 · Vite · TypeScript · TailwindCSS · Auth0 · TanStack Query

**Pages:**

| Route | Who uses it | Description |
|---|---|---|
| `/` | Employees | Submit vacation/time-off requests, view upcoming scheduled vacations |
| `/info` | Everyone | Info and help |
| `/admin-panel/Overview` | Managers | Dashboard overview |
| `/admin-panel/Employees` | Managers | View and manage the employee list |
| `/admin-panel/Scheduling` | Managers | Create new schedules — triggers the Python optimizer via the C# API |
| `/admin-panel/Vacations` | Managers | View and manage all employee vacation requests |

**Local dev:**

```bash
# Copy and fill in environment variables first (see Environment Variables section)
cp frontend/.env.example frontend/front/.env

cd frontend/front
npm install
npm run dev      # http://localhost:5173
```

The `predev` hook auto-regenerates the TypeScript API client if the C# API's `openapi.json` has changed. First run takes ~30s; subsequent runs are instant if nothing changed.

**Environment variables** (in `frontend/front/.env`):

| Variable | Description |
|---|---|
| `VITE_AUTH0DOMAIN` | Auth0 tenant domain (e.g. `https://dev-xxx.us.auth0.com/`) |
| `VITE_AUTH0CLIENTID` | Auth0 SPA Client ID |
| `VITE_AUTH0AUDIENCE` | Auth0 API audience (e.g. `https://UsersShiftsApi/`) |
| `VITE_HOMEPAGEURL` | Redirect URI after login (e.g. `http://localhost:5173`) |
| `VITE_BACKEND_BASE_URL` | C# API base URL (e.g. `http://localhost:5029`) |

---

### `backend-csharp/` — ASP.NET Core API

**Tech:** .NET 9 · Entity Framework Core · PostgreSQL (Npgsql) · Auth0 JWT · Swashbuckle

Central data API. Manages users, shift schedules, and vacation requests. Verifies Auth0 JWTs on all protected endpoints. Swagger UI at `/swagger`.

**API Endpoints:**

| Method | Path | Auth | Description |
|---|---|---|---|
| `POST` | `/auth0-maintenance/save-new-user` | HMAC signature | Called by Auth0 Action on new user registration — saves user to DB |
| `POST` | `/manager-schedule-actions/create-schedule` | JWT | Create a new shift schedule |
| `GET` | `/manager-schedule-actions/schedules` | JWT | List schedules |
| `POST` | `/manager-schedule-actions/change-schedule-status` | JWT | Update schedule status |
| `POST` | `/user-schedule-preferences-request/date-range-preference-request` | JWT | Employee submits a vacation/time-off request |
| `POST` | `/user-schedule-preferences-request/vacations-by-date-range` | JWT | Get a user's vacations in a date range |

**Local dev:**

```bash
# Start postgres first
docker compose up postgres -d

# Set secrets (stored in dotnet user-secrets, never in appsettings.json)
cd backend-csharp/UserShiftsApiService
dotnet user-secrets set "Auth0:Domain" "https://<your-tenant>.auth0.com/"
dotnet user-secrets set "Auth0:Audience" "https://UsersShiftsApi/"
dotnet user-secrets set "Auth0:HMAC_SECRET" "<your-hmac-secret>"

# Run DB migrations (first time only, or after new migrations are added)
dotnet ef database update

# Start the API
dotnet run       # http://localhost:5029
```

**Environment variables:**

| Variable | Description |
|---|---|
| `Auth0__Domain` | Auth0 tenant domain |
| `Auth0__Audience` | Auth0 API audience identifier |
| `Auth0__HMAC_SECRET` | Shared secret for verifying Auth0 Action HMAC-SHA256 signatures |
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string |
| `FrontUrl` | Frontend origin for the CORS policy |

---

### `backend-python/` — FastAPI Schedule Optimizer

**Tech:** Python · FastAPI · Google OR-Tools

Constraint-based schedule optimizer. Given a fixed set of employees and weekly shifts, it uses OR-Tools to generate up to 100 valid schedule options that respect employee preferences and priorities. Called by the C# API when a manager creates a new schedule.

**API Endpoints:**

| Method | Path | Description |
|---|---|---|
| `GET` | `/create_and_get_schedule_options` | Returns up to 100 optimized schedule options with employee and shift metadata |

Interactive docs: `http://localhost:8000/docs`

**Local dev:**

```bash
cd backend-python
python -m venv .venv
source .venv/bin/activate          # Windows: .venv\Scripts\activate
pip install -r requirements.txt
uvicorn src.server.app:app --reload --port 8000
```

---

## External Setup

### Auth0

1. Sign up at [auth0.com](https://auth0.com) and create a tenant
2. **SPA Application**: Applications → Create Application → Single Page Web Application
   - Copy **Domain** → `VITE_AUTH0DOMAIN` in `frontend/front/.env`
   - Copy **Client ID** → `VITE_AUTH0CLIENTID` in `frontend/front/.env`
   - Set **Allowed Callback URLs**: `http://localhost:5173`
   - Set **Allowed Logout URLs**: `http://localhost:5173`
   - Set **Allowed Web Origins**: `http://localhost:5173`
3. **API**: Applications → APIs → Create API
   - Set **Identifier** to `https://UsersShiftsApi/`
   - Copy identifier → `VITE_AUTH0AUDIENCE` and `Auth0__Audience`
4. **Post User Registration Action**: Actions → Flows → Post User Registration → `+` → Build from scratch
   - Paste the script from [`auth0/actions/save-new-user.js`](auth0/actions/save-new-user.js)
   - Add **Secrets** (Secrets tab):
     - `HMAC_SECRET` — any random string; must exactly match `Auth0__HMAC_SECRET` in the C# config
     - `API_URL` — your ngrok URL (see below) for local dev, or your production URL in prod
   - Click **Deploy**

### ngrok (local dev only)

Auth0 Actions run in the cloud and cannot reach `localhost`. ngrok creates a secure public tunnel to your local C# API so Auth0 can call it when a user registers.

```bash
# Install: https://ngrok.com/download
ngrok http 5029
# Example: Forwarding  https://abc123.ngrok-free.app -> http://localhost:5029
```

After starting ngrok:
1. Copy the HTTPS forwarding URL
2. Auth0 dashboard → Actions → the **Post User Registration** action → **Secrets** tab
3. Update `API_URL` to the new URL → **Save**

No code change or action redeploy is needed — the script reads `API_URL` at runtime.

> **Note:** The free ngrok URL changes on every restart. Repeat the 3 steps above each time.  
> In production, set `API_URL` to your stable deployed server URL once and you're done.

---

## API Client Generation

The frontend uses `@noadudai/scheduler-backend-client` — a TypeScript axios client auto-generated from the C# API's OpenAPI spec (`openapi.json`). It is generated locally and lives in `packages/scheduler-backend-client/` (gitignored, never published).

**How it works:**
- The C# project generates `openapi.json` on every `dotnet build`
- When you run `npm run dev` in `frontend/front`, a `predev` script checks if `openapi.json` has changed since last generation
- If changed → regenerates the client automatically (~30s first time, then faster)
- If unchanged → skips and starts Vite immediately

To manually force regeneration: `Cmd+Shift+P → Tasks: Run Task → Force regenerate API client`

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