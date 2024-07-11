using DeSo1.Models;
using Microsoft.AspNetCore.Mvc;

namespace DeSo1.Controllers
{
    public class SanPhamController : Controller
    {
        private readonly SanPhamDbContext _db;
        public SanPhamController(SanPhamDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
           var sanPhamList = _db.SanPhams.ToList();
            return View(sanPhamList);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(SanPham sanPham, IFormFile imgFile) 
            //là 1 interface trong asp.net core sử dụng để đại diện cho các file đc upload từ clinet lên server
        {
            //xây dựng 1 đường dẫn để lưu ảnh 
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", imgFile.FileName);
            //Tao 1 đối tượng FileStream để ghi dữ liệu vào file mới tại vừa tạo
            var stream = new FileStream(path, FileMode.Create);
            //sao chép hình ảnh vào thư mục root
            imgFile.CopyTo(stream);
            //gán tên file ảnh cho thuộc tính
            sanPham.ImgURL = imgFile.FileName;
            _db.SanPhams.Add(sanPham);
            _db.SaveChanges();
            return RedirectToAction("Index");
            
        }
    }
}
