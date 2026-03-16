export const getNextWeeksDates = () => {
    const daysInWeek: number = 7;
    const today: Date = new Date();
    const nextSunday = daysInWeek - today.getDay();

    return Array.from(
        { length: daysInWeek },
        (_, i) => new Date(today.getFullYear(), today.getMonth(), today.getDate() + nextSunday + i),
    );
};
