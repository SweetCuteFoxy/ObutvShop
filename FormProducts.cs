using Microsoft.EntityFrameworkCore;
using ObutvShop.Models;

namespace ObutvShop;

public partial class FormProducts : Form
{
    private readonly User? _currentUser;
    private List<Product> _allProducts = new();
    private int _totalCount;

    public FormProducts(User? currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();
        SetupUserInfo();
        SetupFilter();
        Load += FormProducts_Load;
    }

    private void SetupUserInfo()
    {
        if (_currentUser != null)
            labelUserInfo.Text = $"{_currentUser.FullName} ({_currentUser.Role.Name})";
        else
            labelUserInfo.Text = "Гость";
    }

    private void SetupFilter()
    {
        comboBoxFilter.Items.Add("Все скидки");
        comboBoxFilter.Items.Add("0 – 5%");
        comboBoxFilter.Items.Add("5 – 15%");
        comboBoxFilter.Items.Add("15 – 30%");
        comboBoxFilter.Items.Add("30 – 70%");
        comboBoxFilter.Items.Add("70 – 100%");
        comboBoxFilter.SelectedIndex = 0;
    }

    private void FormProducts_Load(object? sender, EventArgs e)
    {
        LoadProducts();
    }

    private void LoadProducts()
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

    private void ApplyFilters()
    {
        var filtered = _allProducts.AsEnumerable();

        // Поиск по тексту
        string search = textBoxSearch.Text.Trim().ToLower();
        if (!string.IsNullOrEmpty(search))
        {
            filtered = filtered.Where(p =>
                p.Name.ToLower().Contains(search) ||
                p.Article.ToLower().Contains(search) ||
                (p.Description ?? "").ToLower().Contains(search) ||
                p.Manufacturer.Name.ToLower().Contains(search));
        }

        // Фильтр по скидке
        int filterIndex = comboBoxFilter.SelectedIndex;
        filtered = filterIndex switch
        {
            1 => filtered.Where(p => p.DiscountPct >= 0 && p.DiscountPct < 5),
            2 => filtered.Where(p => p.DiscountPct >= 5 && p.DiscountPct < 15),
            3 => filtered.Where(p => p.DiscountPct >= 15 && p.DiscountPct < 30),
            4 => filtered.Where(p => p.DiscountPct >= 30 && p.DiscountPct < 70),
            5 => filtered.Where(p => p.DiscountPct >= 70 && p.DiscountPct <= 100),
            _ => filtered
        };

        var list = filtered.ToList();
        DisplayProducts(list);
    }

    private void DisplayProducts(List<Product> products)
    {
        dataGridViewProducts.DataSource = null;
        var displayData = products.Select(p => new
        {
            Артикул = p.Article,
            Наименование = p.Name,
            Категория = p.Category.Name,
            Производитель = p.Manufacturer.Name,
            Цена = p.Price,
            Скидка = p.DiscountPct,
            ЦенаСоСкидкой = p.PriceDiscounted,
            Остаток = p.StockQty,
            Поставщик = p.Supplier.Name
        }).ToList();

        dataGridViewProducts.DataSource = displayData;

        // Подсветка строк со скидкой > 15%
        dataGridViewProducts.CellFormatting -= DataGridView_CellFormatting;
        dataGridViewProducts.CellFormatting += DataGridView_CellFormatting;

        labelCount.Text = $"Записей: {products.Count} из {_totalCount}";
    }

    private void DataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;

        var row = dataGridViewProducts.Rows[e.RowIndex];
        var discountCell = row.Cells["Скидка"];
        if (discountCell.Value is decimal discount && discount > 15)
        {
            row.DefaultCellStyle.BackColor = Color.FromArgb(46, 139, 87);
            row.DefaultCellStyle.ForeColor = Color.White;
        }
    }

    private void TextBoxSearch_TextChanged(object? sender, EventArgs e)
    {
        ApplyFilters();
    }

    private void ComboBoxFilter_SelectedIndexChanged(object? sender, EventArgs e)
    {
        ApplyFilters();
    }
}
