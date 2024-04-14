namespace pbERP.Domain.DTOs;

//public class TranjectionModel
//{
//    public int? Success200 { get; set; }
//    public int? BadRequest400 { get; set; }
//    public int? NotFound404 { get; set; }
//    public int? Conflict409 { get; set; }
//    public string Message { get; set; }
//    //public object EntityModel { get; set; }
//}

public class TransectionModel
{
    public TransectionModel(){}
    public TransectionModel(int statusCode, string message = null)
   {
      StatusCode = statusCode;
      Message = message ?? GetDefaultMessageForStatusCode(statusCode);
   }

   public int StatusCode { get; set; }
   public string Message { get; set; }

   private string GetDefaultMessageForStatusCode(int statusCode)
   {
      return statusCode switch
      {
         200 => "OK",
         400 => "A bad requet, you have made!",
         401 => "Authorized, you are not!",
         404 => "Resource found, it was not!",
         409 => "Duplicate data found!",
         500 => "Internal server error!",
         _ => null
      };
   }
}
