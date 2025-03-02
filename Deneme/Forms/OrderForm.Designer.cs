namespace Deneme.Forms
{
    partial class OrderForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            lblOrderStatus = new Label();
            lblTotalAmount = new Label();
            nmrTable = new NumericUpDown();
            label5 = new Label();
            button1 = new Button();
            btnMainCourse = new Button();
            btnDesserts = new Button();
            btnBeverages = new Button();
            button5 = new Button();
            dataGridView1 = new DataGridView();
            btnCompleteOrder = new Button();
            btnCancelOrder = new Button();
            btnPayment = new Button();
            btnAddNote = new Button();
            btnOrderHistory = new Button();
            ((System.ComponentModel.ISupportInitialize)nmrTable).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(19, 79);
            label1.Name = "label1";
            label1.Size = new Size(68, 20);
            label1.TabIndex = 0;
            label1.Text = "Masa No";
            // 
            // label2
            // 
            label2.AutoEllipsis = true;
            label2.Font = new Font("Segoe UI", 16F);
            label2.Location = new Point(357, 18);
            label2.Name = "label2";
            label2.Size = new Size(271, 36);
            label2.TabIndex = 0;
            label2.Text = "Sipariş Yönetimi";
            label2.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // lblOrderStatus
            // 
            lblOrderStatus.AutoSize = true;
            lblOrderStatus.Location = new Point(185, 79);
            lblOrderStatus.Name = "lblOrderStatus";
            lblOrderStatus.Size = new Size(57, 20);
            lblOrderStatus.TabIndex = 0;
            lblOrderStatus.Text = "Durum:";
            // 
            // lblTotalAmount
            // 
            lblTotalAmount.AutoSize = true;
            lblTotalAmount.Location = new Point(357, 81);
            lblTotalAmount.Name = "lblTotalAmount";
            lblTotalAmount.Size = new Size(62, 20);
            lblTotalAmount.TabIndex = 0;
            lblTotalAmount.Text = "Toplam:";
            // 
            // nmrTable
            // 
            nmrTable.Location = new Point(93, 79);
            nmrTable.Name = "nmrTable";
            nmrTable.Size = new Size(61, 27);
            nmrTable.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(538, 81);
            label5.Name = "label5";
            label5.Size = new Size(37, 20);
            label5.TabIndex = 2;
            label5.Text = "Not:";
            // 
            // button1
            // 
            button1.Location = new Point(24, 149);
            button1.Name = "button1";
            button1.Size = new Size(130, 51);
            button1.TabIndex = 3;
            button1.Text = "Starters";
            button1.UseVisualStyleBackColor = true;
            // 
            // btnMainCourse
            // 
            btnMainCourse.Location = new Point(24, 228);
            btnMainCourse.Name = "btnMainCourse";
            btnMainCourse.Size = new Size(130, 51);
            btnMainCourse.TabIndex = 3;
            btnMainCourse.Text = "Main Courses";
            btnMainCourse.UseVisualStyleBackColor = true;
            btnMainCourse.Click += btnMainCourse_Click;
            // 
            // btnDesserts
            // 
            btnDesserts.Location = new Point(24, 299);
            btnDesserts.Name = "btnDesserts";
            btnDesserts.Size = new Size(130, 51);
            btnDesserts.TabIndex = 3;
            btnDesserts.Text = "Desserts";
            btnDesserts.UseVisualStyleBackColor = true;
            btnDesserts.Click += btnDesserts_Click;
            // 
            // btnBeverages
            // 
            btnBeverages.Location = new Point(24, 373);
            btnBeverages.Name = "btnBeverages";
            btnBeverages.Size = new Size(130, 51);
            btnBeverages.TabIndex = 3;
            btnBeverages.Text = "Beverages";
            btnBeverages.UseVisualStyleBackColor = true;
            btnBeverages.Click += btnBeverages_Click;
            // 
            // button5
            // 
            button5.Location = new Point(24, 18);
            button5.Name = "button5";
            button5.Size = new Size(158, 30);
            button5.TabIndex = 3;
            button5.Text = "Ana Menüye Dön";
            button5.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Location = new Point(184, 121);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.Size = new Size(708, 340);
            dataGridView1.TabIndex = 4;
            dataGridView1.CellContentClick += dataGridView1_CellContentClick;
            dataGridView1.CellEndEdit += dataGridView1_CellEndEdit;
            // 
            // btnCompleteOrder
            // 
            btnCompleteOrder.Location = new Point(24, 527);
            btnCompleteOrder.Name = "btnCompleteOrder";
            btnCompleteOrder.Size = new Size(158, 50);
            btnCompleteOrder.TabIndex = 5;
            btnCompleteOrder.Text = "Siparişi Tamamla";
            btnCompleteOrder.UseVisualStyleBackColor = true;
            btnCompleteOrder.Click += btnCompleteOrder_Click;
            // 
            // btnCancelOrder
            // 
            btnCancelOrder.Location = new Point(201, 527);
            btnCancelOrder.Name = "btnCancelOrder";
            btnCancelOrder.Size = new Size(158, 50);
            btnCancelOrder.TabIndex = 5;
            btnCancelOrder.Text = "Siparişi İptal Et";
            btnCancelOrder.UseVisualStyleBackColor = true;
            // 
            // btnPayment
            // 
            btnPayment.Location = new Point(375, 527);
            btnPayment.Name = "btnPayment";
            btnPayment.Size = new Size(158, 50);
            btnPayment.TabIndex = 5;
            btnPayment.Text = "Ödeme Al";
            btnPayment.UseVisualStyleBackColor = true;
            btnPayment.Click += btnPayment_Click;
            // 
            // btnAddNote
            // 
            btnAddNote.Location = new Point(553, 527);
            btnAddNote.Name = "btnAddNote";
            btnAddNote.Size = new Size(158, 50);
            btnAddNote.TabIndex = 5;
            btnAddNote.Text = "Not Ekle";
            btnAddNote.UseVisualStyleBackColor = true;
            btnAddNote.Click += btnAddNote_Click;
            // 
            // btnOrderHistory
            // 
            btnOrderHistory.Location = new Point(735, 527);
            btnOrderHistory.Name = "btnOrderHistory";
            btnOrderHistory.Size = new Size(158, 50);
            btnOrderHistory.TabIndex = 6;
            btnOrderHistory.Text = "Sipariş Geçmişi";
            btnOrderHistory.UseVisualStyleBackColor = true;
            btnOrderHistory.Click += btnOrderHistory_Click;
            // 
            // OrderForm
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(904, 592);
            Controls.Add(btnOrderHistory);
            Controls.Add(btnAddNote);
            Controls.Add(btnPayment);
            Controls.Add(btnCancelOrder);
            Controls.Add(btnCompleteOrder);
            Controls.Add(dataGridView1);
            Controls.Add(button5);
            Controls.Add(btnBeverages);
            Controls.Add(btnDesserts);
            Controls.Add(btnMainCourse);
            Controls.Add(button1);
            Controls.Add(label5);
            Controls.Add(nmrTable);
            Controls.Add(label2);
            Controls.Add(lblTotalAmount);
            Controls.Add(lblOrderStatus);
            Controls.Add(label1);
            Name = "OrderForm";
            Text = "OrderForm";
            Load += OrderForm_Load;
            ((System.ComponentModel.ISupportInitialize)nmrTable).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label lblOrderStatus;
        private Label lblTotalAmount;
        private NumericUpDown nmrTable;
        private Label label5;
        private Button button1;
        private Button btnMainCourse;
        private Button btnDesserts;
        private Button btnBeverages;
        private Button button5;
        private DataGridView dataGridView1;
        private Button btnCompleteOrder;
        private Button btnCancelOrder;
        private Button btnPayment;
        private Button btnAddNote;
        private Button btnOrderHistory;
    }
}