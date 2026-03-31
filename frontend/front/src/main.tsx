import { createRoot } from 'react-dom/client';
import './index.css';
import HomePage from './pages/HomePage.tsx';
import { BrowserRouter, Navigate, Route, Routes } from 'react-router-dom';
import { Auth0Provider, useAuth0 } from '@auth0/auth0-react';
import InfoPage from './pages/InfoPage.tsx';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import AdminPage from './pages/AdminPage.tsx';
import { useQueryCurrentUser } from './apis.ts';

import 'react-datetime-picker/dist/DateTimePicker.css';
import 'react-time-picker/dist/TimePicker.css';
import 'react-calendar/dist/Calendar.css';
import 'react-clock/dist/Clock.css';

const queryClient = new QueryClient();

const ManagerOnlyRoute = () => {
    const { isAuthenticated, isLoading: isAuth0Loading } = useAuth0();
    const {
        data: currentUser,
        isError: isCurrentUserError,
        isLoading: isCurrentUserLoading,
    } = useQueryCurrentUser();

    if (isAuth0Loading || (isAuthenticated && isCurrentUserLoading)) {
        return null;
    }

    if (!isAuthenticated) {
        return <Navigate to="/info" replace />;
    }

    if (isCurrentUserError || !currentUser) {
        return <Navigate to="/info" replace />;
    }

    if (currentUser.role !== 'Manager') {
        return <Navigate to="/" replace />;
    }

    return <AdminPage />;
};

const AppRoutes = () => {
    return (
        <Routes>
            <Route path="info" element={<InfoPage />} />
            <Route path="/" element={<HomePage />} />
            <Route path="admin-panel/*" element={<ManagerOnlyRoute />} />
        </Routes>
    );
};

createRoot(document.getElementById('root')!).render(
    <QueryClientProvider client={queryClient}>
        <BrowserRouter>
            <Auth0Provider
                domain={`${import.meta.env.VITE_AUTH0DOMAIN}`}
                clientId={`${import.meta.env.VITE_AUTH0CLIENTID}`}
                authorizationParams={{
                    redirect_uri: `${import.meta.env.VITE_HOMEPAGEURL}`,
                    audience: `${import.meta.env.VITE_AUTH0AUDIENCE}`,
                }}
                useRefreshTokens={true}
                cacheLocation="localstorage"
            >
                <div className="min-h-screen bg-custom-cream">
                    <AppRoutes />
                </div>
            </Auth0Provider>
        </BrowserRouter>
    </QueryClientProvider>,
);
