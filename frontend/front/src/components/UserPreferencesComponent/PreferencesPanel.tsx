import { IoIosAdd } from 'react-icons/io';

const PreferencesPanel = () => {
    return (
        <div className="p-4">
            <h1 className="text-center text-2xl font-opensans">
                Set schedule preferences for next week
            </h1>
            <p className="pb-6 text-center text-base font-opensans">
                (If not set, implied full availability)
            </p>
            <div className="flex justify-center">
                <button
                    type="button"
                    aria-label="Add preferences"
                    className="rounded-3xl bg-custom-pastel-green/50 px-12 py-4 text-custom-cream transition-colors hover:bg-custom-pastel-green/60"
                >
                    <IoIosAdd size={56} />
                </button>
            </div>
        </div>
    );
};

export default PreferencesPanel;
