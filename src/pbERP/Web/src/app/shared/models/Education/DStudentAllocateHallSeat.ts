export interface DStudentAllocateHallSeat{
  allocateStudentHallSeatId : number;
  studentId : number;
  classRoomId : number;
  hallSeatId : number;
  studentName : string;
  classRoomName : string;
  hallSeatNumber : number;

     //public virtual FEduBClassOrHallRoom ClassRoom { get; set; }

   //public virtual FEduCClassOrHall HallSeat { get; set; }

   //public virtual FEduAStudent Student { get; set; }
}