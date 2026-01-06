using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhachHang.Common
{
    public interface ISearchable
    {
        // keyword rỗng => reset về danh sách đầy đủ
        void ApplySearch(string keyword);
    }
}
