using System;
using System.Windows.Forms;

namespace InventorySystemCsharp
{
    static class Program
    {
        [STAThread] // Required for Windows Forms applications
        static void Main()
        {
            // Enable visual styles for modern UI appearance
            Application.EnableVisualStyles();

            // Ensure consistent text rendering across controls
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the application with the Login form
            Application.Run(new Login());
        }
    }
}
