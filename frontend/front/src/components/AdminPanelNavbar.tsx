import { LogoutOptions, useAuth0 } from '@auth0/auth0-react';
import { FaRegUserCircle } from 'react-icons/fa';
import { GiHamburgerMenu } from 'react-icons/gi';
import { Link } from 'react-router-dom';

const AdminPanelNavbar = () => {
    const { logout } = useAuth0();

    return (
        <div>
            <nav className="bg-custom-pastel-green rounded-sm shadow-sm flex justify-center">
                <div className="w-2/3 flex p-2 justify-between items-center">
                    <div className="flex text-custom-cream font-medium pr-4">
                        <GiHamburgerMenu size={25} />
                    </div>
                    <div className="pl-22">
                        <Link className="text-4xl text-custom-cream font-opensans" to="/">
                            Keep On Time Shifts
                        </Link>
                    </div>
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
            </nav>
        </div>
    );
};

export default AdminPanelNavbar;
