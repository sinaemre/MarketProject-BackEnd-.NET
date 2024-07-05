using ApplicationCore.DTO_s.CategoryDTO;
using ApplicationCore.Entities.Abstract;
using ApplicationCore.Entities.Concrete;
using AutoMapper;
using DataAccess.Services.Interface;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("MyPolicy")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepo;
        private readonly IMapper _mapper;

        public CategoriesController(ICategoryRepository categoryRepo, IMapper mapper)
        {
            _categoryRepo = categoryRepo;
            _mapper = mapper;
        }

        [HttpGet("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _categoryRepo.GetFilteredListAsync
                (
                    select: x => new GetCategoryDTO
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        CreatedDate = x.CreatedDate,
                        UpdatedDate = x.UpdatedDate,
                        Status = x.Status
                    },
                    where: x => x.Status != Status.Passive,
                    orderBy: x => x.OrderByDescending(z => z.CreatedDate)
                );

            return Ok(categories);
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var category = await _categoryRepo.GetByIdAsync(id);

            var dto = _mapper.Map<GetCategoryDTO>(category);

            if (dto is not null)
                return Ok(dto);
            
            return NotFound("Bu id'ye sahip bir categori yok!");
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromForm]CreateCategoryDTO model)
        {
            if (model == null)
                return BadRequest("Bir şeyler ters gitti!");

            if (await _categoryRepo.AnyAsync(x => x.Name == model.Name))
                return BadRequest("Bu isimde bir kategori var. Farklı bir isim seçiniz!");

            var category = _mapper.Map<Category>(model);

            await _categoryRepo.AddAsync(category);

            return Ok($"Kategori eklenmiştir! \n{category.Name}\n{category.Description}");
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromForm]UpdateCategoryDTO model)
        {
            if (model is null)
                return BadRequest("Bir şeyler ters gitti!");

            if (await _categoryRepo.AnyAsync(x => x.Name == model.Name && x.Id != model.Id))
                return BadRequest("Bu isim kullanılmaktadır. Başka bir isim seçiniz!");

            if (!await _categoryRepo.AnyAsync(x => x.Id == model.Id && x.Status != Status.Passive))
                return NotFound("Bu id'ye sahip bir kategori bulunamadı!");

            var entity = await _categoryRepo.GetByIdAsync(model.Id);
            
            var category = _mapper.Map<Category>(model);
            category.CreatedDate = entity.CreatedDate;

            await _categoryRepo.UpdateAsync(category);
            return Ok($"Kategori güncellenmiştir!\nKategori Bilgileri: \n{category.Name}\n{category.Description}");
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            if (id <= 0)
                return BadRequest("Bi şeyler ters gitti!");

            var category = await _categoryRepo.GetByIdAsync(id);

            if (category is null)
                return NotFound("Kategori bulunamadı!");

            await _categoryRepo.DeleteAsync(category);
            return Ok("Kategori silinmiştir!");
        }
    }
}
