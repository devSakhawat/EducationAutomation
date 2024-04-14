using pbERP.Domain.DTOs.BSecurity;
using pbERP.Domain.Models.BSecurity;
using pbERP.Infrastructure.DataMapping;
using System.Linq;

namespace pbERP.Api.Helpers;

public static class SecurityMappingProfile
{
   #region BSecELinkUserGroupScreen
   public static IReadOnlyList<BSecELinkUserGroupScreenDto> UserGroupScreenEntitiesToDtos(IReadOnlyList<BSecELinkUserGroupScreen> models)
   {
      IReadOnlyList<BSecELinkUserGroupScreenDto> records = GenericDataMapping.EntitiesToDtos<BSecELinkUserGroupScreen, BSecELinkUserGroupScreenDto>(models, CustomMappingAction);
      void CustomMappingAction(BSecELinkUserGroupScreen entity, BSecELinkUserGroupScreenDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.UserGroupName = (entity.UserGroupId != null) ? entity.UserGroup.UserGroupName : null;
         dto.ScreenName = (entity.ScreenId != null) ? entity.Screen.ScreenName : null;
      }
      return records;
   }

   public static BSecELinkUserGroupScreenDto UserGroupScreenEntityToDto(BSecELinkUserGroupScreen model)
   {
      BSecELinkUserGroupScreenDto records = GenericDataMapping.EntityToDto<BSecELinkUserGroupScreen, BSecELinkUserGroupScreenDto>(model, CustomMappingAction);
      void CustomMappingAction(BSecELinkUserGroupScreen entity, BSecELinkUserGroupScreenDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.UserGroupName = entity.UserGroup.UserGroupName;
         dto.ScreenName = entity.Screen.ScreenName;
      }
      return records;
   }
   #endregion BSecELinkUserGroupScreen

   #region BSecBUser
   public static IReadOnlyList<BSecBUserDto> UserEntitiesToDtos(IReadOnlyList<BSecBUser> models)
   {
      IReadOnlyList<BSecBUserDto> records = GenericDataMapping.EntitiesToDtos<BSecBUser, BSecBUserDto>(models, CustomMappingAction);
      void CustomMappingAction(BSecBUser entity, BSecBUserDto dto)
      {
         // Perform custom mapping for non-matching columns here
         dto.UserGroupName = entity.UserGroup.UserGroupName;
      }
      return records;
   }

   public static BSecBUserDto UserEntityToDto(BSecBUser models)
   {
      BSecBUserDto record = GenericDataMapping.EntityToDto<BSecBUser, BSecBUserDto>(models, CustomMappingAction);
      void CustomMappingAction(BSecBUser entity, BSecBUserDto dto)
      {
         dto.UserGroupName = entity.UserGroup.UserGroupName;
      }
      return record;
   }
   #endregion BSecBUser

   #region BSecDScreen
   public static IReadOnlyList<BSecDScreenDto> ScreenEntitiesToDtos(IReadOnlyList<BSecDScreen> models)
   {
      IReadOnlyList<BSecDScreenDto> records = GenericDataMapping.EntitiesToDtos<BSecDScreen, BSecDScreenDto>(models, CustomMappingAction);

      void CustomMappingAction(BSecDScreen entity, BSecDScreenDto dto)
      {
         dto.ModuleName = entity.Module != null ? entity.Module.ModuleName : null;
         dto.ParentName = entity.ParentId != null ? models.FirstOrDefault(s => s.ScreenId == entity.ParentId)?.ScreenName: null;
      }
      return records;
   }


   public static BSecDScreenDto ScreenEntityToDto(BSecDScreen models)
   {
      BSecDScreenDto record = GenericDataMapping.EntityToDto<BSecDScreen, BSecDScreenDto>(models, CustomMappingAction);
      void CustomMappingAction(BSecDScreen entity, BSecDScreenDto dto)
      {
         dto.ModuleName = entity.Module != null ? entity.Module.ModuleName : null;
      }
      return record;
   }
   #endregion BSecDScreen

}
