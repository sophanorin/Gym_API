using System;
using Microsoft.EntityFrameworkCore;

namespace Gym_API.Common.Middleware.Exception
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;

		public ExceptionMiddleware(RequestDelegate next)
		{
			this._next = next;
		}

        public async Task InvokeAsync(HttpContext httpContext)
        {
            httpContext.Response.ContentType = "application/json";
            string message = "";
            int statusCode = httpContext.Response.StatusCode;

            try
            {

                await _next(httpContext);

            }
            catch (DbUpdateException ex)
            {
                message = ex.InnerException?.Message;
                statusCode = 500;

                httpContext.Response.StatusCode = statusCode;

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = statusCode,
                    Message = message
                }.ToString());
            }
            catch (BadHttpRequestException ex)
            {
                message = ex.Message;
                statusCode = ex.StatusCode;

                httpContext.Response.StatusCode = statusCode;

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = statusCode,
                    Message = message
                }.ToString());

            }
            catch(UnauthorizedAccessException ex)
            {
                message = ex.Message;
                statusCode = 401;

                httpContext.Response.StatusCode = statusCode;

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = statusCode,
                    Message = message
                }.ToString());

            }
            catch(InvalidOperationException ex)
            {
                message = ex.Message;
                statusCode = 500;

                httpContext.Response.StatusCode = statusCode;

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = statusCode,
                    Message = message
                }.ToString());

            }
            catch(HttpRequestException ex)
            {
                message = ex.Message;
                statusCode = (int)ex.StatusCode;

                httpContext.Response.StatusCode = statusCode;

                await httpContext.Response.WriteAsync(new ErrorDetails()
                {
                    StatusCode = statusCode,
                    Message = message
                }.ToString());
            }
            
        }
    }
}

