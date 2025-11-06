using System;
using System.Windows.Forms;

namespace BouncyShapes
{
    //this literally just starts the application
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}