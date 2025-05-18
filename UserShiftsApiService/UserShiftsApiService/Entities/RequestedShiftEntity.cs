namespace UserShiftsApiService.Entities;

public class RequestedShiftEntity
{
    public string Id { get; set; }
    public string UserShiftsPreferenceRequestId { get; set; }
    public string ShiftId { get; set; }
    public virtual ShiftEntity Shift { get; set; }
}