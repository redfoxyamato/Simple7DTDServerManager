using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Simple7DTDServer
{
    public class Util
    {
        public static void SetDoubleBuffering(Control c, bool flag)
        {
            c.GetType().InvokeMember(
               "DoubleBuffered",
               BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty,
               null,
               c,
               new object[] { flag });
        }
        [Conditional("DEBUG")]
        public static void WriteConsole(object obj)
        {
            Console.WriteLine(obj);
        }

        public static void SetValue(NumericUpDown obj,decimal value)
        {
            decimal val = Math.Min(Math.Max(obj.Minimum, value), obj.Maximum);
            obj.Value = val;
        }
    }
}
