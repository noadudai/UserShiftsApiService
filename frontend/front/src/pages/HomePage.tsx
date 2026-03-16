import { HomePageNavbar } from '../components/HomePageNavbar.tsx';
import VacationsPanel from '../components/UserVacationsComponent/VacationsPanel.tsx';

const HomePage = () => {
    return (
        <div>
            <HomePageNavbar />
            <div className="grid justify-items-center">
                <div className="w-2/3 m-20 rounded-xl bg-white">
                    <VacationsPanel />
                </div>
                <div className="w-2/3 m-20 rounded-xl bg-white"></div>
            </div>
        </div>
    );
};

export default HomePage;
