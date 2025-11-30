using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class WalkingServiceRecordImage
{
    public int Id { get; set; }

    public string? RowId { get; set; }

    public int WalkingServiceRecordMasterId { get; set; }

    public int ImageUploadSystemId { get; set; }

    public string ImageName { get; set; } = null!;

    public double Lattitude { get; set; }

    public double Longitude { get; set; }

    public DateTime RecordTime { get; set; }

    public virtual SystemParameter ImageUploadSystem { get; set; } = null!;

    public virtual WalkingServiceRecord WalkingServiceRecordMaster { get; set; } = null!;
}
