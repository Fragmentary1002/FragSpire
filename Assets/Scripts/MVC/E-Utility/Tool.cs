using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

namespace Frag
{
    public enum LogLevel
    {
        Info,
        Warning,
        Error
    }

    public class Tool:MonoSingleton<Tool>
	{
        // Start is called before the first frame update
        //public void LogStackMethod(int callFun=1)
        //{
        //    var stackTrace = new StackTrace();
        //    var frame = stackTrace.GetFrame(callFun); // 1表示上一层调用者，0表示当前方法  
        //    var methodName = frame.GetMethod().Name;
        //    var className = frame.GetMethod().DeclaringType.FullName;

        //    System.Console.WriteLine($"Called by method: {methodName} in class: {className}");
        //}
        

        public static void Log(string message, LogLevel level = LogLevel.Info)
        {
            switch (level)
            {
                case LogLevel.Info:
                    Debug.Log(message);
                    break;
                case LogLevel.Warning:
                    Debug.LogWarning(message);
                    break;
                case LogLevel.Error:
                    Debug.LogError(message);
                    break;
                default:
                    Debug.Log(message);
                    break;
            }
        }



    }

}