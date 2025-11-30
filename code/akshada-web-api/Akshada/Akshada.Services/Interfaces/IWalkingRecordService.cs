using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
   public interface IWalkingRecordService
    {
        PagedList<DTO_WalkingRecordList> GetAllRecords(DTO_FilterAndPaging filterAndPaging);

        DTO_WalkingRecord GetById(string rowId);

        bool SaveWalkingServiceRecord(string customerRowId, string petRowId, DTO_WalkingServiceRecord walkingRecord);

        bool DeleteWalkingServiceRecord(string rowId);
    }
}
