namespace UserShiftsApiService.Entities;

public class RequestedShiftEntity
{
    public string Id { get; set; }
    public virtual string UserShiftsRequestId { get; set; }
    public virtual UserShiftsPreferenceRequestEntity UserShiftsRequest { get; set; }
    public virtual string ShiftId { get; set; }
    public virtual ShiftEntity Shift { get; set; }
}