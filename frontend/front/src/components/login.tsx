import { useAuth0 } from '@auth0/auth0-react';

const LoginButton = () => {
    const { loginWithRedirect } = useAuth0();

    return (
        <button
            onClick={() => loginWithRedirect()}
            className="bg-green-300 text-green-950 font-medium border border-green-950 hover:bg-green-200 hover:text-green-900 px-3 py-2 mx-0.5 rounded-lg"
        >
            Log In
        </button>
    );
};

export default LoginButton;
