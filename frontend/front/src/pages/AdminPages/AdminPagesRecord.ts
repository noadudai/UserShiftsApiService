import Overview from './Overview.tsx';
import Employees from './Employees.tsx';
import Scheduling from './Scheduling.tsx';
import Vacations from './Vacations.tsx';

export const AdminPages = {
    Overview: Overview,
    Employees: Employees,
    Scheduling: Scheduling,
    Vacations: Vacations,
} as const;

export type AdminPageKey = keyof typeof AdminPages;

export type AdminPageName = (typeof AdminPages)[AdminPageKey];

export const isAdminPageName = (pageName: string): pageName is AdminPageKey => {
    return pageName in AdminPages;
};
