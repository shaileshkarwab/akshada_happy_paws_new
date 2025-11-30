using System;
using System.Collections.Generic;

namespace Akshada.EFCore.DbModels;

public partial class WebsiteService
{
    public int Id { get; set; }

    public string RowId { get; set; } = null!;

    public string Body { get; set; } = null!;

    public string JsonData { get; set; } = null!;

    public virtual ICollection<WebsiteServiceProcess> WebsiteServiceProcessUpdatedByNavigations { get; set; } = new List<WebsiteServiceProcess>();

    public virtual ICollection<WebsiteServiceProcess> WebsiteServiceProcessWebsiteServiceMasters { get; set; } = new List<WebsiteServiceProcess>();
}
