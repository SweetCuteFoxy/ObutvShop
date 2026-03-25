using LibraryV1.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryV1
{
    public class FormMyLoans : Form
    {
        private User currentUser;
        private DataGridView dgvLoans = null!;

        public FormMyLoans(User user)
        {
            currentUser = user;
            InitUI();
            LoadMyLoans();
        }

        private void InitUI()
        {
            Text = "Мои книги";
            Size = new Size(800, 450);
            StartPosition = FormStartPosition.CenterScreen;
            BackColor = Color.White;
            Font = new Font("Times New Roman", 10);

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
        }

        private void LoadMyLoans()
        {
            using var db = new LibraryContext();
            var loans = db.BookLoans
                .Include(l => l.Book)
                .Include(l => l.Status)
                .Where(l => l.UserId == currentUser.Id)
                .OrderByDescending(l => l.LoanDate)
                .Select(l => new
                {
                    Книга = l.Book!.Title,
                    ISBN = l.Book.Isbn,
                    ДатаВыдачи = l.LoanDate.ToShortDateString(),
                    Вернуть_до = l.ReturnDateExpected.ToShortDateString(),
                    Возвращена = l.ReturnDateActual != null ? l.ReturnDateActual.Value.ToShortDateString() : "-",
                    Статус = l.Status!.Name
                })
                .ToList();

            dgvLoans.DataSource = loans;
        }
    }
}
