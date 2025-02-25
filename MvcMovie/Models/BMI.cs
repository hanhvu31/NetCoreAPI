namespace MvcMovie.Models
{
    public class BMI
    {
        public float Weight { get; set; } // Cân nặng (kg)
        public float Height { get; set; } // Chiều cao (m)
        public float BMIValue { get; set; } // Chỉ số BMI
        public string Result { get; set; } = "";
    }
}