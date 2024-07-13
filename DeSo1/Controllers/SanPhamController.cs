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

        public IActionResult Edit(Guid?id)
        {
            var spEdit = _db.SanPhams.Find(id);
            return View(spEdit);
        }
        [HttpPost]
        public IActionResult Edit(Guid? Id, SanPham sanPham, IFormFile imgFile)
        {
            //lấy đối tương sp hiện có trong csdl
            var spEdit = _db.SanPhams.Find(Id);
            if (spEdit == null)
            {
                return NotFound();
            }
            //cập nhật các thuộc tính củ đối tượng hiện
            spEdit.Name = sanPham.Name;
            spEdit.SoLuong = sanPham.SoLuong;

            //cạp nhật các thuộc tinh khác
            if (imgFile != null && imgFile.Length > 0)
            {
                //định nghĩa đường dẫn để lưu file
                string fileName = Path.GetFileName(imgFile.FileName);
                string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);

                //Kiểm tra và tạo thư mục nếu chưa tồn tại
                string directory = Path.GetDirectoryName(filePath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                //lưu ảnh mứi
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    imgFile.CopyTo(stream);
                }
                //cập nhật thuộc tính imgUrl với tên file mớ
                spEdit.ImgURL = fileName;
            }
                _db.Update(spEdit);
                _db.SaveChanges();
                         
            return RedirectToAction("Index");

        }
    }
}
