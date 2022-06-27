using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web04.Core.Interfaces;
using MISA.Web04.Core.Entities;

namespace MISA.Web04.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PositionsController : BaseController<Position>
    {
        IPositionRepository _repository;
        IPositionService _service;
        public PositionsController(IPositionRepository repository, IPositionService service) : base(repository, service)
        {
            _repository = repository;
            _service = service;
        }
    }
}
