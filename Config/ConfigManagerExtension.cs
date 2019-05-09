using System;
using System.Linq.Expressions;

namespace SSOAuth.Config
{
    public static class ConfigManagerExtension
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static bool IsNull(this object value)
        {
            return (value == null);
        }

        public static string GetMemberName<T>(Expression<Func<T>> memberExpression)
        {
            MemberExpression expressionBody = (MemberExpression)memberExpression.Body;
            return expressionBody.Member.Name;
        }

    }
}
