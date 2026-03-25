using LibraryV1.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryV1
{
    public class FormLogin : Form
    {
        public User? AuthenticatedUser { get; private set; }

        private TextBox txtLogin = null!;
        private TextBox txtPassword = null!;
        private Label lblError = null!;

        public FormLogin()
        {
            InitUI();
        }

        private void InitUI()
        {
            Text = "Библиотека Читай-Город";
            Size = new Size(460, 380);
            StartPosition = FormStartPosition.CenterScreen;
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            BackColor = Color.White;
            Font = new Font("Times New Roman", 10);

            var panel = new Panel
            {
                Size = new Size(360, 280),
                Location = new Point(40, 30),
                BackColor = Color.FromArgb(240, 248, 255)
            };
            Controls.Add(panel);

            var lblTitle = new Label
            {
                Text = "Авторизация",
                Font = new Font("Times New Roman", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(74, 111, 165),
                AutoSize = true,
                Location = new Point(100, 15)
            };
            panel.Controls.Add(lblTitle);

            var lblLogin = new Label { Text = "Логин:", Location = new Point(30, 60), AutoSize = true };
            panel.Controls.Add(lblLogin);

            txtLogin = new TextBox
            {
                Location = new Point(30, 82),
                Size = new Size(300, 26),
                Font = new Font("Times New Roman", 11)
            };
            panel.Controls.Add(txtLogin);

            var lblPass = new Label { Text = "Пароль:", Location = new Point(30, 115), AutoSize = true };
            panel.Controls.Add(lblPass);

            txtPassword = new TextBox
            {
                Location = new Point(30, 137),
                Size = new Size(300, 26),
                UseSystemPasswordChar = true,
                Font = new Font("Times New Roman", 11)
            };
            panel.Controls.Add(txtPassword);

            lblError = new Label
            {
                Text = "",
                ForeColor = Color.Red,
                Location = new Point(30, 170),
                Size = new Size(300, 20),
                Font = new Font("Times New Roman", 9)
            };
            panel.Controls.Add(lblError);

            var btnLogin = new Button
            {
                Text = "Войти",
                Size = new Size(140, 38),
                Location = new Point(30, 200),
                BackColor = Color.FromArgb(74, 111, 165),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Times New Roman", 11, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += BtnLogin_Click;
            panel.Controls.Add(btnLogin);

            var btnGuest = new Button
            {
                Text = "Гость",
                Size = new Size(140, 38),
                Location = new Point(190, 200),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(74, 111, 165),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Times New Roman", 11),
                Cursor = Cursors.Hand
            };
            btnGuest.FlatAppearance.BorderColor = Color.FromArgb(74, 111, 165);
            btnGuest.Click += BtnGuest_Click;
            panel.Controls.Add(btnGuest);

            AcceptButton = btnLogin;
        }

        private void BtnLogin_Click(object? sender, EventArgs e)
        {
            lblError.Text = "";

            if (string.IsNullOrWhiteSpace(txtLogin.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
            {
                lblError.Text = "Заполните все поля";
                return;
            }

            using var db = new LibraryContext();
            var user = db.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Login == txtLogin.Text.Trim() && u.PasswordText == txtPassword.Text);

            if (user == null)
            {
                lblError.Text = "Неверный логин или пароль";
                return;
            }

            AuthenticatedUser = user;
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BtnGuest_Click(object? sender, EventArgs e)
        {
            AuthenticatedUser = null;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
