using System.ComponentModel.DataAnnotations;

namespace DeSo1.Models
{
    public class SanPham
    {
        [Key]
        public Guid ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int SoLuong {  get; set; }
        public string ImgURL {  get; set; }
    }
}
