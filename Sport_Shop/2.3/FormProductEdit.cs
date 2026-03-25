using Microsoft.EntityFrameworkCore;
using SportShopV22.Models;

namespace SportShopV22;

public partial class FormProductEdit : Form
{
    private readonly int? _productId;
    private readonly string _imgDir;
    private string? _selectedImagePath;
    private string? _currentImageFile;

    public FormProductEdit(int? productId)
    {
        _productId = productId;
        InitializeComponent();
        _imgDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources");

        Text = _productId.HasValue ? "Редактирование товара" : "Добавление товара";
        labelFormTitle.Text = Text;

        LoadComboData();

        if (_productId.HasValue)
            LoadProduct();
        else
            textBoxArticle.ReadOnly = false;
    }

    private void LoadComboData()
    {
        using var db = new SportShopContext();

        comboBoxCategory.DataSource = db.Categories.OrderBy(c => c.Name).ToList();
        comboBoxCategory.DisplayMember = "Name";
        comboBoxCategory.ValueMember = "Id";

        comboBoxManufacturer.DataSource = db.Manufacturers.OrderBy(m => m.Name).ToList();
        comboBoxManufacturer.DisplayMember = "Name";
        comboBoxManufacturer.ValueMember = "Id";

        comboBoxSupplier.DataSource = db.Suppliers.OrderBy(s => s.Name).ToList();
        comboBoxSupplier.DisplayMember = "Name";
        comboBoxSupplier.ValueMember = "Id";
    }

    private void LoadProduct()
    {
        using var db = new SportShopContext();
        var p = db.Products
            .Include(x => x.Category)
            .Include(x => x.Manufacturer)
            .Include(x => x.Supplier)
            .FirstOrDefault(x => x.Id == _productId);

        if (p == null)
        {
            MessageBox.Show("Товар не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
            return;
        }

        labelId.Text = $"ID: {p.Id}";
        labelId.Visible = true;
        textBoxArticle.Text = p.Article;
        textBoxArticle.ReadOnly = true;
        textBoxName.Text = p.Name;
        textBoxDescription.Text = p.Description ?? "";
        numericPrice.Value = p.Price;
        numericDiscount.Value = p.DiscountPct;
        numericStock.Value = p.StockQty;
        comboBoxCategory.SelectedValue = p.CategoryId;
        comboBoxManufacturer.SelectedValue = p.ManufacturerId;
        comboBoxSupplier.SelectedValue = p.SupplierId;
        textBoxUnit.Text = p.Unit;

        _currentImageFile = p.Photo;
        LoadImage(p.Photo);
    }

    private void LoadImage(string? fileName)
    {
        string path = "";
        if (!string.IsNullOrEmpty(fileName))
            path = Path.Combine(_imgDir, fileName);
        if (string.IsNullOrEmpty(path) || !File.Exists(path))
            path = Path.Combine(_imgDir, "picture.png");

        if (File.Exists(path))
        {
            try
            {
                using var fs = new FileStream(path, FileMode.Open, FileAccess.Read);
                pictureBoxPhoto.Image = Image.FromStream(fs);
            }
            catch { }
        }
    }

    private void ButtonChangePhoto_Click(object? sender, EventArgs e)
    {
        using var ofd = new OpenFileDialog
        {
            Filter = "Изображения|*.png;*.jpg;*.jpeg;*.bmp;*.gif",
            Title = "Выберите изображение товара"
        };

        if (ofd.ShowDialog() != DialogResult.OK) return;

        using var img = Image.FromFile(ofd.FileName);
        if (img.Width > 300 || img.Height > 200)
        {
            MessageBox.Show("Размер изображения не должен превышать 300×200 пикселей.",
                "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        _selectedImagePath = ofd.FileName;
        pictureBoxPhoto.Image = Image.FromFile(ofd.FileName);
    }

    private void ButtonSave_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(textBoxName.Text))
        {
            MessageBox.Show("Введите наименование товара.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxName.Focus();
            return;
        }
        if (string.IsNullOrWhiteSpace(textBoxArticle.Text))
        {
            MessageBox.Show("Введите артикул товара.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxArticle.Focus();
            return;
        }
        if (numericPrice.Value < 0)
        {
            MessageBox.Show("Цена не может быть отрицательной.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            numericPrice.Focus();
            return;
        }
        if (numericStock.Value < 0)
        {
            MessageBox.Show("Количество на складе не может быть отрицательным.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            numericStock.Focus();
            return;
        }

        try
        {
            using var db = new SportShopContext();
            Product product;

            if (_productId.HasValue)
            {
                product = db.Products.Find(_productId.Value)!;
            }
            else
            {
                product = new Product();
                db.Products.Add(product);
            }

            product.Article = textBoxArticle.Text.Trim();
            product.Name = textBoxName.Text.Trim();
            product.Description = string.IsNullOrWhiteSpace(textBoxDescription.Text) ? null : textBoxDescription.Text.Trim();
            product.Price = numericPrice.Value;
            product.DiscountPct = (int)numericDiscount.Value;
            product.StockQty = (int)numericStock.Value;
            product.CategoryId = (int)comboBoxCategory.SelectedValue!;
            product.ManufacturerId = (int)comboBoxManufacturer.SelectedValue!;
            product.SupplierId = (int)comboBoxSupplier.SelectedValue!;
            product.Unit = textBoxUnit.Text.Trim();

            if (_selectedImagePath != null)
            {
                // Remove old image
                if (!string.IsNullOrEmpty(_currentImageFile) && _currentImageFile != "picture.png")
                {
                    var oldPath = Path.Combine(_imgDir, _currentImageFile);
                    if (File.Exists(oldPath))
                        try { File.Delete(oldPath); } catch { }
                }

                var ext = Path.GetExtension(_selectedImagePath);
                var newFileName = $"{product.Article}{ext}";
                var destPath = Path.Combine(_imgDir, newFileName);
                File.Copy(_selectedImagePath, destPath, true);
                product.Photo = newFileName;
            }

            db.SaveChanges();
            DialogResult = DialogResult.OK;
            Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка сохранения: {ex.Message}", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ButtonCancel_Click(object? sender, EventArgs e)
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }
}
