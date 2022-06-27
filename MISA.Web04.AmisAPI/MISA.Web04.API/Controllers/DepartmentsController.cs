using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Interfaces;
using MISA.Web04.Core.Entities;

namespace MISA.Web04.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : BaseController<Department>
    {
        IDepartmentRepository _repository;
        IDepartmentService _service;
        public DepartmentsController(IDepartmentRepository repository, IDepartmentService service):base(repository,service)
        {
            _repository = repository;
            _service = service;
        }
    }
}
