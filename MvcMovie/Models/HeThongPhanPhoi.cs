using System.Collections.Generic;

namespace MvcMovie.Models
{
    public class HeThongPhanPhoi
    {
        public int MaHTPP { get; set; } = 0;
        public string TenHTPP { get; set; } = "";
        public List<DaiLy> DaiLys { get; set; } = new List<DaiLy>();
    }
}