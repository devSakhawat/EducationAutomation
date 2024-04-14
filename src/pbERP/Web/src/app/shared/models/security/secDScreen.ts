export interface SecDScreen
{
  screenId : number;  
  moduleId : number | null;
  parentId : number | null;
  screenName : string;
  controllerName : string | null;
  actionName : string | null;
  moduleName : string | null;
  parentName : string | null;
}