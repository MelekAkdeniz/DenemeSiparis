using DenemeSiparis.Business.Services;
using DenemeSiparis.DataAccess.Context;
using DenemeSiparis.DataAccess.Reposiyories;
using DenemeSiparis.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Deneme.Forms
{
    public partial class OrderForm : Form
    {

        private readonly MenuItemService _menuItemService;
        private readonly OrderService _orderService;
        private readonly OrderDetailsService _orderDetailsService;
        private readonly TableService _tableService;

        private readonly MenuItemRpository _menuItemRpository;
        private readonly OrderRepository _orderRepository;
        private readonly OrderDetailsRepository _orderDetailsRepository;
        private readonly TableRepository _tableRepository;

        private readonly AppDbContext _context;

        private Order _currentOrder;
        private List<OrderDetail> _orderDetails;
        private int _tableNumber = 1; // Varsayılan masa numarası

        public OrderForm()
        {
            InitializeComponent();
            _context = new AppDbContext();
            _orderRepository = new OrderRepository(_context);
            _orderDetailsRepository = new OrderDetailsRepository(_context);
            _menuItemRpository = new MenuItemRpository(_context);
            _orderService = new OrderService(_orderRepository);
            _menuItemService = new MenuItemService(_menuItemRpository);
            _orderDetailsService = new OrderDetailsService(_orderDetailsRepository);

            // Sipariş detayları listesi oluştur
            _orderDetails = new List<OrderDetail>();
        }

        private void OrderForm_Load(object sender, EventArgs e)
        {
            //Form yüklenirken yeni sipariş oluştur.
            CreateNewOrder();
            // DataGridView'i ayarla
            SetupDataGridView();

            // Masa numarasını göster (NumericUpDown kontrolü eklenebilir)
            ShowTableNumber();

        }

        private void ShowTableNumber()
        {
            // Form üzerinde bir numericUpDown veya textBox kullanarak masa numarasını göster
            // numericUpDownTable.Value = _tableNumber;
            nmrTable.Value = _orderService.GetAll().Count();

            // textBoxTable.Text = _tableNumber.ToString();

        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            if (dataGridView1.Columns.Count == 0)
            {
                dataGridView1.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "RemoveButton",
                    HeaderText = "Sil",
                    Text = "Sil",
                    UseColumnTextForButtonValue = true,
                    Width = 50
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "ProductName",
                    HeaderText = "Ürün Adı",
                    DataPropertyName = "ProductName",
                    Width = 150
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Price",
                    HeaderText = "Fiyat",
                    DataPropertyName = "Price",
                    Width = 50
                });
                dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Total",
                    HeaderText = "Adet",
                    Width = 50
                });
            }
        }
        private void GetAllMenuItems()
        {
            try
            {
                var menuItems = _menuItemService.GetAll();
                // Bu metot artık kategorileri listelemek için kullanılacak
                // Her kategori için bir buton oluştur
                SetupCategoryButtons(menuItems);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Menü öğeleri alınırken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupCategoryButtons(IEnumerable<MenuItem> menuItems)
        {
            var categories = menuItems.Select(m => m.Category).Distinct().ToList();
            int yPosition = 20;

            foreach (var category in categories)
            {
                var button = new Button
                {
                    Text = category,
                    Left = 10,
                    Top = yPosition,
                    Width = 150,
                    Height = 40
                };

                button.Click += (s, e) =>
                {
                    ShowCategoryItems(category);
                };

                this.Controls.Add(button);
                yPosition += 50;
            }
        }

        private void ShowCategoryItems(string category)
        {
            try
            {
                // Tüm menü öğelerini al
                var tumMenuOgeleri = _menuItemService.GetAll();

                // Seçilen kategoriye göre filtreleme yap
                var kategoridekiUrunler = tumMenuOgeleri.Where(oge => oge.Category == category).ToList();

                if (kategoridekiUrunler.Count == 0)
                {
                    MessageBox.Show($"{category} kategorisinde ürün bulunamadı.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Kategorideki ürünleri görüntülemek için yeni bir form oluştur ve göster
                var urunlerForm = new Form
                {
                    Text = $"{category} Menüsü",
                    Size = new Size(600, 500),
                    StartPosition = FormStartPosition.CenterScreen,
                    FormBorderStyle = FormBorderStyle.FixedDialog,
                    MaximizeBox = false,
                    MinimizeBox = false
                };

                // Butonları içerecek bir flowLayoutPanel oluştur
                var flowLayoutPanel = new FlowLayoutPanel
                {
                    Dock = DockStyle.Fill,
                    AutoScroll = true,
                    Padding = new Padding(10)
                };

                // Her ürün için bir buton oluştur
                int butonGenislik = 120;
                int butonYukseklik = 80;

                foreach (var urun in kategoridekiUrunler)
                {
                    var buton = new Button
                    {
                        Text = $"{urun.Name}\n{urun.Price:C}",
                        Width = butonGenislik,
                        Height = butonYukseklik,
                        Tag = urun,  // Ürünü butonun Tag özelliğinde sakla
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    // Seçimi işlemek için tıklama olayı ekle
                    buton.Click += (s, args) =>
                    {
                        // Seçilen ürünü al
                        var secilenUrun = (MenuItem)((Button)s).Tag;

                        // Ürünü siparişe ekle
                        AddItemToOrder(secilenUrun);

                        // Formu kapat
                        urunlerForm.Close();
                    };

                    flowLayoutPanel.Controls.Add(buton);
                }

                urunlerForm.Controls.Add(flowLayoutPanel);
                urunlerForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{category} kategorisi gösterilirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AddItemToOrder(MenuItem secilenUrun)
        {

            try
            {
                // Sipariş tamamlanmışsa ürün eklenemez
                if (_currentOrder.Status == "Tamamlandı")
                {
                    MessageBox.Show("Tamamlanmış siparişe ürün eklenemez!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Aynı ürün daha önce eklenmişse adedini artır
                var existingItem = _orderDetails.FirstOrDefault(od => od.Id == secilenUrun.Id);

                if (existingItem != null)
                {
                    existingItem.Quantity++;
                    existingItem.TotalPrice = existingItem.Price * existingItem.Quantity;
                }
                else
                {
                    // Yeni ürün ekle
                    var orderDetail = new OrderDetail
                    {
                        OrderId = _currentOrder.Id,
                        MenuItemId = secilenUrun.Id,
                        MenuItemName = secilenUrun.Name,
                        Price = secilenUrun.Price,
                        Quantity = 1,
                        TotalPrice = secilenUrun.Price
                    };

                    _orderDetails.Add(orderDetail);
                }

                // DataGridView'i güncelle
                RefreshOrderDetailsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ürün eklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //// Silme butonuna tıklandığında ilgili ürünü sil
            //if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["RemoveButton"].Index)
            //{
            //    // Eğer sipariş tamamlanmadıysa ürünü silebilir
            //    if (_currentOrder.Status != "Tamamlandı")
            //    {
            //        _orderDetails.RemoveAt(e.RowIndex);
            //        RefreshOrderDetailsGrid();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Tamamlanmış siparişten ürün çıkarılamaz!", "Uyarı",
            //            MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
            // Satır ve sütun indeksinin geçerli olup olmadığını kontrol et
            if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["RemoveButton"].Index)
            {
                // Eğer sipariş tamamlanmadıysa ürünü silebilir
                if (_currentOrder.Status != "Tamamlandı")
                {
                    // Geçerli satırın ürünlerini sil
                    if (e.RowIndex < _orderDetails.Count) // Satırın geçerli olup olmadığını kontrol et
                    {
                        _orderDetails.RemoveAt(e.RowIndex);
                        RefreshOrderDetailsGrid();
                    }
                }
                else
                {
                    MessageBox.Show("Tamamlanmış siparişten ürün çıkarılamaz!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void RefreshOrderDetailsGrid()
        {
            // DataGridView'i temizle
            dataGridView1.Rows.Clear();

            // Sipariş detaylarını ekle
            foreach (var detail in _orderDetails)
            {
                dataGridView1.Rows.Add(
                    detail.MenuItemName,
                    detail.Quantity,
                    detail.Price.ToString("C"),
                    detail.TotalPrice.ToString("C")
                );
            }

            // Toplam tutarı güncelle
            UpdateTotalAmount();
        }

        private void UpdateTotalAmount()
        {
            // Toplam tutarı hesapla
            decimal total = _orderDetails.Sum(d => d.TotalPrice);

            // Toplam tutarı göster - bir Label kontrolü eklenebilir
            lblTotalAmount.Text = $"Toplam: {total:C}";
        }

        private void btnCompleteOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (_orderDetails.Count == 0)
                {
                    MessageBox.Show("Sipariş boş! Lütfen ürün ekleyin.", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Siparişi veritabanına kaydet
                _currentOrder.TotalAmount = _orderDetails.Sum(d => d.TotalPrice);
                _currentOrder.Status = "Tamamlandı";

                // OrderService ile siparişi kaydet
                _orderService.Create(_currentOrder);

                // Detayları kaydeder
                foreach (var detail in _orderDetails)
                {
                    detail.OrderId = _currentOrder.Id;
                    _orderDetailsService.Create(detail);
                }

                MessageBox.Show("Sipariş başarıyla tamamlandı!", "Bilgi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Yeni sipariş için formu sıfırla
                CreateNewOrder();
                _orderDetails.Clear();
                RefreshOrderDetailsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sipariş tamamlanırken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateNewOrder()
        {
            // Yeni bir sipariş oluştur
            _currentOrder = new Order
            {
                OrderDate = DateTime.Now,
                TableNumber = _tableNumber,
                Status = "Beklemede"
            };
        }
        // Masa değiştirmek için
        private void ChangeTable(int newTableNumber)
        {
            try
            {
                if (_currentOrder.Status == "Tamamlandı")
                {
                    MessageBox.Show("Tamamlanmış siparişin masası değiştirilemez!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                _tableNumber = newTableNumber;
                _currentOrder.TableNumber = newTableNumber;
                ShowTableNumber();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Masa değiştirilirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMainCourse_Click(object sender, EventArgs e)
        {
            ShowCategoryItems("Main Courses");
        }

        private void btnDesserts_Click(object sender, EventArgs e)
        {
            ShowCategoryItems("Desserts");
        }

        private void btnBeverages_Click(object sender, EventArgs e)
        {
            ShowCategoryItems("Beverages");
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex == dataGridView1.Columns["Quantity"].Index)
                {
                    // Sipariş tamamlanmışsa düzenlemeye izin verme
                    if (_currentOrder.Status == "Tamamlandı")
                    {
                        MessageBox.Show("Tamamlanmış sipariş düzenlenemez!", "Uyarı",
                            MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        // DataGridView'i tekrar yükle (değişiklikleri geri al)
                        RefreshOrderDetailsGrid();
                        return;
                    }

                    // Yeni miktarı al
                    if (int.TryParse(dataGridView1.Rows[e.RowIndex].Cells["Quantity"].Value.ToString(), out int newQuantity))
                    {
                        if (newQuantity <= 0)
                        {
                            // Miktar 0 veya daha az ise ürünü kaldır
                            _orderDetails.RemoveAt(e.RowIndex);
                        }
                        else
                        {
                            // Miktarı güncelle
                            _orderDetails[e.RowIndex].Quantity = newQuantity;
                            _orderDetails[e.RowIndex].TotalPrice = _orderDetails[e.RowIndex].Price * newQuantity;
                        }

                        // DataGridView'i güncelle
                        RefreshOrderDetailsGrid();
                    }
                    else
                    {
                        MessageBox.Show("Lütfen geçerli bir miktar girin!", "Hata",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);

                        // DataGridView'i tekrar yükle (değişiklikleri geri al)
                        RefreshOrderDetailsGrid();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Miktar güncellenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);

                // DataGridView'i tekrar yükle (değişiklikleri geri al)
                RefreshOrderDetailsGrid();
            }
        }
        // Sipariş durumunu görüntülemek için metot
        private void UpdateOrderStatus(string status)
        {
            _currentOrder.Status = status;

            // Durum bilgisini form üzerinde göster - bir label kontrolü eklenebilir
            lblOrderStatus.Text = $"Durum: {status}";

            // Durum değişikliğine göre butonları etkinleştir/devre dışı bırak
            if (status == "Tamamlandı")
            {
                btnCompleteOrder.Enabled = false;
                btnCancelOrder.Enabled = false;
            }
        }

        private void btnPayment_Click(object sender, EventArgs e)
        {
            try
            {
                if (_orderDetails.Count == 0)
                {
                    MessageBox.Show("Ödeme yapılacak sipariş bulunamadı!", "Uyarı",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (_currentOrder.Status != "Tamamlandı")
                {
                    var result = MessageBox.Show("Sipariş henüz tamamlanmadı. Önce siparişi tamamlamak ister misiniz?",
                        "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Siparişi tamamla
                        btnCompleteOrder_Click(sender, e);
                    }
                    else
                    {
                        return;
                    }
                }

                // Burada ödeme formunu açabilirsiniz
                decimal totalAmount = _orderDetails.Sum(d => d.TotalPrice);

                MessageBox.Show($"Toplam Ödeme Tutarı: {totalAmount:C}\nÖdeme alındı.", "Ödeme Bilgisi",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Ödeme alındıktan sonra yeni sipariş için formu sıfırla
                CreateNewOrder();
                _orderDetails.Clear();
                RefreshOrderDetailsGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ödeme işlemi sırasında hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Sipariş notları eklemek için (müşteri özel istekleri)
        private string _orderNotes = "";

        private void btnAddNote_Click(object sender, EventArgs e)
        {
            try
            {
                // Metin girişi için bir form veya input box açılabilir
                string note = Microsoft.VisualBasic.Interaction.InputBox("Sipariş için not ekleyin:", "Sipariş Notu", _orderNotes);

                if (!string.IsNullOrWhiteSpace(note))
                {
                    _orderNotes = note;
                    _currentOrder.Notes = note;

                    // Not bilgisini form üzerinde göster - bir label kontrolü eklenebilir
                    // lblOrderNotes.Text = $"Not: {note}";

                    MessageBox.Show("Sipariş notu eklendi.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Not eklenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnOrderHistory_Click(object sender, EventArgs e)
        {
            try
            {
                // Tüm siparişleri getir
                var allOrders = _orderService.GetAll();

                if (allOrders == null || !allOrders.Any())
                {
                    MessageBox.Show("Henüz kayıtlı sipariş bulunmamaktadır.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Sipariş geçmişini görüntülemek için yeni bir form açılabilir
                var historyForm = new Form
                {
                    Text = "Sipariş Geçmişi",
                    Size = new Size(800, 600),
                    StartPosition = FormStartPosition.CenterScreen
                };

                var dataGridView = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoGenerateColumns = false,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false,
                    SelectionMode = DataGridViewSelectionMode.FullRowSelect
                };

                // Sütunları ekle               

                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Date",
                    HeaderText = "Tarih",
                    Width = 150
                });

                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Table",
                    HeaderText = "Masa",
                    Width = 80
                });

                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Status",
                    HeaderText = "Durum",
                    Width = 100
                });

                dataGridView.Columns.Add(new DataGridViewTextBoxColumn
                {
                    Name = "Total",
                    HeaderText = "Toplam Tutar",
                    Width = 120
                });

                dataGridView.Columns.Add(new DataGridViewButtonColumn
                {
                    Name = "ViewButton",
                    HeaderText = "Detay",
                    Text = "Görüntüle",
                    UseColumnTextForButtonValue = true,
                    Width = 80
                });

                // Siparişleri ekle
                foreach (var order in allOrders)
                {
                    dataGridView.Rows.Add(
                        order.OrderDate.ToString("dd.MM.yyyy HH:mm"),
                        order.TableNumber,
                        order.Status,
                        order.TotalAmount.ToString("C"),
                        "Görüntüle"
                    );
                }

                // Detay görüntüleme olayı
                dataGridView.CellClick += (s, args) =>
                {
                    if (args.RowIndex >= 0 && args.ColumnIndex == dataGridView.Columns["ViewButton"].Index)
                    {
                        Guid orderId = Guid.Parse(dataGridView.Rows[args.RowIndex].Cells["Id"].Value.ToString());
                        ShowOrderDetails(orderId);
                    }
                };

                historyForm.Controls.Add(dataGridView);
                historyForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sipariş geçmişi görüntülenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowOrderDetails(Guid orderId)
        {
            try
            {
                var orderDetails = _orderDetailsService.GetAll().Where(od => od.OrderId == orderId).ToList();

                if (orderDetails == null || !orderDetails.Any())
                {
                    MessageBox.Show("Bu siparişe ait detay bulunamadı.", "Bilgi",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // Detayları görüntülemek için yeni bir form aç
                var detailsForm = new Form
                {
                    Text = $"Sipariş #{orderId} Detayları",
                    Size = new Size(600, 400),
                    StartPosition = FormStartPosition.CenterScreen
                };

                var dataGridView = new DataGridView
                {
                    Dock = DockStyle.Fill,
                    AutoGenerateColumns = false,
                    ReadOnly = true,
                    AllowUserToAddRows = false,
                    AllowUserToDeleteRows = false
                };

                // Sütunları ekle
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Name", HeaderText = "Ürün Adı", Width = 150 });
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantity", HeaderText = "Adet", Width = 50 });
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Price", HeaderText = "Birim Fiyat", Width = 100 });
                dataGridView.Columns.Add(new DataGridViewTextBoxColumn { Name = "Total", HeaderText = "Toplam", Width = 100 });

                // Detayları ekle
                foreach (var detail in orderDetails)
                {
                    dataGridView.Rows.Add(
                        detail.MenuItemName,
                        detail.Quantity,
                        detail.Price.ToString("C"),
                        detail.TotalPrice.ToString("C")
                    );
                }

                detailsForm.Controls.Add(dataGridView);
                detailsForm.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Sipariş detayları görüntülenirken hata oluştu: {ex.Message}", "Hata",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStarters_Click(object sender, EventArgs e)
        {
            ShowCategoryItems("Starters");
        }
    }
}
