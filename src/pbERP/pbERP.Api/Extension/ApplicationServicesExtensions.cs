using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.DataStructure;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.Constracts.FEducation;
using pbERP.Infrastructure.Repositories;
using pbERP.Infrastructure.Repositories.Education;

namespace pbERP.Api.Extension
{
    public static class ApplicationServicesExtensions
   {
      public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
      {
         services.AddEndpointsApiExplorer();
         services.AddSwaggerGen();
         services.AddScoped<IUnitOfWork, UnitOfWork>();
         services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); ;
         services.AddAutoMapper(typeof(MappingProfiles));
         services.AddDbContext<pbERPContext>(op => op.UseSqlServer(config.GetConnectionString("DbLocation")));


         services.Configure<ApiBehaviorOptions>(options =>
         {
            options.InvalidModelStateResponseFactory = actionContext =>
               {
                  var errors = actionContext.ModelState
                     .Where(e => e.Value.Errors.Count() > 0)
                     .SelectMany(x => x.Value.Errors)
                     .Select(y => y.ErrorMessage).ToArray();

                  var errorResponse = new ApiValidationErrorResponse
                  {
                     Errors = errors
                  };
                  return new BadRequestObjectResult(errorResponse);
               };
         });

         services.AddCors(option =>
         {
            option.AddPolicy("CorsPolicy", policy =>
               {
                  //policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("http://localhost:4200/");
                  policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
               });
         });
         return services;
      }
   }
}