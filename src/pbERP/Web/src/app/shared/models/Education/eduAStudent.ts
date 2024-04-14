export interface EduAStudent {
    studentId: number;
    studentCode: string;
    studentName: string;
    studentFathersName?: string;
    studentMothersName?: string;
    genderId?: number;
    bloodGroupId?: number;
    religionId?: number;
    studentPhoto: number[] | null;
    studentImage: File | null;

    genderName?: string;
    bloodGroupName?: string;
    religionName?: string;

    // Present Address
    presentAddressId: number | null;
    presentAddress?: string;
    presentPostOffice?: string;
    presentPoliceStationId?: number;
    presentPoliceStationName?: string;
    presentDistrictId?: number;
    presentDistrictName?: string;
    presentDivisionId?: number;
    presentDivisionName?: string;
    presentCountryId?: number;
    presentCountryName?: string;
    // presentReferenceTypeName?: string;

    // Permanent Address
    permanentAddressId?: number;
    permanentAddress?: string;
    permanentPostOffice?: string;
    permanentPoliceStationId?: number;
    permanentPoliceStationName?: string;
    permanentDistrictId?: number;
    permanentDistrictName?: string;
    permanentDivisionId?: number;
    permanentDivisionName?: string;
    permanentCountryId?: number;
    permanentCountryName?: string;
    // permanentReferenceTypeName?: string;
}


// export interface EduAStudent {
//   studentId: number;
//   studentCode: string;
//   studentName: string;
//   studentFathersName: string;
//   studentMothersName: string;
//   genderId: number;
//   bloodGroupId: number;
//   religionId: number;
//   stuPresAdd: string | null;
//   stuPresAddPsid: number;
//   stuPerAdd: string | null;
//   studPerAddPsid: number;
//   studentPhoto: number[] | null;

//   studentImage: File | null;
//   bloodGroupName: string | null;
//   genderName: string | null;
//   religionName: string | null;
//   stuPresAddPSName: string | null;
//   studPerAddPSName: string | null;
//   // [key: string]: string | number | File | null | number[] | undefined;
// }




// export interface EduAStudent {
//   studentId: number;
//   studentCode: string | null;
//   studentName: string | null;
//   studentFathersName: string | null;
//   studentMothersName: string | null;
//   bloodGroupId: number | null;
//   religionId: number | null;
//   genderId: number | null;
//   stuPresAdd: string | null;
//   stuPresAddPsid: number | null;
//   stuPerAdd: string | null;
//   studPerAddPsid: number | null;
//   studentImage: File | null;
//   // for data show
//   studentPhoto: number[] | null;
//   bloodGroupName: string | null;
//   genderName: string | null;
//   religionName: string | null;
// }