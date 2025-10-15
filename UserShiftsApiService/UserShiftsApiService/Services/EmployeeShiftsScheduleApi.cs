using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using noadudai.schedule_generator_client.Api;
using noadudai.schedule_generator_client.Client;
using noadudai.schedule_generator_client.Model;

namespace UserShiftsApiService.Services;

public class EmployeeShiftsScheduleApi : IEmployeeShiftsScheduleApi
{
    private readonly DefaultApi _api;
    
    public EmployeeShiftsScheduleApi(HttpClient httpClient,
        ILogger<DefaultApi> logger,
        ILoggerFactory loggerFactory,
        JsonSerializerOptionsProvider jsonOptionsProvider,
        DefaultApiEvents defaultApiEvents,
        IConfiguration configuration)
    {
        httpClient.BaseAddress = new Uri(configuration["EmployeeShiftsScheduleUrl"]);
        _api = new DefaultApi(logger: logger, 
            loggerFactory: loggerFactory, 
            httpClient: httpClient, 
            jsonSerializerOptionsProvider: jsonOptionsProvider, 
            defaultApiEvents: defaultApiEvents);
    }

    public async Task<SchedulesAndEmpsMetadata> CreateAndGetScheduleOptionsAsync(ScheduleCreationData data)
    {
       var response = await _api.CreateAndGetScheduleOptionsCreateAndGetScheduleOptionsPostAsync(data);
       
       if (response.TryOk(out var result))
           return result;
       
       throw new Exception($"Unexpected Error: {response.StatusCode}");
    }
}