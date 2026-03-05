namespace ObutvShop;

static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();

        var formLogin = new FormLogin();
        if (formLogin.ShowDialog() == DialogResult.OK)
        {
            Application.Run(new FormProducts(formLogin.AuthenticatedUser));
        }
    }
}