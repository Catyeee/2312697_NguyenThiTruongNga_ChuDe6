using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;
using BusinessLogic;

namespace RestaurantManagementProject
{
    public partial class frmFood : Form
    {
        public frmFood()
        {
            InitializeComponent();
        }
        //Sự kiện nút Thoát
        private void cmdExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        //Sự kiện nút Nhập lại
        private void cmdClear_Click(object sender, EventArgs e)
        {
            txtName.Text = "";
            txtPrice.Text = "";
            txtUnit.Text = "";
            txtNotes.Text = "";
            if(cbbCategory.Items.Count > 0)
                cbbCategory.SelectedIndex = 0;
        }
        List<Category> listCategory=new List<Category>();
        List<Food> listFood = new List<Food>();
        Food foodCurrent=new Food();

        private void frmFood_Load(object sender, EventArgs e)
        {
            LoadCategory();
            LoadFoodDataToListView();
        }
        //Hàm load danh mục lên combobox
        private void LoadCategory()
        {
            CategoryBL categoryBL = new CategoryBL();
            listCategory =categoryBL.GetAll();
            cbbCategory.DataSource = listCategory;
            cbbCategory.ValueMember = "ID";
            cbbCategory.DisplayMember = "Name";
        }
        //Hàm load dữ liệu món ăn lên ListView
        private void LoadFoodDataToListView()
        {
            FoodBL foodBL = new FoodBL();
            listFood = foodBL.GetAll();
            int count = 1;
            lsvFood.Items.Clear();
            foreach(var food in listFood)
            {
                ListViewItem item=lsvFood.Items.Add(count.ToString());
                item.SubItems.Add(food.Name);
                item.SubItems.Add(food.Unit);
                item.SubItems.Add(food.Price.ToString());
                string foodName=listCategory.Find(x=> x.ID == food.FoodCategoryID).Name;
                item.SubItems.Add(foodName);
                item.SubItems.Add(food.Notes);
                count++;

            }    
        }
        //Sự kiện Click lên dòng của ListView
        private void lsvFood_Click(object sender, EventArgs e)
        {
            for(int i=0; i < lsvFood.Items.Count; i++)
            {
                if (lsvFood.Items[i].Selected)
                {
                    foodCurrent = listFood[i];
                    txtName.Text = foodCurrent.Name;
                    txtUnit.Text = foodCurrent.Unit;
                    txtPrice.Text = foodCurrent.Price.ToString();
                    txtNotes.Text = foodCurrent.Notes;
                    cbbCategory.SelectedValue = listCategory.FindIndex(x=>x.ID==foodCurrent.FoodCategoryID);
                }
            }
        }
        //Phương thức thêm dữ liệu cho bảng Food
        //Trả về số dương nếu thành công, ngược lại trả về số âm
        public int InsertFood()
        {
            Food food = new Food();
            food.ID = 0;
            if(txtName.Text=="" || txtUnit.Text == "" || txtPrice.Text == "")
            {
                MessageBox.Show("Chưa nhập dữ liệu cho các ô, vui lòng nhập lại");
            }
            else
            {
                food.Name = txtName.Text;
                food.Unit = txtUnit.Text;
                food.Notes = txtNotes.Text;
                int price=0;
                try
                {
                    price=int.Parse(txtPrice.Text);
                }
                catch
                {
                    price = 0;
                }
                food.Price = price;
                food.FoodCategoryID=int.Parse(cbbCategory.SelectedValue.ToString());
                FoodBL foodBL = new FoodBL();
                return foodBL.Insert(food);
            }
            return -1;
        }
        private void cmdAdd_Click(object sender, EventArgs e)
        {
            int result = InsertFood();
            if(result > 0)
            {
                MessageBox.Show("Thêm món ăn thành công");
                LoadFoodDataToListView();
            }
            else
            {
                MessageBox.Show("Thêm món ăn thất bại, vui lòng kiểm tra lại dữ liệu");
            }
        }

        private void cmdDelete_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Bạn có chắc chắn muốn xoá món ăn?","Thông báo",
                MessageBoxButtons.YesNo,MessageBoxIcon.Warning)==DialogResult.Yes)
            {
                FoodBL foodBL = new FoodBL();
                if(foodBL.Delete(foodCurrent) > 0)
                {
                    MessageBox.Show("Xoá món ăn thành công");
                    LoadFoodDataToListView();
                }
                else
                {
                    MessageBox.Show("Xoá món ăn thất bại, vui lòng kiểm tra lại dữ liệu");
                }
            }    
        }
        //Phương thức cập nhật dữ liệu cho bảng Food
        //Trả về số dương nếu thành công, ngược lại trả về số âm
        //public int UpdateFood()
        //{
        //    Food food = new Food();
        //}
        private void cmdUpdate_Click(object sender, EventArgs e)
        {

        }
    }
}
