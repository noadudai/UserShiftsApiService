import { ScheduleModel, ShiftModel } from '@noadudai/scheduler-backend-client';

type scheduleIsInGivenDateRangeProps = {
    schedules: { shifts: ShiftModel[]; schedule: ScheduleModel | null }[];
    dateRange: Date[];
};

export const isShiftInDateRange = (shift: ShiftModel, dateRange: Date[]) => {
    const shiftDate = new Date(shift.shiftStartTime);
    return dateRange.some((dateInRange) => shiftDate.toDateString() === dateInRange.toDateString());
};

export const getScheduleInGivenDateRange = ({
    schedules,
    dateRange,
}: scheduleIsInGivenDateRangeProps) => {
    return schedules.find((schedule) => {
        if (schedule.shifts.length == 0) return undefined;
        return schedule.shifts.every((shift) => isShiftInDateRange(shift, dateRange));
    });
};
