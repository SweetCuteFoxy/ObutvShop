using Microsoft.EntityFrameworkCore;
using ObutvShop.Models;

namespace ObutvShop;

public partial class FormOrders : Form
{
    private readonly User? _currentUser;
    private List<Order> _allOrders = new();
    private int _totalCount;
    private bool _isLoaded;

    public FormOrders(User? currentUser)
    {
        _currentUser = currentUser;
        InitializeComponent();

        if (_currentUser != null)
            labelUserInfo.Text = $"{_currentUser.FullName} ({_currentUser.Role.Name})";
        else
            labelUserInfo.Text = "Гость";

        Load += (_, _) => { LoadStatuses(); LoadOrders(); _isLoaded = true; };
    }

    private void LoadStatuses()
    {
        try
        {
            using var db = new ObutvShopContext();
            var statuses = db.OrderStatuses.OrderBy(s => s.Id).ToList();
            comboBoxStatus.Items.Add("Все");
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
            using var db = new ObutvShopContext();
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
                (o.User?.FullName ?? "").Contains(search, StringComparison.OrdinalIgnoreCase));
        }

        int si = comboBoxStatus.SelectedIndex;
        if (si > 0)
        {
            string statusName = comboBoxStatus.Items[si].ToString()!;
            filtered = filtered.Where(o => o.Status.Name == statusName);
        }

        var list = filtered.ToList();

        dataGridViewOrders.DataSource = list.Select(o => new
        {
            Номер = o.OrderNum,
            Дата = o.OrderDate?.ToString("dd.MM.yyyy") ?? "",
            Доставка = o.DeliveryDate?.ToString("dd.MM.yyyy") ?? "",
            ПунктВыдачи = o.PickupPoint?.Address ?? "",
            Клиент = o.User?.FullName ?? "Гость",
            Позиций = o.OrderItems.Count,
            Сумма = o.OrderItems.Sum(i => i.Quantity * i.Product.PriceDiscounted),
            Статус = o.Status.Name,
            _OrderNum = o.OrderNum
        }).ToList();

        if (dataGridViewOrders.Columns.Contains("_OrderNum"))
            dataGridViewOrders.Columns["_OrderNum"]!.Visible = false;

        labelCount.Text = $"{list.Count} из {_totalCount}";
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
