using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace CP.NLayer.Common
{
    public class EnumHelper
    {
        public static string StringValueOf(Enum value)
        {
            return StringValueOf((object)value);
        }

        public static string StringValueOf(object value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return (attributes != null && attributes.Length > 0) ? attributes[0].Description : value.ToString();
        }

        public static object EnumValueOf(string value, Type enumType, bool isNameDesc = true)
        {
            string[] names = Enum.GetNames(enumType);
            foreach (string name in names)
            {
                if (isNameDesc && StringValueOf((Enum)Enum.Parse(enumType, name)).Equals(value))
                    return Enum.Parse(enumType, name);
                else if (Enum.Parse(enumType, name).ToString().Equals(value))
                    return Enum.Parse(enumType, name);
            }

            throw new ArgumentException("The string is not a description or value of the specified enum.");
        }

        public static string StringValueOf(byte value, Type enumType, string seperator = " ")
        {
            return StringValueOf((uint)value, enumType, seperator);
        }

        public static string StringValueOf(ushort value, Type enumType, string seperator = " ")
        {
            return StringValueOf((uint)value, enumType, seperator);
        }

        public static bool IsFlagSelected(ushort flag, ushort value)
        {
            return IsFlagSelected((uint)flag, (uint)value);
        }

        public static bool IsFlagSelected(byte flag, byte value)
        {
            return IsFlagSelected((uint)flag, (uint)value);
        }

        public static bool IsFlagSelected(uint flag, uint value)
        {
            return (value & (uint)flag) == flag;
        }

        public static string StringValueOf(uint value, Type enumType, string seperator = " ")
        {
            string[] names = Enum.GetNames(enumType);

            object tmpEnumMember = null;
            uint tmpInMemberValue = 0;
            string returnStr = string.Empty;
            List<string> listOfSelectedValues = new List<string>();
            foreach (string name in names)
            {
                tmpEnumMember = Enum.Parse(enumType, name);
                tmpInMemberValue = (uint)tmpEnumMember;

                if ((tmpInMemberValue & value) == tmpInMemberValue)
                {
                    listOfSelectedValues.Add(StringValueOf(tmpEnumMember));
                }
            }

            returnStr = string.Join(seperator, listOfSelectedValues);
            return returnStr;
        }

        public static IList GetList(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            ArrayList list = new ArrayList();
            Array enumValues = Enum.GetValues(type);

            foreach (var value in enumValues)
            {
                list.Add(new KeyValuePair<int, string>((int)value, StringValueOf(value)));
            }

            return list;
        }

        public static List<KeyValuePair<int, string>> GetKeyValuePairList(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var list = new List<KeyValuePair<int, string>>();
            Array enumValues = Enum.GetValues(type);

            foreach (var value in enumValues)
            {
                list.Add(new KeyValuePair<int, string>((int)value, StringValueOf(value)));
            }

            return list;
        }

        public static IList<string> GetStringList(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            var list = new List<string>();
            Array enumValues = Enum.GetValues(type);

            foreach (var value in enumValues)
            {
                list.Add(StringValueOf(value));
            }

            return list;
        }
    }
}