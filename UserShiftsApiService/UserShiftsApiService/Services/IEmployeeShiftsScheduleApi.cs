using System.Threading.Tasks;
using noadudai.schedule_generator_client.Model;

namespace UserShiftsApiService.Services;

public interface IEmployeeShiftsScheduleApi
{
    Task<SchedulesAndEmpsMetadata> CreateAndGetScheduleOptionsAsync(ScheduleCreationData data);
}