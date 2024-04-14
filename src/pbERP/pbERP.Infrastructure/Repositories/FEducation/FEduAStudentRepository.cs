using Microsoft.EntityFrameworkCore;
using pbERP.DataStructure;
using pbERP.Domain.DTOs;
using pbERP.Domain.DTOs.FEducation;
using pbERP.Domain.Enums;
using pbERP.Domain.Models.AGeneralConfig;
using pbERP.Domain.Models.DHR;
using pbERP.Domain.Models.FEducation;
using pbERP.Infrastructure.Constracts.FEducation;
using pbERP.Utilities.Constant;

namespace pbERP.Infrastructure.Repositories.FEducation;

public class FEduAStudentRepository : GenericRepository<FEduAStudent>, IFEduAStudentRepository
{
  public FEduAStudentRepository(pbERPContext _context) : base(_context)
  {
  }

  //public async Task<int> InsertFEduAStudent(FEduAStudentDto model)
  //{
  //   if (model == null) return await Task.FromResult(0);
  //   int rowAffect = 0;

  //   model.StudentId = await GetNextId("StudentId");

  //   using var transaction = await context.Database.BeginTransactionAsync();
  //   try
  //   {
  //      FEduAStudent student = new FEduAStudent
  //      {
  //         StudentId = model.StudentId,
  //         StudentCode = model.StudentCode,
  //         StudentName = model.StudentName,
  //         StudentFathersName = model.StudentFathersName,
  //         StudentMothersName = model.StudentMothersName,
  //         GenderId = model.GenderId,
  //         BloodGroupId = model.BloodGroupId,
  //         ReligionId = model.ReligionId,
  //         StudentPhoto = model.StudentPhoto
  //      };
  //      await context.FEduAStudents.AddAsync(student);
  //      await context.SaveChangesAsync();

  //      DHrLPresentAddress presentAddress = new DHrLPresentAddress
  //      {
  //         PresentAddressId = model.PresentAddressId,
  //         PresentAddress = model.PresentAddress,
  //         PostOffice = model.PresentPostOffice,
  //         PoliceStationId = model.PresentPoliceStationId,
  //         ReferenceTypeId = (long)RefferenceTypeEnum.Student,
  //         ReferenceId = model.StudentId
  //      };
  //      await context.DHrLPresentAddresses.AddAsync(presentAddress);
  //      await context.SaveChangesAsync();

  //      DHrMPermanentAddress permanentAddress = new DHrMPermanentAddress
  //      {
  //         PermanentAddressId = model.PermanentAddressId,
  //         PermanentAddress = model.PermanentAddress,
  //         PostOffice = model.PermanentPostOffice,
  //         PoliceStationId = model.PermanentPoliceStationId,
  //         ReferenceTypeId = (long)RefferenceTypeEnum.Student,
  //         ReferenceId = model.StudentId
  //      };
  //      await context.DHrMPermanentAddresses.AddAsync(permanentAddress);
  //      //await context.SaveChangesAsync();

  //      rowAffect = await context.SaveChangesAsync();

  //      await transaction.CommitAsync();
  //      return rowAffect;
  //   }
  //   catch (Exception)
  //   {
  //      await transaction.RollbackAsync();
  //   }
  //   return rowAffect;
  //}

  public async Task<TransectionModel> InsertFEduAStudent(FEduAStudentDto model)
  {
    if (model == null) return new TransectionModel(400);

    using var transaction = await context.Database.BeginTransactionAsync();

    try
    {
      AGenConfigACountry country = new();
      AGenConfigBDivisionOrState division = new();
      AGenConfigCDistrictOrCity district = new();

      if ((model.PresentCountryId == 0 || model.PresentCountryId == null) && !string.IsNullOrEmpty(model.PresentCountryName))
      {
        //country = (await context.AGenConfigACountries.AnyAsync(x => x.CountryName == model.PresentCountryName))
        //   ? (await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryName == model.PresentCountryName))
        //   : (new AGenConfigACountry
        //   {
        //      CountryId = await context.AGenConfigACountries.MaxAsync(x => x.CountryId) + 1,
        //      CountryName = model.PresentCountryName
        //   });
        if (await context.AGenConfigACountries.AnyAsync(x => x.CountryName == model.PresentCountryName))
        {
          country = await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryName == model.PresentCountryName);
        }
        else
        {
          country = new AGenConfigACountry
          {
            CountryId = await context.AGenConfigACountries.MaxAsync(x => x.CountryId) + 1,
            CountryName = model.PresentCountryName
          };
          await context.AGenConfigACountries.AddAsync(country);
          await context.SaveChangesAsync();
        }
      }
      else if (model.PresentCountryId > 1 && string.IsNullOrEmpty(model.PresentCountryName))
      {
        country.CountryId = (long)((await context.AGenConfigACountries.AnyAsync(x => x.CountryId == model.PresentCountryId))
           ? model.PresentCountryId : 1);
      }
      else if (model.PresentCountryId > 1 && !string.IsNullOrEmpty(model.PresentCountryName))
      {
        country = await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryId == model.PresentCountryId);
        country.CountryId = (long)((country != null && country.CountryName == model.PresentCountryName) ? model.PresentCountryId : 1);
      }

      // Present Division
      if ((model.PresentDivisionId == 0 || model.PresentDivisionId == null) && !string.IsNullOrEmpty(model.PresentDivisionName))
      {
        //division = (await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionName == model.PresentDivisionName))
        //   ? (await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionName == model.PresentDivisionName))
        //   : (new AGenConfigBDivisionOrState
        //   {
        //      DivisionId = await context.AGenConfigBDivisionOrStates.MaxAsync(x => x.DivisionId) + 1,
        //      DivisionName = model.PresentDivisionName,
        //      CountryId = country.CountryId
        //   });
        if (await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionName == model.PresentDivisionName))
        {
          division = await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionName == model.PresentDivisionName);
        }
        else
        {
          division = (new AGenConfigBDivisionOrState
          {
            DivisionId = await context.AGenConfigBDivisionOrStates.MaxAsync(x => x.DivisionId) + 1,
            DivisionName = model.PresentDivisionName,
            CountryId = country.CountryId
          });
          await context.AGenConfigBDivisionOrStates.AddAsync(division);
          await context.SaveChangesAsync();
        }
      }
      else if (model.PresentDivisionId > 1 && string.IsNullOrEmpty(model.PresentDivisionName))
      {
        division.DivisionId = (long)((await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionId == model.PresentDivisionId))
           ? model.PresentDivisionId : 1);
      }
      else if (model.PresentDivisionId > 1 && !string.IsNullOrEmpty(model.PresentDivisionName))
      {
        division = await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionId == model.PresentDivisionId);
        division.DivisionId = (long)((division != null && division.DivisionName == model.PresentDivisionName) ? model.PresentDivisionId : 1);
      }

      // Present District
      if ((model.PresentDistrictId == 0 || model.PresentDistrictId == null) && !string.IsNullOrEmpty(model.PresentDistrictName))
      {
        //district = (await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictName == model.PresentDistrictName))
        //   ? (await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictName == model.PresentDistrictName))
        //   : (new AGenConfigCDistrictOrCity
        //   {
        //      DistrictId = await context.AGenConfigCDistrictOrCities.MaxAsync(x => x.DistrictId) + 1,
        //      DistrictName = model.PresentDistrictName,
        //      DivisionId = division.DivisionId
        //   });
        if (await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictName == model.PresentDistrictName))
        {
          district = await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictName == model.PresentDistrictName);
        }
        else
        {
          district = (new AGenConfigCDistrictOrCity
          {
            DistrictId = await context.AGenConfigCDistrictOrCities.MaxAsync(x => x.DistrictId) + 1,
            DistrictName = model.PresentDistrictName,
            DivisionId = division.DivisionId
          });
          await context.AGenConfigCDistrictOrCities.AddAsync(district);
          await context.SaveChangesAsync();
        }
      }
      else if (model.PresentDistrictId > 1 && string.IsNullOrEmpty(model.PresentDistrictName))
      {
        district.DistrictId = (long)((await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictId == model.PresentDistrictId))
           ? model.PresentDistrictId : 1);
      }
      else if (model.PresentDistrictId > 1 && !string.IsNullOrEmpty(model.PresentDistrictName))
      {
        district = await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictId == model.PresentDistrictId);
        district.DistrictId = (long)((district != null && district.DistrictName == model.PresentDistrictName) ? model.PresentDistrictId : 1);
      }

      // Present Police Station
      AGenConfigDPoliceStation presentPS = new();
      if ((model.PresentPoliceStationId == 0 || model.PresentPoliceStationId == null) && string.IsNullOrEmpty(model.PresentPoliceStationName))
      {
        presentPS.PoliceStationId = 1;
      }
      else if ((model.PresentPoliceStationId == 0 || model.PresentPoliceStationId == null) && !string.IsNullOrEmpty(model.PresentPoliceStationName))
      {
        //presentPS = (await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationName == model.PresentPoliceStationName))
        //   ? (await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationName == model.PresentPoliceStationName))
        //   : (new AGenConfigDPoliceStation
        //   {
        //      PoliceStationId = await context.AGenConfigDPoliceStations.MaxAsync(x => x.PoliceStationId) + 1,
        //      PoliceStationName = model.PresentPoliceStationName,
        //      DistrictId = district.DistrictId
        //   });
        if (await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationName == model.PresentPoliceStationName))
        {
          presentPS = (await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationName == model.PresentPoliceStationName));
        }
        else
        {
          presentPS = (new AGenConfigDPoliceStation
          {
            PoliceStationId = await context.AGenConfigDPoliceStations.MaxAsync(x => x.PoliceStationId) + 1,
            PoliceStationName = model.PresentPoliceStationName,
            DistrictId = district.DistrictId
          });
          await context.AGenConfigDPoliceStations.AddAsync(presentPS);
          await context.SaveChangesAsync();
        }
      }
      else if (model.PresentPoliceStationId > 1 && string.IsNullOrEmpty(model.PresentPoliceStationName))
      {
        presentPS.PoliceStationId = (long)((await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationId == model.PresentPoliceStationId))
           ? model.PresentPoliceStationId : 1);
      }
      else if (model.PresentPoliceStationId > 1 && !string.IsNullOrEmpty(model.PresentPoliceStationName))
      {
        presentPS = await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationId == model.PresentPoliceStationId);
        presentPS.PoliceStationId = (long)((presentPS != null && presentPS.PoliceStationName == model.PresentPoliceStationName) ? model.PresentPoliceStationId : 1);
      }
      else
      {
        presentPS.PoliceStationId = 1;
      }

      // Permanent Country
      if ((model.PermanentCountryId == 0 || model.PermanentCountryId == null) && !string.IsNullOrEmpty(model.PermanentCountryName))
      {
        //country = (await context.AGenConfigACountries.AnyAsync(x => x.CountryName == model.PermanentCountryName))
        //   ? (await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryName == model.PermanentCountryName))
        //   : (new AGenConfigACountry
        //   {
        //      CountryId = await context.AGenConfigACountries.MaxAsync(x => x.CountryId) + 1,
        //      CountryName = model.PermanentCountryName
        //   });
        if (await context.AGenConfigACountries.AnyAsync(x => x.CountryName == model.PermanentCountryName))
        {
          country = (await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryName == model.PermanentCountryName));
        }
        else
        {
          country = (new AGenConfigACountry
          {
            CountryId = await context.AGenConfigACountries.MaxAsync(x => x.CountryId) + 1,
            CountryName = model.PermanentCountryName
          });
          await context.AGenConfigACountries.AddAsync(country);
          await context.SaveChangesAsync();
        }
      }
      else if (model.PermanentCountryId > 1 && string.IsNullOrEmpty(model.PermanentCountryName))
      {
        country.CountryId = (long)((await context.AGenConfigACountries.AnyAsync(x => x.CountryId == model.PermanentCountryId))
           ? model.PermanentCountryId : 1);
      }
      else if (model.PermanentCountryId > 1 && !string.IsNullOrEmpty(model.PermanentCountryName))
      {
        country = await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryId == model.PermanentCountryId);
        country.CountryId = (long)((country != null && country.CountryName == model.PermanentCountryName) ? model.PermanentCountryId : 1);
      }

      // Permanent Division
      if ((model.PermanentDivisionId == 0 || model.PermanentDivisionId == null) && !string.IsNullOrEmpty(model.PermanentDivisionName))
      {
        //division = (await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionName == model.PermanentDivisionName))
        //   ? (await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionName == model.PermanentDivisionName))
        //   : (new AGenConfigBDivisionOrState
        //   {
        //      DivisionId = await context.AGenConfigBDivisionOrStates.MaxAsync(x => x.DivisionId) + 1,
        //      DivisionName = model.PermanentDivisionName,
        //      CountryId = country.CountryId
        //   });
        if (await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionName == model.PermanentDivisionName))
        {
          division = (await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionName == model.PermanentDivisionName));
        }
        else
        {
          division = (new AGenConfigBDivisionOrState
          {
            DivisionId = await context.AGenConfigBDivisionOrStates.MaxAsync(x => x.DivisionId) + 1,
            DivisionName = model.PermanentDivisionName,
            CountryId = country.CountryId
          });
          await context.AGenConfigBDivisionOrStates.AddAsync(division);
          await context.SaveChangesAsync();
        }
      }
      else if (model.PermanentDivisionId > 1 && string.IsNullOrEmpty(model.PermanentDivisionName))
      {
        division.DivisionId = (long)((await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionId == model.PermanentDivisionId))
           ? model.PermanentDivisionId : 1);
      }
      else if (model.PermanentDivisionId > 1 && !string.IsNullOrEmpty(model.PermanentDivisionName))
      {
        division = await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionId == model.PermanentDivisionId);
        division.DivisionId = (long)((division != null && division.DivisionName == model.PermanentDivisionName) ? model.PermanentDivisionId : 1);
      }

      // Permanent District
      if ((model.PermanentDistrictId == 0 || model.PermanentDistrictId == null) && !string.IsNullOrEmpty(model.PermanentDistrictName))
      {
        //district = (await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictName == model.PermanentDistrictName))
        //   ? (await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictName == model.PermanentDistrictName))
        //   : (new AGenConfigCDistrictOrCity
        //   {
        //      DistrictId = await context.AGenConfigCDistrictOrCities.MaxAsync(x => x.DistrictId) + 1,
        //      DistrictName = model.PermanentDistrictName,
        //      DivisionId = division.DivisionId
        //   });
        if (await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictName == model.PermanentDistrictName))
        {
          district = (await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictName == model.PermanentDistrictName));
        }
        else
        {
          district = (new AGenConfigCDistrictOrCity
          {
            DistrictId = await context.AGenConfigCDistrictOrCities.MaxAsync(x => x.DistrictId) + 1,
            DistrictName = model.PermanentDistrictName,
            DivisionId = division.DivisionId
          });
          await context.AGenConfigCDistrictOrCities.AddAsync(district);
          await context.SaveChangesAsync();
        }
      }
      else if (model.PermanentDistrictId > 1 && string.IsNullOrEmpty(model.PermanentDistrictName))
      {
        district.DistrictId = (long)((await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictId == model.PermanentDistrictId))
           ? model.PermanentDistrictId : 1);
      }
      else if (model.PermanentDistrictId > 1 && !string.IsNullOrEmpty(model.PermanentDistrictName))
      {
        district = await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictId == model.PermanentDistrictId);
        district.DistrictId = (long)((district != null && district.DistrictName == model.PermanentDistrictName) ? model.PermanentDistrictId : 1);
      }

      // Permanent Police Station
      AGenConfigDPoliceStation permanentPS = new();
      if ((model.PermanentPoliceStationId == 0 || model.PermanentPoliceStationId == null) && string.IsNullOrEmpty(model.PermanentPoliceStationName))
      {
        permanentPS.PoliceStationId = 1;
      }
      else if ((model.PermanentPoliceStationId == 0 || model.PermanentPoliceStationId == null) && !string.IsNullOrEmpty(model.PermanentPoliceStationName))
      {
        //permanentPS = (await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationName == model.PermanentPoliceStationName))
        //   ? (await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationName == model.PermanentPoliceStationName))
        //   : (new AGenConfigDPoliceStation
        //   {
        //      PoliceStationId = await context.AGenConfigDPoliceStations.MaxAsync(x => x.PoliceStationId) + 1,
        //      PoliceStationName = model.PermanentPoliceStationName,
        //      DistrictId = district.DistrictId
        //   });

        if (await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationName == model.PermanentPoliceStationName))
        {
          permanentPS = (await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationName == model.PermanentPoliceStationName));
        }
        else
        {
          permanentPS = (new AGenConfigDPoliceStation
          {
            PoliceStationId = await context.AGenConfigDPoliceStations.MaxAsync(x => x.PoliceStationId) + 1,
            PoliceStationName = model.PermanentPoliceStationName,
            DistrictId = district.DistrictId
          });
          await context.AGenConfigDPoliceStations.AddAsync(permanentPS);
          await context.SaveChangesAsync();
        }
      }
      else if (model.PermanentPoliceStationId > 1 && string.IsNullOrEmpty(model.PermanentPoliceStationName))
      {
        permanentPS.PoliceStationId = (long)((await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationId == model.PermanentPoliceStationId))
           ? model.PermanentPoliceStationId : 1);
      }
      else if (model.PermanentPoliceStationId > 1 && !string.IsNullOrEmpty(model.PermanentPoliceStationName))
      {
        permanentPS = await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationId == model.PermanentPoliceStationId);
        permanentPS.PoliceStationId = (long)((permanentPS != null && permanentPS.PoliceStationName == model.PermanentPoliceStationName) ? model.PermanentPoliceStationId : 1);
      }
      else
      {
        permanentPS.PoliceStationId = 1;
      }

      var studentEntity = new FEduAStudent
      {
        StudentId = await GetNextId("StudentId"),
        StudentCode = model.StudentCode,
        StudentName = model.StudentName,
        StudentFathersName = model.StudentFathersName,
        StudentMothersName = model.StudentMothersName,
        GenderId = (model.GenderId != 0) ? model.GenderId : null,
        BloodGroupId = (model.BloodGroupId != 0) ? model.BloodGroupId : null,
        ReligionId = (model.ReligionId != 0) ? model.ReligionId : null,
        StudentPhoto = model.StudentPhoto
      };

      //studentEntity.GenderId = (model.GenderId != 0) ? model.GenderId : null;
      //studentEntity.BloodGroupId = (model.BloodGroupId != 0) ? model.BloodGroupId : null;
      //studentEntity.ReligionId = (model.ReligionId != 0) ? model.ReligionId : null;

      await context.FEduAStudents.AddAsync(studentEntity);
      await context.SaveChangesAsync();

      if (studentEntity != null)
      {
        var presentAddressEntity = new DHrLPresentAddress
        {
          PresentAddressId = await context.DHrLPresentAddresses.MaxAsync(x => x.PresentAddressId) + 1,
          PresentAddress = model.PresentAddress,
          PostOffice = model.PresentPostOffice,
          PoliceStationId = presentPS.PoliceStationId,
          ReferenceTypeId = (long)ReferenceTypeEnum.Student,
          ReferenceId = studentEntity.StudentId
        };
        await context.DHrLPresentAddresses.AddAsync(presentAddressEntity);
        await context.SaveChangesAsync();
      }
      else
      {
        return new TransectionModel(400, MessageConstants.UnauthorizedAttemptOfRecordInsert + "Present Address");
      }

      if (studentEntity != null)
      {
        var permanentAddressEntity = new DHrMPermanentAddress
        {
          PermanentAddressId = await context.DHrMPermanentAddresses.MaxAsync(x => x.PermanentAddressId) + 1,
          PermanentAddress = model.PermanentAddress,
          PostOffice = model.PermanentPostOffice,
          PoliceStationId = permanentPS.PoliceStationId,
          ReferenceTypeId = (long)ReferenceTypeEnum.Student,
          ReferenceId = studentEntity.StudentId
        };
        await context.DHrMPermanentAddresses.AddAsync(permanentAddressEntity);
        await context.SaveChangesAsync();
      }
      else
      {
        return new TransectionModel(400, MessageConstants.UnauthorizedAttemptOfRecordInsert + "Permanent Address");
      }

      // Commit the transaction
      await transaction.CommitAsync();

      return new TransectionModel(200); // Successfully added one student
    }
    catch (Exception)
    {
      // Rollback the transaction in case of exception
      await transaction.RollbackAsync();
      return new TransectionModel(400);
    }
  }

  public async Task<FEduAStudentDto> GetStudentWithAddress(long key)
  {
    FEduAStudentDto student = await (
        from s in context.FEduAStudents
        where s.StudentId == key
        join g in context.AGenConfigEGenders on s.GenderId equals g.GenderId into genderGroup
        from gender in genderGroup.DefaultIfEmpty()
        join bg in context.AGenConfigFBloodGroups on s.BloodGroupId equals bg.BloodGroupId into bloodGroup
        from blood in bloodGroup.DefaultIfEmpty()
        join r in context.AGenConfigGReligions on s.ReligionId equals r.ReligionId into religionGroup
        from religion in religionGroup.DefaultIfEmpty()
        join pra in context.DHrLPresentAddresses on s.StudentId equals pra.ReferenceId into presentAddressGroup
        from pra in presentAddressGroup.DefaultIfEmpty()
          // Left outer join for Present Address Reference Type
        join prrt in context.DHrKReferenceTypes on pra.ReferenceTypeId equals prrt.ReferenceTypeId into presentReferenceTypeGroup
        from prrt in presentReferenceTypeGroup.DefaultIfEmpty()
          // Left outer join for Present Address Police Station
        join prps in context.AGenConfigDPoliceStations on pra.PoliceStationId equals prps.PoliceStationId into presentPoliceStationGroup
        from prps in presentPoliceStationGroup.DefaultIfEmpty()
          // Left outer join for Present Address District
        join prdc in context.AGenConfigCDistrictOrCities on prps.DistrictId equals prdc.DistrictId into presentDistrictGroup
        from prdc in presentDistrictGroup.DefaultIfEmpty()
          // Left outer join for Present Address Division
        join prds in context.AGenConfigBDivisionOrStates on prdc.DivisionId equals prds.DivisionId into presentDivisionGroup
        from prds in presentDivisionGroup.DefaultIfEmpty()
          // Left outer join for Present Address Country
        join prc in context.AGenConfigACountries on prds.CountryId equals prc.CountryId into presentCountryGroup
        from prc in presentCountryGroup.DefaultIfEmpty()
          // Left outer join for Permanent Address
        join pea in context.DHrMPermanentAddresses on s.StudentId equals pea.ReferenceId into permanentAddressGroup
        from pea in permanentAddressGroup.DefaultIfEmpty()
          // Left outer join for Permanent Address Reference Type
        join pert in context.DHrKReferenceTypes on pea.ReferenceTypeId equals pert.ReferenceTypeId into permanentReferenceTypeGroup
        from pert in permanentReferenceTypeGroup.DefaultIfEmpty()
          // Left outer join for Permanent Address Police Station
        join peps in context.AGenConfigDPoliceStations on pea.PoliceStationId equals peps.PoliceStationId into permanentPoliceStationGroup
        from peps in permanentPoliceStationGroup.DefaultIfEmpty()
          // Left outer join for Permanent Address District
        join pedc in context.AGenConfigCDistrictOrCities on peps.DistrictId equals pedc.DistrictId into permanentDistrictGroup
        from pedc in permanentDistrictGroup.DefaultIfEmpty()
          // Left outer join for Permanent Address Division
        join peds in context.AGenConfigBDivisionOrStates on pedc.DivisionId equals peds.DivisionId into permanentDivisionGroup
        from peds in permanentDivisionGroup.DefaultIfEmpty()
          // Left outer join for Permanent Address Country
        join pec in context.AGenConfigACountries on peds.CountryId equals pec.CountryId into permanentCountryGroup
        from pec in permanentCountryGroup.DefaultIfEmpty()
        select new FEduAStudentDto
        {
          StudentId = s.StudentId,
          StudentCode = s.StudentCode,
          StudentName = s.StudentName,
          StudentFathersName = s.StudentFathersName,
          StudentMothersName = s.StudentMothersName,
          StudentPhoto = s.StudentPhoto,
          // Gender
          GenderId = s.GenderId,
          GenderName = gender != null ? gender.GenderName : null,
          // Blood Group
          BloodGroupId = s.BloodGroupId,
          BloodGroupName = blood != null ? blood.BloodGroupName : null,
          // Religion
          ReligionId = s.ReligionId,
          ReligionName = religion != null ? religion.ReligionName : null,
          // Present Address
          PresentAddressId = pra != null ? pra.PresentAddressId : (long?)null,
          PresentAddress = pra != null ? pra.PresentAddress : null,
          PresentPostOffice = pra != null ? pra.PostOffice : null,
          PresentPoliceStationId = pra != null ? pra.PoliceStationId : (long?)null,
          // Present Address Police Station
          PresentPoliceStationName = prps != null ? prps.PoliceStationName : null,
          // Present Address District
          PresentDistrictId = prps != null ? prps.DistrictId : (long?)null,
          PresentDistrictName = prdc != null ? prdc.DistrictName : null,
          // Present Address Division
          PresentDivisionId = prdc != null ? prdc.DivisionId : null,
          PresentDivisionName = prds != null ? prds.DivisionName : null,
          // Present Address Country
          PresentCountryId = prds != null ? prds.CountryId : null,
          PresentCountryName = prc != null ? prc.CountryName : null,

          // Permanent Address
          PermanentAddressId = pea != null ? pea.PermanentAddressId : (long?)null,
          PermanentAddress = pea != null ? pea.PermanentAddress : null,
          PermanentPostOffice = pea != null ? pea.PostOffice : null,
          PermanentPoliceStationId = pea != null ? (long?)pea.PoliceStationId : (long?)null,
          // Permanent Address Police Station
          PermanentPoliceStationName = peps != null ? peps.PoliceStationName : null,
          // Permanent Address District
          PermanentDistrictId = peps != null ? peps.DistrictId : (long?)null,
          PermanentDistrictName = pedc != null ? pedc.DistrictName : null,
          // Permanent Address Division
          PermanentDivisionId = pedc != null ? peds.DivisionId : (long?)null,
          PermanentDivisionName = peds != null ? peds.DivisionName : null,
          // Permanent Address Country
          PermanentCountryId = peds != null ? peds.CountryId : (long?)null,
          PermanentCountryName = pec != null ? pec.CountryName : null
        }).FirstOrDefaultAsync();

    return student;
  }

  //public async Task<FEduAStudentDto> GetStudentWithAddress(long key)
  //{
  //   FEduAStudentDto student = await (from s in context.FEduAStudents
  //                                          where s.StudentId == key
  //                                          join g in context.AGenConfigEGenders on s.GenderId equals g.GenderId
  //                                          join bg in context.AGenConfigFBloodGroups on s.BloodGroupId equals bg.BloodGroupId
  //                                          join r in context.AGenConfigGReligions on s.ReligionId equals r.ReligionId

  //                                          join pra in context.DHrLPresentAddresses on s.StudentId equals pra.ReferenceId
  //                                          join prrt in context.DHrKReferenceTypes on pra.ReferenceTypeId equals prrt.ReferenceTypeId
  //                                          join prps in context.AGenConfigDPoliceStations on pra.PoliceStationId equals prps.PoliceStationId
  //                                          join prdc in context.AGenConfigCDistrictOrCities on prps.DistrictId equals prdc.DistrictId
  //                                          join prds in context.AGenConfigBDivisionOrStates on prdc.DivisionId equals prds.DivisionId
  //                                          join prc in context.AGenConfigACountries on prds.CountryId equals prc.CountryId

  //                                          join pea in context.DHrMPermanentAddresses on s.StudentId equals pea.ReferenceId
  //                                          join pert in context.DHrKReferenceTypes on pea.ReferenceTypeId equals pert.ReferenceTypeId
  //                                          join peps in context.AGenConfigDPoliceStations on pea.PoliceStationId equals peps.PoliceStationId
  //                                          join pedc in context.AGenConfigCDistrictOrCities on peps.DistrictId equals pedc.DistrictId
  //                                          join peds in context.AGenConfigBDivisionOrStates on pedc.DivisionId equals peds.DivisionId
  //                                          join pec in context.AGenConfigACountries on peds.CountryId equals pec.CountryId
  //                                          select new FEduAStudentDto
  //                                          {
  //                                             StudentId = s.StudentId,
  //                                             StudentCode = s.StudentCode,
  //                                             StudentName = s.StudentName,
  //                                             StudentFathersName = s.StudentFathersName,
  //                                             StudentMothersName = s.StudentMothersName,
  //                                             GenderId = s.GenderId,
  //                                             BloodGroupId = s.BloodGroupId,
  //                                             ReligionId = s.ReligionId,
  //                                             StudentPhoto = s.StudentPhoto,
  //                                             // Gender
  //                                             GenderName = g.GenderName,
  //                                             // Blood Group
  //                                             BloodGroupName = bg.BloodGroupName,
  //                                             // Religion
  //                                             ReligionName = r.ReligionName,
  //                                             // Present Address
  //                                             PresentAddressId = pra.PresentAddressId,
  //                                             PresentAddress = pra.PresentAddress,
  //                                             PresentPostOffice = pra.PostOffice,
  //                                             PresentPoliceStationId = pra.PoliceStationId,
  //                                             //PresentReferenceTypeId = pra.ReferenceTypeId,
  //                                             //PresentReferenceId = pra.ReferenceId,
  //                                             //PresentReferenceTypeName = prrt.ReferenceTypeName,
  //                                             PresentPoliceStationName = prps.PoliceStationName,
  //                                             PresentDistrictName = prdc.DistrictName,
  //                                             PresentDivisionName = prds.DivisionName,
  //                                             PresentCountryName = prc.CountryName,

  //                                             // Permanent Address
  //                                             PermanentAddressId = pea.PermanentAddressId,
  //                                             PermanentAddress = pea.PermanentAddress,
  //                                             PermanentPostOffice = pea.PostOffice,
  //                                             PermanentPoliceStationId = (long)pea.PoliceStationId,
  //                                             //PermanentReferenceTypeId = (long)pea.ReferenceTypeId,
  //                                             //PermanentReferenceId = (long)pea.ReferenceId,
  //                                             //PermanentReferenceTypeName = pert.ReferenceTypeName,
  //                                             PermanentPoliceStationName = prps.PoliceStationName,
  //                                             PermanentDistrictName = prdc.DistrictName,
  //                                             PermanentDivisionName = prds.DivisionName,
  //                                             PermanentCountryName = prc.CountryName
  //                                          }).FirstOrDefaultAsync();

  //   //var results = await context.FEduAStudents.FromSqlRaw("EXEC spGetStudentWithAddress").ToListAsync();
  //   //var resultt = await context.FromExpression("EXEC spGetStudentWithAddress").ToList();
  //   //var results = await this.Database.ExecuteStoreQueryAsync<FEduAStudentDto>("spGetStudentWithAddress");
  //   //IReadOnlyList<FEduAStudentDto> students = (IReadOnlyList<FEduAStudentDto>)results;
  //   return student;
  //}

  public async Task<TransectionModel> UpdateFEduAStudent(FEduAStudentDto model)
  {
    if (model == null || model.StudentId == 0) new TransectionModel(400);
    using var transaction = await context.Database.BeginTransactionAsync();

    try
    {
      var studentEntity = await context.FEduAStudents.FindAsync(model.StudentId);

      if (studentEntity == null) new TransectionModel(400); // Student not found

      // Update properties of the student entity
      studentEntity.StudentCode = model.StudentCode;
      studentEntity.StudentName = model.StudentName;
      studentEntity.StudentFathersName = model.StudentFathersName;
      studentEntity.StudentMothersName = model.StudentMothersName;
      studentEntity.StudentPhoto = model.StudentPhoto;
      studentEntity.GenderId = model.GenderId;
      studentEntity.BloodGroupId = model.BloodGroupId;
      studentEntity.ReligionId = model.ReligionId;
      if (studentEntity.StudentId == model.StudentId)
      {
        context.Entry(studentEntity).State = EntityState.Modified;
        context.FEduAStudents.Update(studentEntity);
        await context.SaveChangesAsync();
      }
      else
      {
        return new TransectionModel(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError + "student information");
      }

      // Update data for country, division, district, police-station
      AGenConfigACountry country = new();
      AGenConfigBDivisionOrState division = new();
      AGenConfigCDistrictOrCity district = new();

      // Update properties of the present address entity
      var presentAddressEntity = await context.DHrLPresentAddresses
         .Include(x => x.PoliceStation)
         .ThenInclude(x => x.District)
         .ThenInclude(x => x.Division)
         .ThenInclude(x => x.Country)
         .FirstOrDefaultAsync(pa => pa.ReferenceId == model.StudentId);

      long dbPresentCountryId = presentAddressEntity.PoliceStation.District.Division.Country.CountryId;
      string dbPresentCountryName = presentAddressEntity.PoliceStation.District.Division.Country.CountryName;
      if (dbPresentCountryId == model.PresentCountryId && dbPresentCountryName.Trim().ToLower() == model.PresentCountryName.Trim().ToLower())
      {
        country.CountryId = (long)model.PresentCountryId;
      }
      else if (dbPresentCountryName.Trim().ToLower() != model.PresentCountryName.Trim().ToLower() || model.PresentCountryId == 0 || model.PresentCountryId == null && !string.IsNullOrEmpty(model.PresentCountryName))
      {
        if (await context.AGenConfigACountries.AnyAsync(x => x.CountryName.Trim().ToLower() == model.PresentCountryName.Trim().ToLower()))
        {
          country = await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryName.Trim().ToLower() == model.PresentCountryName.Trim().ToLower());
        }
        else
        {
          country = new AGenConfigACountry
          {
            CountryId = await context.AGenConfigACountries.MaxAsync(x => x.CountryId) + 1,
            CountryName = model.PresentCountryName
          };
          await context.AGenConfigACountries.AddAsync(country);
          await context.SaveChangesAsync();
        }
      }
      else if (dbPresentCountryId != model.PresentCountryId && dbPresentCountryName.Trim().ToLower() != model.PresentCountryName.Trim().ToLower() && model.PresentCountryId > 1 && string.IsNullOrEmpty(model.PresentCountryName))
      {
        country.CountryId = (long)((await context.AGenConfigACountries.AnyAsync(x => x.CountryId == model.PresentCountryId))
           ? model.PresentCountryId : 1);
      }
      else if (dbPresentCountryId != model.PresentCountryId && model.PresentCountryId > 1 && !string.IsNullOrEmpty(model.PresentCountryName))
      {
        //country = await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryId == model.PresentCountryId);
        country.CountryId = (long)((country != null && country.CountryName == model.PresentCountryName) ? model.PresentCountryId : 1);
      }

      // Present Division
      long dbPresentDivisionId = presentAddressEntity.PoliceStation.District.Division.DivisionId;
      string dbPresentDivisionName = presentAddressEntity.PoliceStation.District.Division.DivisionName;
      if (dbPresentDivisionId == model.PresentDivisionId && dbPresentDivisionName.Trim().ToLower() == model.PresentDivisionName.Trim().ToLower())
      {
        division.DivisionId = (long)model.PresentDivisionId;
      }
      else if (presentAddressEntity.PoliceStation.District.DivisionId != model.PresentDivisionId && (model.PresentDivisionId == 0 || model.PresentDivisionId == null) && !string.IsNullOrEmpty(model.PresentDivisionName))
      {
        if (await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionName == model.PresentDivisionName))
        {
          division = await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionName == model.PresentDivisionName);
        }
        else
        {
          division = (new AGenConfigBDivisionOrState
          {
            DivisionId = await context.AGenConfigBDivisionOrStates.MaxAsync(x => x.DivisionId) + 1,
            DivisionName = model.PresentDivisionName,
            CountryId = country.CountryId
          });
          await context.AGenConfigBDivisionOrStates.AddAsync(division);
          await context.SaveChangesAsync();
        }
      }
      else if (presentAddressEntity.PoliceStation.District.DivisionId != model.PresentDivisionId && model.PresentDivisionId > 1 && string.IsNullOrEmpty(model.PresentDivisionName))
      {
        division.DivisionId = (long)((await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionId == model.PresentDivisionId))
           ? model.PresentDivisionId : 1);
      }
      else if (presentAddressEntity.PoliceStation.District.DivisionId != model.PresentDivisionId && model.PresentDivisionId > 1 && !string.IsNullOrEmpty(model.PresentDivisionName))
      {
        division = await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionId == model.PresentDivisionId);
        division.DivisionId = (long)((division != null && division.DivisionName == model.PresentDivisionName) ? model.PresentDivisionId : 1);
      }

      // Present District
      if (presentAddressEntity.PoliceStation.DistrictId == model.PresentDistrictId)
      {
        district.DistrictId = (long)model.PresentDistrictId;
      }
      else if (presentAddressEntity.PoliceStation.DistrictId != model.PresentDivisionId && (model.PresentDistrictId == 0 || model.PresentDistrictId == null) && !string.IsNullOrEmpty(model.PresentDistrictName))
      {
        if (await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictName == model.PresentDistrictName))
        {
          district = await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictName == model.PresentDistrictName);
        }
        else
        {
          district = (new AGenConfigCDistrictOrCity
          {
            DistrictId = await context.AGenConfigCDistrictOrCities.MaxAsync(x => x.DistrictId) + 1,
            DistrictName = model.PresentDistrictName,
            DivisionId = division.DivisionId
          });
          await context.AGenConfigCDistrictOrCities.AddAsync(district);
          await context.SaveChangesAsync();
        }
      }
      else if (presentAddressEntity.PoliceStation.DistrictId != model.PresentDivisionId && model.PresentDistrictId > 1 && string.IsNullOrEmpty(model.PresentDistrictName))
      {
        district.DistrictId = (long)((await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictId == model.PresentDistrictId))
           ? model.PresentDistrictId : 1);
      }
      else if (presentAddressEntity.PoliceStation.DistrictId != model.PresentDivisionId && model.PresentDistrictId > 1 && !string.IsNullOrEmpty(model.PresentDistrictName))
      {
        district = await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictId == model.PresentDistrictId);
        district.DistrictId = (long)((district != null && district.DistrictName == model.PresentDistrictName) ? model.PresentDistrictId : 1);
      }

      // Present Police Station
      AGenConfigDPoliceStation presentPS = new();
      if (presentAddressEntity.PoliceStationId == model.PresentPoliceStationId)
      {
        presentPS.PoliceStationId = (long)model.PresentPoliceStationId;
      }
      else if (presentAddressEntity.PoliceStationId != model.PresentPoliceStationId && (model.PresentPoliceStationId == 0 || model.PresentPoliceStationId == null) && string.IsNullOrEmpty(model.PresentPoliceStationName))
      {
        presentPS.PoliceStationId = 1;
      }
      else if (presentAddressEntity.PoliceStationId != model.PresentPoliceStationId && (model.PresentPoliceStationId == 0 || model.PresentPoliceStationId == null) && !string.IsNullOrEmpty(model.PresentPoliceStationName))
      {
        if (await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationName == model.PresentPoliceStationName))
        {
          presentPS = (await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationName == model.PresentPoliceStationName));
        }
        else
        {
          presentPS = (new AGenConfigDPoliceStation
          {
            PoliceStationId = await context.AGenConfigDPoliceStations.MaxAsync(x => x.PoliceStationId) + 1,
            PoliceStationName = model.PresentPoliceStationName,
            DistrictId = district.DistrictId
          });
          await context.AGenConfigDPoliceStations.AddAsync(presentPS);
          await context.SaveChangesAsync();
        }
      }
      else if (presentAddressEntity.PoliceStationId != model.PresentPoliceStationId && model.PresentPoliceStationId > 1 && string.IsNullOrEmpty(model.PresentPoliceStationName))
      {
        presentPS.PoliceStationId = (long)((await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationId == model.PresentPoliceStationId))
           ? model.PresentPoliceStationId : 1);
      }
      else if (presentAddressEntity.PoliceStationId != model.PresentPoliceStationId && model.PresentPoliceStationId > 1 && !string.IsNullOrEmpty(model.PresentPoliceStationName))
      {
        presentPS = await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationId == model.PresentPoliceStationId);
        presentPS.PoliceStationId = (long)((presentPS != null && presentPS.PoliceStationName == model.PresentPoliceStationName) ? model.PresentPoliceStationId : 1);
      }
      else
      {
        presentPS.PoliceStationId = 1;
      }

      if (studentEntity != null)
      {
        if (presentAddressEntity != null)
        {
          presentAddressEntity.PresentAddress = model.PresentAddress;
          presentAddressEntity.PostOffice = model.PresentPostOffice;
          presentAddressEntity.PoliceStationId = presentPS.PoliceStationId;
          presentAddressEntity.ReferenceTypeId = (long)ReferenceTypeEnum.Student;
          presentAddressEntity.ReferenceId = studentEntity.StudentId;
          context.Entry(presentAddressEntity).State = EntityState.Modified;
          context.DHrLPresentAddresses.Update(presentAddressEntity);
          await context.SaveChangesAsync();
        }
        else
        {
          return new TransectionModel(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError + "present address");
        }
      }
      else
      {
        return new TransectionModel(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError + "student information null");
      }

      // Update properties of the permanent address entity
      var permanentAddressEntity = await context.DHrMPermanentAddresses
          .Include(x => x.PoliceStation)
          .ThenInclude(x => x.District)
          .ThenInclude(x => x.Division)
          .FirstOrDefaultAsync(pa => pa.ReferenceId == model.StudentId);

      // Permanent Country
      if (permanentAddressEntity.PoliceStation.District.Division.CountryId == model.PermanentCountryId)
      {
        country.CountryId = (long)model.PresentCountryId;
      }
      else if (permanentAddressEntity.PoliceStation.District.Division.CountryId != model.PermanentCountryId && (model.PermanentCountryId == 0 || model.PermanentCountryId == null) && !string.IsNullOrEmpty(model.PermanentCountryName))
      {
        //country = (await context.AGenConfigACountries.AnyAsync(x => x.CountryName == model.PermanentCountryName))
        //   ? (await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryName == model.PermanentCountryName))
        //   : (new AGenConfigACountry
        //   {
        //      CountryId = await context.AGenConfigACountries.MaxAsync(x => x.CountryId) + 1,
        //      CountryName = model.PermanentCountryName
        //   });
        if (await context.AGenConfigACountries.AnyAsync(x => x.CountryName == model.PermanentCountryName))
        {
          country = (await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryName == model.PermanentCountryName));
        }
        else
        {
          country = (new AGenConfigACountry
          {
            CountryId = await context.AGenConfigACountries.MaxAsync(x => x.CountryId) + 1,
            CountryName = model.PermanentCountryName
          });
          await context.AGenConfigACountries.AddAsync(country);
          await context.SaveChangesAsync();
        }
      }
      else if (permanentAddressEntity.PoliceStation.District.Division.CountryId != model.PermanentCountryId && model.PermanentCountryId > 1 && string.IsNullOrEmpty(model.PermanentCountryName))
      {
        country.CountryId = (long)((await context.AGenConfigACountries.AnyAsync(x => x.CountryId == model.PermanentCountryId))
           ? model.PermanentCountryId : 1);
      }
      else if (permanentAddressEntity.PoliceStation.District.Division.CountryId != model.PermanentCountryId && model.PermanentCountryId > 1 && !string.IsNullOrEmpty(model.PermanentCountryName))
      {
        country = await context.AGenConfigACountries.FirstOrDefaultAsync(x => x.CountryId == model.PermanentCountryId);
        country.CountryId = (long)((country != null && country.CountryName == model.PermanentCountryName) ? model.PermanentCountryId : 1);
      }

      // Permanent Division
      if (permanentAddressEntity.PoliceStation.District.DivisionId == model.PermanentDivisionId)
      {
        division.DivisionId = (long)model.PermanentDivisionId;
      }
      else if (permanentAddressEntity.PoliceStation.District.DivisionId != model.PermanentDivisionId && (model.PermanentDivisionId == 0 || model.PermanentDivisionId == null) && !string.IsNullOrEmpty(model.PermanentDivisionName))
      {
        if (await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionName == model.PermanentDivisionName))
        {
          division = (await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionName == model.PermanentDivisionName));
        }
        else
        {
          division = (new AGenConfigBDivisionOrState
          {
            DivisionId = await context.AGenConfigBDivisionOrStates.MaxAsync(x => x.DivisionId) + 1,
            DivisionName = model.PermanentDivisionName,
            CountryId = country.CountryId
          });
          await context.AGenConfigBDivisionOrStates.AddAsync(division);
          await context.SaveChangesAsync();
        }
      }
      else if (permanentAddressEntity.PoliceStation.District.DivisionId != model.PermanentDivisionId && model.PermanentDivisionId > 1 && string.IsNullOrEmpty(model.PermanentDivisionName))
      {
        division.DivisionId = (long)((await context.AGenConfigBDivisionOrStates.AnyAsync(x => x.DivisionId == model.PermanentDivisionId))
           ? model.PermanentDivisionId : 1);
      }
      else if (permanentAddressEntity.PoliceStation.District.DivisionId != model.PermanentDivisionId && model.PermanentDivisionId > 1 && !string.IsNullOrEmpty(model.PermanentDivisionName))
      {
        division = await context.AGenConfigBDivisionOrStates.FirstOrDefaultAsync(x => x.DivisionId == model.PermanentDivisionId);
        division.DivisionId = (long)((division != null && division.DivisionName == model.PermanentDivisionName) ? model.PermanentDivisionId : 1);
      }

      // Permanent District
      if (permanentAddressEntity.PoliceStation.DistrictId == model.PermanentDistrictId)
      {
        district.DistrictId = (long)model.PermanentDistrictId;
      }
      else if (permanentAddressEntity.PoliceStation.DistrictId != model.PermanentDistrictId && (model.PermanentDistrictId == 0 || model.PermanentDistrictId == null) && !string.IsNullOrEmpty(model.PermanentDistrictName))
      {
        //district = (await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictName == model.PermanentDistrictName))
        //   ? (await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictName == model.PermanentDistrictName))
        //   : (new AGenConfigCDistrictOrCity
        //   {
        //      DistrictId = await context.AGenConfigCDistrictOrCities.MaxAsync(x => x.DistrictId) + 1,
        //      DistrictName = model.PermanentDistrictName,
        //      DivisionId = division.DivisionId
        //   });
        if (await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictName == model.PermanentDistrictName))
        {
          district = (await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictName == model.PermanentDistrictName));
        }
        else
        {
          district = (new AGenConfigCDistrictOrCity
          {
            DistrictId = await context.AGenConfigCDistrictOrCities.MaxAsync(x => x.DistrictId) + 1,
            DistrictName = model.PermanentDistrictName,
            DivisionId = division.DivisionId
          });
          await context.AGenConfigCDistrictOrCities.AddAsync(district);
          await context.SaveChangesAsync();
        }
      }
      else if (permanentAddressEntity.PoliceStation.DistrictId != model.PermanentDistrictId && model.PermanentDistrictId > 1 && string.IsNullOrEmpty(model.PermanentDistrictName))
      {
        district.DistrictId = (long)((await context.AGenConfigCDistrictOrCities.AnyAsync(x => x.DistrictId == model.PermanentDistrictId))
           ? model.PermanentDistrictId : 1);
      }
      else if (permanentAddressEntity.PoliceStation.DistrictId != model.PermanentDistrictId && model.PermanentDistrictId > 1 && !string.IsNullOrEmpty(model.PermanentDistrictName))
      {
        district = await context.AGenConfigCDistrictOrCities.FirstOrDefaultAsync(x => x.DistrictId == model.PermanentDistrictId);
        district.DistrictId = (long)((district != null && district.DistrictName == model.PermanentDistrictName) ? model.PermanentDistrictId : 1);
      }

      // Permanent Police Station
      AGenConfigDPoliceStation permanentPS = new();
      if (permanentAddressEntity.PoliceStationId == model.PermanentPoliceStationId)
      {
        permanentPS.PoliceStationId = (long)model.PermanentPoliceStationId;
      }
      else if (permanentAddressEntity.PoliceStationId != model.PermanentPoliceStationId && (model.PermanentPoliceStationId == 0 || model.PermanentPoliceStationId == null) && string.IsNullOrEmpty(model.PermanentPoliceStationName))
      {
        permanentPS.PoliceStationId = 1;
      }
      else if (permanentAddressEntity.PoliceStationId != model.PermanentPoliceStationId && (model.PermanentPoliceStationId == 0 || model.PermanentPoliceStationId == null) && !string.IsNullOrEmpty(model.PermanentPoliceStationName))
      {
        //permanentPS = (await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationName == model.PermanentPoliceStationName))
        //   ? (await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationName == model.PermanentPoliceStationName))
        //   : (new AGenConfigDPoliceStation
        //   {
        //      PoliceStationId = await context.AGenConfigDPoliceStations.MaxAsync(x => x.PoliceStationId) + 1,
        //      PoliceStationName = model.PermanentPoliceStationName,
        //      DistrictId = district.DistrictId
        //   });

        if (await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationName == model.PermanentPoliceStationName))
        {
          permanentPS = (await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationName == model.PermanentPoliceStationName));
        }
        else
        {
          permanentPS = (new AGenConfigDPoliceStation
          {
            PoliceStationId = await context.AGenConfigDPoliceStations.MaxAsync(x => x.PoliceStationId) + 1,
            PoliceStationName = model.PermanentPoliceStationName,
            DistrictId = district.DistrictId
          });
          await context.AGenConfigDPoliceStations.AddAsync(permanentPS);
          await context.SaveChangesAsync();
        }
      }
      else if (permanentAddressEntity.PoliceStationId != model.PermanentPoliceStationId && model.PermanentPoliceStationId > 1 && string.IsNullOrEmpty(model.PermanentPoliceStationName))
      {
        permanentPS.PoliceStationId = (long)((await context.AGenConfigDPoliceStations.AnyAsync(x => x.PoliceStationId == model.PermanentPoliceStationId))
           ? model.PermanentPoliceStationId : 1);
      }
      else if (permanentAddressEntity.PoliceStationId != model.PermanentPoliceStationId && model.PermanentPoliceStationId > 1 && !string.IsNullOrEmpty(model.PermanentPoliceStationName))
      {
        permanentPS = await context.AGenConfigDPoliceStations.FirstOrDefaultAsync(x => x.PoliceStationId == model.PermanentPoliceStationId);
        permanentPS.PoliceStationId = (long)((permanentPS != null && permanentPS.PoliceStationName == model.PermanentPoliceStationName) ? model.PermanentPoliceStationId : 1);
      }
      else
      {
        permanentPS.PoliceStationId = 1;
      }

      if (studentEntity != null)
      {
        if (permanentAddressEntity != null)
        {
          permanentAddressEntity.PermanentAddress = model.PermanentAddress;
          permanentAddressEntity.PostOffice = model.PermanentPostOffice;
          permanentAddressEntity.PoliceStationId = permanentPS.PoliceStationId;
          permanentAddressEntity.ReferenceTypeId = (long)ReferenceTypeEnum.Student;
          permanentAddressEntity.ReferenceId = studentEntity.StudentId;
          context.Entry(permanentAddressEntity).State = EntityState.Modified;
          context.DHrMPermanentAddresses.Update(permanentAddressEntity);
          await context.SaveChangesAsync();
        }
        else
        {
          return new TransectionModel(400, MessageConstants.UnauthorizedAttemptOfRecordInsert + "permanent address");
        }
      }
      else
      {
        return new TransectionModel(400, MessageConstants.UnauthorizedAttemptOfRecordUpdateError + "student information null");
      }

      // Commit the transaction
      await transaction.CommitAsync();

      return new TransectionModel(200); // Successfully updated one student
    }
    catch (Exception)
    {
      // Rollback the transaction in case of exception
      await transaction.RollbackAsync();
      return new TransectionModel(400);
    }
  }

  public async Task<TransectionModel> DeleteFEduAStudent(long key)
  {
    if (key <= 0) new TransectionModel(400);

    using var transaction = await context.Database.BeginTransactionAsync();

    try
    {
      if (await context.DHrLPresentAddresses.AnyAsync(x => x.ReferenceId == key))
      {
        DHrLPresentAddress presentAddress = await context.DHrLPresentAddresses.FirstOrDefaultAsync(x => x.ReferenceId == key);
        context.DHrLPresentAddresses.Remove(presentAddress);
        await context.SaveChangesAsync();
      }

      if (await context.DHrMPermanentAddresses.AnyAsync(x => x.ReferenceId == key))
      {
        DHrMPermanentAddress permanentAddress = await context.DHrMPermanentAddresses.FirstOrDefaultAsync(x => x.ReferenceId == key);
        context.DHrMPermanentAddresses.Remove(permanentAddress);
        await context.SaveChangesAsync();
      }

      if (await context.FEduAStudents.AnyAsync(x => x.StudentId == key))
      {
        FEduAStudent student = await context.FEduAStudents.FirstOrDefaultAsync(x => x.StudentId == key);
        context.FEduAStudents.Remove(student);
        await context.SaveChangesAsync();
      }
      else
      {
        return new TransectionModel(400, MessageConstants.UnauthorizedAttemptOfRecordDeleteError + "student information null");
      }

      // Commit the transaction
      await transaction.CommitAsync();

      return new TransectionModel(200); // Successfully updated one student
    }
    catch (Exception)
    {
      // Rollback the transaction in case of exception
      await transaction.RollbackAsync();
      return new TransectionModel(400);
    }
  }
}