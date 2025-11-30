using Akshada.DTO.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.Services.Interfaces
{
   public interface IHolidayService
    {
        List<DTO_HolidaySchedule> GetAllHolidaysForYear(int year);

        DTO_HolidaySchedule GetDetailByDate(string selectedDate);

        bool DeleteHoliday(string rowId);

        bool UpdateHoliday(string rowId, DTO_HolidaySchedule holidaySchedule);

        bool SaveHoliday(DTO_HolidaySchedule holidaySchedule);
    }
}
