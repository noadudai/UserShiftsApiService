import { useState } from 'react';
import { UserDateRangePreferenceRequestModel } from '@noadudai/scheduler-backend-client/api.ts';
import { DateRangeRequestType } from '@noadudai/scheduler-backend-client';
import { IoIosCheckmark } from 'react-icons/io';
import { useUserDateRangePreferenceRequest } from '../../apis.ts';

const VacationRequestForm = ({ onClose }: { onClose: () => void }) => {
    const [vacationStartDate, setVacationStartDate] = useState<Date | null>(null);
    const [vacationEndDate, setVacationEndDate] = useState<Date | null>(null);

    const mutation = useUserDateRangePreferenceRequest({
        onSuccessCallback: () => {
            setVacationStartDate(null);
            setVacationEndDate(null);
        },
    });

    const handleSubmitVacation = () => {
        const data: UserDateRangePreferenceRequestModel = {
            start_date: vacationStartDate?.toISOString(),
            end_date: vacationEndDate?.toISOString(),
            request_type: DateRangeRequestType.Vacation,
        };

        mutation.mutate(data);

        onClose();
    };

    return (
        <div className="flex pt-64 justify-evenly inset-0 bg-opacity-30 backdrop-blur-sm fixed items-center">
            <div className="border border-gray-200 rounded-lg bg-grey-200 p-2">
                <div className="flex items-center justify-evenly text-black">
                    <p>Start Date</p>
                    <p>End Date</p>
                </div>
                <input
                    type="date"
                    id="start_date"
                    className="shadow border rounded py-1 px-2 bg-gray-100 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                    onChange={(e) => setVacationStartDate(new Date(e.target.value))}
                />
                <input
                    type="date"
                    id="end_date"
                    className="shadow border rounded py-1 px-2 bg-gray-100 text-gray-700 leading-tight focus:outline-none focus:shadow-outline"
                    onChange={(e) => setVacationEndDate(new Date(e.target.value))}
                />

                <div className="flex justify-center space-x-2 p-3" onClick={handleSubmitVacation}>
                    <button
                        disabled={!(vacationStartDate && vacationEndDate)}
                        className="disabled:bg-gray-300 disabled:text-gray-950 bg-custom-pastel-green text-black  rounded-full"
                    >
                        <IoIosCheckmark size={40} />
                    </button>
                </div>
            </div>
        </div>
    );
};

export default VacationRequestForm;
