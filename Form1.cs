using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Json;
using System.IO;

namespace Winform
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadDataGridView();
            LoadCombobox();
        }
        public void LoadDataGridView()
        {
            string link = "http://localhost:90/kthp/api/sanpham";
            HttpWebRequest request = WebRequest.CreateHttp(link);
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(SanPham[]));
            object data = js.ReadObject(request.GetResponse().GetResponseStream());
            SanPham[] arr = data as SanPham[];
            dataGridView1.DataSource = arr;
        }
        public void LoadCombobox()
        {
            string link = "http://localhost:90/kthp/api/danhmuc";
            HttpWebRequest request = WebRequest.CreateHttp(link);
            WebResponse response = request.GetResponse();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(DanhMuc[]));
            object data = js.ReadObject(response.GetResponseStream());
            DanhMuc[] arr = data as DanhMuc[];
            cbxDM.DataSource = arr;
            cbxDM.ValueMember = "MaDanhMuc";
            cbxDM.DisplayMember = "TenDanhMuc";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Thêm
            string link = "http://localhost:90/kthp/api/sanpham?ma=" + txtMa.Text + "&ten=" + txtTen.Text + "&gia=" + txtDon.Text + "&madm=" + cbxDM.SelectedValue;
            HttpWebRequest resquest = WebRequest.CreateHttp(link);
            resquest.Method = "POST";
            Stream stream = resquest.GetRequestStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(resquest.GetResponse().GetResponseStream());
            bool result = (bool)data;
            if (result)
            {
                LoadDataGridView();
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm không thành công");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Sửa
            string link = "http://localhost:90/kthp/api/sanpham?ma=" + txtMa.Text + "&ten=" + txtTen.Text + "&gia=" + txtDon.Text + "&madm=" + cbxDM.SelectedValue;
            HttpWebRequest resquest = WebRequest.CreateHttp(link);
            resquest.Method = "PUT";
            Stream stream = resquest.GetRequestStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(resquest.GetResponse().GetResponseStream());
            bool result = (bool)data;
            if (result)
            {
                LoadDataGridView();
                MessageBox.Show("Sửa thành công");
            }
            else
            {
                MessageBox.Show("Sửa không thành công");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            // Xóa
            string link = "http://localhost:90/kthp/api/sanpham?ma=" + txtMa.Text;
            HttpWebRequest resquest = WebRequest.CreateHttp(link);
            resquest.Method = "DELETE";
            Stream stream = resquest.GetRequestStream();
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(bool));
            object data = js.ReadObject(resquest.GetResponse().GetResponseStream());
            bool result = (bool)data;
            if (result)
            {
                LoadDataGridView();
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Xóa không thành công");
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int d = e.RowIndex;
            txtMa.Text = dataGridView1.Rows[d].Cells[0].Value.ToString();
            txtTen.Text = dataGridView1.Rows[d].Cells[1].Value.ToString();
            txtDon.Text = dataGridView1.Rows[d].Cells[2].Value.ToString();
            foreach (DanhMuc item in cbxDM.Items)
            {
                if (item.MaDanhMuc.ToString() == dataGridView1.Rows[d].Cells[3].Value.ToString())
                {
                    cbxDM.SelectedItem = item;
                }
            }
        }
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
