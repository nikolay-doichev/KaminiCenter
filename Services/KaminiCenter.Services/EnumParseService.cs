namespace KaminiCenter.Services
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Text;

    public class EnumParseService : IEnumParseService
    {
        public string GetEnumDescription(string name, Type typeOfEnum)
        {
            FieldInfo specificField = typeOfEnum.GetField(name);

            if (specificField != null)
            {
                DescriptionAttribute attr =
                       Attribute.GetCustomAttribute(
                           specificField,
                           typeof(DescriptionAttribute)) as DescriptionAttribute;

                if (attr != null)
                {
                    return attr.Description;
                }
            }

            return null;
        }
    }
}
