using System;

namespace AP.Web.Common.Validations
{
    /// <summary>
    ///  ValidationId Generator
    ///  Provide method for creating ValidationId
    /// </summary>
    public static class ValidationIdGenerator
    {
        private const ulong MinHexValue = 0x100000000;

        /// <summary>
        /// Generate ValidationId
        /// </summary>
        /// <returns>string</returns>
        public static string Gen()
        {
            ulong currentTime = ulong.Parse(DateTime.Now.ToString("HmmssFFF"));
            return (MinHexValue ^ currentTime).ToString("X");
        }
    }
}
