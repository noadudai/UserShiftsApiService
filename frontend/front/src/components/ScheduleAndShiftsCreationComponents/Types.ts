import { Guid } from 'guid-typescript';

export const AllShiftTypes = {
    MORNING: 'Morning',
    EVENING: 'Evening',
    CLOSING: 'Closing',
} as const;
export type ShiftType = (typeof AllShiftTypes)[keyof typeof AllShiftTypes];

export type ShiftMetadata = {
    id: Guid;
    startDateAndTime: Date;
    endDateAndTime?: Date;
    shiftType: ShiftType;
};

export type EditingShift = {
    id: Guid;
    startDateAndTime?: Date;
    endDateAndTime?: Date;
};

export type ShiftMetadataWithEndDate = ShiftMetadata & { endDateAndTime: Date };
