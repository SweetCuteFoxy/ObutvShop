using Microsoft.EntityFrameworkCore;
using ObutvShop.Models;

namespace ObutvShop;

public partial class FormLogin : Form
{
    public User? AuthenticatedUser { get; private set; }

    public FormLogin()
    {
        InitializeComponent();
        CenterPanel();
        Resize += (_, _) => CenterPanel();
    }

    private void CenterPanel()
    {
        panelMain.Left = (ClientSize.Width - panelMain.Width) / 2;
        panelMain.Top = (ClientSize.Height - panelMain.Height) / 2;
    }

    private void ButtonLogin_Click(object? sender, EventArgs e)
    {
        string login = textBoxLogin.Text.Trim();
        string password = textBoxPassword.Text;

        if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
        {
            labelError.Text = "Введите логин и пароль";
            return;
        }

        using var db = new ObutvShopContext();
        var user = db.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Login == login && u.Password == password);

        if (user == null)
        {
            labelError.Text = "Неверный логин или пароль";
            return;
        }

        if (!user.IsActive)
        {
            labelError.Text = "Учётная запись заблокирована";
            return;
        }

        AuthenticatedUser = user;
        DialogResult = DialogResult.OK;
        Close();
    }

    private void ButtonGuest_Click(object? sender, EventArgs e)
    {
        AuthenticatedUser = null;
        DialogResult = DialogResult.OK;
        Close();
    }
}
