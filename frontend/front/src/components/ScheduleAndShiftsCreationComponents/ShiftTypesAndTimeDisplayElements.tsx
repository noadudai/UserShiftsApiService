import { ShiftType } from './Types.ts';

export type ShiftTimingLabel = 'Starts' | 'Ends';
export const ShiftTypeLabel = ({ shiftType }: { shiftType: ShiftType }) => (
    <p className="bg-custom-pastel-green rounded-lg text-xl italic flex items-center justify-center text-center pb-1">
        {shiftType}
    </p>
);

export const ShiftTimeInfo = ({
    timingLabel,
    timeToRepresent,
}: {
    timingLabel: ShiftTimingLabel;
    timeToRepresent: string;
}) => {
    return (
        <>
            <p className="text-black text-xs">{timingLabel} at</p>
            <div className="bg-custom-cream w-14 p-0.5 rounded-lg text-xs text-center">
                {timeToRepresent}
            </div>
        </>
    );
};
