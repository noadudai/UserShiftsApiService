import TimePicker from 'react-time-picker';
import { format } from 'date-fns';
import { timePickerTimeChangeHandler } from './TimePickerTimeChangeHandler.ts';
import DateTimePicker from 'react-datetime-picker';

export type TimePickerAndLabelProps = {
    label: string;
    startTime?: Date;
    setTimeCallback: (date: Date) => void;
};

export type DateTimePickerAndLabelProps = {
    label: string;
    endTime?: Date;
    setDatetimeCallback: (date: Date) => void;
};

export const TimePickerAndLabel = ({
    label,
    startTime,
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
                value={startTime ? format(startTime, 'HH:mm') : ''}
            />
        </div>
    );
};

export const DateTimePickerAndLabel = ({
    label,
    endTime,
    setDatetimeCallback,
}: DateTimePickerAndLabelProps) => {
    return (
        <div>
            <label>{label}</label>
            <DateTimePicker
                onChange={(date) => {
                    if (date) {
                        setDatetimeCallback(date);
                    }
                }}
                value={endTime}
                className="border border-custom-cream-warm bg-custom-cream p-2 text-xs w-40"
                disableClock={true}
                calendarIcon={null}
                clearIcon={null}
            />
        </div>
    );
};
