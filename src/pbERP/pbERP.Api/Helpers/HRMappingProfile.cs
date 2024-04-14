using pbERP.Domain.DTOs.DHR;
using pbERP.Domain.Models.DHR;
using pbERP.Infrastructure.DataMapping;

namespace pbERP.Api.Helpers;

public class HRMappingProfile
{
   #region DHrLPresentAddress
   public static IReadOnlyList<DHrLPresentAddressDto> DHrLPresentAddressEntitiesToDtos(IReadOnlyList<DHrLPresentAddress> models)
   {
      IReadOnlyList<DHrLPresentAddressDto> records = GenericDataMapping.EntitiesToDtos<DHrLPresentAddress, DHrLPresentAddressDto>(models, CustomMappingAction);
      void CustomMappingAction(DHrLPresentAddress entity, DHrLPresentAddressDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.PoliceStationName = (entity.PoliceStationId != null) ? entity.PoliceStation.PoliceStationName : null;
         dto.ReferenceTypeName = (entity.ReferenceTypeId != null) ? entity.ReferenceType.ReferenceTypeName : null;
      }
      return records;
   }

   public static DHrLPresentAddressDto DHrLPresentAddressEntityToDto(DHrLPresentAddress model)
   {
      DHrLPresentAddressDto records = GenericDataMapping.EntityToDto<DHrLPresentAddress, DHrLPresentAddressDto>(model, CustomMappingAction);
      void CustomMappingAction(DHrLPresentAddress entity, DHrLPresentAddressDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.PoliceStationName = (entity.PoliceStationId != null) ? entity.PoliceStation.PoliceStationName : null;
         dto.ReferenceTypeName = (entity.ReferenceTypeId != null) ? entity.ReferenceType.ReferenceTypeName : null;
      }
      return records;
   }
   #endregion DHrLPresentAddress


   #region DHrMPermanentAddress
   public static IReadOnlyList<DHrMPermanentAddressDto> DHrMPermanentAddressEntitiesToDtos(IReadOnlyList<DHrMPermanentAddress> models)
   {
      IReadOnlyList<DHrMPermanentAddressDto> records = GenericDataMapping.EntitiesToDtos<DHrMPermanentAddress, DHrMPermanentAddressDto>(models, CustomMappingAction);
      void CustomMappingAction(DHrMPermanentAddress entity, DHrMPermanentAddressDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.PoliceStationName = (entity.PoliceStationId != null) ? entity.PoliceStation.PoliceStationName : null;
         dto.ReferenceTypeName = (entity.ReferenceTypeId != null) ? entity.ReferenceType.ReferenceTypeName : null;
      }
      return records;
   }

   public static DHrMPermanentAddressDto DHrMPermanentAddressEntityToDto(DHrMPermanentAddress model)
   {
      DHrMPermanentAddressDto records = GenericDataMapping.EntityToDto<DHrMPermanentAddress, DHrMPermanentAddressDto>(model, CustomMappingAction);
      void CustomMappingAction(DHrMPermanentAddress entity, DHrMPermanentAddressDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.PoliceStationName = (entity.PoliceStationId != null) ? entity.PoliceStation.PoliceStationName : null;
         dto.ReferenceTypeName = (entity.ReferenceTypeId != null) ? entity.ReferenceType.ReferenceTypeName : null;
      }
      return records;
   }
   #endregion DHrMPermanentAddress


}
