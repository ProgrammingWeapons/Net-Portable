using System;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace ProgrammingWeapons
{
    public static class NameOfExtensions
    {
        [NotNull] public static string NameOf<T, TRes>(this T objct, Expression<Func<T, TRes>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return ProgrammingWeapons.NameOf<T>.Member(expression);
        }

        [NotNull] public static string NameOf<T>(this T objct, Expression<Func<T, Action>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return ProgrammingWeapons.NameOf<T>.Member(expression);
        }

        [NotNull] public static string NameOf<T>(this T objct, Expression<Func<T, Action<T>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return ProgrammingWeapons.NameOf<T>.Member(expression);
        }

        [NotNull] public static string NameOf<T>(this T objct, Expression<Func<T, Func<T>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return ProgrammingWeapons.NameOf<T>.Member(expression);
        }
    }



    public static class NameOf<T>
    {
        [NotNull] public static string Member<TProp>(Expression<Func<T, TProp>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            Contract.Requires<ArgumentException>(expression.Body is MemberExpression, "Body of expression should by of type MemberExpression");
            return expression.Body.CastTo<MemberExpression>().Member.Name;
        }



        #region Method

        #region Without Parameters

        [NotNull] public static string Member(Expression<Func<T, Action>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        [NotNull] public static string Member<TRes>(Expression<Func<T, Func<TRes>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        #endregion


        #region T1

        [NotNull] public static string Member<T1>(Expression<Func<T, Action<T1>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        [NotNull] public static string Member<TRes, T1>(Expression<Func<T, Func<T1, TRes>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        #endregion


        #region T1, T2

        [NotNull] public static string Member<TRes, T1, T2>(Expression<Func<T, Action<T1, T2, TRes>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        [NotNull] public static string Member<TRes, T1, T2>(Expression<Func<T, Func<T1, T2, TRes>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        #endregion


        #region T1, T2, T3

        [NotNull] public static string Member<TRes, T1, T2, T3>(Expression<Func<T, Action<T1, T2, T3, TRes>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        [NotNull] public static string Member<TRes, T1, T2, T3>(Expression<Func<T, Func<T1, T2, T3, TRes>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        #endregion


        #region T1, T2, T3, T4

        [NotNull] public static string Member<TRes, T1, T2, T3, T4>(Expression<Func<T, Action<T1, T2, T3, T4, TRes>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        [NotNull] public static string Member<TRes, T1, T2, T3, T4>(Expression<Func<T, Func<T1, T2, T3, T4, TRes>>> expression) {
            Contract.Requires<ArgumentNullException>(expression != null);
            return member_info(expression.Body.CastTo<UnaryExpression>()).Name;
        }

        #endregion


        #endregion




        [NotNull] private static MemberInfo member_info(UnaryExpression unaryExpression) {
            Contract.Requires<ArgumentNullException>(unaryExpression != null);
            var methodCallExpression = (MethodCallExpression) unaryExpression.Operand;
            var methodInfoExpression = (ConstantExpression) methodCallExpression.Arguments.Last();
            var methodInfo = (MemberInfo) methodInfoExpression.Value;
            return methodInfo;
        }
    }
}
