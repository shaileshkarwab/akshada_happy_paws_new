using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Enums
{
    public enum NotificationEnum
    {
        [Description("Vaccination Notification")]
        VaccinationNotification = 1,

        [Description("Holiday/Festival Notification")]
        HolidayFestivalNotification = 2,

        [Description("Pet Birthday Notification")]
        PetBirthdayNotification = 3,

        [Description("Walking Service End Notification")]
        WalkingServiceEndNotification = 4,
    }

    public enum EmaiNotificationTemplate
    {
        [Description("Pet Walking Service Notification")]
        PetWalkingServiceNotification = 1,
    }
}
