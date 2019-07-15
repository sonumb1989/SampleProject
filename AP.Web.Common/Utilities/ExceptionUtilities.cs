using AP.Web.Common.Exceptions;
using AP.Web.Common.Extensions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace AP.Web.Common.Utilities
{
    /// <summary>
    /// Exception utilities class
    /// </summary>
    public static class ExceptionUtilities
    {
        /// <summary>
        /// Adds exception data dictionary
        /// </summary>
        /// <param name="dic">Exception dictionary</param>
        /// <param name="msgId">Message id</param>
        /// <param name="statusCode">Status code</param>
        /// <param name="parameters">Parameters</param>
        public static void AddExceptionData(this IDictionary dic, string msgId, HttpStatusCode statusCode, IDictionary<string, string> parameters = null)
        {
            dic.Add("MessageId", msgId);
            dic.Add("StatusCode", statusCode);

            if (parameters.IsNotNull())
            {
                dic.Add("Params", parameters);
            }
        }

        /// <summary>
        /// Get exception resource
        /// </summary>
        /// <param name="messageId">Message id</param>
        /// <param name="parameters">Parameters</param>
        /// <returns>string</returns>
        public static string GetExceptionResource(string messageId, IDictionary<string, string> parameters = null)
        {
            string msg = messageId;

            if (IsValidMessage(messageId, "MsgExp").IsTrue())
            {
                msg = "Business exception";
            }

            if (parameters.IsNotNull())
            {
                var pattern = @"{(.*?)}";
                var count = Regex.Matches(msg, pattern).Count;

                if (count.IsEquals(parameters.Count))
                {
                    return string.Format(msg, parameters.Select(x => x.Value).ToArray());
                }
            }

            return msg;
        }

        /// <summary>
        /// Check if message is valid with specific pattern
        /// </summary>
        /// <param name="source">The input source</param>
        /// <param name="pattern">string</param>
        /// <returns>Is valid</returns>
        public static bool IsValidMessage(string source, string pattern = "MsgExp")
        {
            if (source.IsNotEmpty())
            {
                Match m = Regex.Match(source, pattern);
                if (m.Success && m.Value.IsNotNull())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Get stack trace exception
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <param name="resultDto">The resultDto</param>
        /// <param name="msgId">The messageId</param>
        /// <param name="code">The statusCode</param>
        /// <example>
        /// <code>
        ///         try {
        ///             int t = 0, x = 5, y = 5;
        ///             t = (x + y) / (x - y);
        ///         }
        ///        catch (Exception ex)
        ///         {
        ///             // return a ExceptionResultDto
        ///             ExceptionResultDto resultDto = new ExceptionResultDto();
        ///             var trace = ExceptionHandling.GetStackTraceException(ex, "Msg01", out resultDto);
        ///         }
        /// </code>
        /// </example>
        public static void GetStackTraceException(Exception ex, out ExceptionHandleInfo resultDto, string msgId = null, int? code = null)
        {
            var stackTrace = new StackTrace(ex, true);
            var stackFrames = stackTrace.GetFrames();
            var statusCode = (int)HttpStatusCode.InternalServerError;

            if (code.IsNotNull())
            {
                statusCode = (int)code;
            }
            else if (ex.Data["StatusCode"].IsNotNull())
            {
                statusCode = (int)ex.Data["StatusCode"];
            }

            if (stackFrames.IsNotNull() && stackFrames.Length > 0)
            {
                var stackFrame = stackFrames.First();
                resultDto = new ExceptionHandleInfo
                {
                    ApplicationId = stackFrame.GetType().GUID,
                    ErrorCode = statusCode,
                    MessageId = msgId,
                    FileName = Path.GetFileName(stackFrame.GetFileName()),
                    FullMessage = GetFullExceptionMessage(ex),
                    FuncCaller = (stackFrame.GetMethod() as MethodInfo).ToString(),
                    LineNumber = stackFrame.GetFileLineNumber(),
                    Messages = ex.Message,
                    TypeName = stackFrame.GetType().Name
                };
            }
            else
            {
                resultDto = new ExceptionHandleInfo
                {
                    ErrorCode = statusCode,
                    FileName = string.Empty,
                    FullMessage = GetFullExceptionMessage(ex),
                    FuncCaller = string.Empty,
                    LineNumber = 0,
                    MessageId = msgId ?? (string)ex.Data["MessageId"],
                    Messages = ex.Message,
                };
            }
        }

        /// <summary>
        /// Get full exception message
        /// </summary>
        /// <param name="ex">The exception</param>
        /// <returns>String value</returns>
        /// <example>
        ///     <code>
        ///         try {
        ///             int t = 0, x = 5, y = 5;
        ///             t = (x + y) / (x - y);
        ///         }
        ///        catch (Exception ex)
        ///         {
        ///             var msg = ExceptionHandling.GetFullExceptionMessage(ex);
        ///         }
        ///     </code>
        /// </example>
        public static string GetFullExceptionMessage(Exception ex)
        {
            StringBuilder sb = new StringBuilder();
            int count = 0;

            while (ex.IsNotNull())
            {
                for (int i = 0; i < count; i++)
                {
                    sb.Append("--");
                }

                if (count > 0)
                {
                    sb.Append('>');
                }

                sb.AppendLine(ex.Message);

                for (int i = 0; i < count; i++)
                {
                    sb.Append("--");
                }

                if (count > 0)
                {
                    sb.Append('>');
                }

                sb.AppendLine(ex.StackTrace);

                ex = ex.InnerException;
                count++;
            }

            return sb.ToString();
        }
    }
}
