using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.DataMapping;

namespace pbERP.Api.Helpers;

public static class EducationMappingProfile
{
  #region EduAStudent
  public static IReadOnlyList<FEduAStudentDto> StudentEntitiesToDtos(IReadOnlyList<FEduAStudent> models)
  {
    IReadOnlyList<FEduAStudentDto> records = GenericDataMapping.EntitiesToDtos<FEduAStudent, FEduAStudentDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduAStudent entity, FEduAStudentDto dto)
    {
      // Perform custom mapping for non-matching columns here
      dto.BloodGroupName = (entity.BloodGroupId != null) ? entity.BloodGroup.BloodGroupName : null;
      dto.GenderName = (entity.GenderId != null) ? entity.Gender.GenderName : null;
      dto.ReligionName = (entity.ReligionId != null) ? entity.Religion.ReligionName : null;
      //dto.PresentPSName = (entity.StuPresAddPsid != null) ? entity.p.CountryName : null;
    }
    return records;
  }

  public static FEduAStudentDto StudentEntityToDto(FEduAStudent model)
  {
    FEduAStudentDto record = GenericDataMapping.EntityToDto<FEduAStudent, FEduAStudentDto>(model, CustomMappingAction);
    void CustomMappingAction(FEduAStudent entity, FEduAStudentDto dto)
    {
      // Perform custom mapping for non-matching columns here
      dto.BloodGroupName = (entity.BloodGroupId != null) ? entity.BloodGroup.BloodGroupName : null;
      dto.GenderName = (entity.GenderId != null) ? entity.Gender.GenderName : null;
      dto.ReligionName = (entity.ReligionId != null) ? entity.Religion.ReligionName : null;
    }
    return record;
  }
  #endregion EduAStudent

  #region EduBBuilding
  public static IReadOnlyList<FEduBBuildingDto> BuidingEntitiesToDtos(IReadOnlyList<FEduBBuilding> models)
  {
    IReadOnlyList<FEduBBuildingDto> records = GenericDataMapping.EntitiesToDtos<FEduBBuilding, FEduBBuildingDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduBBuilding entity, FEduBBuildingDto dto)
    {
      // Perform custom mapping for non-matching columns here
      dto.CompanyName = (entity.CompanyId != null) ? entity.Company.CompanyName : null;
    }
    return records;
  }

  public static FEduBBuildingDto BuildingEntityToDto(FEduBBuilding model)
  {
    FEduBBuildingDto records = GenericDataMapping.EntityToDto<FEduBBuilding, FEduBBuildingDto>(model, CustomMappingAction);
    void CustomMappingAction(FEduBBuilding entity, FEduBBuildingDto dto)
    {
      // Perform custom mapping for non-matching columns here
      dto.CompanyName = (entity.CompanyId != null) ? entity.Company.CompanyName : null;
    }
    return records;
  }
  #endregion EduBBuilding

  #region EduBClassOrHallRoom
  public static IReadOnlyList<FEduBClassOrHallRoomDto> ClassOrHallRoomEntitiesToDtos(IReadOnlyList<FEduBClassOrHallRoom> models)
  {
    IReadOnlyList<FEduBClassOrHallRoomDto> records = GenericDataMapping.EntitiesToDtos<FEduBClassOrHallRoom, FEduBClassOrHallRoomDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduBClassOrHallRoom entity, FEduBClassOrHallRoomDto dto)
    {
      // Perform custom mapping for non-matching columns here
      dto.BuildingName = (entity.BuildingId != null) ? entity.Building.BuildingName : null;
    }
    return records;
  }

  public static FEduBClassOrHallRoomDto ClassOrHallRoomEntityToDto(FEduBClassOrHallRoom model)
  {
    FEduBClassOrHallRoomDto records = GenericDataMapping.EntityToDto<FEduBClassOrHallRoom, FEduBClassOrHallRoomDto>(model, CustomMappingAction);
    void CustomMappingAction(FEduBClassOrHallRoom entity, FEduBClassOrHallRoomDto dto)
    {
      // Perform custom mapping for non-matching columns here
      dto.BuildingName = (entity.BuildingId != null) ? entity.Building.BuildingName : null;
    }
    return records;
  }
  #endregion EduBClassOrHallRoom

  #region FEduCClassOrHall
  public static IReadOnlyList<FEduCClassOrHallDto> ClassOrHallEntitiesToDtos(IReadOnlyList<FEduCClassOrHall> models)
  {
    IReadOnlyList<FEduCClassOrHallDto> records = GenericDataMapping.EntitiesToDtos<FEduCClassOrHall, FEduCClassOrHallDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduCClassOrHall entity, FEduCClassOrHallDto dto)
    {
      // Perform custom mapping for non-matching columns here
      dto.ClassRoomName = (entity.ClassRoomId != null) ? entity.ClassRoom.ClassRoomName : null;
    }
    return records;
  }

  public static FEduCClassOrHallDto ClassOrHallEntityToDto(FEduCClassOrHall model)
  {
    FEduCClassOrHallDto records = GenericDataMapping.EntityToDto<FEduCClassOrHall, FEduCClassOrHallDto>(model, CustomMappingAction);
    void CustomMappingAction(FEduCClassOrHall entity, FEduCClassOrHallDto dto)
    {
      // Perform custom mapping for non-matching columns here
      dto.ClassRoomName = (entity.ClassRoomId != null) ? entity.ClassRoom.ClassRoomName : null;
    }
    return records;
  }
  #endregion FEduCClassOrHall

  #region FEduFTransportCharge
  public static IReadOnlyList<FEduFTransportChargeDto> TransportChargeEntitiesToDtos(IReadOnlyList<FEduFTransportCharge> models)
  {
    IReadOnlyList<FEduFTransportChargeDto> records = GenericDataMapping.EntitiesToDtos<FEduFTransportCharge, FEduFTransportChargeDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduFTransportCharge entity, FEduFTransportChargeDto dto)
    {
      dto.TransportName = (entity.TransportId != null) ? entity.Transport.TransportName : null;
      dto.TransportAreaName = (entity.TransportArea != null) ? entity.TransportArea.TransportAreaName : null;
    }
    return records;
  }

  public static FEduFTransportChargeDto TransportChargeEntityToDto(FEduFTransportCharge model)
  {
    FEduFTransportChargeDto records = GenericDataMapping.EntityToDto<FEduFTransportCharge, FEduFTransportChargeDto>(model, CustomMappingAction);
    void CustomMappingAction(FEduFTransportCharge entity, FEduFTransportChargeDto dto)
    {
      dto.TransportName = (entity.TransportId != null) ? entity.Transport.TransportName : null;
      dto.TransportAreaName = (entity.TransportArea != null) ? entity.TransportArea.TransportAreaName : null;
    }
    return records;
  }
  #endregion FEduFTransportCharge

  #region FEduDStudentAllocateHallSeat
  public static IReadOnlyList<FEduDStudentAllocateHallSeatDto> StudentSeatEntitiesToDtos(IReadOnlyList<FEduDStudentAllocateHallSeat> models)
  {
    IReadOnlyList<FEduDStudentAllocateHallSeatDto> records = GenericDataMapping.EntitiesToDtos<FEduDStudentAllocateHallSeat, FEduDStudentAllocateHallSeatDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduDStudentAllocateHallSeat entity, FEduDStudentAllocateHallSeatDto dto)
    {
      dto.ClassRoomName = (entity.ClassRoomId != null) ? entity.ClassRoom.ClassRoomName : null;
      dto.HallSeatNumber = (entity.HallSeatId != null) ? entity.HallSeat.HallSeatNumber : null;
    }
    return records;
  }

  public static FEduDStudentAllocateHallSeatDto StudentSeatEntityToDto(FEduDStudentAllocateHallSeat model)
  {
    FEduDStudentAllocateHallSeatDto records = GenericDataMapping.EntityToDto<FEduDStudentAllocateHallSeat, FEduDStudentAllocateHallSeatDto>(model, CustomMappingAction);
    void CustomMappingAction(FEduDStudentAllocateHallSeat entity, FEduDStudentAllocateHallSeatDto dto)
    {
      dto.ClassRoomName = (entity.ClassRoomId != null) ? entity.ClassRoom.ClassRoomName : null;
      dto.HallSeatNumber = (entity.HallSeatId != null) ? entity.HallSeat.HallSeatNumber : null;
    }
    return records;
  }
  #endregion FEduDStudentAllocateHallSeat

  #region FEduGLinkTransportArea
  public static IReadOnlyList<FEduGLinkTransportAreaDto> LinkTransportEntitiesToDtos(IReadOnlyList<FEduGLinkTransportArea> models)
  {
    IReadOnlyList<FEduGLinkTransportAreaDto> records = GenericDataMapping.EntitiesToDtos<FEduGLinkTransportArea, FEduGLinkTransportAreaDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduGLinkTransportArea entity, FEduGLinkTransportAreaDto dto)
    {
      dto.TransportName = (entity.TransportId != null) ? entity.Transport.TransportName : null;
      dto.TransportAreaName = (entity.TransportAreaId != null) ? entity.TransportArea.TransportAreaName : null;
    }
    return records;
  }

  public static FEduGLinkTransportAreaDto LinkTransportEntityToDto(FEduGLinkTransportArea model)
  {
    FEduGLinkTransportAreaDto records = GenericDataMapping.EntityToDto<FEduGLinkTransportArea, FEduGLinkTransportAreaDto>(model, CustomMappingAction);
    void CustomMappingAction(FEduGLinkTransportArea entity, FEduGLinkTransportAreaDto dto)
    {
      dto.TransportName = (entity.TransportId != null) ? entity.Transport.TransportName : null;
      dto.TransportAreaName = (entity.TransportAreaId != null) ? entity.TransportArea.TransportAreaName : null;
    }
    return records;
  }
  #endregion FEduDStudentAllocateHallSeat

  #region FEduNLinkClassGroup
  public static IReadOnlyList<FEduNLinkClassGroupDto> LinkClassGroupesToDtos(IReadOnlyList<FEduNLinkClassGroup> models)
  {
    IReadOnlyList<FEduNLinkClassGroupDto> records = GenericDataMapping.EntitiesToDtos<FEduNLinkClassGroup, FEduNLinkClassGroupDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduNLinkClassGroup entity, FEduNLinkClassGroupDto dto)
    {
      dto.ClassName = (entity.ClassId != null) ? entity.Class.ClassName : null;
      dto.ClassGroupName = (entity.ClassGroupId != null) ? entity.ClassGroup.ClassGroupName : null;
    }
    return records;
  }
  #endregion FEduNLinkClassGroup

  #region FEduOLinkClassSection
  public static IReadOnlyList<FEduOLinkClassSectionDto> LinkClassSectionesToDtos(IReadOnlyList<FEduOLinkClassSection> models)
  {
    IReadOnlyList<FEduOLinkClassSectionDto> records = GenericDataMapping.EntitiesToDtos<FEduOLinkClassSection, FEduOLinkClassSectionDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduOLinkClassSection entity, FEduOLinkClassSectionDto dto)
    {
      dto.ClassName = (entity.ClassId != null) ? entity.Class.ClassName : null;
      dto.ClassSectionName = (entity.ClassSectionId != null) ? entity.ClassSection.ClassSectionName : null;
    }
    return records;
  }
  #endregion FEduOLinkClassSection

  #region FEduPLinkClassShift
  public static IReadOnlyList<FEduPLinkClassShiftDto> LinkClassShiftsToDtos(IReadOnlyList<FEduPLinkClassShift> models)
  {
    IReadOnlyList<FEduPLinkClassShiftDto> records = GenericDataMapping.EntitiesToDtos<FEduPLinkClassShift, FEduPLinkClassShiftDto>(models, CustomMappingAction);
    void CustomMappingAction(FEduPLinkClassShift entity, FEduPLinkClassShiftDto dto)
    {
      dto.ClassName = (entity.ClassId != null) ? entity.Class.ClassName : null;
      dto.ClassShiftName = (entity.ClassShiftId != null) ? entity.ClassShift.ClassShiftName : null;
    }
    return records;
  }
   #endregion FEduPLinkClassShift

  #region FEduQLinkClassSubject
public static IReadOnlyList<FEduQLinkClassSubjectDto> LinkClassShiftsToDtos(IReadOnlyList<FEduQLinkClassSubject> models)
{
   IReadOnlyList<FEduQLinkClassSubjectDto> records = GenericDataMapping.EntitiesToDtos<FEduQLinkClassSubject, FEduQLinkClassSubjectDto>(models, CustomMappingAction);
   void CustomMappingAction(FEduQLinkClassSubject entity, FEduQLinkClassSubjectDto dto)
   {
      dto.ClassName = (entity.ClassId != null) ? entity.Class.ClassName : null;
      dto.ClassGroupName = (entity.ClassGroupId != null) ? entity.ClassGroup.ClassGroupName : null;
      dto.ClassSubjectName = (entity.ClassSubjectId != null) ? entity.ClassSubject.ClassSubjectName : null;
   }
   return records;
}
#endregion FEduQLinkClassSubject

  #region FEduHStudentAllocateTransport
public static IReadOnlyList<FEduHStudentAllocateTransportDto> AllocateTransportEntitiesToDtos(IReadOnlyList<FEduHStudentAllocateTransport> models)
{
   IReadOnlyList<FEduHStudentAllocateTransportDto> records = GenericDataMapping.EntitiesToDtos<FEduHStudentAllocateTransport, FEduHStudentAllocateTransportDto>(models, CustomMappingAction);
   void CustomMappingAction(FEduHStudentAllocateTransport entity, FEduHStudentAllocateTransportDto dto)
   {
   dto.TransportName = (entity.TransportId != null) ? entity.Transport.TransportName : null;
   dto.TransportAreaName = (entity.TransportAreaId != null) ? entity.TransportArea.TransportAreaName : null;
   }
   return records;
}

public static FEduHStudentAllocateTransportDto AllocateTransportEntityToDto(FEduHStudentAllocateTransport model)
{
   FEduHStudentAllocateTransportDto records = GenericDataMapping.EntityToDto<FEduHStudentAllocateTransport, FEduHStudentAllocateTransportDto>(model, CustomMappingAction);
   void CustomMappingAction(FEduHStudentAllocateTransport entity, FEduHStudentAllocateTransportDto dto)
   {
   dto.TransportName = (entity.TransportId != null) ? entity.Transport.TransportName : null;
   dto.TransportAreaName = (entity.TransportAreaId != null) ? entity.TransportArea.TransportAreaName : null;
   }
   return records;
}
#endregion FEduHStudentAllocateTransport

}
