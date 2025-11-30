using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class OtherServicesOfferedImage
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public int OtherServicesOfferedMasterId { get; set; }

    public int ImageTypeSystemId { get; set; }

    public string ImageName { get; set; } = null!;

    public string UploadImageName { get; set; } = null!;

    public virtual OtherServicesOffered OtherServicesOfferedMaster { get; set; } = null!;
}
