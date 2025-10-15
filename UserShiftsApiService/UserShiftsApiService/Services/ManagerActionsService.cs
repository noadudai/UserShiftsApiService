using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using UserShiftsApiService.Entities;
using UserShiftsApiService.Models;
using UserShiftsApiService.UserContext;

using noadudai.schedule_generator_client.Api;
using noadudai.schedule_generator_client.Model;

namespace UserShiftsApiService.Services;

public class ManagerActionsService : IManagerActionsService
{
    private readonly ShiftsSchedulingContext _dbContext;
    private readonly IUserContextProvider _userContextProvider;
    private readonly IEmployeeShiftsScheduleApi _employeeShiftsScheduleApi;

    
    public ManagerActionsService(ShiftsSchedulingContext dbContext, IUserContextProvider userContextProvider, IEmployeeShiftsScheduleApi employeeShiftsScheduleApi)
    {
        _dbContext = dbContext;
        _userContextProvider = userContextProvider;
        _employeeShiftsScheduleApi = employeeShiftsScheduleApi;
    }
    
    public async Task CreateNewShiftScheduleAsync(CreateNewShiftsScheduleModel shiftsSchedule)
    {
        var newSchedule = new ScheduleEntity
        {
            Id = Guid.NewGuid().ToString(),
            CreatedByManagerId = _userContextProvider.GetUserContext().UserId,
            CreationDate = DateTime.UtcNow,
            Status = shiftsSchedule.Status,
        };

        var shiftEntities = shiftsSchedule.Shifts.Select(shift => new ShiftEntity
        {
            Id = Guid.NewGuid().ToString(),
            ShiftType = shift.ShiftType,
            StartDate = shift.ShiftStartTime,
            EndDate = shift.ShiftEndTime,
            ScheduleId = newSchedule.Id
        });
        
        _dbContext.ShiftsSchedules.Add(newSchedule);
        _dbContext.Shifts.AddRange(shiftEntities);
        
        await _dbContext.SaveChangesAsync();
    }

    public async Task<SchedulesResponseModel> GetSchedulesAsync(ScheduleFetchingModel scheduleFetchingModel)
    {
        var schedulesQuery = _dbContext.ShiftsSchedules.GroupJoin(
            _dbContext.Shifts, 
            schedule => schedule.Id,
            shifts => shifts.ScheduleId,
            (schedule, shifts) => new 
            {
                Schedule = schedule,
                Shifts = shifts.ToList(),
            });

        if (scheduleFetchingModel.CreationTimeOrder == Order.Ascending)
        {
            schedulesQuery = schedulesQuery.OrderBy(g => g.Schedule.CreationDate);
        }
        if (scheduleFetchingModel.CreationTimeOrder == Order.Descending)
        {
            schedulesQuery = schedulesQuery.OrderByDescending(g => g.Schedule.CreationDate);
        }

        var schedules = await schedulesQuery.ToListAsync();
        
        var response = schedules.Select(
            group => new ScheduleResponseModel
            {
                Schedule = new ScheduleModel
                {
                    Id = group.Schedule.Id,
                    CreationDate = group.Schedule.CreationDate,
                    CreatedByManagerId = group.Schedule.CreatedByManagerId,
                    Status = group.Schedule.Status
                }, 
                Shifts = group.Shifts.Select
                    (
                        shift => new ShiftModel
                        {
                            ShiftStartTime = shift.StartDate, 
                            ShiftEndTime = shift.EndDate, 
                            ShiftType = shift.ShiftType
                        }).ToArray()
            }).ToArray();
        
        return new SchedulesResponseModel { Schedules = response };
    }

    public async Task ChangeShiftScheduleStatusAsync(ChangeShiftsScheduleStatusModel schedule)
    {
        var scheduleToChangeStatus = await _dbContext.ShiftsSchedules.SingleAsync(s => s.Id == schedule.ScheduleId);
        scheduleToChangeStatus.Status = schedule.Status;
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateNewWorkingScheduleAsync(CreateScheduleModel newSchedule)
    {        
        var employees = await _dbContext.Users.ToListAsync();
        
        var employeesForCreateSchedule = employees.Select(employee =>
            new EmployeeApiModel(
                name: employee.Email, 
                employeeId: employee.Id, 
                position: EmployeePositionApiEnum.FULLTIMER, 
                employeeStatus:EmployeeStatusApiEnum.MIDLEVELEMPLOYEE, 
                priority:EmployeePriorityApiEnum.HIGHEST, 
                shiftTypesTrainedToDo: new List<ShiftTypesApiEnum>
                {
                    ShiftTypesApiEnum.MORNING
                })).ToList();
        
        var shiftsForNextSchedule = await _dbContext.Shifts.Where(shift => shift.ScheduleId == newSchedule.ShiftsScheduleId).ToListAsync();
        
        var shiftsForCreateSchedule = shiftsForNextSchedule.Select(shift => 
            new Shift(
                shiftType: shift.GetScheduleGeneratorClientShiftType(), 
                startTime: shift.StartDate, 
                endTime: shift.EndDate, 
                shiftId: shift.Id)).ToList();
        
        var scheduleRequest = new ScheduleCreationData(
            employees: employeesForCreateSchedule,
            shifts: shiftsForCreateSchedule, 
            numberOfSchedules: 5);
        
        var scheduleOptions = await _employeeShiftsScheduleApi.CreateAndGetScheduleOptionsAsync(scheduleRequest);
    }
}
