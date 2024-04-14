using pbERP.Domain.DTOs;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;

namespace pbERP.Infrastructure.Constracts.FEducation;

public interface IFEduAStudentRepository : IGenericRepository<FEduAStudent>
{
  Task<TransectionModel> InsertFEduAStudent(FEduAStudentDto model);

  Task<FEduAStudentDto> GetStudentWithAddress(long key);

  Task<TransectionModel> UpdateFEduAStudent(FEduAStudentDto model);

  Task<TransectionModel> DeleteFEduAStudent(long key);
}