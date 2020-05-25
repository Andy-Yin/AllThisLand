using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using log4net;

namespace Core.Util
{
    public class Log4NetHelper
    {
        #region 变量定义

        //ILog对象
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //信息模板
        //private const string _ConversionPattern = "%n【记录时间】%date%n【描述】%message%n";
        #endregion

        #region 封装Log4net
        public async static void Debug(object message)
        {

            if (log.IsDebugEnabled)
            {
                await Task.Run(() => log.Debug(message));
            }
        }
        public async static void Debug(object message, Exception ex)
        {

            if (log.IsDebugEnabled)
            {
                await Task.Run(() => log.Debug(message, ex));
            }
        }
        public async static void DebugFormat(string format, params object[] args)
        {

            if (log.IsDebugEnabled)
            {
                await Task.Run(() => log.DebugFormat(format, args));
            }
        }
        public async static void Error(object message)
        {

            if (log.IsErrorEnabled)
            {
                await Task.Run(() => log.Error(message));
            }
        }
        public async static void Error(object message, Exception ex)
        {

            if (log.IsErrorEnabled)
            {
                await Task.Run(() => log.Error(message, ex));
            }
        }
        public async static void ErrorFormat(string format, params object[] args)
        {

            if (log.IsErrorEnabled)
            {
                await Task.Run(() => log.ErrorFormat(format, args));
            }
        }
        public async static void Fatal(object message)
        {

            if (log.IsFatalEnabled)
            {
                await Task.Run(() => log.Fatal(message));
            }
        }
        public async static void Fatal(object message, Exception ex)
        {

            if (log.IsFatalEnabled)
            {
                await Task.Run(() => log.Fatal(message, ex));
            }
        }
        public async static void FatalFormat(string format, params object[] args)
        {

            if (log.IsFatalEnabled)
            {
                await Task.Run(() => log.FatalFormat(format, args));
            }
        }
        public async static void Info(object message)
        {

            if (log.IsInfoEnabled)
            {
                await Task.Run(() => log.Info(message));
            }
        }
        public async static void Info(object message, Exception ex)
        {

            if (log.IsInfoEnabled)
            {
                await Task.Run(() => log.Info(message, ex));
            }
        }
        public async static void InfoFormat(string format, params object[] args)
        {

            if (log.IsInfoEnabled)
            {
                await Task.Run(() => log.InfoFormat(format, args));
            }
        }
        public async static void Warn(object message)
        {

            if (log.IsWarnEnabled)
            {
                await Task.Run(() => log.Warn(message));
            }
        }
        public async static void Warn(object message, Exception ex)
        {

            if (log.IsWarnEnabled)
            {
                await Task.Run(() => log.Warn(message, ex));
            }
        }
        public async static void WarnFormat(string format, params object[] args)
        {

            if (log.IsWarnEnabled)
            {
                await Task.Run(() => log.WarnFormat(format, args));
            }
        }
        #endregion

        #region 定义常规应用程序中未处理的异常信息记录方式
        public async static void LoadUnhandledException()
        {
            await Task.Run(() => AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler
                ((sender, e) => { log.Fatal("未处理的异常", e.ExceptionObject as Exception); }));
        }
        #endregion

        /// <summary>
        /// 输出指定信息到文本文件
        /// </summary>
        /// <param name="path">文本文件路径</param>
        /// <param name="msg">输出信息</param>
        public void WriteMessage(string path, string msg)
        {
            using (var fs = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine("{0}--{1}\n", msg, DateTime.Now);
                    sw.Flush();
                }
            }
        }

        /// <summary>
        /// 日志记录
        /// </summary>
        /// <param name="key">要记录的键</param>
        /// <param name="value">要记录的值</param>
        public async static void LogInfo(string key, string value)
        {
            var method = new System.Diagnostics.StackTrace().GetFrame(1).GetMethod();
            //调用日志的类名
            var className = method.ReflectedType?.FullName;
            //调用日志的方法名
            //String methodName = method.Name;
            //记录日志
            var log = LogManager.GetLogger(className, method.ReflectedType);
            await Task.Run(() => log.Info(key + "//" + value));
        }
    }
}