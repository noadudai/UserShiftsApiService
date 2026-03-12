# Keep On Time Shifts
This service is the **frontend of the Keep On Time Shifts** project.  
It provides a user-friendly interface for managing employee data, viewing optimized shift schedules, and interacting with the backend services.

---

## 🚀 Tech Stack
- **React** (via Vite)
- **TypeScript**
- **Tailwind CSS**
- **Auth0** – Authentication and user management
- **Axios** – HTTP client for communicating with the API service
- **React Query** – Efficient data fetching and caching

---

## 🧠 Role in the Architecture
This service allows authenticated users to register, mange shift preferences, view schedules and send vacations/date range requests.  
It also interacts with the C# service to receive data, receive schedules and save the data to the DataBase.  
All communication with the backend is protected by Auth0 authentication, and data is rendered dynamically  
based on user interaction and schedule updates.

---

## ✨ Features
- **Authentication via Auth0**: Seamless sign-in/signup using secure Auth0 flow.
- **Home Page**: Overview of the application and navigation to other sections.
- **Shift Preferences Submission**: Users can submit date-based requests for preferred availability or vacations.

---

## ⚙️ Installation
```bash
# Clone the repository
git clone https://github.com/noadudai/KeepOnTimeShifts
cd KeepOnTimeShifts

# Install dependencies
npm install
```

---

## 🔐 Auth0 Setup
This service uses Auth0 for authentication.
To configure:
- Create a tenant on [Auth0](https://auth0.com/docs/quickstart/spa/react/interactive).
- Create an application (Single Page Application) and configure allowed Callback URLs, allowed Logout URLs,  
allowed Web Origins.

you must define the following environment variables in a .env file:
```env
VITE_AUTH0_DOMAIN=your-auth0-domain
VITE_AUTH0_CLIENT_ID=your-client-id
VITE_AUTH0_AUDIENCE=your-api-identifier
VITE_API_URL=http://<localhost port>  
```
Configure a Post-Registration Action in the Auth0 dashboard that triggers a call to the backend, 
to save the new user into the database.

---

## Example Of The Interaction With The C# Service
```ts
const ax = axios.create({
    baseURL: `${import.meta.env.VITE_BACKEND_BASE_URL}`,
});

// using the C# client package, downloaded from GitHub
const userSchedulePreferenceRequestApi: AddNewUserScheduleRequestApi = new AddNewUserScheduleRequestApi(undefined, undefined, ax);

const userSchedulePrefReqPostRequest = useMutation({
    mutationFn: async(data: UserDateRangePreferenceRequestModel) =>
    {
        const token = await getAccessTokenSilently(); // Auth0 function to receive the Access Token

        const response = await userSchedulePreferenceRequestApi.userSchedulePreferencesRequestDateRangePreferenceRequestPost(data, {
            headers: {
                Authorization: `Bearer ${token}`,
            },
        });

        return response;
    }
});
```

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