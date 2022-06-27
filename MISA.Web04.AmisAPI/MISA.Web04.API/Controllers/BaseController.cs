using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Interfaces;

namespace MISA.Web04.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController<MISAEntity> : ControllerBase
    {
        IBaseRepository<MISAEntity> _repository;
        IBaseService<MISAEntity> _service;
        public BaseController(IBaseRepository<MISAEntity> repository, IBaseService<MISAEntity> service)
        {
            _repository = repository;
            _service = service;
        }
        /// <summary>
        /// Lấy tất cả đối tượng
        /// </summary>
        /// <returns>Array</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(_repository.GetAll());

        }

        /// <summary>
        /// lấy đối tượng theo id
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>Entity</returns>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {

            return Ok(_repository.GetById(id));

        }

        /// <summary>
        /// Thêm đối tượng
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>StatusCode</returns>
        [HttpPost]
        public IActionResult Insert(MISAEntity entity)
        {
            return StatusCode(201, _service.InsertService(entity));
        }

        /// <summary>
        /// Câpk nhật 1 đối tượng
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>StatusCode</returns>
        [HttpPut("{id}")]
        public IActionResult Update(MISAEntity entity)
        {  
            return StatusCode(201, _repository.Update(entity));
        }

        /// <summary>
        /// Xoá 1 đối tượng
        /// </summary>
        /// <param name="id">EntityId</param>
        /// <returns>StatusCode</returns>
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            return StatusCode(201, _repository.Delete(id));
        }
    }
}
