//using AutoMapper;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using pbERP.Api.Errors;
//using pbERP.Domain.DTOs.Education;
//using pbERP.Domain.Models.FEducation;
//using pbERP.Infrastructure.Constracts;
//using pbERP.Infrastructure.Specifications;
//using pbERP.Utilities.Constant;
//using SixLabors.ImageSharp.Formats;
//using SixLabors.ImageSharp.Formats.Gif;
//using SixLabors.ImageSharp.Formats.Jpeg;
//using SixLabors.ImageSharp.Formats.Png;
////using Image = SixLabors.ImageSharp.Image;

//namespace pbERP.Api.Controllers.Education;

//public class EduAStudentController : BaseApiController
//{
//   private readonly IUnitOfWork context;
//   private readonly IMapper mapper;

//   public EduAStudentController(IUnitOfWork context, IMapper mapper)
//   {
//      this.context = context;
//      this.mapper = mapper;
//   }
//   #region CreateEduAStudent
//   [Route(RouteConstant.CreateEduAStudent)]
//   [HttpPost]
//   [ProducesResponseType(typeof(EduAStudentDto), StatusCodes.Status200OK)]
//   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//   //public async Task<IActionResult> CreateStudent([FromForm] EduAStudentDto model)
//   public async Task<IActionResult> CreateStudent([FromForm] EduAStudentDto model)
//   {
//      if (model == null || !ModelState.IsValid)
//      {
//         return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));
//      }

//      // Assign a unique identifier to the student
//      model.StudentId = await context.EduAStudent.GetNextId("StudentId");

//      // Process the uploaded image
//      if (model.StudentImage != null && model.StudentImage.Length > 0)
//      {
//         // Check if the file is an image
//         if (!IsSupportedImageFile(model.StudentImage))
//         {
//            return BadRequest(new ApiResponse(400, "Invalid file format. Only JPG, GIF, PNG, and JPEG formats are allowed."));
//         }

//         // Check if the file size is within the limit
//         //if (model.StudentImage.Length > 1 * 1024 * 1024)
//         //{
//         //   return BadRequest(new ApiResponse(400, "File size exceeds the limit of 1MB."));
//         //}

//         // Compress the image to 50kb
//         using (var compressedStream = new MemoryStream())
//         {
//            CompressImage(model.StudentImage, compressedStream);
//            model.StudentPhoto = compressedStream.ToArray();
//         }
//      }

//      // Map the DTO to the entity
//      FEduAStudent eduAStudent = mapper.Map<FEduAStudent>(model);

//      // Add the entity to the context
//      context.EduAStudent.AddAsync(eduAStudent);

//      // Save changes to the database
//      var saveChange = await context.SaveChangesAsync();

//      if (saveChange <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));

//      return Ok();
//   }

//   private bool IsSupportedImageFile(IFormFile file)
//   {
//      string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
//      string fileExtension = Path.GetExtension(file.FileName);
//      return allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase);
//   }

//   private void CompressImage(IFormFile file, Stream outputStream)
//   {
//      using (var image = Image.Load(file.OpenReadStream()))
//      {
//         // Determine the appropriate encoder based on the file format
//         IImageEncoder encoder;

//         switch (file.ContentType)
//         {
//            case "image/jpeg":
//               encoder = new JpegEncoder { Quality = 50 };
//               break;
//            case "image/png":
//               encoder = new PngEncoder { CompressionLevel = PngCompressionLevel.BestCompression };
//               break;
//            case "image/gif":
//               encoder = new GifEncoder { ColorTableMode = GifColorTableMode.Local };
//               break;
//            default:
//               throw new NotSupportedException("Unsupported image format");
//         }

//         // Save the compressed image using the specified encoder
//         image.Save(outputStream, encoder);
//      }
//   }


//   //private ImageCodecInfo GetEncoder(IFormFile file)
//   //{
//   //   var fileExtension = Path.GetExtension(file.FileName);
//   //   var encoderParams = ImageCodecInfo.GetImageEncoders();
//   //   return encoderParams.FirstOrDefault(x => x.FilenameExtension.Contains(fileExtension));
//   //}

//   //public async Task<IActionResult> CreateStudent(EduAStudentDto model)
//   //{
//   //   if (model.StudentId != 0 || model == null) return BadRequest(new ApiResponse(400, MessageConstants.ModelStateInvalid));

//   //   model.StudentId = await context.FEduAStudent.GetNextId("StudentId");

//   //   FEduAStudent eduAStudent = mapper.Map<FEduAStudent>(model);
//   //   context.FEduAStudent.AddAsync(eduAStudent);

//   //   var saveChange = await context.SaveChangesAsync();

//   //   if (saveChange <= 0) return BadRequest(new ApiResponse(400, MessageConstants.SaveFailed));
//   //   return Ok();
//   //}
//   #endregion CreateEduAStudent

//   #region Read_EduAStudents
//   [Route(RouteConstant.ReadEduAStudents)]
//   [HttpGet]
//   [ProducesResponseType(typeof(EduAStudentDto), StatusCodes.Status200OK)]
//   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//   public async Task<IActionResult> ReadEduAStudents()
//   {
//      IReadOnlyList<FEduAStudent> students = await context.EduAStudent.ListAsync(x => x.StudentId);
//      if (students.Count() == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));

//      var records = mapper.Map<IReadOnlyList<EduAStudentDto>>(students);

//      //IReadOnlyList<EduAStudentDto> dtoList = GenericDataMapping.EntitiesToDtos<FEduAStudent, EduAStudentDto>(students);

//      return Ok(records);
//   }

//   //private IFormFile ConvertByteArrayToFormFile(byte[] studentPhoto, string imageName)
//   //{
//   //   if (studentPhoto == null || studentPhoto.Length == 0)
//   //      return null;

//   //   // Create a memory stream from the byte[] data
//   //   var memoryStream = new MemoryStream(studentPhoto);

//   //   // Create a new FormFile from the memory stream
//   //   var formFile = new FormFile(
//   //       baseStream: memoryStream,
//   //       baseStreamOffset: 0,
//   //       length: studentPhoto.Length,
//   //       name: imageName,
//   //       fileName: imageName + ".jpg")
//   //   {
//   //      ContentType = "image/jpeg" // Set the appropriate content type
//   //   };

//   //   return formFile;
//   //}

//   //public async Task<IActionResult> ReadEduAStudents([FromQuery] SpecificationParams studentParams)
//   //{
//   //   var spec = new EduAStudentSpecification(studentParams);
//   //   var countSpec = new EduAStudentsWithFiltersForCountSpecification(studentParams);

//   //   var students = await context.FEduAStudent.ListAsyncWithSpec(spec);
//   //   var totalItems = await context.FEduAStudent.CountAsync(countSpec);

//   //   if (students.Count() == 0) return NotFound(new ApiResponse(404, MessageConstants.NoRecordError));

//   //   var data = mapper.Map<IReadOnlyList<EduAStudentDto>>(students);
//   //   return Ok(new Pagination<EduAStudentDto>(studentParams.PageIndex,
//   //       studentParams.PageSize, totalItems, data));
//   //}
//   #endregion

//   #region Read_EduAStudentByKey
//   [Route(RouteConstant.ReadEduAStudentByKey)]
//   [HttpGet]
//   [ProducesResponseType(typeof(FEduAStudent), StatusCodes.Status200OK)]
//   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

//   public async Task<IActionResult> ReadEduAStudentByKey(int key)
//   {
//      if (key == 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));

//      var spec = new FEduAStudentSpecification(key);
//      FEduAStudent student = await context.EduAStudent.GetByKeyWithSpec(spec);
//      if (student is null) return NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError));

//      return Ok(mapper.Map<FEduAStudent, EduAStudentDto>(student));
//   }
//   //public async Task<IActionResult> ReadEduAStudentByKey(int key)
//   //{
//   //   if (key == 0) return BadRequest(new ApiResponse(400, MessageConstants.InvalidParameterError));
//   //   FEduAStudent student = await context.FEduAStudent.GetByKeyAsync(key);
//   //   if (student is null) return NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError));

//   //   return Ok(student);
//   //}
//   #endregion Read_EduAStudentByKey

//   #region UpdateEduAStudent
//   [Route(RouteConstant.UpdateEduAStudent)]
//   [HttpPut]
//   [ProducesResponseType(typeof(EduAStudentDto), StatusCodes.Status200OK)]
//   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//   public async Task<IActionResult> UpdateStudent(long key, EduAStudentDto model)
//   {
//      if (key == 0 && model.StudentId == 0 && model.StudentId != key)
//         return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError));

//      //FEduAStudent record = await context.EduAStudent.GetFirstOrDefaultAsync(s => s.StudentId == model.StudentId);
//      var spec = new FEduAStudentSpecification(model.StudentId);
//      FEduAStudent record = await context.EduAStudent.GetByKeyWithSpec(spec);

//      if (record == null)
//         return NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError));

//      // Update the record using AutoMapper
//      mapper.Map(model, record);

//      context.EduAStudent.UpdateEntity(record);
//      var saveChange = await context.SaveChangesAsync();

//      if (saveChange <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UpdateFailed));
//      return Ok();
//   }
//   #endregion UpdateEduAStudent

//   #region DeleteEduAStudent
//   [Route(RouteConstant.DeleteEduAStudent)]
//   [HttpDelete]
//   [ProducesResponseType(typeof(EduAStudentDto), StatusCodes.Status200OK)]
//   [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
//   public async Task<IActionResult> DeleteStudent(long key)
//   {
//      if (key <= 0) return BadRequest(new ApiResponse(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError));

//      //FEduAStudent record = await context.FEduAStudent.GetFirstOrDefaultAsync(s => s.StudentId == model.StudentId);
//      var spec = new FEduAStudentSpecification(key);
//      FEduAStudent record = await context.EduAStudent.GetByKeyWithSpec(spec);

//      if (record == null)
//         return NotFound(new ApiResponse(404, MessageConstants.NoMatchFoundError));

//      context.EduAStudent.DeleteEntity(record);
//      var saveChange = await context.SaveChangesAsync();

//      if (saveChange <= 0) return BadRequest(new ApiResponse(400, MessageConstants.DeleteFailed));
//      return Ok();
//   }
//   #endregion DeleteEduAStudent
//}
