namespace SportShopV22
{
    partial class FormProductEdit
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

            pictureBoxPhoto = new PictureBox();
            buttonChangePhoto = new Button();

            labelNameLabel = new Label();
            textBoxName = new TextBox();
            labelArticleLabel = new Label();
            textBoxArticle = new TextBox();
            labelDescLabel = new Label();
            textBoxDescription = new TextBox();

            labelCategoryLabel = new Label();
            comboBoxCategory = new ComboBox();
            labelManufacturerLabel = new Label();
            comboBoxManufacturer = new ComboBox();
            labelSupplierLabel = new Label();
            comboBoxSupplier = new ComboBox();
            labelUnitLabel = new Label();
            textBoxUnit = new TextBox();

            labelPriceLabel = new Label();
            numericPrice = new NumericUpDown();
            labelDiscountLabel = new Label();
            numericDiscount = new NumericUpDown();
            labelStockLabel = new Label();
            numericStock = new NumericUpDown();

            buttonSave = new Button();
            buttonCancel = new Button();

            panelMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericPrice).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericDiscount).BeginInit();
            ((System.ComponentModel.ISupportInitialize)numericStock).BeginInit();
            SuspendLayout();

            // panelMain
            panelMain.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelMain.AutoScroll = true;
            panelMain.BackColor = Color.White;
            panelMain.Location = new Point(20, 20);
            panelMain.Size = new Size(640, 620);
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

            // pictureBoxPhoto
            pictureBoxPhoto.Location = new Point(x1, y);
            pictureBoxPhoto.Size = new Size(120, 90);
            pictureBoxPhoto.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxPhoto.BackColor = Color.FromArgb(248, 249, 250);
            pictureBoxPhoto.BorderStyle = BorderStyle.FixedSingle;

            // buttonChangePhoto
            buttonChangePhoto.Location = new Point(x1 + 130, y + 25);
            buttonChangePhoto.Size = new Size(140, 32);
            buttonChangePhoto.Text = "Выбрать фото";
            buttonChangePhoto.Font = new Font("Times New Roman", 10F);
            buttonChangePhoto.FlatStyle = FlatStyle.Flat;
            buttonChangePhoto.BackColor = Color.FromArgb(233, 245, 255);
            buttonChangePhoto.ForeColor = Color.FromArgb(67, 97, 238);
            buttonChangePhoto.FlatAppearance.BorderColor = Color.FromArgb(67, 97, 238);
            buttonChangePhoto.Cursor = Cursors.Hand;
            buttonChangePhoto.Click += ButtonChangePhoto_Click;
            y += 100;

            // Наименование
            labelNameLabel.Text = "Наименование:";
            labelNameLabel.Font = new Font("Times New Roman", 10F);
            labelNameLabel.Location = new Point(x1, y + 4);
            labelNameLabel.Size = new Size(130, 22);
            textBoxName.Font = new Font("Times New Roman", 11F);
            textBoxName.Location = new Point(x2, y);
            textBoxName.Size = new Size(w, 28);
            textBoxName.BorderStyle = BorderStyle.FixedSingle;
            y += 38;

            // Артикул
            labelArticleLabel.Text = "Артикул:";
            labelArticleLabel.Font = new Font("Times New Roman", 10F);
            labelArticleLabel.Location = new Point(x1, y + 4);
            labelArticleLabel.Size = new Size(130, 22);
            textBoxArticle.Font = new Font("Times New Roman", 11F);
            textBoxArticle.Location = new Point(x2, y);
            textBoxArticle.Size = new Size(w, 28);
            textBoxArticle.BorderStyle = BorderStyle.FixedSingle;
            y += 38;

            // Описание
            labelDescLabel.Text = "Описание:";
            labelDescLabel.Font = new Font("Times New Roman", 10F);
            labelDescLabel.Location = new Point(x1, y + 4);
            labelDescLabel.Size = new Size(130, 22);
            textBoxDescription.Font = new Font("Times New Roman", 11F);
            textBoxDescription.Location = new Point(x2, y);
            textBoxDescription.Size = new Size(w, 60);
            textBoxDescription.BorderStyle = BorderStyle.FixedSingle;
            textBoxDescription.Multiline = true;
            y += 70;

            // Категория
            labelCategoryLabel.Text = "Категория:";
            labelCategoryLabel.Font = new Font("Times New Roman", 10F);
            labelCategoryLabel.Location = new Point(x1, y + 4);
            labelCategoryLabel.Size = new Size(130, 22);
            comboBoxCategory.Font = new Font("Times New Roman", 10F);
            comboBoxCategory.Location = new Point(x2, y);
            comboBoxCategory.Size = new Size(w, 28);
            comboBoxCategory.DropDownStyle = ComboBoxStyle.DropDownList;
            y += 38;

            // Производитель
            labelManufacturerLabel.Text = "Производитель:";
            labelManufacturerLabel.Font = new Font("Times New Roman", 10F);
            labelManufacturerLabel.Location = new Point(x1, y + 4);
            labelManufacturerLabel.Size = new Size(130, 22);
            comboBoxManufacturer.Font = new Font("Times New Roman", 10F);
            comboBoxManufacturer.Location = new Point(x2, y);
            comboBoxManufacturer.Size = new Size(w, 28);
            comboBoxManufacturer.DropDownStyle = ComboBoxStyle.DropDownList;
            y += 38;

            // Поставщик
            labelSupplierLabel.Text = "Поставщик:";
            labelSupplierLabel.Font = new Font("Times New Roman", 10F);
            labelSupplierLabel.Location = new Point(x1, y + 4);
            labelSupplierLabel.Size = new Size(130, 22);
            comboBoxSupplier.Font = new Font("Times New Roman", 10F);
            comboBoxSupplier.Location = new Point(x2, y);
            comboBoxSupplier.Size = new Size(w, 28);
            comboBoxSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            y += 38;

            // Единица измерения
            labelUnitLabel.Text = "Ед. измерения:";
            labelUnitLabel.Font = new Font("Times New Roman", 10F);
            labelUnitLabel.Location = new Point(x1, y + 4);
            labelUnitLabel.Size = new Size(130, 22);
            textBoxUnit.Font = new Font("Times New Roman", 10F);
            textBoxUnit.Location = new Point(x2, y);
            textBoxUnit.Size = new Size(200, 28);
            textBoxUnit.BorderStyle = BorderStyle.FixedSingle;
            y += 38;

            // Цена
            labelPriceLabel.Text = "Цена:";
            labelPriceLabel.Font = new Font("Times New Roman", 10F);
            labelPriceLabel.Location = new Point(x1, y + 4);
            labelPriceLabel.Size = new Size(130, 22);
            numericPrice.Font = new Font("Times New Roman", 11F);
            numericPrice.Location = new Point(x2, y);
            numericPrice.Size = new Size(150, 28);
            numericPrice.DecimalPlaces = 2;
            numericPrice.Maximum = 999999;
            numericPrice.Minimum = 0;
            y += 38;

            // Скидка
            labelDiscountLabel.Text = "Скидка (%):";
            labelDiscountLabel.Font = new Font("Times New Roman", 10F);
            labelDiscountLabel.Location = new Point(x1, y + 4);
            labelDiscountLabel.Size = new Size(130, 22);
            numericDiscount.Font = new Font("Times New Roman", 11F);
            numericDiscount.Location = new Point(x2, y);
            numericDiscount.Size = new Size(100, 28);
            numericDiscount.DecimalPlaces = 0;
            numericDiscount.Maximum = 100;
            numericDiscount.Minimum = 0;
            y += 38;

            // Остаток
            labelStockLabel.Text = "На складе:";
            labelStockLabel.Font = new Font("Times New Roman", 10F);
            labelStockLabel.Location = new Point(x1, y + 4);
            labelStockLabel.Size = new Size(130, 22);
            numericStock.Font = new Font("Times New Roman", 11F);
            numericStock.Location = new Point(x2, y);
            numericStock.Size = new Size(100, 28);
            numericStock.DecimalPlaces = 0;
            numericStock.Maximum = 999999;
            numericStock.Minimum = 0;
            y += 50;

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
            panelMain.Controls.Add(pictureBoxPhoto);
            panelMain.Controls.Add(buttonChangePhoto);
            panelMain.Controls.Add(labelNameLabel);
            panelMain.Controls.Add(textBoxName);
            panelMain.Controls.Add(labelArticleLabel);
            panelMain.Controls.Add(textBoxArticle);
            panelMain.Controls.Add(labelDescLabel);
            panelMain.Controls.Add(textBoxDescription);
            panelMain.Controls.Add(labelCategoryLabel);
            panelMain.Controls.Add(comboBoxCategory);
            panelMain.Controls.Add(labelManufacturerLabel);
            panelMain.Controls.Add(comboBoxManufacturer);
            panelMain.Controls.Add(labelSupplierLabel);
            panelMain.Controls.Add(comboBoxSupplier);
            panelMain.Controls.Add(labelUnitLabel);
            panelMain.Controls.Add(textBoxUnit);
            panelMain.Controls.Add(labelPriceLabel);
            panelMain.Controls.Add(numericPrice);
            panelMain.Controls.Add(labelDiscountLabel);
            panelMain.Controls.Add(numericDiscount);
            panelMain.Controls.Add(labelStockLabel);
            panelMain.Controls.Add(numericStock);
            panelMain.Controls.Add(buttonSave);
            panelMain.Controls.Add(buttonCancel);

            // FormProductEdit
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(248, 249, 250);
            ClientSize = new Size(680, 660);
            Controls.Add(panelMain);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "FormProductEdit";
            StartPosition = FormStartPosition.CenterParent;
            Text = "Товар";

            var iconPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Resources", "Icon.ico");
            if (File.Exists(iconPath))
            {
                using var bmp = new Bitmap(iconPath);
                Icon = System.Drawing.Icon.FromHandle(bmp.GetHicon());
            }

            panelMain.ResumeLayout(false);
            panelMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBoxPhoto).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericPrice).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericDiscount).EndInit();
            ((System.ComponentModel.ISupportInitialize)numericStock).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panelMain;
        private Label labelFormTitle;
        private Label labelId;
        private PictureBox pictureBoxPhoto;
        private Button buttonChangePhoto;
        private Label labelNameLabel;
        private TextBox textBoxName;
        private Label labelArticleLabel;
        private TextBox textBoxArticle;
        private Label labelDescLabel;
        private TextBox textBoxDescription;
        private Label labelCategoryLabel;
        private ComboBox comboBoxCategory;
        private Label labelManufacturerLabel;
        private ComboBox comboBoxManufacturer;
        private Label labelSupplierLabel;
        private ComboBox comboBoxSupplier;
        private Label labelUnitLabel;
        private TextBox textBoxUnit;
        private Label labelPriceLabel;
        private NumericUpDown numericPrice;
        private Label labelDiscountLabel;
        private NumericUpDown numericDiscount;
        private Label labelStockLabel;
        private NumericUpDown numericStock;
        private Button buttonSave;
        private Button buttonCancel;
    }
}
