using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Akshada.DTO.Enums
{
    public static class EnumHelper
    {
        public class EnumNameValue
        {
            public int Id { get; set; }
            public string Description { get; set; }
        }
        public class EnumList
        {
            public List<EnumNameValue> EnumNamesValues { get; set; }
        }
        public static EnumList EnumToJson<T>() where T : struct, Enum
        {
            var type = typeof(T);
            var values = Enum.GetValues<T>()
                .Select(x => new EnumNameValue
                {
                    Id = (int)(object)x,
                    Description = type.GetField(x.ToString())
                        .GetCustomAttribute<DescriptionAttribute>().Description
                });

            return new EnumList { EnumNamesValues = values.ToList() };
        }
    }
}
