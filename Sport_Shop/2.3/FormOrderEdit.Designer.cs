namespace SportShopV22
{
    partial class FormOrderEdit
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            panelMain = new Panel();
            labelFormTitle = new Label();
            labelId = new Label();

            labelClientLabel = new Label();
            textBoxClient = new TextBox();
            labelPickupLabel = new Label();
            comboBoxPickup = new ComboBox();
            labelCodeLabel = new Label();
            numericCode = new NumericUpDown();
            labelStatusLabel = new Label();
            comboBoxStatus = new ComboBox();
            labelDeliveryLabel = new Label();
            datePickerDelivery = new DateTimePicker();
            checkBoxDelivery = new CheckBox();

            labelItemsTitle = new Label();
            dataGridViewItems = new DataGridView();
            panelItemButtons = new Panel();
            buttonAddItem = new Button();
            buttonRemoveItem = new Button();

            buttonSave = new Button();
            buttonCancel = new Button();

            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericCode).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewItems).BeginInit();
            SuspendLayout();

            // panelMain
            panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMain.AutoScroll = true;
            panelMain.BackColor = Color.White;
            panelMain.Location = new Point(20, 20);
            panelMain.Size = new Size(640, 610);
            panelMain.Padding = new Padding(30, 20, 30, 20);

            int x1 = 30, x2 = 170, w = 430, y = 10;

            // labelFormTitle
            labelFormTitle.Font = new Font("Times New Roman", 16F, FontStyle.Bold);
            labelFormTitle.ForeColor = Color.FromArgb(67, 97, 238);
            labelFormTitle.Location = new Point(x1, y);
            labelFormTitle.Size = new Size(560, 35);
            y += 40;

            // labelId
            labelId.Font = new Font("Times New Roman", 10F);
            labelId.ForeColor = Color.Gray;
            labelId.Location = new Point(x1, y);
            labelId.Size = new Size(200, 22);
            labelId.Visible = false;
            y += 28;

            // Клиент
            labelClientLabel.Text = "Клиент:";
            labelClientLabel.Font = new Font("Times New Roman", 10F);
            labelClientLabel.Location = new Point(x1, y + 4);
            labelClientLabel.Size = new Size(130, 22);
            textBoxClient.Font = new Font("Times New Roman", 11F);
            textBoxClient.Location = new Point(x2, y);
            textBoxClient.Size = new Size(w, 28);
            textBoxClient.BorderStyle = BorderStyle.FixedSingle;
            y += 38;

            // Пункт выдачи
            labelPickupLabel.Text = "Пункт выдачи:";
            labelPickupLabel.Font = new Font("Times New Roman", 10F);
            labelPickupLabel.Location = new Point(x1, y + 4);
            labelPickupLabel.Size = new Size(130, 22);
            comboBoxPickup.Font = new Font("Times New Roman", 10F);
            comboBoxPickup.Location = new Point(x2, y);
            comboBoxPickup.Size = new Size(w, 28);
            comboBoxPickup.DropDownStyle = ComboBoxStyle.DropDownList;
            y += 38;

            // Код получения
            labelCodeLabel.Text = "Код получения:";
            labelCodeLabel.Font = new Font("Times New Roman", 10F);
            labelCodeLabel.Location = new Point(x1, y + 4);
            labelCodeLabel.Size = new Size(130, 22);
            numericCode.Font = new Font("Times New Roman", 11F);
            numericCode.Location = new Point(x2, y);
            numericCode.Size = new Size(150, 28);
            numericCode.Minimum = 100;
            numericCode.Maximum = 999999;
            numericCode.Value = 100;
            y += 38;

            // Статус
            labelStatusLabel.Text = "Статус:";
            labelStatusLabel.Font = new Font("Times New Roman", 10F);
            labelStatusLabel.Location = new Point(x1, y + 4);
            labelStatusLabel.Size = new Size(130, 22);
            comboBoxStatus.Font = new Font("Times New Roman", 10F);
            comboBoxStatus.Location = new Point(x2, y);
            comboBoxStatus.Size = new Size(250, 28);
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            y += 38;

            // Дата доставки
            checkBoxDelivery.Text = "Дата доставки:";
            checkBoxDelivery.Font = new Font("Times New Roman", 10F);
            checkBoxDelivery.Location = new Point(x1, y + 2);
            checkBoxDelivery.Size = new Size(130, 24);
            checkBoxDelivery.CheckedChanged += CheckBoxDelivery_CheckedChanged;
            labelDeliveryLabel.Text = "";
            labelDeliveryLabel.Location = new Point(0, 0);
            labelDeliveryLabel.Size = new Size(0, 0);
            labelDeliveryLabel.Visible = false;
            datePickerDelivery.Font = new Font("Times New Roman", 10F);
            datePickerDelivery.Location = new Point(x2, y);
            datePickerDelivery.Size = new Size(250, 28);
            datePickerDelivery.Format = DateTimePickerFormat.Short;
            datePickerDelivery.Enabled = false;
            y += 48;

            // Товары заказа
            labelItemsTitle.Text = "Товары в заказе:";
            labelItemsTitle.Font = new Font("Times New Roman", 11F, FontStyle.Bold);
            labelItemsTitle.ForeColor = Color.FromArgb(67, 97, 238);
            labelItemsTitle.Location = new Point(x1, y);
            labelItemsTitle.Size = new Size(200, 24);
            y += 28;

            // dataGridViewItems
            dataGridViewItems.AllowUserToAddRows = false;
            dataGridViewItems.AllowUserToDeleteRows = false;
            dataGridViewItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewItems.BackgroundColor = Color.FromArgb(248, 249, 250);
            dataGridViewItems.BorderStyle = BorderStyle.FixedSingle;
            dataGridViewItems.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dataGridViewItems.ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(233, 245, 255),
                Font = new Font("Times New Roman", 10F, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 60),
                SelectionBackColor = Color.FromArgb(233, 245, 255),
                SelectionForeColor = Color.FromArgb(60, 60, 60),
            };
            dataGridViewItems.ColumnHeadersHeight = 34;
            dataGridViewItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewItems.DefaultCellStyle = new DataGridViewCellStyle
            {
                Font = new Font("Times New Roman", 10F),
                ForeColor = Color.FromArgb(50, 50, 50),
                SelectionBackColor = Color.FromArgb(210, 230, 255),
                SelectionForeColor = Color.Black,
            };
            dataGridViewItems.EnableHeadersVisualStyles = false;
            dataGridViewItems.GridColor = Color.FromArgb(230, 230, 230);
            dataGridViewItems.Location = new Point(x1, y);
            dataGridViewItems.Size = new Size(580, 170);
            dataGridViewItems.ReadOnly = true;
            dataGridViewItems.RowHeadersVisible = false;
            dataGridViewItems.RowTemplate.Height = 28;
            dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            y += 175;

            // panelItemButtons
            panelItemButtons.Location = new Point(x1, y);
            panelItemButtons.Size = new Size(580, 36);

            // buttonAddItem
            buttonAddItem.BackColor = Color.FromArgb(233, 245, 255);
            buttonAddItem.FlatStyle = FlatStyle.Flat;
            buttonAddItem.FlatAppearance.BorderColor = Color.FromArgb(67, 97, 238);
            buttonAddItem.Font = new Font("Times New Roman", 9F);
            buttonAddItem.ForeColor = Color.FromArgb(67, 97, 238);
            buttonAddItem.Location = new Point(0, 2);
            buttonAddItem.Size = new Size(130, 30);
            buttonAddItem.Text = "Добавить товар";
            buttonAddItem.Cursor = Cursors.Hand;
            buttonAddItem.Click += ButtonAddItem_Click;

            // buttonRemoveItem
            buttonRemoveItem.BackColor = Color.FromArgb(255, 235, 238);
            buttonRemoveItem.FlatStyle = FlatStyle.Flat;
            buttonRemoveItem.FlatAppearance.BorderColor = Color.FromArgb(211, 47, 47);
            buttonRemoveItem.Font = new Font("Times New Roman", 9F);
            buttonRemoveItem.ForeColor = Color.FromArgb(211, 47, 47);
            buttonRemoveItem.Location = new Point(140, 2);
            buttonRemoveItem.Size = new Size(130, 30);
            buttonRemoveItem.Text = "Убрать выбранный";
            buttonRemoveItem.Cursor = Cursors.Hand;
            buttonRemoveItem.Click += ButtonRemoveItem_Click;

            panelItemButtons.Controls.Add(buttonAddItem);
            panelItemButtons.Controls.Add(buttonRemoveItem);
            y += 48;

            // buttonSave
            buttonSave.BackColor = Color.FromArgb(67, 97, 238);
            buttonSave.FlatAppearance.BorderSize = 0;
            buttonSave.FlatStyle = FlatStyle.Flat;
            buttonSave.Font = new Font("Times New Roman", 12F, FontStyle.Bold);
            buttonSave.ForeColor = Color.White;
            buttonSave.Location = new Point(x2, y);
            buttonSave.Size = new Size(150, 40);
            buttonSave.Text = "Сохранить";
            buttonSave.Cursor = Cursors.Hand;
            buttonSave.Click += ButtonSave_Click;

            // buttonCancel
            buttonCancel.BackColor = Color.White;
            buttonCancel.FlatAppearance.BorderColor = Color.FromArgb(150, 150, 150);
            buttonCancel.FlatAppearance.BorderSize = 1;
            buttonCancel.FlatStyle = FlatStyle.Flat;
            buttonCancel.Font = new Font("Times New Roman", 11F);
            buttonCancel.ForeColor = Color.FromArgb(80, 80, 80);
            buttonCancel.Location = new Point(x2 + 165, y);
            buttonCancel.Size = new Size(120, 40);
            buttonCancel.Text = "Отмена";
            buttonCancel.Cursor = Cursors.Hand;
            buttonCancel.Click += ButtonCancel_Click;

            panelMain.Controls.Add(labelFormTitle);
            panelMain.Controls.Add(labelId);
            panelMain.Controls.Add(labelClientLabel);
            panelMain.Controls.Add(textBoxClient);
            panelMain.Controls.Add(labelPickupLabel);
            panelMain.Controls.Add(comboBoxPickup);
            panelMain.Controls.Add(labelCodeLabel);
            panelMain.Controls.Add(numericCode);
            panelMain.Controls.Add(labelStatusLabel);
            panelMain.Controls.Add(comboBoxStatus);
            panelMain.Controls.Add(checkBoxDelivery);
            panelMain.Controls.Add(labelDeliveryLabel);
            panelMain.Controls.Add(datePickerDelivery);
            panelMain.Controls.Add(labelItemsTitle);
            panelMain.Controls.Add(dataGridViewItems);
            panelMain.Controls.Add(panelItemButtons);
            panelMain.Controls.Add(buttonSave);
            panelMain.Controls.Add(buttonCancel);

            // FormOrderEdit
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(680, 650);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormOrderEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Заказ";

            var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Icon.ico");
            if (File.Exists(iconPath))
            {
                using var bmp = new Bitmap(iconPath);
                Icon = System.Drawing.Icon.FromHandle(bmp.GetHicon());
            }

            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericCode).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewItems).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Label labelFormTitle;
        private Label labelId;
        private Label labelClientLabel;
        private TextBox textBoxClient;
        private Label labelPickupLabel;
        private ComboBox comboBoxPickup;
        private Label labelCodeLabel;
        private NumericUpDown numericCode;
        private Label labelStatusLabel;
        private ComboBox comboBoxStatus;
        private Label labelDeliveryLabel;
        private DateTimePicker datePickerDelivery;
        private CheckBox checkBoxDelivery;
        private Label labelItemsTitle;
        private DataGridView dataGridViewItems;
        private Panel panelItemButtons;
        private Button buttonAddItem;
        private Button buttonRemoveItem;
        private Button buttonSave;
        private Button buttonCancel;
    }
}
