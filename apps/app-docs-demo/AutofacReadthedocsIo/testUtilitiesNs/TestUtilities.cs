using System;
using System.Diagnostics;

namespace AutofacReadthedocsIo.testUtilitiesNs
{
    public class TestUtilities
    {
        public static void Attach()
        {
            if (Debugger.IsAttached)
                Debugger.Break();
        }

        public static void WriteLine(object message)
        {
            if (message == null)
            {
                throw new ArgumentNullException("message");
            }
            Console.WriteLine(message);
        }

        internal static void Attach(object logObject)
        {
            WriteLine(logObject);
            if (Debugger.IsAttached)
                Debugger.Break();
        }
    }
}
