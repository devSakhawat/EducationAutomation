using pbERP.Domain.DTOs.BSecurity;
using pbERP.Domain.Models.BSecurity;

namespace pbERP.Infrastructure.DataMapping;

public static class MappingSecurityModule
{
   // Using basic mapping
   public static BSecAUserGroup DtoToEntity(BSecAUserGroupDto model)
   {
      var record = new BSecAUserGroup
      {
         UserGroupId = model.UserGroupId,
         UserGroupName = model.UserGroupName,
         UserGroupNameLocal = model.UserGroupNameLocal,
         UserGroupSaleDiscount = model.UserGroupSaleDiscount,
         UserGroupDescription = model.UserGroupDescription,
         UserGroupDescriptionLocal = model.UserGroupDescriptionLocal
      };
      return record;
   }

   // List using basic mapping
   public static IReadOnlyList<BSecAUserGroupDto> EntitiesToDtos(IReadOnlyList<BSecAUserGroup> models)
   {
      IReadOnlyList<BSecAUserGroupDto> records = models.Select(u => new BSecAUserGroupDto
      {
         UserGroupId = u.UserGroupId,
         UserGroupName = u.UserGroupName,
         UserGroupNameLocal = u.UserGroupNameLocal,
         UserGroupDescription = u.UserGroupDescription,
         UserGroupDescriptionLocal = u.UserGroupDescriptionLocal,
         UserGroupSaleDiscount = u.UserGroupSaleDiscount
      }).ToList();
      return records;
   }

   // reflection-based mapping
   public static BSecAUserGroup SecAUserGroupDtoToEntity(BSecAUserGroupDto model)
   {
      var record = new BSecAUserGroup();
      var entityProperties = typeof(BSecAUserGroup).GetProperties();
      var dtoProperties = typeof(BSecAUserGroupDto).GetProperties();

      foreach (var entityProp in entityProperties)
      {
         var correspondingDtoProp = dtoProperties.FirstOrDefault(p => p.Name.Equals(entityProp.Name, StringComparison.OrdinalIgnoreCase));

         if (correspondingDtoProp != null)
         {
            var entityPropValue = entityProp.GetValue(model);
            correspondingDtoProp.SetValue(record, entityPropValue);
         }
      }
      return record;
   }

   // Reflection-based mapping List
   public static IReadOnlyList<BSecAUserGroupDto> SecAUserGroupsToDtos(IReadOnlyList<BSecAUserGroup> models)
   {
      var records = new List<BSecAUserGroupDto>(models.Count);

      var entityProperties = typeof(BSecAUserGroup).GetProperties();
      var dtoProperties = typeof(BSecAUserGroupDto).GetProperties();

      foreach (var entity in models)
      {
         var record = new BSecAUserGroupDto();

         foreach (var enttiyProp in entityProperties)
         {
            var correspondingDtoProp = dtoProperties.FirstOrDefault(p => p.Name.Equals(enttiyProp.Name, StringComparison.OrdinalIgnoreCase));

            if (correspondingDtoProp != null)
            {
               var entityPropValue = enttiyProp.GetValue(entity);
               correspondingDtoProp.SetValue(record, entityPropValue);
            }
         }
         records.Add(record);
      }
      return records;
   }

   // Generic Data Mapping
   public static List<TDto> MapEntityListToDtoList<TEntity, TDto>(List<TEntity> entityList) where TEntity : class where TDto : class, new()
   {
      var dtoList = new List<TDto>(entityList.Count);

      var entityProperties = typeof(TEntity).GetProperties();
      var dtoProperties = typeof(TDto).GetProperties();

      foreach (var entity in entityList)
      {
         var dto = new TDto();

         foreach (var entityProp in entityProperties)
         {
            var correspondingDtoProp = dtoProperties.FirstOrDefault(prop =>
                prop.Name.Equals(entityProp.Name, StringComparison.OrdinalIgnoreCase));

            if (correspondingDtoProp != null)
            {
               var entityPropValue = entityProp.GetValue(entity);
               correspondingDtoProp.SetValue(dto, entityPropValue);
            }
         }

         dtoList.Add(dto);
      }

      return dtoList;
   }


   // secvuser
   public static BSecBUser DtoToSecBUser(BSecBUserDto model)
   {
      var record = new BSecBUser
      {
         UserId = model.UserId,
         LoginName = model.LoginName,
         LoginNameLocal = model.LoginNameLocal,
         UserGroupId = model.UserGroupId,
         Password = model.Password,
         StartDate = model.StartDate,
         EndDate = model.EndDate
      };
      return record;
   }


}
