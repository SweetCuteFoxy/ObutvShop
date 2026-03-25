using LibraryV1.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryV1
{
    public class FormLoans : Form
    {
        private User? currentUser;
        private DataGridView dgvLoans = null!;
        private ComboBox cmbStatus = null!;
        private TextBox txtSearch = null!;

        public FormLoans(User? user)
        {
            currentUser = user;
            InitUI();
            LoadLoans();
        }

        private void InitUI()
        {
            Text = "Выдачи книг";
            Size = new Size(950, 550);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Times New Roman", 10);

            var filterPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.FromArgb(240, 248, 255)
            };
            Controls.Add(filterPanel);

            txtSearch = new TextBox
            {
                Location = new Point(10, 12),
                Size = new Size(200, 26),
                PlaceholderText = "Поиск по читателю...",
                Font = new Font("Times New Roman", 10)
            };
            txtSearch.TextChanged += (s, e) => ApplyFilter();
            filterPanel.Controls.Add(txtSearch);

            cmbStatus = new ComboBox
            {
                Location = new Point(220, 12),
                Size = new Size(150, 26),
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Times New Roman", 10)
            };
            cmbStatus.Items.AddRange(new object[] { "Все", "На руках", "Возвращена" });
            cmbStatus.SelectedIndex = 0;
            cmbStatus.SelectedIndexChanged += (s, e) => ApplyFilter();
            filterPanel.Controls.Add(cmbStatus);

            string role = currentUser?.Role?.Name ?? "";
            if (role == "Библиотекарь" || role == "Администратор")
            {
                var btnIssue = new Button
                {
                    Text = "Выдать книгу",
                    Size = new Size(120, 28),
                    Location = new Point(400, 10),
                    BackColor = Color.FromArgb(74, 111, 165),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Times New Roman", 9, FontStyle.Bold),
                    Cursor = Cursors.Hand
                };
                btnIssue.FlatAppearance.BorderSize = 0;
                btnIssue.Click += BtnIssue_Click;
                filterPanel.Controls.Add(btnIssue);

                var btnReturn = new Button
                {
                    Text = "Вернуть",
                    Size = new Size(90, 28),
                    Location = new Point(530, 10),
                    BackColor = Color.FromArgb(100, 140, 80),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Times New Roman", 9),
                    Cursor = Cursors.Hand
                };
                btnReturn.FlatAppearance.BorderSize = 0;
                btnReturn.Click += BtnReturn_Click;
                filterPanel.Controls.Add(btnReturn);
            }

            dgvLoans = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                Font = new Font("Times New Roman", 9)
            };
            Controls.Add(dgvLoans);
            dgvLoans.BringToFront();
        }

        private List<BookLoan> allLoans = new();

        private void LoadLoans()
        {
            using var db = new LibraryContext();
            allLoans = db.BookLoans
                .Include(l => l.User)
                .Include(l => l.Book)
                .Include(l => l.Status)
                .OrderByDescending(l => l.LoanDate)
                .ToList();
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var filtered = allLoans.AsEnumerable();

            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                var q = txtSearch.Text.Trim().ToLower();
                filtered = filtered.Where(l =>
                    (l.User?.FullName ?? "").ToLower().Contains(q) ||
                    (l.User?.LibraryCard ?? "").ToLower().Contains(q));
            }

            if (cmbStatus.SelectedIndex > 0)
            {
                var status = cmbStatus.SelectedItem?.ToString();
                filtered = filtered.Where(l => l.Status?.Name == status);
            }

            var data = filtered.Select(l => new
            {
                ID = l.Id,
                Читатель = l.User?.FullName ?? "",
                Билет = l.User?.LibraryCard ?? "",
                Книга = l.Book?.Title ?? "",
                ISBN = l.Book?.Isbn ?? "",
                ДатаВыдачи = l.LoanDate.ToShortDateString(),
                Вернуть_до = l.ReturnDateExpected.ToShortDateString(),
                Возвращена = l.ReturnDateActual?.ToShortDateString() ?? "-",
                Статус = l.Status?.Name ?? ""
            }).ToList();

            dgvLoans.DataSource = data;
        }

        private void BtnIssue_Click(object? sender, EventArgs e)
        {
            var form = new FormIssueLoan();
            if (form.ShowDialog() == DialogResult.OK)
                LoadLoans();
        }

        private void BtnReturn_Click(object? sender, EventArgs e)
        {
            if (dgvLoans.SelectedRows.Count == 0) return;
            var loanId = (int)dgvLoans.SelectedRows[0].Cells["ID"].Value;

            using var db = new LibraryContext();
            var loan = db.BookLoans.Include(l => l.Book).FirstOrDefault(l => l.Id == loanId);
            if (loan == null || loan.ReturnDateActual != null) return;

            loan.ReturnDateActual = DateTime.Now;
            var statusReturned = db.LoanStatuses.FirstOrDefault(s => s.Name == "Возвращена");
            if (statusReturned != null)
                loan.StatusId = statusReturned.Id;

            if (loan.Book != null)
                loan.Book.AvailableCopies++;

            db.SaveChanges();
            LoadLoans();
            MessageBox.Show("Книга возвращена", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
