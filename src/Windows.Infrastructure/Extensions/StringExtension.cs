namespace Windows.Infrastructure.Extensions
{
    public static class StringExtension
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string str)
        {
            return string.IsNullOrEmpty(str);
        }
        /// <summary>
        /// 是否为空or空格
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }
        /// <summary>
        /// 比较字符串(默认不区分大小写)
        /// </summary>
        /// <param name="strA"></param>
        /// <param name="strB"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        public static bool Compare(this string strA,string strB, bool ignoreCase = true)
        {
            return string.Compare(strA, strB, ignoreCase) > 0;
        }
    }
}
