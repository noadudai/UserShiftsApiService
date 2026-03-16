import { DateTimePickerAndLabel, TimePickerAndLabel } from './TimeAndDatePickerComponents.tsx';
import { ShiftMetadata } from './Types.ts';
import { Guid } from 'guid-typescript';

export type EditingShiftPageProps = {
    editingShift: ShiftMetadata;
    shiftInSchedule: ShiftMetadata;
    saveEditingShiftDateAndTimesToScheduleCallBack: (id: Guid, start: Date, end: Date) => void;
    updateEditingShiftStartTime: (date: Date) => void;
    updateEditingShiftEndDateAndTime: (date: Date) => void;
};

export const EditingShiftPane = ({
    editingShift,
    shiftInSchedule,
    saveEditingShiftDateAndTimesToScheduleCallBack,
    updateEditingShiftStartTime,
    updateEditingShiftEndDateAndTime,
}: EditingShiftPageProps) => {
    const { id, startDateAndTime, endDateAndTime } = editingShift;
    const hasStartAndEndTime = startDateAndTime !== undefined && endDateAndTime !== undefined;
    return (
        <>
            <div className="flex pt-64 justify-evenly inset-0 fixed items-center bg-black/5 backdrop-blur-sm">
                <div className="bg-white border rounded-xl border-gray-200 p-6 flex flex-col gap-3 items-center">
                    <TimePickerAndLabel
                        label={'Set shift starting time '}
                        startTime={startDateAndTime}
                        setTimeCallback={(date: Date) => updateEditingShiftStartTime(date)}
                    />
                    <DateTimePickerAndLabel
                        label={'Set shift ending shift '}
                        endTime={endDateAndTime ?? undefined} // show the end time or nothing
                        setDatetimeCallback={(date: Date) => updateEditingShiftEndDateAndTime(date)}
                    />
                    <button
                        className="disabled:bg-gray-300 disabled:text-gray-950 bg-custom-pastel-green p-2 text-center text-custom-cream rounded-xl items-center"
                        disabled={!hasStartAndEndTime}
                        onClick={() => {
                            if (hasStartAndEndTime) {
                                // shiftInSchedule.startDateAndTime has only the target date — time is irrelevant.
                                // editingShift.startDateAndTime has only the target new time — date is irrelevant.
                                // newEditingStartDateAndTime combine the target date and time
                                const newEditingStartDateAndTime = new Date(
                                    shiftInSchedule.startDateAndTime,
                                );

                                newEditingStartDateAndTime.setHours(startDateAndTime.getHours());
                                newEditingStartDateAndTime.setMinutes(
                                    startDateAndTime.getMinutes(),
                                );
                                newEditingStartDateAndTime.setSeconds(0);
                                newEditingStartDateAndTime.setMilliseconds(0);

                                saveEditingShiftDateAndTimesToScheduleCallBack(
                                    id,
                                    newEditingStartDateAndTime,
                                    endDateAndTime,
                                );
                            }
                        }}
                    >
                        Save
                    </button>
                </div>
            </div>
        </>
    );
};
