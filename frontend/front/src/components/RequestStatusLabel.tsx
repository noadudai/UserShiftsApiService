import { IconType } from 'react-icons/lib/iconBase';

const RequestStatusLabel = ({
    status,
    backgroundColor,
    icon,
}: {
    status: string;
    backgroundColor: string;
    icon: IconType;
}) => {
    return (
        <div
            className={`flex items-center ${backgroundColor} text-xs text-black gap-2 rounded-xl p-2`}
        >
            {icon({ size: 20 })}
            <h1 className="font-opensans">{status}</h1>
        </div>
    );
};

export default RequestStatusLabel;
