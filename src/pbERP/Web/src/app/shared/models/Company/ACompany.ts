export interface ACompany{
  companyId: number;
  groupOfCompanyName: string;
  companyCode : string;
  companyName : string;
  companyAddress : string;
  policeStationId? : number;
  policeStationName? : string;
  companyPhone? : string;
  companyFax? : string;
  companyWhatsApp? : string;
  companyEmailAddress? : string;
  companyWebAddress? : string;
  companyTin? : string;
  companyBin? : string;
  companyEin? : string;
  companyVatRegistration? : string;
  businessTypeId? : number;
  businessTypeName? : string;
  companyLogo? : number[];
  companyLogoImage? : File;
  companyBackgroundImage? : number[];
  companyBackgroundImageIFormFIle? : File;
}