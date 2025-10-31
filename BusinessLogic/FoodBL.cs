using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace BusinessLogic
{
    public class FoodBL
    {
        FoodDA foodDA = new FoodDA();
        //Phương thức lấy hết dữ liệu
        public List<Food> GetAll()
        {
            return foodDA.GetAll();
        }
        //Phương thức lấy về đối tượng Food theo khoá chính
        public Food GetByID(int ID)
        {
            List<Food> list = GetAll();
            foreach (var item in list)
            {
                if (item.ID == ID)
                    return item;
            }
            return null;
        }
        //Phương thức tìm kiếm theo khoá
        public List<Food> Find(string key)
        {
            List<Food> list = GetAll();
            List<Food> result = new List<Food>();
            foreach (var item in list)
            {
                if (item.ID.ToString().Contains(key)
                     || item.Name.Contains(key)
                     || item.Unit.Contains(key)
                     || item.Price.ToString().Contains(key)
                     || item.Notes.Contains(key))
                    result.Add(item);
            }
            return result;
        }
        //Phương thức thêm dữ liệu
        public int Insert(Food food)
        {
            return foodDA.Insert_Update_Delete(food, 0);
        }
        //Phương thức cập nhật dữ liệu
        public int Update(Food food)
        {
            return foodDA.Insert_Update_Delete(food, 1);
        }
        //Phương thức xoá dữ liệu với ID cho trước
        public int Delete(Food food)
        {
            return foodDA.Insert_Update_Delete(food, 2);
        }
    }
}
