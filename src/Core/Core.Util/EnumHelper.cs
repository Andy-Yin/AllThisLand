using System;
using System.Collections.Generic;
using System.Reflection;

namespace Core.Util
{
    /// <summary>
    /// 枚举操作
    /// </summary>
    public static class EnumHelper
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名或值,范例:Enum1枚举有成员A=0,则传入"A"或"0"获取 Enum1.A</param>
        public static TEnum Parse<TEnum>(object member)
        {
            string value = member.SafeString();
            if (string.IsNullOrWhiteSpace(value))
            {
                if (typeof(TEnum).IsGenericType)
                    return default(TEnum);
                throw new ArgumentNullException(nameof(member));
            }
            return (TEnum)Enum.Parse(CommonHelper.GetType<TEnum>(), value, true);
        }

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可,范例:Enum1枚举有成员A=0,则传入Enum1.A或0,获取成员名"A"</param>
        public static string GetName<TEnum>(object member)
        {
            return GetName(CommonHelper.GetType<TEnum>(), member);
        }

        /// <summary>
        /// 获取成员名
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可</param>
        public static string GetName(Type type, object member)
        {
            if (type == null)
                return string.Empty;
            if (member == null)
                return string.Empty;
            if (member is string)
                return member.ToString();
            if (type.GetTypeInfo().IsEnum == false)
                return string.Empty;
            return Enum.GetName(type, member);
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可，范例:Enum1枚举有成员A=0,可传入"A"、0、Enum1.A，获取值0</param>
        public static int GetValue<TEnum>(object member)
        {
            return GetValue(CommonHelper.GetType<TEnum>(), member);
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可</param>
        public static int GetValue(Type type, object member)
        {
            string value = member.SafeString();
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentNullException(nameof(member));
            return (int)System.Enum.Parse(type, member.ToString(), true);
        }

        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="member">成员名、值、实例均可</param>
        public static string GetDescription<TEnum>(object member)
        {
            return CommonHelper.GetDescription<TEnum>(GetName<TEnum>(member));
        }

        /// <summary>
        /// 获取描述,使用System.ComponentModel.Description特性设置描述
        /// </summary>
        /// <param name="type">枚举类型</param>
        /// <param name="member">成员名、值、实例均可</param>
        public static string GetDescription(Type type, object member)
        {
            return CommonHelper.GetDescription(type, GetName(type, member));
        }

        /// <summary>
        /// 获取名称集合
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        public static List<string> GetNames<TEnum>()
        {
            return GetNames(typeof(TEnum));
        }

        /// <summary>
        /// 获取名称集合
        /// </summary>
        /// <param name="type">枚举类型</param>
        public static List<string> GetNames(Type type)
        {
            type = CommonHelper.GetType(type);
            if (type.IsEnum == false)
                throw new InvalidOperationException($"类型 {type} 不是枚举");
            var result = new List<string>();
            foreach (var field in type.GetFields())
            {
                if (!field.FieldType.IsEnum)
                    continue;
                result.Add(field.Name);
            }
            return result;
        }

        /// <summary>
        /// 获取项字典,Key为Value,Value为Description
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        public static Dictionary<int, string> GetItems<TEnum>()
        {
            return GetItems(typeof(TEnum));
        }

        /// <summary>
        /// 获取项字典,Key为Value,Value为Description
        /// </summary>
        /// <param name="type">枚举类型</param>
        public static Dictionary<int, string> GetItems(Type type)
        {
            type = CommonHelper.GetType(type);
            if (type.IsEnum == false)
                throw new InvalidOperationException($"类型 {type} 不是枚举");
            var result = new Dictionary<int, string>();
            foreach (var value in Enum.GetValues(type))
            {
                result.Add((int)value, GetDescription(type, value));
            }
            return result;
        }
    }
}
