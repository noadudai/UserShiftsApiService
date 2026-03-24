export type SchedulingPageButtonTone =
    | 'default'
    | 'upcomingDeadline'
    | 'lastChance'
    | 'published';

export type SchedulingPageButtonState = {
    borderTone: SchedulingPageButtonTone;
    helperText: string;
    disabled: boolean;
};

const createButtonState = ({
    borderTone = 'default',
    helperText = '',
    disabled = false,
}: Partial<SchedulingPageButtonState> = {}): SchedulingPageButtonState => {
    return {
        borderTone,
        helperText,
        disabled,
    };
};

export const getNextWeeksShiftsButtonState = ({
    hasScheduleForNextWeek,
    todayIsNotYetWednesday,
    todayIsWednesday,
}: {
    hasScheduleForNextWeek: boolean;
    todayIsNotYetWednesday: boolean;
    todayIsWednesday: boolean;
}): SchedulingPageButtonState => {
    if (hasScheduleForNextWeek) {
        return createButtonState({ borderTone: 'published' });
    }

    if (todayIsNotYetWednesday) {
        return createButtonState({
            borderTone: 'upcomingDeadline',
            helperText: "Create next week's shifts",
        });
    }

    if (todayIsWednesday) {
        return createButtonState({
            borderTone: 'lastChance',
            helperText: "Last day to create next week's shifts!!",
        });
    }

    return createButtonState();
};

export const getNextWeeksScheduleButtonState = ({
    hasScheduleForNextWeek,
    todayIsThursday,
    todayIsFriday,
    workingScheduleIsPublished,
}: {
    hasScheduleForNextWeek: boolean;
    todayIsThursday: boolean;
    todayIsFriday: boolean;
    workingScheduleIsPublished: boolean;
}): SchedulingPageButtonState => {
    if (workingScheduleIsPublished) {
        return createButtonState({ borderTone: 'published' });
    }

    if (!hasScheduleForNextWeek && !todayIsThursday && !todayIsFriday) {
        return createButtonState({
            helperText: 'create the shifts schedule first',
            disabled: true,
        });
    }

    if (todayIsFriday) {
        return createButtonState({
            borderTone: 'lastChance',
            helperText: "Last day to create next week's schedule!!",
        });
    }

    if (hasScheduleForNextWeek) {
        return createButtonState({
            borderTone: 'upcomingDeadline',
            helperText: todayIsThursday ? "Create next week's schedule" : '',
        });
    }

    if (todayIsThursday) {
        return createButtonState({
            helperText: "Create next week's schedule",
        });
    }

    return createButtonState();
};
