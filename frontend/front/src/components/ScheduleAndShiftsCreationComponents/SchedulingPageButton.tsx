import { DAYS } from './Days.ts';

type SchedulingPageButtonTone = 'default' | 'upcomingDeadline' | 'lastChance' | 'published';

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
    today: Date;
    hasScheduleForNextWeek: boolean;
    workingScheduleIsPublished?: boolean;
    onClick?: () => void;
};

const SchedulingPageButton = ({
    label,
    today,
    hasScheduleForNextWeek,
    workingScheduleIsPublished,
    onClick,
}: SchedulingPageButtonProps) => {
    const day = today.getDay();

    let borderTone: SchedulingPageButtonTone = 'default';
    let helperText = '';
    let disabled = false;

    if (label === "Next Week's Shifts") {
        if (hasScheduleForNextWeek) {
            borderTone = 'published';
        } else if (day < DAYS.WEDNESDAY) {
            borderTone = 'upcomingDeadline';
            helperText = "Create next week's shifts";
        } else if (day === DAYS.WEDNESDAY) {
            borderTone = 'lastChance';
            helperText = "Last day to create next week's shifts!!";
        }
    } else if (workingScheduleIsPublished) {
        borderTone = 'published';
    } else if (!hasScheduleForNextWeek && day !== DAYS.THURSDAY && day !== DAYS.FRIDAY) {
        helperText = 'create the shifts schedule first';
        disabled = true;
    } else if (day === DAYS.FRIDAY) {
        borderTone = 'lastChance';
        helperText = "Last day to create next week's schedule!!";
    } else if (hasScheduleForNextWeek) {
        borderTone = 'upcomingDeadline';
        helperText = day === DAYS.THURSDAY ? "Create next week's schedule" : '';
    } else if (day === DAYS.THURSDAY) {
        helperText = "Create next week's schedule";
    }

    return (
        <div className="p-5 group relative">
            <button
                className={getSchedulingPageButtonClassName(borderTone)}
                disabled={disabled}
                onClick={onClick}
            >
                {label}
            </button>
            <div className="opacity-0 group-hover:opacity-100 transition-all text-xs">{helperText}</div>
        </div>
    );
};

export default SchedulingPageButton;
