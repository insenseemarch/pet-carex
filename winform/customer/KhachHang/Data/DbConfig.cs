using System;
using System.Configuration;

namespace KhachHang.Data
{
    public static class DbConfig
    {
        public static string ConnStr
        {
            get
            {
                var cs = ConfigurationManager.ConnectionStrings["PetCareDb"];
                if (cs == null)
                    throw new Exception("Không tìm thấy connection string 'PetCareDb' trong App.config. Kiểm tra <connectionStrings> và tên key.");
                return cs.ConnectionString;
            }
        }
    }
}
