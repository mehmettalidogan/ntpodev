using System;
using System.Windows.Forms;

namespace TestVirus
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                Application.SetHighDpiMode(HighDpiMode.SystemAware);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Uygulama başlatılırken hata oluştu:\n\n{ex.Message}\n\nStack Trace:\n{ex.StackTrace}", 
                    "Kritik Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}

