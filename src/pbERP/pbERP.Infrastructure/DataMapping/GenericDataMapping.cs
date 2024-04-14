using System.Linq.Expressions;

namespace pbERP.Infrastructure.DataMapping;

public static class GenericDataMapping
{
   public static TEntity DtoToEntity<TEntity, TDto>(TDto dto) where TEntity : class, new() where TDto : class
   {
      // Step 1: Get the properties of the DTO and Entity types
      var dtoProperties = typeof(TDto).GetProperties();
      var entitiesProperties = typeof(TEntity).GetProperties();

      // Step 2: Create parameter expressions for the Entity and DTO
      var entityParam = Expression.Parameter(typeof(TEntity), "entity");
      var dtoParam = Expression.Parameter(typeof(TDto), "dto");

      // Step 3: Build a list of property assignments between DTO and Entity
      var propertyAssignments = entitiesProperties
          .Where(entityProp => dtoProperties.Any(dtoProp => dtoProp.Name.Equals(entityProp.Name, StringComparison.OrdinalIgnoreCase)))
          .Select(entityProp =>
          {
             var dtoProp = dtoProperties.FirstOrDefault(prop => prop.Name.Equals(entityProp.Name, StringComparison.OrdinalIgnoreCase));
             var dtoPropValue = Expression.Property(dtoParam, dtoProp);
             var entityPropValue = Expression.Convert(dtoPropValue, entityProp.PropertyType);

             var entityMemberBinding = Expression.Bind(entityProp, entityPropValue);
             return entityMemberBinding;
          }).ToList();

      // Step 4: Create a MemberInit expression to construct the Entity
      var entityMemberInit = Expression.MemberInit(Expression.New(typeof(TEntity)), propertyAssignments);

      // Step 5: Compile the expression to create a mapping function
      var mapFunction = Expression.Lambda<Func<TDto, TEntity>>(entityMemberInit, dtoParam).Compile();

      // Step 6: Call the mapping function with the provided DTO and return the Entity
      var entity = mapFunction(dto);
      return entity;
   }

   //public static TDto EntityToDto<TEntity, TDto>(TEntity entity) where TDto : class, new() where TEntity : class
   public static TDto EntityToDto<TEntity, TDto>(TEntity entity, Action<TEntity, TDto> customMapping = null) where TEntity : class where TDto : class, new()
   {
      // Step 1: Get the properties of the DTO and Entity types
      var dtoProperties = typeof(TDto).GetProperties();
      var entitiesProperties = typeof(TEntity).GetProperties();

      // Step 2: Create parameter expressions for the Entity and DTO
      var dtoParam = Expression.Parameter(typeof(TDto), "dto");
      var entityParam = Expression.Parameter(typeof(TEntity), "entity");

      // Step 3: Build a list of property assignments between Entity and  DTO
      var propertyAssignments = dtoProperties
          .Where(dtoProp => entitiesProperties.Any(entityProp => entityProp.Name.Equals(dtoProp.Name, StringComparison.OrdinalIgnoreCase)))
          .Select(dtoProp =>
          {
             var entityProp = entitiesProperties.FirstOrDefault(prop => prop.Name.Equals(dtoProp.Name, StringComparison.OrdinalIgnoreCase));

             var entityPropValue = Expression.Property(entityParam, entityProp);
             var dtoPropValue = Expression.Convert(entityPropValue, dtoProp.PropertyType);

             var dtoMemberBinding = Expression.Bind(dtoProp, dtoPropValue);
             return dtoMemberBinding;
          }).ToList();

      // Step 4: Create a MemberInit expression to construct the Entity
      var dtoMemberInit = Expression.MemberInit(Expression.New(typeof(TDto)), propertyAssignments);

      // Step 5: Compile the expression to create a mapping function
      var mapFunction = Expression.Lambda<Func<TEntity, TDto>>(dtoMemberInit, entityParam).Compile();

      // Step 6: Call the mapping function with the provided DTO and return the Entity
      var dto = mapFunction(entity);

      // Call custom mapping delegate to handle non-matching columns
      customMapping?.Invoke(entity, dto);

      return dto;
   }
   
   //public static TDto EntityToDto<TEntity, TDto>(TEntity entity) where TDto : class, new() where TEntity : class
   //{
   //   // Step 1: Get the properties of the DTO and Entity types
   //   var dtoProperties = typeof(TDto).GetProperties();
   //   var entitiesProperties = typeof(TEntity).GetProperties();

   //   // Step 2: Create parameter expressions for the Entity and DTO
   //   var dtoParam = Expression.Parameter(typeof(TDto), "dto");
   //   var entityParam = Expression.Parameter(typeof(TEntity), "entity");

   //   // Step 3: Build a list of property assignments between Entity and  DTO
   //   var propertyAssignments = dtoProperties
   //       .Where(dtoProp => entitiesProperties.Any(entityProp => entityProp.Name.Equals(dtoProp.Name, StringComparison.OrdinalIgnoreCase)))
   //       .Select(dtoProp =>
   //       {
   //          var entityProp = entitiesProperties.FirstOrDefault(prop => prop.Name.Equals(dtoProp.Name, StringComparison.OrdinalIgnoreCase));
   //          var entityPropValue = Expression.Property(entityParam, entityProp);

   //          var dtoPropValue = Expression.Convert(entityPropValue, dtoProp.PropertyType);
   //          var dtoMemberBinding = Expression.Bind(dtoProp, dtoPropValue);
   //          return dtoMemberBinding;
   //       }).ToList();

   //   // Step 4: Create a MemberInit expression to construct the Entity
   //   var dtoMemberInit = Expression.MemberInit(Expression.New(typeof(TDto)), propertyAssignments);

   //   // Step 5: Compile the expression to create a mapping function
   //   var mapFunction = Expression.Lambda<Func<TEntity, TDto>>(dtoMemberInit, entityParam).Compile();

   //   // Step 6: Call the mapping function with the provided DTO and return the Entity
   //   var dto = mapFunction(entity);
   //   return dto;
   //}

   public static IReadOnlyList<TDto> EntitiesToDtos<TEntity, TDto>( IReadOnlyList<TEntity> entityList, Action<TEntity, TDto> customMapping = null ) where TEntity : class where TDto : class, new()
   {
      var dtoList = new List<TDto>(entityList.Count);

      var entityProperties = typeof(TEntity).GetProperties();
      var dtoProperties = typeof(TDto).GetProperties();

      var entityParam = Expression.Parameter(typeof(TEntity), "entity");
      var dtoParam = Expression.Parameter(typeof(TDto), "dto");

      var propertyAssignments = dtoProperties
          .Where(dtoProp => entityProperties.Any(entityProp =>
              entityProp.Name.Equals(dtoProp.Name, StringComparison.OrdinalIgnoreCase)))
          .Select(dtoProp =>
          {
             var entityProp = entityProperties.FirstOrDefault(ep =>
                   ep.Name.Equals(dtoProp.Name, StringComparison.OrdinalIgnoreCase));

             var entityPropValue = Expression.Property(entityParam, entityProp);
             var dtoPropValue = Expression.Convert(entityPropValue, dtoProp.PropertyType);

             var dtoMemberBinding = Expression.Bind(dtoProp, dtoPropValue);

             return dtoMemberBinding;
          })
          .ToList();

      var dtoMemberInit = Expression.MemberInit(Expression.New(typeof(TDto)), propertyAssignments);

      var mapFn = Expression.Lambda<Func<TEntity, TDto>>(dtoMemberInit, entityParam).Compile();

      foreach (var entity in entityList)
      {
         var dto = mapFn(entity);
         // Call custom mapping delegate to handle non-matching columns
         customMapping?.Invoke(entity, dto);
         dtoList.Add(dto);
      }

      return dtoList;
   }

  //   public static IReadOnlyList<TDto> EntitiesToDtos<TEntity, TDto>(IReadOnlyList<TEntity> entityList) where TEntity : class where TDto : class, new()
  //   {
  //      var entityProperties = typeof(TEntity).GetProperties();
  //      var dtoProperties = typeof(TDto).GetProperties();

  //      var propertyMappings = entityProperties.Where(entityProp => dtoProperties.Any(dtoProp =>
  //              dtoProp.Name.Equals(entityProp.Name, StringComparison.OrdinalIgnoreCase)))
  //          .Select(entityProp => new
  //          {
  //             EntityProperty = entityProp,
  //             DtoProperty = dtoProperties.FirstOrDefault(dtoProp =>
  //               dtoProp.Name.Equals(entityProp.Name, StringComparison.OrdinalIgnoreCase))
  //          })
  //          .Where(mapping => mapping.DtoProperty != null)
  //          .ToList();

  //      var dtoList = entityList.Select(entity =>
  //      {
  //         var dto = new TDto();

  //         propertyMappings.ForEach(mapping =>
  //         {
  //            var entityPropValue = mapping.EntityProperty.GetValue(entity);
  //            mapping.DtoProperty.SetValue(dto, entityPropValue);
  //         });

  //         return dto;
  //      }).ToList();

  //      return dtoList;
  //   }

  //public static IReadOnlyList<TDto> EntitiesToDtos<TEntity, TDto>(IReadOnlyList<TEntity> entityList) where TEntity : class where TDto : class, new()
  //{
  //   var dtoList = new List<TDto>(entityList.Count);

  //   var entityProperties = typeof(TEntity).GetProperties();
  //   var dtoProperties = typeof(TDto).GetProperties();

  //   var entityParam = Expression.Parameter(typeof(TEntity), "entity");
  //   var dtoParam = Expression.Parameter(typeof(TDto), "dto");

  //   var propertyAssignments = dtoProperties
  //       .Where(dtoProp => entityProperties.Any(entityProp =>
  //           entityProp.Name.Equals(dtoProp.Name, StringComparison.OrdinalIgnoreCase)))
  //       .Select(dtoProp =>
  //       {
  //          var entityProp = entityProperties.FirstOrDefault(ep =>
  //            ep.Name.Equals(dtoProp.Name, StringComparison.OrdinalIgnoreCase));

  //          var entityPropValue = Expression.Property(entityParam, entityProp);
  //          var dtoPropValue = Expression.Convert(entityPropValue, dtoProp.PropertyType);

  //          var dtoMemberBinding = Expression.Bind(dtoProp, dtoPropValue);

  //          return dtoMemberBinding;
  //       })
  //       .ToList();

  //   var dtoMemberInit = Expression.MemberInit( Expression.New(typeof(TDto)), propertyAssignments);

  //   var mapFn = Expression.Lambda<Func<TEntity, TDto>>(dtoMemberInit, entityParam).Compile();

  //   foreach (var entity in entityList)
  //   {
  //      var dto = mapFn(entity);
  //      dtoList.Add(dto);
  //   }

  //   return dtoList;
  //}

  // replace entity
  //public static TEntity ReplaceEntityValuesWithDto<TEntity, TDto>(TEntity entity, TDto dto) where TEntity : class where TDto : class
  //{
  //   var dtoProperties = typeof(TDto).GetProperties();
  //   var entityProperties = typeof(TEntity).GetProperties();

  //   foreach (var entityProp in entityProperties)
  //   {
  //      var correspondingDtoProp = dtoProperties.FirstOrDefault(prop =>
  //          prop.Name.Equals(entityProp.Name, StringComparison.OrdinalIgnoreCase));

  //      if (correspondingDtoProp != null)
  //      {
  //         var dtoPropValue = correspondingDtoProp.GetValue(dto);
  //         entityProp.SetValue(entity, dtoPropValue);
  //      }
  //   }
  //   return entity;
  //}

  // expression tree approch
  public static TEntity ReplaceEntityWithDto<TEntity, TDto>(TEntity entity, TDto dto)
    where TEntity : class
    where TDto : class
  {
    var dtoProperties = typeof(TDto).GetProperties();
    var entityProperties = typeof(TEntity).GetProperties();

    var entityParam = Expression.Parameter(typeof(TEntity), "entity");
    var dtoParam = Expression.Parameter(typeof(TDto), "dto");

    var propertyAssignments = entityProperties
        .Where(entityProp => dtoProperties.Any(dtoProp =>
            dtoProp.Name.Equals(entityProp.Name, StringComparison.OrdinalIgnoreCase)))
        .Select(entityProp =>
        {
          var dtoProp = dtoProperties.FirstOrDefault(prop =>
              prop.Name.Equals(entityProp.Name, StringComparison.OrdinalIgnoreCase));

          var dtoPropValue = Expression.Property(dtoParam, dtoProp);
          var entityPropValue = Expression.Convert(dtoPropValue, entityProp.PropertyType);

          return Expression.Assign(Expression.Property(entityParam, entityProp), entityPropValue);
        }).ToList();

    var updateExpression = Expression.Block(propertyAssignments);

    var lambda = Expression.Lambda<Action<TEntity, TDto>>(updateExpression, entityParam, dtoParam);

    var updateAction = lambda.Compile();
    updateAction(entity, dto);

    return entity;
  }

}