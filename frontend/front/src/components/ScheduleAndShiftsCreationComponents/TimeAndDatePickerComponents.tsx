import TimePicker from 'react-time-picker';
import { format } from 'date-fns';
import { timePickerTimeChangeHandler } from './TimePickerTimeChangeHandler.ts';

export type TimePickerAndLabelProps = {
    label: string;
    time?: Date;
    setTimeCallback: (date: Date) => void;
};

export const TimePickerAndLabel = ({
    label,
    time,
    setTimeCallback,
}: TimePickerAndLabelProps) => {
    return (
        <div className="flex items-center">
            <label>{label}</label>
            <TimePicker
                clearIcon={null}
                disableClock={true}
                className="border border-custom-cream-warm bg-custom-cream p-2 text-xs w-28"
                onChange={(e) => {
                    if (e) {
                        timePickerTimeChangeHandler({
                            time: e,
                            onCallback: (date) => setTimeCallback(date),
                        });
                    }
                }}
                value={time ? format(time, 'HH:mm') : ''}
            />
        </div>
    );
};
