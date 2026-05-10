using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace ExtensionsLibrary;

/// <summary>
/// Provides extension methods for working with HTTP status codes.
/// </summary>
/// <remarks>
/// From
/// https://www.telerik.com/blogs/extension-properties-csharp-14-game-changing-feature-cleaner-code
/// </remarks>
public static class HttpStatusCodeExtensions
{
    extension(HttpStatusCode)
    {
        public static HttpStatusCode OkStatus => HttpStatusCode.OK;
        public static HttpStatusCode NotFoundStatus => HttpStatusCode.NotFound;
        public static HttpStatusCode BadRequestStatus => HttpStatusCode.BadRequest;

        public static HttpStatusCode FromInt(int code) => (HttpStatusCode)code;
    }

    extension(HttpStatusCode status)
    {
        public bool IsSuccess => (int)status >= 200 && (int)status < 300;
        public bool IsRedirect => (int)status >= 300 && (int)status < 400;
        public bool IsClientError => (int)status >= 400 && (int)status < 500;
        public bool IsServerError => (int)status >= 500 && (int)status < 600;

        public string Category => status switch
        {
            _ when status.IsSuccess => "Success",
            _ when status.IsRedirect => "Redirect",
            _ when status.IsClientError => "Client Error",
            _ when status.IsServerError => "Server Error",
            _ => "Unknown"
        };
    }
}
