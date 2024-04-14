namespace pbERP.Utilities.Constant
{
   public static class MessageConstants
   {
      public const string DuplicateError = " allready exist!";

      public const string GenericError = "Something went wrong, please try after sometimes! If you are experiencing similar frequently, please report it to helpdesk.";

      public const string NoRecordError = "No record found!";

      public const string InvalidParameterError = "Invalid paramenter(s)!";

      public const string NoMatchFoundError = "No match found!";

      public const string UnauthorizedAttemptOfRecordUpdateError = "Unauthorized attempt of updating record!";

      public const string UnauthorizedAttemptOfRecordDeleteError = "Unauthorized attempt of deleting record!";

      public const string UnauthorizedAttemptOfRecordInsert = "Unauthorized attempt of inserting record!";

      public const string DependencyError = "This record cannot be deleted. It is already in use.";

      public static string ExceptionError = "";


      public const string RecordSaved = "Record successfully saved";

      public const string RecordUpdated = "Record updated successfully";

      public const string RecordDeleted = "Record successfully deleted";

      public const string SaveFailed = "Record save failed";

      public const string UpdateFailed = "Record updated failed";

      public const string DeleteFailed = "Record deleted failed";

      public const string IfDeleteReffereceRecord = "Cannot be deleted because there are refference related records.";

      public const string IfInvalidUserPassword = "Invalid Username or password";

      public const string ModelStateInvalid = "Model state invalid. Fillup all the field value.";


      public const string MappingFailed = "Mapping Failed!";
   }
}
