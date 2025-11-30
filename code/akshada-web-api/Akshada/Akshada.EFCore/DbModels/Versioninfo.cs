using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class Versioninfo
{
    public long Version { get; set; }

    public DateTime? AppliedOn { get; set; }

    public string? Description { get; set; }
}
