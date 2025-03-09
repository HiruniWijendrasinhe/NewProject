using System.Diagnostics;
using System.Windows;

namespace NewProject
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                using (var context = new Datacontextnew())
                {
                    context.Database.EnsureCreated();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Database initialization failed: {ex.Message}");
            }
            

            
            DatahomeWindow mainWindow = new DatahomeWindow();
            mainWindow.Show();
        }
    }
}
