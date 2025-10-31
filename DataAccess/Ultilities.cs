using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
namespace DataAccess
{
    public class Ultilities
    {
        //lấy chuỗi kết nối từ file App.config
        private static string strName = "ConnectionString";
        public static string ConnectionString = ConfigurationManager.ConnectionStrings[strName].ConnectionString;
        //các biến của bảng Food
        public static string Food_GetAll= "Food_GetAll";
        public static string Food_InsertUpdateDelete = "Food_InsertUpdateDelete";
        //Các biến của bảng Category
        public static string Category_GetAll = "Category_GetAll";
        public static string Category_InsertUpdateDelete = "Category_InsertUpdateDelete";
    }
}
