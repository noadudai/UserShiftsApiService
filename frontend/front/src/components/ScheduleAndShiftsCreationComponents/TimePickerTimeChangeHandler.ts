export const timePickerTimeChangeHandler = ({
    time,
    onCallback,
}: {
    time: string;
    onCallback: (date: Date) => void;
}) => {
    const [hour, minute] = time.split(':');

    const newDate = new Date();
    newDate.setHours(parseInt(hour));
    newDate.setMinutes(parseInt(minute));

    onCallback(newDate);
};
