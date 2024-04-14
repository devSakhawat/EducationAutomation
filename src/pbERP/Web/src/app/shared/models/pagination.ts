
// export interface Pagination {
//   pageIndex: number
//   pageSize: number
//   count: number
//   data: EduABuildingInfoToReturn[]
// }

export interface Pagination<T> {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: T;
}
