import { useState } from 'react';
import { IoIosCheckmark } from 'react-icons/io';
import { Guid } from 'guid-typescript';
import { ShiftScheduleGrid } from './ScheduleAndShiftsCreationComponents/ShiftScheduleGrid.tsx';
import { IoClose } from 'react-icons/io5';
import { ShiftMetadata, AllShiftTypes } from './ScheduleAndShiftsCreationComponents/Types.ts';
import { EditingShiftPane } from './ScheduleAndShiftsCreationComponents/EditingShiftPane.tsx';

type WeeklyShiftCreatorPanelProps = {
    onClose: () => void;
    saveEditingShiftToSchedule: (id: Guid, start: Date, end: Date) => void;
    shiftsSchedule: ShiftMetadata[];
    nextWeeksDayDates: Date[];
    onSubmitSchedule?: () => void;
    mode: 'edit' | 'view';
};

const WeeklyShiftPanel = ({
    onClose,
    saveEditingShiftToSchedule,
    shiftsSchedule,
    nextWeeksDayDates,
    onSubmitSchedule,
    mode,
}: WeeklyShiftCreatorPanelProps) => {
    const [editingShift, setEditingShift] = useState<ShiftMetadata | undefined>(undefined);

    const saveEditingShiftToScheduleCallBack = (
        shiftId: Guid,
        startDateAndTime: Date,
        endDateAndTime: Date,
    ) => {
        saveEditingShiftToSchedule(shiftId, startDateAndTime, endDateAndTime);
        setEditingShift(undefined);
    };

    const updateEditingShiftStartTime = (date: Date) => {
        setEditingShift((prev) => (prev ? { ...prev, startDateAndTime: date } : prev));
    };

    const updateEditingShiftEndDateAndTime = (date: Date) => {
        setEditingShift((prev) => (prev ? { ...prev, endDateAndTime: date } : prev));
    };

    const isEditMode = mode === 'edit';

    return (
        <div className="flex justify-evenly inset-0 bg-opacity-30 backdrop-blur-sm fixed items-center">
            <div className="flex flex-col w-2/3 bg-white place-self-center items-center p-4 gap-2 border border-gray-200 rounded-lg">
                <button
                    className="place-self-end bg-custom-pastel-green text-center text-custom-cream rounded-full p-1"
                    onClick={onClose}
                >
                    <IoClose />{' '}
                </button>
                <div className="flex flex-col gap-2 font-opensans text-center bg-custom-cream-warm w-full p-2 h-full rounded-lg">
                    {isEditMode ? (
                        <h1 className="text-2xl">Create Shifts for next week</h1>
                    ) : (
                        <h1 className="text-2xl">Shifts for next week</h1>
                    )}
                    <h1 className="italic text-xs">
                        {nextWeeksDayDates[0].toDateString()} -{' '}
                        {nextWeeksDayDates.at(-1)?.toDateString()}
                    </h1>
                </div>

                <ShiftScheduleGrid
                    shiftTypes={Array.from(Object.values(AllShiftTypes))}
                    nextWeeksDayDates={nextWeeksDayDates}
                    shiftsSchedule={shiftsSchedule}
                    setEditingShift={setEditingShift}
                    mode={mode}
                />
                {isEditMode && (
                    <button
                        className="bg-custom-pastel-green text-center text-custom-cream rounded-full disabled:bg-gray-300 disabled:text-gray-950"
                        disabled={onSubmitSchedule === undefined}
                        onClick={onSubmitSchedule}
                    >
                        <IoIosCheckmark size={40} />
                    </button>
                )}
                {editingShift && (
                    <EditingShiftPane
                        editingShift={editingShift}
                        shiftInSchedule={shiftsSchedule.find((s) => s.id === editingShift.id)!} // there is definitely a shift if there is an editingShift
                        saveEditingShiftDateAndTimesToScheduleCallBack={
                            saveEditingShiftToScheduleCallBack
                        }
                        updateEditingShiftStartTime={updateEditingShiftStartTime}
                        updateEditingShiftEndDateAndTime={updateEditingShiftEndDateAndTime}
                    />
                )}
            </div>
        </div>
    );
};

export default WeeklyShiftPanel;
