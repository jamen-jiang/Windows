using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Windows.Infrastructure.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// 根据枚举值拿到枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        public static T GetEnum<T>(this int itemValue) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), Enum.GetName(typeof(T), itemValue));
        }
        /// <summary>
        /// 根据名称拿到枚举
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static T GetEnum<T>(this string itemName) where T : Enum
        {
            return (T)Enum.Parse(typeof(T), itemName);
        }
        /// <summary>
        /// 根据枚举值拿到枚举名称
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        public static string GetEnumName<T>(this int itemValue) where T : Enum
        {
            return Enum.GetName(typeof(T), itemValue);
        }
        /// <summary>
        /// 根据名称拿到枚举值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static int GetEnumValue<T>(this string itemName) where T : Enum
        {
            return itemName.GetEnum<T>().GetHashCode();
        }
        /// <summary>
        ///  根据枚举值获取描述
        /// </summary>
        /// <param name="itemValue"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this int itemValue) where T : Enum
        {
            var item= itemValue.GetEnum<T>();
            Type type = item.GetType();
            MemberInfo[] memInfo = type.GetMember(item.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return item.ToString();//如果不存在描述，则返回枚举名称
        }
        /// <summary>
        ///  根据枚举值获取描述
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public static string GetDescription<T>(this string itemName) where T : Enum
        {
            var item = itemName.GetEnum<T>();
            Type type = item.GetType();
            MemberInfo[] memInfo = type.GetMember(item.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return item.ToString();//如果不存在描述，则返回枚举名称
        }
        /// <summary>
        /// 枚举获取描述
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static string GetDescription(this Enum item)
        {
            Type type = item.GetType();
            MemberInfo[] memInfo = type.GetMember(item.ToString());
            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (attrs != null && attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }
            return item.ToString();//如果不存在描述，则返回枚举名称
        }
    }
}
