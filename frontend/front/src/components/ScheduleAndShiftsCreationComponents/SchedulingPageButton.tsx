import { DAYS } from './Days.ts';

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

    return (
        <div className="p-5 group relative">
            <button
                className={`rounded-lg bg-custom-cream-warm transition-colors p-4 border-2 group-hover:bg-custom-cream-warm/80 disabled:cursor-not-allowed disabled:opacity-70 ${
                    label === "Next Week's Shifts"
                        ? hasScheduleForNextWeek
                            ? 'border-custom-pastel-green'
                            : day < DAYS.WEDNESDAY
                              ? 'border-orange-400'
                              : day === DAYS.WEDNESDAY
                                ? 'border-custom-warm-coral-pink'
                                : ''
                        : workingScheduleIsPublished
                          ? 'border-custom-pastel-green'
                          : day === DAYS.FRIDAY
                            ? 'border-custom-warm-coral-pink'
                            : hasScheduleForNextWeek
                              ? 'border-orange-400'
                              : ''
                }`.trim()}
                disabled={
                    label !== "Next Week's Shifts" &&
                    !workingScheduleIsPublished &&
                    !hasScheduleForNextWeek &&
                    day !== DAYS.THURSDAY &&
                    day !== DAYS.FRIDAY
                }
                onClick={onClick}
            >
                {label}
            </button>
            <div className="opacity-0 group-hover:opacity-100 transition-all text-xs">
                {label === "Next Week's Shifts"
                    ? hasScheduleForNextWeek
                        ? ""
                        : day < DAYS.WEDNESDAY
                          ? "Create next week's shifts"
                          : day === DAYS.WEDNESDAY
                            ? "Last day to create next week's shifts!!"
                            : ""
                    : workingScheduleIsPublished
                      ? ""
                      : !hasScheduleForNextWeek
                        ? "create the shifts schedule first"
                        : day === DAYS.THURSDAY
                          ? "Create next week's schedule"
                          : day === DAYS.FRIDAY
                            ? "Last day to create next week's schedule!!"
                            : ""}
            </div>
        </div>
    );
};

export default SchedulingPageButton;
