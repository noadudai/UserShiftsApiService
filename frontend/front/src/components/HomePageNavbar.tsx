import { LogoutOptions, useAuth0 } from '@auth0/auth0-react';
import { FaRegUserCircle } from 'react-icons/fa';
import { GiHamburgerMenu } from 'react-icons/gi';
import { Link } from 'react-router-dom';

export const HomePageNavbar = () => {
    const { logout } = useAuth0();

    return (
        <div>
            <nav className="bg-custom-pastel-green rounded-sm shadow-sm flex justify-center">
                <div className="w-2/3 flex p-2 items-center">
                    <div className="flex text-custom-cream font-medium  ">
                        <GiHamburgerMenu size={25} />
                    </div>
                    <div className="flex-1 text-center pl-24">
                        <h1 className="text-4xl text-custom-cream font-opensans">
                            Keep On Time Shifts
                        </h1>
                    </div>
                    <div className="flex gap-4">
                        <Link className="text-custom-cream" to="admin-panel/Overview">
                            Overview
                        </Link>
                        <button
                            onClick={() =>
                                logout({
                                    returnTo: `${import.meta.env.VITE_INFOPAGEURL}`,
                                } as LogoutOptions)
                            }
                            className=" text-custom-cream font-medium  rounded-lg"
                        >
                            <FaRegUserCircle size={25} />
                        </button>
                    </div>
                </div>
            </nav>
        </div>
    );
};
