using System;
using System.Collections.Generic;
using pbERP.Domain.Models.AGeneralConfig;

namespace pbERP.Domain.Models.FEducation;

public partial class FEduAStudent
{
    public long StudentId { get; set; }

    public string StudentCode { get; set; }

    public string StudentName { get; set; }

    public string StudentNameInLocal { get; set; }

    public string StudentFathersName { get; set; }

    public string StudentFathersNameInLocal { get; set; }

    public string StudentMothersName { get; set; }

    public string StudentMothersNameInLocal { get; set; }

    public long? GenderId { get; set; }

    public long? BloodGroupId { get; set; }

    public long? ReligionId { get; set; }

    public byte[] StudentPhoto { get; set; }

    public virtual AGenConfigFBloodGroup BloodGroup { get; set; }

    public virtual AGenConfigEGender Gender { get; set; }

    public virtual AGenConfigGReligion Religion { get; set; }
}
