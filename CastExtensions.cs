using System;
using System.Globalization;
using JetBrains.Annotations;

namespace ProgrammingWeapons
{
    public static class CastExtensions
    {
        /// <summary>
        /// Tries to convert string to double. If cant - double.NaN will be returned
        /// </summary>
        public static double ToDouble([CanBeNull] this string value) {
            double result;
            return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result) ? result : double.NaN;
        }


        /// <summary>
        /// Tries to convert string to long. Is can't - long.MinValue will be returned
        /// </summary>
        public static long ToLong([CanBeNull] this string value) {
            long result;
            return long.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result) ? result : long.MinValue;
        }


        /// <summary>
        /// Casts object to T. This method never returns null, but can cause exceptions.
        /// It's preferred than "(T) variable" casting, cause it logging any thrown exception.
        /// </summary>
        [NotNull] public static T CastTo<T>(this object o) {
            try {
                return (T) o;
            }
            catch (Exception e) {
                throw;
            }
        }



        /// <summary>
        /// Performs safety casting an object to class T. If casting can't be performed - An error message will be logged.
        /// </summary>
        public static bool TryCastTo<T>([CanBeNull] this object o, out T casted) where T : class {
            casted = null;
            if (o == null)
                return false;

            lock (o) {
                casted = o as T;

                if (casted != null) return true;
                return false;
            }
        }
    }
}
