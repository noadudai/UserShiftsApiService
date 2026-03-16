import { useAuth0 } from '@auth0/auth0-react';
import { InfoPageNavBar } from '../components/InfoPageNavBar.tsx';
import { Link } from 'react-router-dom';

const InfoPage = () => {
    const { loginWithRedirect, isAuthenticated } = useAuth0();

    return (
        <div>
            <InfoPageNavBar
                isAuthenticated={isAuthenticated}
                loginWithRedirect={loginWithRedirect}
            />
            {!isAuthenticated ? (
                <div className="justify-items-center">
                    <p className="text-center p-20 text-7xl">Hello!</p>
                </div>
            ) : (
                <div>
                    <div className="grid justify-items-center p-20">
                        <p className="text-8xl">Welcome</p>
                        <div className="bg-green-300 text-green-950 font-medium border border-green-950 hover:bg-green-200 hover:text-green-900 px-3 py-2 mx-0.5 rounded-lg">
                            <Link to="/">Home page</Link>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
};

export default InfoPage;
