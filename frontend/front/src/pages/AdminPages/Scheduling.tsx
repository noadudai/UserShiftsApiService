import WeeklyShiftPanel from '../../components/WeeklyShiftPanel.tsx';
import { useState } from 'react';
import { Guid } from 'guid-typescript';
import {
    ShiftMetadata,
    AllShiftTypes,
    ShiftMetadataWithEndDate,
} from '../../components/ScheduleAndShiftsCreationComponents/Types.ts';
import { useCreateNewShiftsSchedule, useQueryAllSchedulesDescending } from '../../apis.ts';
import { CreateNewScheduleModel, ScheduleStatus } from '@noadudai/scheduler-backend-client/api.ts';
import { getNextWeeksDates } from '../../components/ScheduleAndShiftsCreationComponents/NextWeeksDates.ts';
import { getScheduleInGivenDateRange } from '../../components/ScheduleAndShiftsCreationComponents/ScheduleIsForNextWeekCheck.ts';
import { DAYS } from '../../components/ScheduleAndShiftsCreationComponents/Days.ts';
import SchedulingPageButton from '../../components/ScheduleAndShiftsCreationComponents/SchedulingPageButton.tsx';
import {
    getNextWeeksScheduleButtonState,
    getNextWeeksShiftsButtonState,
} from '../../components/ScheduleAndShiftsCreationComponents/SchedulingButtonStates.ts';

const Scheduling = () => {
    const nextWeeksDayDates: Date[] = getNextWeeksDates();
    const [isWeeklyShiftPanelOpen, setIsWeeklyShiftPanelOpen] = useState(false);

    const initialState: ShiftMetadata[] = Array.from(Object.values(AllShiftTypes), (type) =>
        nextWeeksDayDates.map((date) => {
            return {
                id: Guid.create(),
                shiftType: type,
                startDateAndTime: date,
                endDateAndTime: undefined,
            };
        }),
    ).flat();
    const [shiftsSchedule, setShiftsSchedule] = useState<ShiftMetadata[]>(initialState);

    const { data: schedulesResponse } = useQueryAllSchedulesDescending();
    const schedules = schedulesResponse?.schedules ?? []; // the schedules from the api can be possibly null
    const ShiftsSchedules = schedules.map((schedule) => ({
        // the schedule's shifts from the api can be possibly null
        ...schedule,
        shifts: schedule.shifts ?? [],
    }));

    const scheduleForNextWeek = getScheduleInGivenDateRange({
        schedules: ShiftsSchedules,
        dateRange: nextWeeksDayDates,
    });

    const nextWeeksShifts: ShiftMetadata[] = scheduleForNextWeek
        ? scheduleForNextWeek.shifts.map((shift) => {
              return {
                  id: Guid.create(),
                  shiftType: shift.shiftType,
                  startDateAndTime: new Date(shift.shiftStartTime),
                  endDateAndTime: new Date(shift.shiftEndTime),
              };
          })
        : [];

    // Only the shifts that have a defined endDateAndTime, are shifts that the manager created for the schedule.
    const shiftHasEndDate = (shift: ShiftMetadata): shift is ShiftMetadataWithEndDate => {
        return shift.endDateAndTime !== undefined;
    };
    const shiftsForMutation: ShiftMetadataWithEndDate[] = shiftsSchedule.filter(shiftHasEndDate);

    const saveEditingShiftToSchedule = (
        shiftId: Guid,
        startDateAndTime: Date,
        endDateAndTime: Date,
    ) => {
        setShiftsSchedule((prev) =>
            prev.map((shift) =>
                shift.id === shiftId
                    ? {
                          ...shift,
                          startDateAndTime: startDateAndTime,
                          endDateAndTime: endDateAndTime,
                      }
                    : shift,
            ),
        );
    };

    const mutation = useCreateNewShiftsSchedule();
    const submitShiftsSchedule =
        shiftsForMutation.length > 0
            ? () => {
                  const data: CreateNewScheduleModel = {
                      shifts: shiftsForMutation.map((shift) => ({
                          shiftStartTime: shift.startDateAndTime.toISOString(),
                          shiftEndTime: shift.endDateAndTime.toISOString(),
                          shiftType: shift.shiftType,
                      })),
                      status: ScheduleStatus.Draft,
                  };

                  mutation.mutate(data);

                  setIsWeeklyShiftPanelOpen(false);
              }
            : undefined;

    const today = new Date();
    const todayIsWednesday = today.getDay() === DAYS.WEDNESDAY;
    const todayIsNotYetWednesday = today.getDay() < DAYS.WEDNESDAY;
    const todayIsThursday = today.getDay() === DAYS.THURSDAY;
    const todayIsFriday = today.getDay() === DAYS.FRIDAY;
    const hasScheduleForNextWeek = scheduleForNextWeek !== undefined;
    const workingScheduleIsPublished = false;

    const nextWeeksShiftsButtonState = getNextWeeksShiftsButtonState({
        hasScheduleForNextWeek,
        todayIsNotYetWednesday,
        todayIsWednesday,
    });
    const nextWeeksScheduleButtonState = getNextWeeksScheduleButtonState({
        hasScheduleForNextWeek,
        todayIsThursday,
        todayIsFriday,
        workingScheduleIsPublished,
    });

    return (
        <div className="flex items-center justify-center gap-4 p-2">
            <SchedulingPageButton
                label="Next Week's Shifts"
                onClick={() => setIsWeeklyShiftPanelOpen(true)}
                state={nextWeeksShiftsButtonState}
            />
            <SchedulingPageButton
                label="Next Week's Schedule"
                state={nextWeeksScheduleButtonState}
            />

            {isWeeklyShiftPanelOpen && (
                <WeeklyShiftPanel
                    onClose={() => setIsWeeklyShiftPanelOpen(false)}
                    saveEditingShiftToSchedule={saveEditingShiftToSchedule}
                    shiftsSchedule={scheduleForNextWeek ? nextWeeksShifts : shiftsSchedule}
                    nextWeeksDayDates={nextWeeksDayDates}
                    onSubmitSchedule={submitShiftsSchedule}
                    mode={scheduleForNextWeek ? 'view' : 'edit'}
                />
            )}
        </div>
    );
};

export default Scheduling;
