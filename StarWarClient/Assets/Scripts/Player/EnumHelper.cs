using System;
using System.Reflection;

public class NameAttribute:Attribute
{
    public NameAttribute(string name)
    {
        this.name=name;
    }
    public string name{get;set;}
}

public class EnumHelper
{
    /// <summary>
    /// 获取枚举的Name特性值
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="thisTypeValue">枚举值</param>
    /// <returns></returns>
    public static string GetEnumName<T>(T thisTypeValue) where T : struct
    {
        Type type = thisTypeValue.GetType();
        if (!type.IsEnum) 　　 return "";
        FieldInfo info = type.GetField(thisTypeValue.ToString());
        NameAttribute attr = info.GetCustomAttribute(typeof(NameAttribute)) as NameAttribute;
        return attr == null ? "" : attr.name;
    }
}
