using AutoMapper;
using pbERP.Domain.DTOs.AGeneralConfig;
using pbERP.Domain.DTOs.Education;
using pbERP.Domain.DTOs.BSecurity;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Domain.Models.BSecurity;
using pbERP.Domain.Models.FEducation;
using pbERP.Domain.DTOs.FEducation;

namespace pbERP.Api.Helpers;

public class MappingProfiles : Profile
{
   public MappingProfiles()
   {
      //CreateMap<EduBBuilding, EduBBuildingDto>()
      //   .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.CompanyNameEnglish)).ReverseMap()
      //   .ForPath(s => s.Company.CompanyNameEnglish, opt => opt.MapFrom(src => src.CompanyName));
      CreateMap<FEduBBuilding, FEduBBuildingDto>()
         .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company != null ? src.Company.CompanyName : null))
         .ReverseMap()
         .ForMember(dest => dest.Company, opt => opt.Ignore())
         .ForMember(dest => dest.CompanyId, opt => opt.Ignore());

      //CreateMap<EduAStudent, EduAStudentDto>().ForMember(dest => dest.StudentPhoto, opt => opt.MapFrom(src => src.StudentPhoto)).ReverseMap();

      //CreateMap<EduAStudent, EduAStudentDto>().ForMember(dest => dest.StudentImage, opt => opt.MapFrom(src => src.StudentPhoto));

      //CreateMap<EduAStudentDto, EduAStudent>()
      //      /*.ForMember(dest => dest.StudentPhoto, opt => opt.Ignore())*/ // Ignore the StudentPhoto property during mapping
      //      .ForMember(dest => dest.StudentPhoto, opt => opt.MapFrom(src => src.StudentPhoto)); // Ignore the StudentPhoto property during mapping

      CreateMap<FEduAStudent, FEduAStudentDto>();
      CreateMap<FEduAStudentDto, FEduAStudent>();

      #region AGenConfigF Module

      #region AGenConfigFBloodGroup
      CreateMap<AGenConfigFBloodGroup, AGenConfigFBloodGroupDto>();
      CreateMap<AGenConfigFBloodGroupDto, AGenConfigFBloodGroup>();
      #endregion AGenConfigFBloodGroup


      #endregion AGenConfigF Module

      #region Security Module

      #region BSecAUserGroup
      CreateMap<BSecAUserGroup, BSecAUserGroupDto>();
      CreateMap<BSecAUserGroupDto, BSecAUserGroup>();
      #endregion BSecAUserGroup

      #endregion Security Module


      //.ForMember(d => d.CustomerName, opt => opt.MapFrom(src => src.Customer.Name))
      //.ReverseMap()
      //.ForPath(s => s.Customer.Name, opt => opt.MapFrom(src => src.CustomerName));

      //CreateMap<SoftConfigJCompanyLinkModule, SoftConfigJCompanyLinkModuleDto>()
      //   .ForMember(slm => slm.CompanyName, com => com.MapFrom(src => src.Company.CompanyNameEnglish))
      //   .ForMember(slm => slm.ModuleName, mod => mod.MapFrom(src => src.Module.ModuleName));

      //CreateMap<SoftConfigJCompanyLinkModule, MainMenuDto>()
      //   .ForMember(scclm => scclm.ModuleName, m => m.MapFrom(src => src.Module.ModuleName));

      //CreateMap<SecDScreen, SubMenuDto>()
      //   .ForMember(slm => slm.ModuleName, mod => mod.MapFrom(src => src.Module.ModuleName));
   }
}
