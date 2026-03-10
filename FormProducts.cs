using Microsoft.EntityFrameworkCore;
using ObutvShop.Models;

namespace ObutvShop;

public partial class FormProducts : Form
{
    private readonly User? _currentUser;
    private List<Product> _allProducts = new();
    private int _totalCount;
    private bool _isLoaded;
    private readonly string _imgDir;

    public FormProducts(User? currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();

        _imgDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

        if (_currentUser != null)
            labelUserInfo.Text = $"{_currentUser.FullName} ({_currentUser.Role.Name})";
        else
            labelUserInfo.Text = "Гость";

        comboBoxDiscount.Items.AddRange(new object[]
        {
            "Все скидки", "0 - 5%", "5 - 15%", "15 - 30%", "30 - 70%", "70 - 100%"
        });
        comboBoxDiscount.SelectedIndex = 0;

        dataGridViewProducts.CellFormatting += DataGridView_CellFormatting;
        dataGridViewProducts.CellPainting += DataGridView_CellPainting;
        Load += (_, _) =>
        {
            LoadCategories();
            LoadManufacturers();
            LoadProducts();
            _isLoaded = true;
        };
    }

    private void LoadCategories()
    {
        try
        {
            using var db = new ObutvShopContext();
            comboBoxCategory.Items.Add("Все категории");
            foreach (var c in db.Categories.OrderBy(c => c.Name))
                comboBoxCategory.Items.Add(c.Name);
            comboBoxCategory.SelectedIndex = 0;
        }
        catch { }
    }

    private void LoadManufacturers()
    {
        try
        {
            using var db = new ObutvShopContext();
            comboBoxManufacturer.Items.Add("Все производители");
            foreach (var m in db.Manufacturers.OrderBy(m => m.Name))
                comboBoxManufacturer.Items.Add(m.Name);
            comboBoxManufacturer.SelectedIndex = 0;
        }
        catch { }
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

        int di = comboBoxDiscount.SelectedIndex;
        filtered = di switch
        {
            1 => filtered.Where(p => p.DiscountPct >= 0 && p.DiscountPct < 5),
            2 => filtered.Where(p => p.DiscountPct >= 5 && p.DiscountPct < 15),
            3 => filtered.Where(p => p.DiscountPct >= 15 && p.DiscountPct < 30),
            4 => filtered.Where(p => p.DiscountPct >= 30 && p.DiscountPct < 70),
            5 => filtered.Where(p => p.DiscountPct >= 70 && p.DiscountPct <= 100),
            _ => filtered
        };

        int ci = comboBoxCategory.SelectedIndex;
        if (ci > 0)
        {
            string? cat = comboBoxCategory.Items[ci]?.ToString();
            if (cat != null) filtered = filtered.Where(p => p.Category.Name == cat);
        }

        int mi = comboBoxManufacturer.SelectedIndex;
        if (mi > 0)
        {
            string? mfr = comboBoxManufacturer.Items[mi]?.ToString();
            if (mfr != null) filtered = filtered.Where(p => p.Manufacturer.Name == mfr);
        }

        var list = filtered.ToList();

        dataGridViewProducts.DataSource = list.Select(p => new
        {
            Фото = p.Photo ?? "",
            Артикул = p.Article,
            Наименование = p.Name,
            Категория = p.Category.Name,
            Производитель = p.Manufacturer.Name,
            Цена = p.Price,
            Скидка = p.DiscountPct,
            СоСкидкой = p.PriceDiscounted,
            Остаток = p.StockQty
        }).ToList();

        if (dataGridViewProducts.Columns.Contains("Фото"))
        {
            dataGridViewProducts.Columns["Фото"]!.Width = 60;
            dataGridViewProducts.Columns["Фото"]!.HeaderText = "";
        }

        labelCount.Text = $"Показано {list.Count} из {_totalCount} товаров";
    }

    private void DataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var row = dataGridViewProducts.Rows[e.RowIndex];

        if (dataGridViewProducts.Columns.Contains("Скидка"))
        {
            var val = row.Cells["Скидка"].Value;
            if (val is decimal d && d > 15)
            {
                e.CellStyle.BackColor = Color.FromArgb(232, 245, 233);
                e.CellStyle.SelectionBackColor = Color.FromArgb(200, 230, 201);
                e.CellStyle.SelectionForeColor = Color.Black;
            }
        }

        if (dataGridViewProducts.Columns.Contains("Остаток"))
        {
            var stockVal = row.Cells["Остаток"].Value;
            if (stockVal is int stock && stock < 3 && dataGridViewProducts.Columns[e.ColumnIndex].Name == "Остаток")
            {
                e.CellStyle.ForeColor = Color.FromArgb(198, 40, 40);
                e.CellStyle.Font = new Font(e.CellStyle.Font ?? dataGridViewProducts.Font, FontStyle.Bold);
            }
        }

        if (dataGridViewProducts.Columns[e.ColumnIndex].Name == "Фото")
        {
            e.FormattingApplied = true;
            e.Value = "";
        }
    }

    private void DataGridView_CellPainting(object? sender, DataGridViewCellPaintingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (!dataGridViewProducts.Columns.Contains("Фото")) return;
        if (e.ColumnIndex != dataGridViewProducts.Columns["Фото"]!.Index) return;

        e.Handled = true;
        e.PaintBackground(e.ClipBounds, true);

        var photoFile = dataGridViewProducts.Rows[e.RowIndex].Cells["Фото"].Value?.ToString();
        if (!string.IsNullOrEmpty(photoFile))
        {
            var path = Path.Combine(_imgDir, photoFile);
            if (File.Exists(path))
            {
                try
                {
                    using var img = Image.FromFile(path);
                    int pad = 4;
                    var dest = new Rectangle(
                        e.CellBounds.X + pad,
                        e.CellBounds.Y + pad,
                        e.CellBounds.Width - pad * 2,
                        e.CellBounds.Height - pad * 2);
                    e.Graphics!.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    e.Graphics.DrawImage(img, dest);
                }
                catch { }
            }
        }
        else
        {
            var fallback = Path.Combine(_imgDir, "picture.png");
            if (File.Exists(fallback))
            {
                try
                {
                    using var img = Image.FromFile(fallback);
                    int pad = 4;
                    var dest = new Rectangle(
                        e.CellBounds.X + pad,
                        e.CellBounds.Y + pad,
                        e.CellBounds.Width - pad * 2,
                        e.CellBounds.Height - pad * 2);
                    e.Graphics!.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    e.Graphics.DrawImage(img, dest);
                }
                catch { }
            }
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
