using pbERP.Domain.DTOs.AGeneralConfig;
using pbERP.Domain.DTOs.BSecurity;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.DataMapping;

namespace pbERP.Api.Helpers;

public static class GeneralConfigMappingProfile
{
   #region DivisionOrSatate
   public static IReadOnlyList<AGenConfigBDivisionOrStateDto> DivisionEntitiesToDtos(IReadOnlyList<AGenConfigBDivisionOrState> models)
   {
      IReadOnlyList<AGenConfigBDivisionOrStateDto> records = GenericDataMapping.EntitiesToDtos<AGenConfigBDivisionOrState, AGenConfigBDivisionOrStateDto>(models, CustomMappingAction);
      void CustomMappingAction(AGenConfigBDivisionOrState entity, AGenConfigBDivisionOrStateDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.CountryName = (entity.CountryId != null) ? entity.Country.CountryName : null;
      }
      return records;
   }

   public static AGenConfigBDivisionOrStateDto DivisionEntityToDto(AGenConfigBDivisionOrState model)
   {
      AGenConfigBDivisionOrStateDto records = GenericDataMapping.EntityToDto<AGenConfigBDivisionOrState, AGenConfigBDivisionOrStateDto>(model, CustomMappingAction);
      void CustomMappingAction(AGenConfigBDivisionOrState entity, AGenConfigBDivisionOrStateDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.CountryName = entity.Country.CountryName;
      }
      return records;
   }
   #endregion DivisionOrSatate

   #region DistrictOrCity
   public static IReadOnlyList<AGenConfigCDistrictOrCityDto> DistrictEntitiesToDtos(IReadOnlyList<AGenConfigCDistrictOrCity> models)
   {
      IReadOnlyList<AGenConfigCDistrictOrCityDto> records = GenericDataMapping.EntitiesToDtos<AGenConfigCDistrictOrCity, AGenConfigCDistrictOrCityDto>(models, CustomMappingAction);
      void CustomMappingAction(AGenConfigCDistrictOrCity entity, AGenConfigCDistrictOrCityDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.DivisionName = (entity.DivisionId != null) ? entity.Division.DivisionName : null;
      }
      return records;
   }

   public static AGenConfigCDistrictOrCityDto DistrictEntityToDto(AGenConfigCDistrictOrCity model)
   {
      AGenConfigCDistrictOrCityDto records = GenericDataMapping.EntityToDto<AGenConfigCDistrictOrCity, AGenConfigCDistrictOrCityDto>(model, CustomMappingAction);
      void CustomMappingAction(AGenConfigCDistrictOrCity entity, AGenConfigCDistrictOrCityDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.DivisionName = entity.Division.DivisionName;
      }
      return records;
   }
   #endregion DivisionOrCity

   #region PoliceStation
   public static IReadOnlyList<AGenConfigDPoliceStationDto> PSEntitiesToDtos(IReadOnlyList<AGenConfigDPoliceStation> models)
   {
      IReadOnlyList<AGenConfigDPoliceStationDto> records = GenericDataMapping.EntitiesToDtos<AGenConfigDPoliceStation, AGenConfigDPoliceStationDto>(models, CustomMappingAction);
      void CustomMappingAction(AGenConfigDPoliceStation entity, AGenConfigDPoliceStationDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.DistrictName = (entity.DistrictId != null) ? entity.District.DistrictName : null;
      }
      return records;
   }

   public static AGenConfigDPoliceStationDto PSEntityToDto(AGenConfigDPoliceStation model)
   {
      AGenConfigDPoliceStationDto records = GenericDataMapping.EntityToDto<AGenConfigDPoliceStation, AGenConfigDPoliceStationDto>(model, CustomMappingAction);
      void CustomMappingAction(AGenConfigDPoliceStation entity, AGenConfigDPoliceStationDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.DistrictName = entity.District.DistrictName;
      }
      return records;
   }
   #endregion PoliceStation
}
