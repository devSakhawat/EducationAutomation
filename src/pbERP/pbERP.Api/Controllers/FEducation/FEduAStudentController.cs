using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using pbERP.Api.Errors;
using pbERP.Api.Helpers;
using pbERP.Domain.DTOs;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts;
using pbERP.Infrastructure.Services;
using pbERP.Infrastructure.Specifications;
using pbERP.Infrastructure.Specifications.Education;
using pbERP.Utilities.Constant;

namespace pbERP.Api.Controllers.FEducation;

public class FEduAStudentController : BaseApiController
{
   private readonly IUnitOfWork context;

   //private readonly ImageProcessingService imageProcess;
   private readonly IMapper mapper;

   public FEduAStudentController(IUnitOfWork _context, IMapper mapper)
   {
      context = _context;
      //imageProcess = _imageProcess;
      this.mapper = mapper;
   }

   #region CreateEduAStudent

   [Route(RouteConstant.CreateEduAStudent)]
   [HttpPost]
   [ProducesResponseType(typeof(FEduAStudentDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> CreateEduAStudent([FromForm] FEduAStudentDto model)

   {
      if (model == null || model.StudentId != 0 || !ModelState.IsValid) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
      if (await context.EduAStudent.IsExists(x => x.StudentCode == model.StudentCode)) return Conflict(new ApiResponse(409, "StudentCode: " + model.StudentCode + MessageConstants.DuplicateError));

      TransectionModel result = new TransectionModel();
      // Process the uploaded image
      if (model.StudentImage != null && model.StudentImage.Length > 0)
      {
         // Check if the file is an image
         if (!ImageProcessingService.IsSupportedImageFile(model.StudentImage)) return BadRequest(new ApiResponse(400, "Invalid file format. Only JPG, GIF, PNG, and JPEG formats are allowed."));

         model.StudentPhoto = await ImageProcessingService.ProcessAndConvertImageAsync(model.StudentImage);

         result = await context.EduAStudent.InsertFEduAStudent(model);
      }
      else
      {
         result = await context.EduAStudent.InsertFEduAStudent(model);
      }
      return (result.StatusCode == 400) ? BadRequest(new ApiResponse(400, result.Message)) : Ok();
   }

   //private ImageCodecInfo GetEncoder(IFormFile file)
   //{
   //   var fileExtension = Path.GetExtension(file.FileName);
   //   var encoderParams = ImageCodecInfo.GetImageEncoders();
   //   return encoderParams.FirstOrDefault(x => x.FilenameExtension.Contains(fileExtension));
   //}

   //public async Task<IActionResult> CreateStudent(FEduAStudentDto model)
   //{
   //   if (model.StudentId != 0 || model == null) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

   //   model.StudentId = await context.FEduAStudent.GetNextId("StudentId");

   //   FEduAStudent eduAStudent = mapper.Map<FEduAStudent>(model);
   //   context.FEduAStudent.AddAsync(eduAStudent);

   //   var saveChange = await context.SaveChangesAsync();

   //   if (saveChange <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
   //   return Ok();
   //}

   #endregion CreateEduAStudent

   #region Read_EduAStudents

   [Route(RouteConstant.ReadEduAStudents)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduAStudentDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadEduAStudents([FromQuery] SpecificationParams specParams)
   {
      var specStudent = new FEduAStudentSpecification(specParams);
      IReadOnlyList<FEduAStudent> students = await context.EduAStudent.ListAsyncWithSpec(specStudent);
      return (students.Count() <= 0) ? Ok(new List<FEduAStudentDto>()) : Ok(EducationMappingProfile.StudentEntitiesToDtos(students));
   }
   #endregion Read_EduAStudents

   #region Read_EduAStudentByKey

   [Route(RouteConstant.ReadEduAStudentByKey)]
   [HttpGet]
   [ProducesResponseType(typeof(FEduAStudentDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> ReadEduAStudentByKey(int key)
   {
      if (key == 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      FEduAStudentDto student = await context.EduAStudent.GetStudentWithAddress(key);
      if (student is null) return NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError));

      return (student is null) ? BadRequest(new ApiResponse(404, MessageConstants.NoRecordError)) : Ok(student);
   }

   //public async Task<IActionResult> ReadEduAStudentByKey(int key)
   //{
   //   if (key == 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
   //   FEduAStudent student = await context.FEduAStudent.GetByKeyAsync(key);
   //   if (student is null) return NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError));

   //   return Ok(student);
   //}

   #endregion Read_EduAStudentByKey

   #region UpdateEduAStudent

   [Route(RouteConstant.UpdateEduAStudent)]
   [HttpPut]
   [ProducesResponseType(typeof(FEduAStudentDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> UpdateEduAStudent(long key, [FromForm] FEduAStudentDto model)
   {
      if (key != model.StudentId) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));
      if (await context.EduAStudent.IsExists(x => x.StudentCode == model.StudentCode)) return Conflict(new ApiResponse(409, MessageConstants.DuplicateError));

      TransectionModel result = new TransectionModel();
      // Process the uploaded image
      if (model.StudentImage != null && model.StudentImage.Length > 0)
      {
         // Check if the file is an image
         if (!ImageProcessingService.IsSupportedImageFile(model.StudentImage)) return BadRequest(new ApiResponse(400, "Invalid file format. Only JPG, GIF, PNG, and JPEG formats are allowed."));

         model.StudentPhoto = await ImageProcessingService.ProcessAndConvertImageAsync(model.StudentImage);

         result = await context.EduAStudent.UpdateFEduAStudent(model);
      }
      else
      {
         result = await context.EduAStudent.UpdateFEduAStudent(model);
      }
      return (result.StatusCode == 400) ? BadRequest(new ApiResponse(400, result.Message)) : Ok();
   }

   //public async Task<IActionResult> UpdateEduAStudent(long key, FEduAStudentDto model)
   //{
   //   if (key == 0 && model.StudentId == 0 && model.StudentId != key)
   //      return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));

   //   //FEduAStudent record = await context.EduAStudent.GetFirstOrDefaultAsync(s => s.StudentId == model.StudentId);
   //   var spec = new FEduAStudentSpecification(model.StudentId);
   //   FEduAStudent record = await context.EduAStudent.GetByKeyWithSpec(spec);

   //   if (record == null)
   //      return NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError));
   //   // Update the record using AutoMapper
   //   mapper.Map(model, record);
   //   context.EduAStudent.UpdateEntity(record);
   //   var saveChange = await context.SaveChangesAsync();

   //   if (saveChange <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UpdateFailed));
   //   return Ok();
   //}

   #endregion UpdateEduAStudent

   #region DeleteEduAStudent

   [Route(RouteConstant.DeleteEduAStudent)]
   [HttpDelete]
   [ProducesResponseType(typeof(FEduAStudentDto), StatusCodes.Status200OK)]
   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
   public async Task<IActionResult> DeleteEduAStudent(long key)
   {
      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
      TransectionModel result = new TransectionModel();
      result = await context.EduAStudent.DeleteFEduAStudent(key);
      return (result.StatusCode == 400) ? BadRequest(new ApiResponse(400, result.Message)) : Ok();
   }

   #endregion DeleteEduAStudent
}