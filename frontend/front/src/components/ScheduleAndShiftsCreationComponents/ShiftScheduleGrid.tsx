import { ShiftTypeLabel } from './ShiftTypesAndTimeDisplayElements.tsx';
import { isSameDay } from './SameDateCheck.ts';
import { ShiftTimePane } from './ShiftTimePane.tsx';
import { TiPlus } from 'react-icons/ti';
import { ShiftMetadata, ShiftType } from './Types.ts';
import { MdOutlineModeEdit } from 'react-icons/md';

export type ShiftScheduleGridProps = {
    shiftTypes: ShiftType[];
    nextWeeksDayDates: Date[];
    shiftsSchedule: ShiftMetadata[];
    setEditingShift: (shift: ShiftMetadata | undefined) => void;
    mode: 'edit' | 'view';
};

export const ShiftScheduleGrid = ({
    shiftTypes,
    nextWeeksDayDates,
    shiftsSchedule,
    setEditingShift,
    mode,
}: ShiftScheduleGridProps) => {
    const days = nextWeeksDayDates.map((date) => (
        <p
            key={date.toISOString()}
            className="bg-custom-pastel-green rounded-lg items-center text-center italic pb-1"
        >
            {date.toLocaleDateString('en-us', {
                weekday: 'short',
                day: 'numeric',
                month: 'numeric',
            })}
        </p>
    ));

    return (
        <>
            <div className="grid grid-cols-8 w-full h-full gap-4">
                <span className="bg-custom-pastel-green rounded-lg text-2xl text-center italic" />
                {days}
            </div>
            {shiftTypes.map((sType) => (
                <div key={sType} className="grid grid-cols-8 w-full h-full gap-4">
                    <ShiftTypeLabel shiftType={sType} />
                    {nextWeeksDayDates.map((date) => {
                        const shift = shiftsSchedule.find(
                            (s) => s.shiftType === sType && isSameDay(s.startDateAndTime, date),
                        );
                        const isEditMode = mode === 'edit';
                        // "shiftEndTime !== undefined" means the manager created/set the shift for next week
                        return shift ? (
                            <div
                                key={`${sType}-${date.toISOString()}`}
                                className={`border border-gray-100 rounded-lg w-full min-h-20 h-auto flex flex-col justify-center p-2 gap-1 ${
                                    shift.endDateAndTime !== undefined
                                        ? 'bg-custom-cream-warm'
                                        : 'bg-custom-cream'
                                }`}
                            >
                                {shift.endDateAndTime !== undefined ? (
                                    <>
                                        <div className="flex flex-col w-full gap-1">
                                            <ShiftTimePane
                                                dayClickedInWeek={date}
                                                timeToRender={shift.startDateAndTime}
                                                label={'Starts'}
                                            />
                                            <ShiftTimePane
                                                dayClickedInWeek={date}
                                                timeToRender={shift.endDateAndTime}
                                                label={'Ends'}
                                            />
                                        </div>
                                        {isEditMode && (
                                            <div className="flex justify-end w-full">
                                                <button
                                                    className="bg-custom-pastel-green rounded-full p-0.5"
                                                    onClick={() => setEditingShift(shift)}
                                                >
                                                    <MdOutlineModeEdit size={12} />
                                                </button>
                                            </div>
                                        )}
                                    </>
                                ) : isEditMode ? (
                                    <button
                                        className="text-custom-pastel-green self-center"
                                        onClick={() => setEditingShift(shift)}
                                    >
                                        <TiPlus size={35} />
                                    </button>
                                ) : (
                                    <></>
                                )}
                            </div>
                        ) : (
                            <div
                                key={`${sType}-${date.toISOString()}`}
                                className="border border-gray-100 rounded-lg w-full h-20 flex justify-center items-center bg-custom-cream"
                            >
                                {/*Later, loading shift animation if the component is waiting for the response from db */}
                            </div>
                        );
                    })}
                </div>
            ))}
        </>
    );
};
