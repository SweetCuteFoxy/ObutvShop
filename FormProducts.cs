using Microsoft.EntityFrameworkCore;
using ObutvShop.Models;

namespace ObutvShop;

public partial class FormProducts : Form
{
    private readonly User? _currentUser;
    private List<Product> _allProducts = new();
    private int _totalCount;
    private bool _isLoaded;

    public FormProducts(User? currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();

        if (_currentUser != null)
            labelUserInfo.Text = $"{_currentUser.FullName} ({_currentUser.Role.Name})";
        else
            labelUserInfo.Text = "Гость";

        comboBoxFilter.Items.AddRange(new object[]
        {
            "Все",
            "0-5%",
            "5-15%",
            "15-30%",
            "30-70%",
            "70-100%"
        });
        comboBoxFilter.SelectedIndex = 0;

        dataGridViewProducts.CellFormatting += DataGridView_CellFormatting;
        Load += (_, _) => { LoadProducts(); _isLoaded = true; };
    }

    private void LoadProducts()
    {
        try
        {
            using var db = new ObutvShopContext();
            _allProducts = db.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .Include(p => p.Manufacturer)
                .OrderBy(p => p.Name)
                .ToList();
            _totalCount = _allProducts.Count;
            ApplyFilters();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки: " + ex.Message, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplyFilters()
    {
        var filtered = _allProducts.AsEnumerable();

        string search = textBoxSearch.Text.Trim();
        if (search.Length > 0)
        {
            filtered = filtered.Where(p =>
                p.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Article.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                (p.Description ?? "").Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Manufacturer.Name.Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        int fi = comboBoxFilter.SelectedIndex;
        filtered = fi switch
        {
            1 => filtered.Where(p => p.DiscountPct >= 0 && p.DiscountPct < 5),
            2 => filtered.Where(p => p.DiscountPct >= 5 && p.DiscountPct < 15),
            3 => filtered.Where(p => p.DiscountPct >= 15 && p.DiscountPct < 30),
            4 => filtered.Where(p => p.DiscountPct >= 30 && p.DiscountPct < 70),
            5 => filtered.Where(p => p.DiscountPct >= 70 && p.DiscountPct <= 100),
            _ => filtered
        };

        var list = filtered.ToList();

        dataGridViewProducts.DataSource = list.Select(p => new
        {
            Артикул = p.Article,
            Наименование = p.Name,
            Категория = p.Category.Name,
            Производитель = p.Manufacturer.Name,
            Цена = p.Price,
            Скидка = p.DiscountPct,
            СоСкидкой = p.PriceDiscounted,
            Остаток = p.StockQty
        }).ToList();

        labelCount.Text = $"{list.Count} из {_totalCount}";
    }

    private void DataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (!dataGridViewProducts.Columns.Contains("Скидка")) return;

        var val = dataGridViewProducts.Rows[e.RowIndex].Cells["Скидка"].Value;
        if (val is decimal d && d > 15)
        {
            e.CellStyle.BackColor = Color.FromArgb(46, 139, 87);
            e.CellStyle.ForeColor = Color.White;
            e.CellStyle.SelectionBackColor = Color.FromArgb(36, 110, 70);
            e.CellStyle.SelectionForeColor = Color.White;
        }
    }

    private void TextBoxSearch_TextChanged(object? sender, EventArgs e)
    {
        if (_isLoaded) ApplyFilters();
    }

    private void ComboBoxFilter_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_isLoaded) ApplyFilters();
    }

    private void ButtonOrders_Click(object? sender, EventArgs e)
    {
        using var form = new FormOrders(_currentUser);
        form.ShowDialog();
    }
}
