using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Entities;
using MISA.Web04.Core.Interfaces;

namespace MISA.Web04.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : BaseController<Employee>
    {
        IEmployeeRepository _repository;
        IEmployeeService _service;
        public EmployeesController(IEmployeeRepository repository, IEmployeeService service):base(repository,service)
        {
            _repository = repository;
            _service = service;
        }

        /// <summary>
        /// Paniation và tìm kiếm theo tên, mã nhân viên, sđt
        /// </summary>
        /// <param name="pageSize">Số bản ghi trên 1 trang</param>
        /// <param name="pageIndex">Trang số</param>
        /// <param name="filter">tên, mã nv, sđt</param>
        /// <returns>Array</returns>
        [HttpGet("filter")]
        public IActionResult Search(int pageSize, int pageIndex, string? filter)
        {
            var employees = _repository.Search(pageSize, pageIndex, filter);
            return Ok(employees);
        }

        /// <summary>
        /// Lấy ra mã nhân viên mới nhất
        /// </summary>
        /// <returns>String</returns>
        [HttpGet("NewEmployeeCode")]
        public IActionResult NewEmployeeCode()
        {
            var newEmployeeCode = _repository.NewEmployeeCode();
            return Ok(newEmployeeCode);
        }
    }
}
