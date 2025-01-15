using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Configuration;

namespace UserShiftsApiService.ActionFilters;

public class RequireHmacSignatureFilter : IAsyncAuthorizationFilter
{
    private readonly IConfiguration _configuration;

    public RequireHmacSignatureFilter(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    private static bool CryptographicEquals(string a, string b)
    {
        if (a.Length != b.Length) return false;

        var result = 0;
        for (var i = 0; i < a.Length; i++)
            result |= a[i] ^ b[i];

        return result == 0;
    }
    
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var request = context.HttpContext.Request;
        

        var requestSignature = context.HttpContext.Request.Headers["X-Signature"];
        request.EnableBuffering();

        using (var reader = new StreamReader(request.Body, Encoding.UTF8, leaveOpen: true))
        {
            string expectedSignature;
            var rawBody = await reader.ReadToEndAsync();
            request.Body.Position = 0;
            
            using (var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(_configuration["Auth0:HMAC_SECRET"])))
            {
                byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(rawBody));
                expectedSignature = BitConverter.ToString(hash).Replace("-", "").ToLower();
            }

            if (!CryptographicEquals(requestSignature, expectedSignature))
            {
                throw new UnauthorizedAccessException();
            }
        }

    }
}