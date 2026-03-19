using Microsoft.EntityFrameworkCore;
using SportShopV22.Models;

namespace SportShopV22;

public partial class FormProducts : Form
{
    private readonly User? _currentUser;
    private List<Product> _allProducts = new();
    private int _totalCount;
    private bool _isLoaded;
    private readonly string _imgDir;
    private bool _editFormOpen;

    public FormProducts(User? currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();

        _imgDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

        if (_currentUser != null)
            labelUserInfo.Text = $"{_currentUser.FullName} ({_currentUser.Role.Name})";
        else
            labelUserInfo.Text = "Гость";

        bool isAdmin = _currentUser?.Role.Name == "Администратор";
        buttonAdd.Visible = isAdmin;
        buttonDelete.Visible = isAdmin;

        bool isManagerOrAdmin = isAdmin || _currentUser?.Role.Name == "Менеджер";
        panelFilters.Visible = isManagerOrAdmin;

        comboBoxDiscount.Items.AddRange(new object[]
        {
            "Все скидки", "0 - 5%", "5 - 15%", "15 - 30%", "30 - 70%", "70 - 100%"
        });
        comboBoxDiscount.SelectedIndex = 0;

        comboBoxSort.Items.AddRange(new object[]
        {
            "Без сортировки", "Остаток ↑", "Остаток ↓"
        });
        comboBoxSort.SelectedIndex = 0;

        dataGridViewProducts.CellFormatting += DataGridView_CellFormatting;
        dataGridViewProducts.CellPainting += DataGridView_CellPainting;
        if (isAdmin)
            dataGridViewProducts.CellDoubleClick += DataGridView_CellDoubleClick;

        Load += (_, _) =>
        {
            LoadCategories();
            LoadManufacturers();
            LoadSuppliers();
            LoadProducts();
            _isLoaded = true;
        };
    }

    private void LoadCategories()
    {
        try
        {
            using var db = new SportShopContext();
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
            using var db = new SportShopContext();
            comboBoxManufacturer.Items.Add("Все производители");
            foreach (var m in db.Manufacturers.OrderBy(m => m.Name))
                comboBoxManufacturer.Items.Add(m.Name);
            comboBoxManufacturer.SelectedIndex = 0;
        }
        catch { }
    }

    private void LoadSuppliers()
    {
        try
        {
            using var db = new SportShopContext();
            comboBoxSupplier.Items.Add("Все поставщики");
            foreach (var s in db.Suppliers.OrderBy(s => s.Name))
                comboBoxSupplier.Items.Add(s.Name);
            comboBoxSupplier.SelectedIndex = 0;
        }
        catch { }
    }

    public void LoadProducts()
    {
        try
        {
            using var db = new SportShopContext();
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
                p.Manufacturer.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Supplier.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Category.Name.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                p.Unit.Contains(search, StringComparison.OrdinalIgnoreCase));
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

        int si = comboBoxSupplier.SelectedIndex;
        if (si > 0)
        {
            string? sup = comboBoxSupplier.Items[si]?.ToString();
            if (sup != null) filtered = filtered.Where(p => p.Supplier.Name == sup);
        }

        int sortIdx = comboBoxSort.SelectedIndex;
        filtered = sortIdx switch
        {
            1 => filtered.OrderBy(p => p.StockQty),
            2 => filtered.OrderByDescending(p => p.StockQty),
            _ => filtered
        };

        var list = filtered.ToList();

        dataGridViewProducts.DataSource = list.Select(p => new
        {
            Фото = p.Photo ?? "",
            Артикул = p.Article,
            Наименование = p.Name,
            Категория = p.Category.Name,
            Описание = p.Description ?? "",
            Производитель = p.Manufacturer.Name,
            Поставщик = p.Supplier.Name,
            Цена = p.Price,
            Ед = p.Unit,
            Скидка = p.DiscountPct,
            СоСкидкой = p.PriceDiscounted,
            Остаток = p.StockQty,
            _Id = p.Id
        }).ToList();

        if (dataGridViewProducts.Columns.Contains("Фото"))
        {
            dataGridViewProducts.Columns["Фото"]!.Width = 60;
            dataGridViewProducts.Columns["Фото"]!.HeaderText = "";
        }
        if (dataGridViewProducts.Columns.Contains("_Id"))
            dataGridViewProducts.Columns["_Id"]!.Visible = false;

        labelCount.Text = $"Показано {list.Count} из {_totalCount} товаров";
    }

    private void DataGridView_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        var row = dataGridViewProducts.Rows[e.RowIndex];

        if (dataGridViewProducts.Columns.Contains("Скидка"))
        {
            var val = row.Cells["Скидка"].Value;
            if (val is int d && d > 15)
            {
                e.CellStyle.BackColor = Color.FromArgb(46, 196, 182);
                e.CellStyle.SelectionBackColor = Color.FromArgb(38, 170, 158);
                e.CellStyle.SelectionForeColor = Color.Black;
            }
        }

        if (dataGridViewProducts.Columns.Contains("Остаток"))
        {
            var stockVal = row.Cells["Остаток"].Value;
            if (stockVal is int stock && stock == 0)
            {
                e.CellStyle.BackColor = Color.FromArgb(233, 245, 255);
                e.CellStyle.SelectionBackColor = Color.FromArgb(200, 225, 245);
                e.CellStyle.SelectionForeColor = Color.Black;
            }
        }

        if (dataGridViewProducts.Columns[e.ColumnIndex].Name == "Цена")
        {
            var discVal = row.Cells["Скидка"].Value;
            if (discVal is int disc && disc > 0)
            {
                e.CellStyle.Font = new Font("Times New Roman", 10F, FontStyle.Strikeout);
                e.CellStyle.ForeColor = Color.Black;
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
        string path = "";
        if (!string.IsNullOrEmpty(photoFile))
            path = Path.Combine(_imgDir, photoFile);

        if (string.IsNullOrEmpty(path) || !File.Exists(path))
            path = Path.Combine(_imgDir, "picture.png");

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

    private void DataGridView_CellDoubleClick(object? sender, DataGridViewCellEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (_editFormOpen) return;
        if (!dataGridViewProducts.Columns.Contains("_Id")) return;

        var val = dataGridViewProducts.Rows[e.RowIndex].Cells["_Id"].Value;
        if (val is int productId)
            OpenEditForm(productId);
    }

    private void OpenEditForm(int? productId)
    {
        if (_editFormOpen) return;
        _editFormOpen = true;
        using var form = new FormProductEdit(productId);
        form.FormClosed += (_, _) => _editFormOpen = false;
        if (form.ShowDialog() == DialogResult.OK)
            LoadProducts();
        _editFormOpen = false;
    }

    private void ButtonAdd_Click(object? sender, EventArgs e)
    {
        OpenEditForm(null);
    }

    private void ButtonDelete_Click(object? sender, EventArgs e)
    {
        if (dataGridViewProducts.CurrentRow == null) return;
        if (!dataGridViewProducts.Columns.Contains("_Id")) return;

        var val = dataGridViewProducts.CurrentRow.Cells["_Id"].Value;
        if (val is not int productId) return;

        var name = dataGridViewProducts.CurrentRow.Cells["Наименование"].Value?.ToString() ?? "";

        using var db = new SportShopContext();
        bool hasOrders = db.OrderItems.Any(oi => oi.ProductId == productId);
        if (hasOrders)
        {
            MessageBox.Show("Невозможно удалить товар, который присутствует в заказах.",
                "Удаление невозможно", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var result = MessageBox.Show($"Удалить товар \"{name}\"?\nЭто действие нельзя отменить.",
            "Подтверждение удаления", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
        if (result != DialogResult.Yes) return;

        var product = db.Products.Find(productId);
        if (product == null) return;

        if (!string.IsNullOrEmpty(product.Photo))
        {
            var imgPath = Path.Combine(_imgDir, product.Photo);
            if (File.Exists(imgPath) && product.Photo != "picture.png")
            {
                try { File.Delete(imgPath); } catch { }
            }
        }

        db.Products.Remove(product);
        db.SaveChanges();
        LoadProducts();
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
