using System.Reflection;

namespace pbERP.Infrastructure.DataMapping;

public static class HelperService
{
   public static bool AnyNavigationPropertyHasRecords<TEntity>(TEntity entity)
    where TEntity : class
   {
      var entityType = typeof(TEntity);
      var entityProperties = entityType.GetProperties();

      foreach (var property in entityProperties)
      {
         if (IsNavigationProperty(property))
         {
            var navigationValue = property.GetValue(entity);
            if (navigationValue is ICollection<object> collection && collection.Any())
            {
               return true;
            }
         }
      }

      return false;
   }

   private static bool IsNavigationProperty(PropertyInfo property)
   {
      return property.PropertyType.IsGenericType &&
             property.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>);
   }
}
