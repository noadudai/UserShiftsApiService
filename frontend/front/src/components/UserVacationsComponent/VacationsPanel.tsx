import { useState } from 'react';
import VacationRequestForm from './VacationRequestForm.tsx';
import { IoIosAdd, IoMdTime } from 'react-icons/io';
import { IoBan } from 'react-icons/io5';
import { BiCheckCircle } from 'react-icons/bi';
import UserVacationsPage from './UserVacationsPage.tsx';
import RequestStatusLabel from '../RequestStatusLabel.tsx';

const VacationsPanel = () => {
    const [isAddNewVacOpen, setIsAddNewVacOpen] = useState(false);

    return (
        <div>
            <h1 className="flex justify-center text-2xl font-opensans">My Vacations</h1>
            <UserVacationsPage />
            <div className="flex justify-between ">
                <div className="place-self-start flex gap-1 p-2">
                    <RequestStatusLabel
                        status={'Approved'}
                        backgroundColor={'bg-custom-pastel-green'}
                        icon={BiCheckCircle}
                    />
                    <RequestStatusLabel
                        status={'Pending'}
                        backgroundColor={'bg-custom-soft-blue'}
                        icon={IoMdTime}
                    />
                    <RequestStatusLabel
                        status={'Denied'}
                        backgroundColor={'bg-custom-warm-coral-pink'}
                        icon={IoBan}
                    />
                </div>
                <button
                    className="bg-custom-soft-blue text-custom-cream rounded-full mb-2 mr-2"
                    onClick={() => setIsAddNewVacOpen(true)}
                >
                    <IoIosAdd size={45} />
                </button>
            </div>
            {isAddNewVacOpen && (
                <VacationRequestForm
                    onClose={() => {
                        setIsAddNewVacOpen(false);
                    }}
                />
            )}
        </div>
    );
};

export default VacationsPanel;
