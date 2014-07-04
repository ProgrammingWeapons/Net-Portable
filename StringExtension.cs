namespace ProgrammingWeapons
{
    public static class StringExtension
    {
        public static bool IsNullEmptyOrWhitespace(this string str) {
            return string.IsNullOrWhiteSpace(str);
        }
    }
}