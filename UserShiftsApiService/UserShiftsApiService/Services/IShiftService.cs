using System.Threading.Tasks;
using UserShiftsApiService.Models;

namespace UserShiftsApiService.Services;

public interface IShiftService
{
    Task CreateNewShiftAsync(ShiftModel shiftModel);
}