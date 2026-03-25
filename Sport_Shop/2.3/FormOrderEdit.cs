using Microsoft.EntityFrameworkCore;
using SportShopV22.Models;

namespace SportShopV22;

public partial class FormOrderEdit : Form
{
    private readonly int? _orderId;
    private readonly User? _currentUser;
    private readonly List<OrderItemRow> _items = new();

    public FormOrderEdit(int? orderId, User? currentUser)
    {
        _orderId = orderId;
        _currentUser = currentUser;
        InitializeComponent();

        Text = _orderId.HasValue ? "Редактирование заказа" : "Новый заказ";
        labelFormTitle.Text = Text;

        LoadComboData();

        if (_orderId.HasValue)
            LoadOrder();
        else
        {
            numericCode.Value = new Random().Next(100, 999999);
        }

        RefreshItemsGrid();
    }

    private void LoadComboData()
    {
        using var db = new SportShopContext();

        var pickups = db.PickupPoints.OrderBy(p => p.Id).ToList();
        comboBoxPickup.DataSource = pickups;
        comboBoxPickup.DisplayMember = "Address";
        comboBoxPickup.ValueMember = "Id";

        var statuses = db.OrderStatuses.OrderBy(s => s.Id).ToList();
        comboBoxStatus.DataSource = statuses;
        comboBoxStatus.DisplayMember = "Name";
        comboBoxStatus.ValueMember = "Id";
    }

    private void LoadOrder()
    {
        using var db = new SportShopContext();
        var order = db.Orders
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
            .Include(o => o.PickupPoint)
            .Include(o => o.Status)
            .FirstOrDefault(o => o.Id == _orderId);

        if (order == null)
        {
            MessageBox.Show("Заказ не найден.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            Close();
            return;
        }

        labelId.Text = $"Заказ #{order.Id}  от {order.OrderDate:dd.MM.yyyy}";
        labelId.Visible = true;
        textBoxClient.Text = order.ClientName;
        numericCode.Value = order.PickupCode ?? 100;
        comboBoxStatus.SelectedValue = order.StatusId;

        if (order.PickupPointId.HasValue)
            comboBoxPickup.SelectedValue = order.PickupPointId.Value;

        if (order.DeliveryDate.HasValue)
        {
            checkBoxDelivery.Checked = true;
            datePickerDelivery.Value = order.DeliveryDate.Value;
        }

        foreach (var oi in order.OrderItems)
        {
            _items.Add(new OrderItemRow
            {
                ProductId = oi.ProductId,
                ProductName = oi.Product.Name,
                Article = oi.Product.Article,
                Price = oi.Product.PriceDiscounted,
                Quantity = oi.Quantity
            });
        }
    }

    private void RefreshItemsGrid()
    {
        dataGridViewItems.DataSource = null;
        dataGridViewItems.DataSource = _items.Select(i => new
        {
            Артикул = i.Article,
            Товар = i.ProductName,
            Цена = i.Price,
            Кол_во = i.Quantity,
            Сумма = i.Price * i.Quantity
        }).ToList();
    }

    private void CheckBoxDelivery_CheckedChanged(object? sender, EventArgs e)
    {
        datePickerDelivery.Enabled = checkBoxDelivery.Checked;
    }

    private void ButtonAddItem_Click(object? sender, EventArgs e)
    {
        using var db = new SportShopContext();
        var products = db.Products.OrderBy(p => p.Name).ToList();

        if (products.Count == 0)
        {
            MessageBox.Show("Нет доступных товаров.", "Предупреждение",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using var pickForm = new Form
        {
            Text = "Выбор товара",
            Size = new Size(500, 380),
            StartPosition = FormStartPosition.CenterParent,
            FormBorderStyle = FormBorderStyle.FixedDialog,
            MaximizeBox = false,
            MinimizeBox = false,
            BackColor = Color.FromArgb(248, 249, 250)
        };

        var combo = new ComboBox
        {
            DataSource = products,
            DisplayMember = "Name",
            ValueMember = "Id",
            DropDownStyle = ComboBoxStyle.DropDownList,
            Font = new Font("Times New Roman", 11F),
            Location = new Point(30, 50),
            Size = new Size(420, 28)
        };

        var lblProduct = new Label
        {
            Text = "Товар:",
            Font = new Font("Times New Roman", 10F),
            Location = new Point(30, 25),
            Size = new Size(100, 22)
        };

        var lblQty = new Label
        {
            Text = "Количество:",
            Font = new Font("Times New Roman", 10F),
            Location = new Point(30, 95),
            Size = new Size(100, 22)
        };

        var numQty = new NumericUpDown
        {
            Font = new Font("Times New Roman", 11F),
            Location = new Point(30, 120),
            Size = new Size(120, 28),
            Minimum = 1,
            Maximum = 9999,
            Value = 1
        };

        var lblInfo = new Label
        {
            Font = new Font("Times New Roman", 10F),
            ForeColor = Color.Gray,
            Location = new Point(30, 160),
            Size = new Size(420, 44)
        };

        combo.SelectedIndexChanged += (_, _) =>
        {
            if (combo.SelectedItem is Product p)
                lblInfo.Text = $"Артикул: {p.Article}   |   Цена: {p.PriceDiscounted:N2} ₽   |   На складе: {p.StockQty}";
        };
        if (combo.SelectedItem is Product first)
            lblInfo.Text = $"Артикул: {first.Article}   |   Цена: {first.PriceDiscounted:N2} ₽   |   На складе: {first.StockQty}";

        var btnOk = new Button
        {
            Text = "Добавить",
            BackColor = Color.FromArgb(67, 97, 238),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Times New Roman", 11F, FontStyle.Bold),
            Size = new Size(130, 38),
            Location = new Point(30, 220),
            Cursor = Cursors.Hand,
            DialogResult = DialogResult.OK
        };
        btnOk.FlatAppearance.BorderSize = 0;

        var btnNo = new Button
        {
            Text = "Отмена",
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Times New Roman", 10F),
            Size = new Size(100, 38),
            Location = new Point(170, 220),
            Cursor = Cursors.Hand,
            DialogResult = DialogResult.Cancel
        };

        pickForm.Controls.AddRange(new Control[] { lblProduct, combo, lblQty, numQty, lblInfo, btnOk, btnNo });
        pickForm.AcceptButton = btnOk;
        pickForm.CancelButton = btnNo;

        if (pickForm.ShowDialog() != DialogResult.OK) return;
        if (combo.SelectedItem is not Product selected) return;

        var existing = _items.FirstOrDefault(i => i.ProductId == selected.Id);
        if (existing != null)
        {
            existing.Quantity += (int)numQty.Value;
        }
        else
        {
            _items.Add(new OrderItemRow
            {
                ProductId = selected.Id,
                ProductName = selected.Name,
                Article = selected.Article,
                Price = selected.PriceDiscounted,
                Quantity = (int)numQty.Value
            });
        }

        RefreshItemsGrid();
    }

    private void ButtonRemoveItem_Click(object? sender, EventArgs e)
    {
        if (dataGridViewItems.CurrentRow == null || dataGridViewItems.CurrentRow.Index < 0) return;
        int idx = dataGridViewItems.CurrentRow.Index;
        if (idx >= _items.Count) return;
        _items.RemoveAt(idx);
        RefreshItemsGrid();
    }

    private void ButtonSave_Click(object? sender, EventArgs e)
    {
        if (string.IsNullOrWhiteSpace(textBoxClient.Text))
        {
            MessageBox.Show("Введите имя клиента.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            textBoxClient.Focus();
            return;
        }

        if (_items.Count == 0)
        {
            MessageBox.Show("Добавьте хотя бы один товар в заказ.", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using var db = new SportShopContext();
            Order order;

            if (_orderId.HasValue)
            {
                order = db.Orders.Include(o => o.OrderItems).First(o => o.Id == _orderId);
                db.OrderItems.RemoveRange(order.OrderItems);
            }
            else
            {
                order = new Order { OrderDate = DateTime.Now };
                db.Orders.Add(order);
            }

            order.ClientName = textBoxClient.Text.Trim();
            order.PickupPointId = (int?)comboBoxPickup.SelectedValue;
            order.PickupCode = (int)numericCode.Value;
            order.StatusId = (int)comboBoxStatus.SelectedValue!;
            order.DeliveryDate = checkBoxDelivery.Checked ? datePickerDelivery.Value.Date : null;
            order.UserId = _currentUser?.Id ?? 1;

            foreach (var item in _items)
            {
                order.OrderItems.Add(new OrderItem
                {
                    ProductId = item.ProductId,
                    Quantity = item.Quantity
                });
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

    private class OrderItemRow
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = "";
        public string Article { get; set; } = "";
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
