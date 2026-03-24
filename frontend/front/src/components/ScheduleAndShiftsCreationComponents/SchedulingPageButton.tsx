import {
    SchedulingPageButtonState,
    SchedulingPageButtonTone,
} from './SchedulingButtonStates.ts';

const schedulingButtonBaseClass =
    'rounded-lg bg-custom-cream-warm transition-colors p-4 border-2 group-hover:bg-custom-cream-warm/80 disabled:cursor-not-allowed disabled:opacity-70';

const borderClassByTone: Record<SchedulingPageButtonTone, string> = {
    default: '',
    upcomingDeadline: 'border-orange-400',
    lastChance: 'border-custom-warm-coral-pink',
    published: 'border-custom-pastel-green',
};

const getSchedulingPageButtonClassName = (borderTone: SchedulingPageButtonTone) => {
    return `${schedulingButtonBaseClass} ${borderClassByTone[borderTone]}`.trim();
};

type SchedulingPageButtonProps = {
    label: string;
    state: SchedulingPageButtonState;
    onClick?: () => void;
};

const SchedulingPageButton = ({
    label,
    state,
    onClick,
}: SchedulingPageButtonProps) => {
    return (
        <div className="p-5 group relative">
            <button
                className={getSchedulingPageButtonClassName(state.borderTone)}
                disabled={state.disabled}
                onClick={onClick}
            >
                {label}
            </button>
            <div className="opacity-0 group-hover:opacity-100 transition-all text-xs">
                {state.helperText}
            </div>
        </div>
    );
};

export default SchedulingPageButton;
