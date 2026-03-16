import { useNavigate } from 'react-router-dom';
import { AdminPageKey, AdminPages } from '../pages/AdminPages/AdminPagesRecord.ts';

const AdminPageSideBar = ({ currentPage }: { currentPage: AdminPageKey }) => {
    const navigate = useNavigate();

    const handleOnClick = (buttonName: string) => {
        const path = `/admin-panel/${buttonName}`;
        navigate(path);
    };

    const Page = (buttonName: string) => (
        <button
            key={buttonName}
            className={`${currentPage === buttonName ? 'bg-custom-pastel-green/80' : 'bg-custom-pastel-green/50'} border border-custom-pastel-green p-2 text-center`}
            onClick={() => handleOnClick(buttonName)}
        >
            {buttonName}
        </button>
    );

    return (
        <div>
            <div className="flex flex-col">{Object.keys(AdminPages).map((key) => Page(key))}</div>
        </div>
    );
};

export default AdminPageSideBar;
