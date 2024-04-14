
export interface User {
  userId: number;
  loginName: string;
  loginNameLocal: string;
  password: string;
  userGroupId: number | null;
  startDate: Date | null;
  endDate: Date | null;
  userGroupName: string | null;
}
