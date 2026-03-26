using LibraryApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryApp
{
    public class FormLoans : Form
    {
        private User? currentUser;
        private DataGridView dgv = null!;
        private ComboBox cmbStatus = null!;
        private TextBox txtSearch = null!;
        private List<BookLoan> allLoans = new();

        public FormLoans(User? user)
        {
            currentUser = user;
            InitUI();
            LoadLoans();
        }

        private void InitUI()
        {
            Text = "Выдачи книг";
            Size = new Size(900, 500);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Segoe UI", 9);

            var top = new Panel { Dock = DockStyle.Top, Height = 45, BackColor = Color.FromArgb(235, 240, 248) };
            Controls.Add(top);

            txtSearch = new TextBox
            {
                Location = new Point(10, 10),
                Size = new Size(180, 24),
                PlaceholderText = "Поиск по читателю..."
            };
            txtSearch.TextChanged += (s, e) => Filter();
            top.Controls.Add(txtSearch);

            cmbStatus = new ComboBox
            {
                Location = new Point(200, 10),
                Size = new Size(130, 24),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbStatus.Items.AddRange(new object[] { "Все", "На руках", "Возвращена" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (s, e) => Filter();
            top.Controls.Add(cmbStatus);

            string r = currentUser?.Role?.Name ?? "";
            if (r == "Библиотекарь" || r == "Администратор")
            {
                var btnIssue = new Button
                {
                    Text = "Выдать книгу",
                    Size = new Size(110, 26),
                    Location = new Point(360, 9),
                    BackColor = Color.FromArgb(50, 80, 130),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btnIssue.FlatAppearance.BorderSize = 0;
                btnIssue.Click += (s, e) =>
                {
                    if (new FormIssueLoan().ShowDialog() == DialogResult.OK) LoadLoans();
                };
                top.Controls.Add(btnIssue);

                var btnReturn = new Button
                {
                    Text = "Вернуть",
                    Size = new Size(80, 26),
                    Location = new Point(480, 9),
                    BackColor = Color.SeaGreen,
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Cursor = Cursors.Hand
                };
                btnReturn.FlatAppearance.BorderSize = 0;
                btnReturn.Click += BtnReturn_Click;
                top.Controls.Add(btnReturn);
            }

            dgv = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None
            };
            Controls.Add(dgv);
            dgv.BringToFront();
        }

        private void LoadLoans()
        {
            using var db = new LibraryContext();
            allLoans = db.BookLoans
                .Include(l => l.User)
                .Include(l => l.Book)
                .Include(l => l.Status)
                .OrderByDescending(l => l.LoanDate)
                .ToList();
            Filter();
        }

        private void Filter()
        {
            var list = allLoans.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                var q = txtSearch.Text.Trim().ToLower();
                list = list.Where(l => (l.User?.FullName ?? "").ToLower().Contains(q)
                    || (l.User?.LibraryCard ?? "").ToLower().Contains(q));
            }
            if (cmbStatus.SelectedIndex > 0)
                list = list.Where(l => l.Status?.Name == cmbStatus.SelectedItem?.ToString());

            dgv.DataSource = list.Select(l => new
            {
                ID = l.Id,
                Читатель = l.User?.FullName ?? "",
                Билет = l.User?.LibraryCard ?? "",
                Книга = l.Book?.Title ?? "",
                Выдана = l.LoanDate.ToShortDateString(),
                Вернуть_до = l.ReturnDateExpected.ToShortDateString(),
                Возвращена = l.ReturnDateActual?.ToShortDateString() ?? "—",
                Статус = l.Status?.Name ?? ""
            }).ToList();
        }

        private void BtnReturn_Click(object? s, EventArgs e)
        {
            if (dgv.SelectedRows.Count == 0) return;
            int id = (int)dgv.SelectedRows[0].Cells["ID"].Value;

            using var db = new LibraryContext();
            var loan = db.BookLoans.Include(l => l.Book).FirstOrDefault(l => l.Id == id);
            if (loan == null || loan.ReturnDateActual != null)
            {
                MessageBox.Show("Эта книга уже возвращена");
                return;
            }

            loan.ReturnDateActual = DateTime.Now;
            var returned = db.LoanStatuses.FirstOrDefault(s => s.Name == "Возвращена");
            if (returned != null) loan.StatusId = returned.Id;
            if (loan.Book != null) loan.Book.AvailableCopies++;
            db.SaveChanges();
            LoadLoans();
        }
    }
}
