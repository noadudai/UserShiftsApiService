import { HiOutlinePencil } from 'react-icons/hi';
import { IoTrashOutline } from 'react-icons/io5';
import { UserVacationModel } from '@noadudai/scheduler-backend-client/api.ts';

const UserVacationsPane = ({
    paneVacations,
}: {
    paneVacations: UserVacationModel[] | undefined;
}) => {
    return (
        <div className="flex flex-wrap justify-normal items-center gap-6">
            {paneVacations ? (
                paneVacations.map((range, index) => (
                    <div
                        key={index}
                        className="flex justify-between bg-custom-soft-blue text-black p-2 gap-2 rounded-lg w-64 font-opensans"
                    >
                        {range.startDate && range.endDate ? (
                            range.startDate === range.endDate ? (
                                new Date(range.startDate).toLocaleDateString()
                            ) : (
                                `${new Date(range.startDate).toLocaleDateString()} - ${new Date(range.endDate).toLocaleDateString()}`
                            )
                        ) : (
                            <p>Something went wrong. </p>
                        )}
                        <div className="flex ">
                            <button className="relative -my-2 pl-2 border-l border-black hover:text-black/50">
                                <HiOutlinePencil size={20} />
                            </button>
                            <button className="relative -my-2 ml-2 pl-2 border-l border-black hover:text-black/50">
                                <IoTrashOutline size={20} />
                            </button>
                        </div>
                    </div>
                ))
            ) : (
                <p>Something went wrong.</p>
            )}
        </div>
    );
};

export default UserVacationsPane;
