namespace ObutvShop
{
    partial class FormOrders
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
            panelTop = new Panel();
            labelTitle = new Label();
            labelUserInfo = new Label();
            labelSearch = new Label();
            textBoxSearch = new TextBox();
            labelFilterStatus = new Label();
            comboBoxStatus = new ComboBox();
            dataGridViewOrders = new DataGridView();
            panelBottom = new Panel();
            labelCount = new Label();
            splitContainer = new SplitContainer();
            dataGridViewItems = new DataGridView();
            labelItems = new Label();
            panelItemsHeader = new Panel();

            panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrders).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewItems).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            panelBottom.SuspendLayout();
            panelItemsHeader.SuspendLayout();
            SuspendLayout();

            // panelTop
            panelTop.BackColor = Color.FromArgb(127, 255, 0);
            panelTop.Controls.Add(labelTitle);
            panelTop.Controls.Add(labelUserInfo);
            panelTop.Controls.Add(labelSearch);
            panelTop.Controls.Add(textBoxSearch);
            panelTop.Controls.Add(labelFilterStatus);
            panelTop.Controls.Add(comboBoxStatus);
            panelTop.Dock = DockStyle.Top;
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1264, 100);

            // labelTitle
            labelTitle.Font = new Font("Times New Roman", 18F, FontStyle.Bold);
            labelTitle.Location = new Point(20, 10);
            labelTitle.Size = new Size(250, 35);
            labelTitle.Text = "Заказы";

            // labelUserInfo
            labelUserInfo.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            labelUserInfo.Font = new Font("Times New Roman", 11F);
            labelUserInfo.Location = new Point(900, 10);
            labelUserInfo.Size = new Size(350, 30);
            labelUserInfo.Text = "Гость";
            labelUserInfo.TextAlign = ContentAlignment.MiddleRight;

            // labelSearch
            labelSearch.Font = new Font("Times New Roman", 11F);
            labelSearch.Location = new Point(20, 60);
            labelSearch.Size = new Size(60, 25);
            labelSearch.Text = "Поиск:";

            // textBoxSearch
            textBoxSearch.Font = new Font("Times New Roman", 11F);
            textBoxSearch.Location = new Point(85, 57);
            textBoxSearch.Name = "textBoxSearch";
            textBoxSearch.Size = new Size(300, 28);
            textBoxSearch.PlaceholderText = "Номер заказа или клиент";
            textBoxSearch.TextChanged += TextBoxSearch_TextChanged;

            // labelFilterStatus
            labelFilterStatus.Font = new Font("Times New Roman", 11F);
            labelFilterStatus.Location = new Point(420, 60);
            labelFilterStatus.Size = new Size(65, 25);
            labelFilterStatus.Text = "Статус:";

            // comboBoxStatus
            comboBoxStatus.Font = new Font("Times New Roman", 11F);
            comboBoxStatus.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxStatus.Location = new Point(490, 57);
            comboBoxStatus.Name = "comboBoxStatus";
            comboBoxStatus.Size = new Size(200, 28);
            comboBoxStatus.SelectedIndexChanged += ComboBoxStatus_SelectedIndexChanged;

            // splitContainer
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Orientation = Orientation.Horizontal;
            splitContainer.SplitterDistance = 350;
            splitContainer.Name = "splitContainer";

            // splitContainer.Panel1 - orders grid
            splitContainer.Panel1.Controls.Add(dataGridViewOrders);

            // splitContainer.Panel2 - order items
            splitContainer.Panel2.Controls.Add(dataGridViewItems);
            splitContainer.Panel2.Controls.Add(panelItemsHeader);

            // dataGridViewOrders
            dataGridViewOrders.AllowUserToAddRows = false;
            dataGridViewOrders.AllowUserToDeleteRows = false;
            dataGridViewOrders.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewOrders.BackgroundColor = Color.White;
            dataGridViewOrders.BorderStyle = BorderStyle.None;
            dataGridViewOrders.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewOrders.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(0, 250, 154);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 11F, FontStyle.Bold);
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridViewOrders.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(0, 250, 154);
            dataGridViewOrders.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewOrders.DefaultCellStyle.Font = new Font("Times New Roman", 10F);
            dataGridViewOrders.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 255, 200);
            dataGridViewOrders.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridViewOrders.Dock = DockStyle.Fill;
            dataGridViewOrders.EnableHeadersVisualStyles = false;
            dataGridViewOrders.GridColor = Color.FromArgb(220, 220, 220);
            dataGridViewOrders.Name = "dataGridViewOrders";
            dataGridViewOrders.ReadOnly = true;
            dataGridViewOrders.RowHeadersVisible = false;
            dataGridViewOrders.RowTemplate.Height = 30;
            dataGridViewOrders.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridViewOrders.SelectionChanged += DataGridViewOrders_SelectionChanged;

            // panelItemsHeader
            panelItemsHeader.Dock = DockStyle.Top;
            panelItemsHeader.Size = new Size(1264, 30);
            panelItemsHeader.Controls.Add(labelItems);

            // labelItems
            labelItems.Dock = DockStyle.Fill;
            labelItems.Font = new Font("Times New Roman", 11F, FontStyle.Bold);
            labelItems.Text = "  Состав заказа";
            labelItems.TextAlign = ContentAlignment.MiddleLeft;

            // dataGridViewItems
            dataGridViewItems.AllowUserToAddRows = false;
            dataGridViewItems.AllowUserToDeleteRows = false;
            dataGridViewItems.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewItems.BackgroundColor = Color.FromArgb(250, 250, 250);
            dataGridViewItems.BorderStyle = BorderStyle.None;
            dataGridViewItems.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridViewItems.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dataGridViewItems.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(200, 230, 200);
            dataGridViewItems.ColumnHeadersDefaultCellStyle.Font = new Font("Times New Roman", 10F, FontStyle.Bold);
            dataGridViewItems.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dataGridViewItems.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 230, 200);
            dataGridViewItems.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewItems.DefaultCellStyle.Font = new Font("Times New Roman", 10F);
            dataGridViewItems.Dock = DockStyle.Fill;
            dataGridViewItems.EnableHeadersVisualStyles = false;
            dataGridViewItems.GridColor = Color.FromArgb(220, 220, 220);
            dataGridViewItems.Name = "dataGridViewItems";
            dataGridViewItems.ReadOnly = true;
            dataGridViewItems.RowHeadersVisible = false;
            dataGridViewItems.RowTemplate.Height = 28;
            dataGridViewItems.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // panelBottom
            panelBottom.BackColor = Color.FromArgb(127, 255, 0);
            panelBottom.Controls.Add(labelCount);
            panelBottom.Dock = DockStyle.Bottom;
            panelBottom.Size = new Size(1264, 35);

            // labelCount
            labelCount.Dock = DockStyle.Fill;
            labelCount.Font = new Font("Times New Roman", 10F);
            labelCount.Padding = new Padding(20, 0, 20, 0);
            labelCount.Text = "0 из 0";

            // FormOrders
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(1264, 681);
            Controls.Add(splitContainer);
            Controls.Add(panelBottom);
            Controls.Add(panelTop);
            Name = "FormOrders";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Заказы";
            WindowState = FormWindowState.Maximized;

            panelTop.ResumeLayout(false);
            panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridViewOrders).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridViewItems).EndInit();
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            panelBottom.ResumeLayout(false);
            panelItemsHeader.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Label labelTitle;
        private Label labelUserInfo;
        private Label labelSearch;
        private TextBox textBoxSearch;
        private Label labelFilterStatus;
        private ComboBox comboBoxStatus;
        private SplitContainer splitContainer;
        private DataGridView dataGridViewOrders;
        private DataGridView dataGridViewItems;
        private Panel panelItemsHeader;
        private Label labelItems;
        private Panel panelBottom;
        private Label labelCount;
    }
}
