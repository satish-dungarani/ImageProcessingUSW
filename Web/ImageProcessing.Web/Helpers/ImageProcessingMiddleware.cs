using ImageProcessing.Services;
using IPinfo;
using IPinfo.Models;

namespace ImageProcessing.Web.Helpers
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ImageProcessingMiddleware
    {
        private readonly RequestDelegate _next;
        //private readonly IUserService _userService;

        public ImageProcessingMiddleware(RequestDelegate next)
        {
            _next = next;
            //_userService = userService;
        }

        public Task Invoke(HttpContext httpContext, IUserService _userService)
        {
            // Get the user's IP address
            string ipAddress = httpContext.Connection.RemoteIpAddress.ToString();

            // Request URL
            string url = httpContext.Request.Path;

            // Get the user's location (if available)
            //string location = "";
            string location = httpContext.Session.GetString("IPAddress") == ipAddress ? httpContext.Session.GetString("Location") : GetLocationFromIpAddress(ipAddress);

            // Get the unique session key
            string sessionKey = httpContext.Session.Id;

            // Get the request datetime
            var requestDateTime = DateTime.Now;

            // Parse the user agent string to get browser information
            var userAgent = httpContext.Request.Headers["User-Agent"].FirstOrDefault();

            _userService.InsertRequestsAuditAsync(new Data.Entities.RequestsAudit()
            {
                IpAddress = ipAddress,
                Location = location,
                Requested_DateTime = requestDateTime,
                Requested_Url = url,
                Unique_Session_Key = sessionKey,
                User_Agent = userAgent
            });


            // Log the information
            string logMessage = $"[{requestDateTime}] - {ipAddress} - {url} - {location} - {sessionKey} - {userAgent}";
            LogHelper.logger.Info(logMessage);

            httpContext.Session.SetString("IPAddress", ipAddress);
            httpContext.Session.SetString("Location", location);

            // Call the next middleware in the pipeline
            return _next(httpContext);
        }
        private string GetLocationFromIpAddress(string ipAddress)
        {
            string token = "61a00a2cc6205b";
            IPinfoClient client = new IPinfoClient.Builder()
                .AccessToken(token)
                .Build();
            //ipAddress = "216.239.36.21";
            IPResponse ipResponse = client.IPApi.GetDetailsAsync(ipAddress).GetAwaiter().GetResult();
            return ipResponse.Country == null ? string.Empty : $"[{ipResponse.Country}] - {ipResponse.CountryName} - {ipResponse.City} - {ipResponse.Postal}";
        }

    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ImageProcessingMiddlewareExtensions
    {
        public static IApplicationBuilder UseImageProcessingMiddleware(this IApplicationBuilder builder, IUserService _userService)
        {
            return builder.UseMiddleware<ImageProcessingMiddleware>();
        }
    }


}
 
