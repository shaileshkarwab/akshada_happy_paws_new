using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Enums
{
    public enum SystemParameterEnum
    {
        [Description("Service Areas or Locations")]
        Location=1,

        [Description("Pets Colours")]
        Colours=2,

        [Description("Pet Breeds")]
        Breeds = 3,

        [Description("Pet Walking Services")]
        PetWalkingServices = 4,

        [Description("Service Frequency In A Day")]
        ServiceFrequencyInADay = 5,

        [Description("Pet Food Services")]
        PetFoodServices = 6,

        [Description("Type Of Images To Upload For Services")]
        TypeOfImagesToUploadForServices = 7,

        [Description("Pet Grooming Services")]
        PetGroomingServices = 8,

        [Description("Pet Vaccination/Medicine Name")]
        PetVaccinationMedicineName = 9,

        [Description("Type Of Holidays And Special Days")]
        TypeOfHolidaysAndSpecialDays = 10,

        [Description("Contact Types")]
        ContactTypes = 11,

        [Description("Contact Address Type")]
        ContactAddressType = 12,

        [Description("Nature Of Pet")]
        NatureOfPet = 13,

        [Description("Standard Reason For Not Providing Service")]
        StandardReasonForNotProvidingService = 14,

        [Description("Message Template Variables")]
        MessageTemplateVariables = 15,

        [Description("Service Time Slots")]
        ServiceTimeSlots = 16,
    }
}
