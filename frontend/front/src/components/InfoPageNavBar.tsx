import { Link } from 'react-router-dom';
import { RedirectLoginOptions } from '@auth0/auth0-react';

export const InfoPageNavBar = ({
    isAuthenticated,
    loginWithRedirect,
}: {
    isAuthenticated: boolean;
    loginWithRedirect: (options?: RedirectLoginOptions) => Promise<void>;
}) => {
    const handleLogin = () => {
        loginWithRedirect();
    };

    return (
        <div>
            <nav className="bg-green-400 border border-green-500 rounded-sm shadow-sm">
                <div className="flex items-center justify-between p-1">
                    <div className="flex items-center pl-8 p-2 shadow-sms">
                        <h1 className="text-4xl text-green-950">Keep On Time Shifts - Info Page</h1>
                    </div>
                    <div className="flex items-center justify-between p-2">
                        {isAuthenticated ? (
                            <div className="bg-green-300 text-green-950 font-medium border border-green-950 hover:bg-green-200 hover:text-green-900 px-3 py-2 mx-0.5 rounded-lg">
                                <Link to="/">Home page</Link>
                            </div>
                        ) : (
                            <button
                                onClick={handleLogin}
                                className="bg-green-300 text-green-950 font-medium border border-green-950 hover:bg-green-200 hover:text-green-900 px-3 py-2 mx-0.5 rounded-lg"
                            >
                                Login
                            </button>
                        )}
                    </div>
                </div>
            </nav>
        </div>
    );
};
