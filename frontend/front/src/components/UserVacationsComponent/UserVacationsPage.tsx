import { useState } from 'react';
import PageNavigator from '../PageNavigator.tsx';
import UserVacationsPane from './UserVacationsPane.tsx';
import { useQueryCurrentUserFutureVacations } from '../../apis.ts';
import { UserVacationModel } from '@noadudai/scheduler-backend-client/api.ts';
import { UserVacationsByDateRangeModel } from '@noadudai/scheduler-backend-client';

const UserVacationsPage = () => {
    const [itemsPerPage] = useState(8);
    const [currentPage, setCurrentPage] = useState(1);

    const today = new Date();
    today.setSeconds(0, 0);

    const nextYear = today.getFullYear() + 1;

    const endDate = new Date(nextYear, today.getMonth(), today.getDate());
    endDate.setSeconds(0, 0);

    const DateRangeForFetchingFutureVacations: UserVacationsByDateRangeModel = {
        start_date: today.toISOString(),
        end_date: endDate.toISOString(),
    };

    const { isLoading, data } = useQueryCurrentUserFutureVacations(
        DateRangeForFetchingFutureVacations,
    );
    const indexVacationPage = (currentPage - 1) * itemsPerPage;

    const sortedVacations = data?.vacations?.sort((a, b) => {
        const dateA = new Date(a.startDate ?? 0).getTime();
        const dateB = new Date(b.startDate ?? 0).getTime();
        return dateA - dateB;
    });

    const paneVacations: UserVacationModel[] | undefined = sortedVacations?.slice(
        indexVacationPage,
        indexVacationPage + itemsPerPage,
    );

    const innerComponent = isLoading ? (
        <div>Loading...</div>
    ) : data && data.vacations && data.vacations.length !== 0 ? (
        <div>
            <UserVacationsPane paneVacations={paneVacations} />
            <PageNavigator
                itemsPerPage={itemsPerPage}
                totalItems={data.vacations.length}
                currentPage={currentPage}
                onPageChange={(pageNumber: number) => setCurrentPage(pageNumber)}
            />
        </div>
    ) : (
        <div>No Future Vacations</div>
    );

    return <div className="p-6">{innerComponent}</div>;
};

export default UserVacationsPage;
