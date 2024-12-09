using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace _2021603523_NguyenThanhHa
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        XmlDocument doc = new XmlDocument();
        string tentep = @"C:\Users\pertc\Desktop\TichHop\2021603523_NguyenThanhHa\nhanvien.xml";
        int d;
        private void HienThi()
        {
            datanhanvien.Rows.Clear();
            doc.Load(tentep);
            XmlNodeList Ds = doc.SelectNodes("/ds/nhanvien");
            int sd = 0;
            datanhanvien.ColumnCount = 4;
            datanhanvien.Rows.Add();
            foreach (XmlNode nhanvien in Ds)
            {
                XmlNode ma_nv = nhanvien.SelectSingleNode("@manv");
                datanhanvien.Rows[sd].Cells[0].Value = ma_nv.InnerText.ToString();
                XmlNode ho = nhanvien.SelectSingleNode("hoten/ho");
                datanhanvien.Rows[sd].Cells[1].Value = ho.InnerText.ToString();
                XmlNode ten = nhanvien.SelectSingleNode("hoten/ten");
                datanhanvien.Rows[sd].Cells[2].Value = ten.InnerText.ToString();
                XmlNode diachi = nhanvien.SelectSingleNode("diachi");
                datanhanvien.Rows[sd].Cells[3].Value = diachi.InnerText.ToString();
                datanhanvien.Rows.Add();
                sd++;
            }

        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;
            XmlNode nhan_vien = doc.CreateElement("nhanvien");
            XmlNode hoten = doc.CreateElement("hoten");
            XmlAttribute manv = doc.CreateAttribute("manv");
            XmlNode ho = doc.CreateElement("ho");
            XmlNode ten = doc.CreateElement("ten");
            XmlNode diachi = doc.CreateElement("diachi");
            manv.InnerText = txtMaNV.Text;
            ho.InnerText = txtHo.Text;
            ten.InnerText = txtTen.Text;
            diachi.InnerText = txtDiaChi.Text;
            nhan_vien.Attributes.Append(manv);
            hoten.AppendChild(ho);
            hoten.AppendChild(ten);
            nhan_vien.AppendChild(hoten);
            nhan_vien.AppendChild(diachi);
            goc.AppendChild(nhan_vien);
            doc.Save(tentep);
            HienThi();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HienThi();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;
            XmlNode deleteNV = goc.SelectSingleNode("/ds/nhanvien[@manv='" +txtMaNV.Text+"']");
            goc.RemoveChild(deleteNV);
            doc.Save(tentep);
            HienThi();
        }

        private void datanhanvien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            d = e.RowIndex;
            txtMaNV.Text = datanhanvien.Rows[d].Cells[0].Value.ToString();
            txtHo.Text = datanhanvien.Rows[d].Cells[1].Value.ToString();
            txtTen.Text = datanhanvien.Rows[d].Cells[2].Value.ToString();
            txtDiaChi.Text = datanhanvien.Rows[d].Cells[3].Value.ToString();
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show(); 
        }

        private void button2_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;
            XmlNode oldNV = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txtMaNV.Text + "']");
            XmlNode nhan_vien = doc.CreateElement("nhanvien");
            XmlNode hoten = doc.CreateElement("hoten");
            XmlAttribute manv = doc.CreateAttribute("manv");
            XmlNode ho = doc.CreateElement("ho");
            XmlNode ten = doc.CreateElement("ten");
            XmlNode diachi = doc.CreateElement("diachi");
            manv.InnerText = txtMaNV.Text;
            ho.InnerText = txtHo.Text;
            ten.InnerText = txtTen.Text;
            diachi.InnerText = txtDiaChi.Text;
            nhan_vien.Attributes.Append(manv);
            hoten.AppendChild(ho);
            hoten.AppendChild(ten);
            nhan_vien.AppendChild(hoten);
            nhan_vien.AppendChild(diachi);
            goc.ReplaceChild(nhan_vien, oldNV);
            doc.Save(tentep);
            HienThi();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            doc.Load(tentep);
            XmlElement goc = doc.DocumentElement;
            try
            {
                XmlNode find = goc.SelectSingleNode("/ds/nhanvien[@manv='" + txtMaNV.Text + "']");
                if (find == null)
                {
                    MessageBox.Show("Khong tim thay nv");
                    datanhanvien.Rows.Clear();
                    return;
                }
                datanhanvien.Rows.Clear();
                XmlNode manv = find.SelectSingleNode("@manv");
                XmlNode ho = find.SelectSingleNode("hoten/ho");
                XmlNode ten = find.SelectSingleNode("hoten/ten");
                XmlNode diachi = find.SelectSingleNode("diachi");
                datanhanvien.Rows[0].Cells[0].Value = manv.InnerText.ToString();
                datanhanvien.Rows[0].Cells[1].Value = ho.InnerText.ToString();
                datanhanvien.Rows[0].Cells[2].Value = ten.InnerText.ToString();
                datanhanvien.Rows[0].Cells[3].Value = diachi.InnerText.ToString();
            } catch {
                MessageBox.Show("Khong tim thay nv");
                datanhanvien.Rows.Clear();
                return;
            }
        }
        //DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn tiếp tục?", "Xác nhận", MessageBoxButtons.YesNo);

        //if (result == DialogResult.Yes)
        //{
        //    // Nếu người dùng chọn Yes
        //    MessageBox.Show("Bạn đã chọn Yes.");
        //}
        //else
        //{
        //    // Nếu người dùng chọn No
        //    MessageBox.Show("Bạn đã chọn No.");
        //}


        //private void Form1_Load(object sender, EventArgs e)
        //{
        //    // Thêm các lựa chọn vào ComboBox
        //    comboBox1.Items.Add("Pizza");
        //    comboBox1.Items.Add("Burger");
        //    comboBox1.Items.Add("Pasta");

        //    // Đặt mặc định cho ComboBox và RadioButton
        //    comboBox1.SelectedIndex = 0; // Pizza
        //    radioButton1.Checked = true;  // Chay
        //}

        //private void btnSubmit_Click(object sender, EventArgs e)
        //{
        //    // Lấy giá trị từ ComboBox và RadioButton
        //    string foodChoice = comboBox1.SelectedItem.ToString();
        //    string dietChoice = radioButton1.Checked ? "Chay" : "Mặn";

        //    MessageBox.Show("Bạn chọn món: " + foodChoice + "\nChế độ ăn: " + dietChoice);
        //}
    }
}
