using Microsoft.EntityFrameworkCore;
using SportShopV2.Models;

namespace SportShopV2;

public partial class FormOrders : Form
{
    private readonly User? _currentUser;
    private List<Order> _allOrders = new();
    private int _totalCount;
    private bool _isLoaded;

    private static readonly Dictionary<string, Color> StatusColors = new()
    {
        ["Новый"] = Color.FromArgb(33, 150, 243),
        ["Завершён"] = Color.FromArgb(76, 175, 80),
        ["Отправлен"] = Color.FromArgb(255, 152, 0),
        ["Оплачен"] = Color.FromArgb(0, 150, 136),
        ["Подтверждён"] = Color.FromArgb(103, 58, 183),
        ["Отменён"] = Color.FromArgb(244, 67, 54),
        ["Возврат"] = Color.FromArgb(158, 158, 158),
    };

    public FormOrders(User? currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();

        if (_currentUser != null)
            labelUserInfo.Text = $"{_currentUser.FullName} ({_currentUser.Role.Name})";
        else
            labelUserInfo.Text = "Гость";

        dataGridViewOrders.CellFormatting += DataGridViewOrders_CellFormatting;
        Load += (_, _) => { LoadStatuses(); LoadOrders(); _isLoaded = true; };
    }

    private void LoadStatuses()
    {
        try
        {
            using var db = new SportShopContext();
            var statuses = db.OrderStatuses.OrderBy(s => s.Id).ToList();
            comboBoxStatus.Items.Add("Все статусы");
            foreach (var s in statuses)
                comboBoxStatus.Items.Add(s.Name);
            comboBoxStatus.SelectedIndex = 0;
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки статусов: " + ex.Message, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void LoadOrders()
    {
        try
        {
            using var db = new SportShopContext();
            _allOrders = db.Orders
                .Include(o => o.Status)
                .Include(o => o.PickupPoint)
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.OrderNum)
                .ToList();
            _totalCount = _allOrders.Count;
            ApplyFilters();
        }
        catch (Exception ex)
        {
            MessageBox.Show("Ошибка загрузки заказов: " + ex.Message, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void ApplyFilters()
    {
        var filtered = _allOrders.AsEnumerable();

        string search = textBoxSearch.Text.Trim();
        if (search.Length > 0)
        {
            filtered = filtered.Where(o =>
                o.OrderNum.ToString().Contains(search) ||
                (o.User?.FullName?.Contains(search, StringComparison.OrdinalIgnoreCase) ?? false));
        }

        int si = comboBoxStatus.SelectedIndex;
        if (si > 0)
        {
            string? statusName = comboBoxStatus.Items[si]?.ToString();
            if (statusName != null) filtered = filtered.Where(o => o.Status?.Name == statusName);
        }

        var list = filtered.ToList();

        dataGridViewOrders.DataSource = list.Select(o => new
        {
            Номер = o.OrderNum,
            Дата = o.OrderDate.ToString("dd.MM.yyyy"),
            Доставка = o.DeliveryDate?.ToString("dd.MM.yyyy") ?? "",
            Клиент = o.User?.FullName ?? "",
            Код = o.PickupCode,
            Позиций = o.OrderItems.Count,
            Сумма = o.OrderItems.Sum(i => i.Quantity * i.Product.PriceDiscounted),
            Статус = o.Status?.Name ?? "",
            ПунктВыдачи = o.PickupPoint?.Address ?? "",
            _OrderNum = o.OrderNum
        }).ToList();

        if (dataGridViewOrders.Columns.Contains("_OrderNum"))
            dataGridViewOrders.Columns["_OrderNum"]!.Visible = false;

        labelCount.Text = $"Показано {list.Count} из {_totalCount} заказов";
    }

    private void DataGridViewOrders_CellFormatting(object? sender, DataGridViewCellFormattingEventArgs e)
    {
        if (e.RowIndex < 0) return;
        if (!dataGridViewOrders.Columns.Contains("Статус")) return;
        if (dataGridViewOrders.Columns[e.ColumnIndex].Name != "Статус") return;

        var val = e.Value?.ToString();
        if (val != null && StatusColors.TryGetValue(val, out var color))
        {
            e.CellStyle.ForeColor = color;
            e.CellStyle.Font = new Font(e.CellStyle.Font ?? dataGridViewOrders.Font, FontStyle.Bold);
        }
    }

    private void DataGridViewOrders_SelectionChanged(object? sender, EventArgs e)
    {
        if (dataGridViewOrders.CurrentRow == null) return;
        if (!dataGridViewOrders.Columns.Contains("_OrderNum")) return;

        var val = dataGridViewOrders.CurrentRow.Cells["_OrderNum"].Value;
        if (val is int orderNum)
        {
            var order = _allOrders.FirstOrDefault(o => o.OrderNum == orderNum);
            if (order != null)
            {
                labelItems.Text = $"  Состав заказа #{order.OrderNum}";
                dataGridViewItems.DataSource = order.OrderItems.Select(i => new
                {
                    Артикул = i.Product.Article,
                    Товар = i.Product.Name,
                    Цена = i.Product.PriceDiscounted,
                    Количество = i.Quantity,
                    Сумма = i.Quantity * i.Product.PriceDiscounted
                }).ToList();
            }
        }
    }

    private void TextBoxSearch_TextChanged(object? sender, EventArgs e)
    {
        if (_isLoaded) ApplyFilters();
    }

    private void ComboBoxStatus_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (_isLoaded) ApplyFilters();
    }
}
