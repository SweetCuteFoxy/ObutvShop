using LibraryV1.Models;

namespace LibraryV1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            ApplicationConfiguration.Initialize();

            while (true)
            {
                var loginForm = new FormLogin();
                var result = loginForm.ShowDialog();
                if (result != DialogResult.OK)
                    break;

                var booksForm = new FormBooks(loginForm.AuthenticatedUser);
                var booksResult = booksForm.ShowDialog();
                if (booksResult != DialogResult.Retry)
                    break;
            }
        }
    }
}