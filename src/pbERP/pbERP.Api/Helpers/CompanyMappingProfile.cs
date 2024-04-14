using pbERP.Domain.DTOs.CCompany;
using pbERP.Domain.Models.CCompany;
using pbERP.Infrastructure.DataMapping;

namespace pbERP.Api.Helpers;


public static class CompanyMappingProfile
{
   #region CCompACompany
   public static IReadOnlyList<CCompACompanyDto> CompanyEntitiesToDtos(IReadOnlyList<CCompACompany> models)
   {
      IReadOnlyList<CCompACompanyDto> records = GenericDataMapping.EntitiesToDtos<CCompACompany, CCompACompanyDto>(models, CustomMappingAction);
      void CustomMappingAction(CCompACompany entity, CCompACompanyDto dto)
      {
         dto.PoliceStationName = (entity.PoliceStationId != null) ? entity.PoliceStation.PoliceStationName : null;
         dto.BusinessTypeName = (entity.BusinessTypeId != null) ? entity.BusinessType.BusinessTypeName : null;
      }
      return records;
   }

   public static CCompACompanyDto CompanyEntityToDto(CCompACompany model)
   {
      CCompACompanyDto record = GenericDataMapping.EntityToDto<CCompACompany, CCompACompanyDto>(model, CustomMappingAction);
      void CustomMappingAction(CCompACompany entity, CCompACompanyDto dto)
      {
         dto.PoliceStationName = (entity.PoliceStationId != null) ? entity.PoliceStation.PoliceStationName : null;
         dto.BusinessTypeName = (entity.BusinessTypeId != null) ? entity.BusinessType.BusinessTypeName : null;
      }
      return record;
   }
   #endregion CCompACompany

   #region CCompDTransport
   public static IReadOnlyList<CCompDTransportDto> TransportEntitiesToDtos(IReadOnlyList<CCompDTransport> models)
   {
      IReadOnlyList<CCompDTransportDto> records = GenericDataMapping.EntitiesToDtos<CCompDTransport, CCompDTransportDto>(models, CustomMappingAction);
      
      void CustomMappingAction(CCompDTransport entity, CCompDTransportDto dto)
      {
         dto.TransportTypeName = (entity.TransportTypeId != null) ? entity.TransportType.TransportType : null;
      }
      return records;
   }

   public static CCompDTransportDto TrasnportEntityToDto(CCompDTransport model)
   {
      CCompDTransportDto record = GenericDataMapping.EntityToDto<CCompDTransport, CCompDTransportDto>(model, CustomMappingAction);
      void CustomMappingAction(CCompDTransport entity, CCompDTransportDto dto)
      {
         dto.TransportTypeName = (entity.TransportTypeId != null) ? entity.TransportType.TransportType : null;
      }
      return record;
   }
   #endregion CCompACompany
}
