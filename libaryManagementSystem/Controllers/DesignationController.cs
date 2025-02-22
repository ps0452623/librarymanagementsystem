using DataAcessLayer.Entities;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Implementation;
using Service.Interface;

namespace LibaryManagementSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignationController : ControllerBase
    {
        private readonly IDesignationService _designationService;
        public DesignationController(IDesignationService designationService)
        {
            _designationService = designationService;
        }
        [HttpGet(" GetAll ")]
        public async Task<ActionResult<IEnumerable<DesignationDto>>> GetAll()
        {
            var designations = await _designationService.GetAll();
            return Ok(designations);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<DesignationDto>> GetById(Guid id)
        {
            var designation = await _designationService.GetById(id);
            if (designation == null) return NotFound("Designation not found");
            return Ok(designation);
        }

    }
}
