import AdminPanelNavbar from '../components/AdminPanelNavbar.tsx';
import AdminPageSideBar from '../components/AdminPageSideBar.tsx';
import { useParams } from 'react-router-dom';
import { AdminPageKey, AdminPages, isAdminPageName } from './AdminPages/AdminPagesRecord.ts';

const AdminPage = () => {
    const { '*': pageName } = useParams();

    const adminPageName: AdminPageKey =
        pageName && isAdminPageName(pageName) ? pageName : 'Overview';

    const InnerPageComponent = AdminPages[adminPageName];

    return (
        <div>
            <AdminPanelNavbar />
            <div className="justify-items-center">
                <div className="w-2/3 m-20 min-h-64 rounded-xl bg-white grid grid-cols-6">
                    <div className="col-span-1">
                        <AdminPageSideBar currentPage={adminPageName} />
                    </div>
                    <div className="col-span-5 bg-custom-pastel-green/">
                        <InnerPageComponent />
                    </div>
                </div>
            </div>
        </div>
    );
};

export default AdminPage;
