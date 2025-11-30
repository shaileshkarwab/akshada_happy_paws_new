using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
    public interface IContactAndAddressService
    {
        bool SaveContact(DTO_ImportantContact importantContact);

        PagedList<DTO_ImportantContact> GetAll(DTO_FilterAndPaging filterAndPaging);

        DTO_ImportantContact Reterive(string customerRowId);

        bool UpdateContactAndAddress(string customerRowId, DTO_ImportantContact updateEntity);

        bool DeleteContactAndAddress(string customerRowId);
    }
}
