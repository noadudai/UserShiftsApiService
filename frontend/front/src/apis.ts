import axios from 'axios';
import {
    ManagerScheduleActionsApi,
    NullableOfOrder,
    SessionApi,
    UserDateRangePreferenceRequestModel,
    UserScheduleRequestApi,
} from '@noadudai/scheduler-backend-client/api.ts';
import { useMutation, useQuery, useQueryClient } from '@tanstack/react-query';
import { useAuth0 } from '@auth0/auth0-react';
import { CreateNewScheduleModel } from '@noadudai/scheduler-backend-client';

const ax = axios.create({
    baseURL: `${import.meta.env.VITE_BACKEND_BASE_URL}`,
});

const userScheduleApi = new UserScheduleRequestApi(undefined, undefined, ax as any);
const sessionApi = new SessionApi(undefined, undefined, ax as any);
const managerActionsApi = new ManagerScheduleActionsApi(undefined, undefined, ax as any);

type UserRole = 'Employee' | 'Manager';

type CurrentUser = {
    name?: string | null;
    email?: string | null;
    role?: UserRole;
};

const toUserRole = (role?: number | string): UserRole | undefined => {
    if (role === 'Manager' || role === 'Employee') {
        return role;
    }

    if (role === 1) {
        return 'Manager';
    }

    if (role === 0) {
        return 'Employee';
    }

    return undefined;
};

export const useQueryCurrentUser = () => {
    const { getAccessTokenSilently, isAuthenticated, isLoading: isAuth0Loading } = useAuth0();

    return useQuery<CurrentUser>({
        queryKey: ['currentUser'],
        enabled: isAuthenticated && !isAuth0Loading,
        queryFn: async () => {
            const token = await getAccessTokenSilently();

            const response = await sessionApi.sessionMeGet({
                headers: {
                    Authorization: `Bearer ${token}`,
                },
            });

            return {
                ...response.data,
                role: toUserRole(response.data.role),
            };
        },
    });
};

export const useCreateNewShiftsSchedule = () => {
    const { getAccessTokenSilently } = useAuth0();
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: async (data: CreateNewScheduleModel) => {
            const token = await getAccessTokenSilently();

            const response = await managerActionsApi.managerScheduleActionsCreateSchedulePost(
                data,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                },
            );

            return response;
        },
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['allSchedules'] });
        },
    });
};

export const useQueryAllSchedulesDescending = () => {
    const { getAccessTokenSilently, isLoading: isAuth0Loading } = useAuth0();

    return useQuery({
        queryKey: ['allSchedules'],
        enabled: !isAuth0Loading,
        queryFn: async () => {
            const token = await getAccessTokenSilently();

            const response = await managerActionsApi.managerScheduleActionsSchedulesGet(
                { 
                    creationTimeOrder: NullableOfOrder.Descending
                },
                { 
                    headers: { 
                        Authorization: `Bearer ${token}` 
                    } 
                },
            );
            return response.data;
        },
    });
};

export const useQueryCurrentUserFutureVacations = (
    dateRange: UserDateRangePreferenceRequestModel,
) => {
    const { getAccessTokenSilently } = useAuth0();

    return useQuery({
        queryKey: ['userFutureVacations', dateRange],
        queryFn: async () => {
            const token = await getAccessTokenSilently();

            const response = await userScheduleApi.userSchedulePreferencesRequestVacationsByDateRangePost(
                dateRange,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                },
            );

            return response.data;
        },
    });
};

export const useUserDateRangePreferenceRequest = ({
    onSuccessCallback,
}: {
    onSuccessCallback: () => void;
}) => {
    const { getAccessTokenSilently } = useAuth0();
    const queryClient = useQueryClient();

    return useMutation({
        mutationFn: async (data: UserDateRangePreferenceRequestModel) => {
            const token = await getAccessTokenSilently();

            const response = await userScheduleApi.userSchedulePreferencesRequestDateRangePreferenceRequestPost(
                data,
                {
                    headers: {
                        Authorization: `Bearer ${token}`,
                    },
                },
            );

            return response;
        },
        onSuccess: () => {
            queryClient.invalidateQueries({ queryKey: ['userFutureVacations'] });
            onSuccessCallback();
        },
    });
};
