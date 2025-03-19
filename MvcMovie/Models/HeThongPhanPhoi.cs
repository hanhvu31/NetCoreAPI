using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class HeThongPhanPhoi
    {
        [Key]
        public int MaHTPP { get; set; } = 0;
        public string TenHTPP { get; set; } = "";
        public List<DaiLy> DaiLys { get; set; } = new List<DaiLy>();
    }
}