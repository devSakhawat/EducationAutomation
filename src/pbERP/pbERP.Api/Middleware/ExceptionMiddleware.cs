//using Microsoft.EntityFrameworkCore;
//using pbERP.Api.Errors;
//using System.Net;
//using System.Text.Json;

//namespace pbERP.Api.Middleware
//{
//   public class ExceptionMiddleware
//   {
//      private readonly RequestDelegate _next;
//      private readonly IHostEnvironment _env;
//      private readonly ILogger<ExceptionMiddleware> _logger;

//      public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger)
//      {
//         _next = next;
//         _env = env;
//         _logger = logger;
//      }

//      public async Task InvokeAsync(HttpContext context)
//      {
//         try
//         {
//            await _next(context);
//         }
//         catch (Exception ex)
//         {
//            _logger.LogError(ex, ex.Message);
//            context.Response.ContentType = "application/json";
//            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

//            var response = new ApiException((int)HttpStatusCode.InternalServerError);
//            //response.Message = "An error occurred while processing your request.";

//            // Handle specific error messages for known exception types
//            if (ex is DbUpdateException)
//            {
//               response.Message = "An error occurred while saving data to the database.";
//            }
//            else if (ex.InnerException != null)
//            {
//               response.Message = ex.InnerException.InnerException.Message;
//            }
//            else if (_env.IsDevelopment())
//            {
//               response.Message = ex.Message;
//               response.Details = ex.StackTrace;
//            }
//            else
//            {
//               response.Message = "An error occurred while processing your request.";

//               //if (ex is DbUpdateException)
//               //{
//               //   response.Message = "An error occurred while saving data to the database.";
//               //}
//               //else if (ex.InnerException != null)
//               //{
//               //   response.Message = ex.InnerException.InnerException.Message;
//               //}
//               //else if (ex is DataValidationException)
//               //{
//               //   response.Message = "Data validation error occurred.";
//               //   // You can also include additional details or error information from the custom exception here.
//               //}
//               //if (ex is AuthorizationException)
//               //{
//               //   response.Message = "You are not authorized to perform this action.";
//               //   // You can include additional details or error information from the custom exception here.
//               //}
//               //if (ex is NotFoundException)
//               //{
//               //   response.Message = "The requested resource was not found.";
//               //   // You can include additional details or error information from the custom exception here.
//               //}
//               //if (ex is ServiceUnavailableException)
//               //{
//               //   response.Message = "The requested service is currently unavailable. Please try again later.";
//               //   // You can include additional details or error information from the custom exception here.
//               //}

//               // You can add more specific error message handling for other exception types here
//            }

//            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
//            var json = JsonSerializer.Serialize(response, options);
//            await context.Response.WriteAsync(json);
//         }
//      }


//   }
//}


//public class DataValidationException : Exception
//{
//   // Your custom exception implementation
//}

//public class AuthorizationException : Exception
//{
//   // Your custom exception implementation
//}

//public class NotFoundException : Exception
//{
//   // Your custom exception implementation
//}

//public class ServiceUnavailableException : Exception
//{
//   // Your custom exception implementation
//}



//public async Task InvokeAsync(HttpContext context)
//{
//   try
//   {
//      await _next(context);
//   }
//   catch (Exception ex)
//   {
//      _logger.LogError(ex, ex.Message);
//      context.Response.ContentType = "application/json";
//      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

//      var response = new ApiException((int)HttpStatusCode.InternalServerError);

//      if (_env.IsDevelopment())
//      {
//         response.Message = ex.Message;
//         response.Details = ex.StackTrace;
//      }
//      else
//      {
//         response.Message = "An error occurred while processing your request.";

//         // Handle specific error messages for known exception types
//         if (ex is DbUpdateException dbUpdateEx)
//         {
//            // Check if the exception contains validation errors
//            var validationErrors = new List<ValidationResult>();
//            foreach (var entry in dbUpdateEx.Entries)
//            {
//               var entity = entry.Entity;
//               var validationContext = new ValidationContext(entity);
//               var entityValidationResults = new List<ValidationResult>();
//               Validator.TryValidateObject(entity, validationContext, entityValidationResults, true);
//               validationErrors.AddRange(entityValidationResults);
//            }

//            if (validationErrors.Any())
//            {
//               // Build a message with all validation error details
//               var validationErrorMessage = "Validation errors occurred:";
//               foreach (var error in validationErrors)
//               {
//                  validationErrorMessage += $" {error.ErrorMessage};";
//               }

//               response.Message = validationErrorMessage;
//            }
//            else
//            {
//               // Handle other types of DbUpdateException
//               response.Message = "An error occurred while saving data to the database.";
//            }
//         }
//         //else if (ex is SomeCustomException)
//         //{
//         //   response.Message = "A specific custom exception occurred.";
//         //}
//         else if (ex.InnerException != null)
//         {
//            response.Message = ex.InnerException.Message;
//         }
//         // You can add more specific error message handling for other exception types here
//      }

//      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
//      var json = JsonSerializer.Serialize(response, options);
//      await context.Response.WriteAsync(json);
//   }
//}



//public async Task InvokeAsync(HttpContext context)
//{
//   try
//   {
//      await _next(context);
//   }
//   catch (Exception ex)
//   {
//      _logger.LogError(ex, ex.Message);
//      context.Response.ContentType = "application/json";
//      context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

//      var response = new ApiException((int)HttpStatusCode.InternalServerError);

//      if (_env.IsDevelopment())
//      {
//         response.Message = ex.Message;
//         response.Details = ex.StackTrace;
//      }
//      else
//      {
//         response.Message = "An error occurred while processing your request.";

//         // Handle specific error messages for known exception types
//         if (ex is DbUpdateException)
//         {
//            response.Message = "An error occurred while saving data to the database.";
//         }
//         else if (ex is SomeCustomException)
//         {
//            response.Message = "A specific custom exception occurred.";
//         }

//         // You can add more specific error message handling for other exception types here
//      }

//      var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
//      var json = JsonSerializer.Serialize(response, options);
//      await context.Response.WriteAsync(json);
//   }
//}




using Microsoft.EntityFrameworkCore;
using pbERP.Api.Errors;
using System.Net;
using System.Text.Json;

namespace pbERP.Api.Middleware;

public class ExceptionMiddleware
{
   private readonly RequestDelegate _next;
   private IHostEnvironment _env;
   private readonly ILogger<ExceptionMiddleware> _logger;

   public ExceptionMiddleware(RequestDelegate next, IHostEnvironment env, ILogger<ExceptionMiddleware> logger)
   {
      _next = next;
      _env = env;
      _logger = logger;
   }

   public async Task InvokeAsync(HttpContext context)
   {
      try
      {
         await _next(context);
      }
      catch (Exception ex)
      {
         _logger.LogError(ex, ex.Message);
         context.Response.ContentType = "application/json";
         context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

         var response = new ApiException((int)HttpStatusCode.InternalServerError);

         if (ex is DbUpdateException)
         {
            response = ex.InnerException?.InnerException != null
               ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.InnerException.Message, ex.InnerException.InnerException.StackTrace)
               : (ex.InnerException != null
                  ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.Message, ex.InnerException.StackTrace)
                  : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
            );
         }
         else if (_env.IsDevelopment())
         {
            response = ex.InnerException?.InnerException != null 
               ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.InnerException.Message, ex.InnerException.InnerException.StackTrace) 
               : (ex.InnerException != null 
                  ? new ApiException((int)HttpStatusCode.InternalServerError, ex.InnerException.Message, ex.InnerException.StackTrace) 
                  : new ApiException((int)HttpStatusCode.InternalServerError, ex.Message, ex.StackTrace)
            );
         }
         var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
         var json = JsonSerializer.Serialize(response, options);
         await context.Response.WriteAsync(json);
      }
   }
}