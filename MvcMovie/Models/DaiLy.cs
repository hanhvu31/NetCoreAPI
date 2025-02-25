using System; // Nhớ thêm using System nếu bạn dùng DateTime

namespace MvcMovie.Models // Thay YourNamespace bằng namespace của bạn
{
    public class DaiLy
    {
        public int MaDaiLy { get; set; } = 0;
        public string TenDaiLy { get; set; } = "";
        public string DiaChi { get; set; } = "";
        public string NguoiDaiDien { get; set; } = "";
        public string DienThoai { get; set; } = "";

        public int MaHTPP { get; set; } // Khóa ngoại

        public HeThongPhanPhoi HeThongPhanPhoi { get; set; } // Thuộc tính tham chiếu

        // Cách 1: Khởi tạo HeThongPhanPhoi trong constructor
        public DaiLy()
        {
            HeThongPhanPhoi = new HeThongPhanPhoi(); // Hoặc khởi tạo với dữ liệu cụ thể nếu cần
        }
    }
}