import { HomePageNavbar } from '../components/HomePageNavbar.tsx';
import PreferencesPanel from '../components/UserPreferencesComponent/PreferencesPanel.tsx';
import VacationsPanel from '../components/UserVacationsComponent/VacationsPanel.tsx';

const HomePage = () => {
    return (
        <div>
            <HomePageNavbar />
            <div className="grid justify-items-center gap-12 px-6 py-20">
                <div className='w-2/3 rounded-xl border border-dashed border-custom-pastel-green bg-white'>
                    <PreferencesPanel />
                </div>
                <div className='w-2/3 rounded-xl bg-white'>
                    <VacationsPanel />
                </div>
            </div>
        </div>
    );
};

export default HomePage;
