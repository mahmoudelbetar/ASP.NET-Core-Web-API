using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Dtos;
using ParkyAPI.Models;
using ParkyAPI.Repository;

namespace ParkyAPI.Controllers
{
    [Route("api/Trails")]
    [ApiController]
    public class TrailsController : ControllerBase
    {
        private readonly ITrailRepository trailRepo;
        private readonly IMapper mapper;

        public TrailsController(ITrailRepository trailRepo, IMapper mapper)
        {
            this.trailRepo = trailRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllTrails()
        {
            var objList = trailRepo.GetAllTrails();
            var objListDto = mapper.Map<ICollection<TrailDto>>(objList);
            return Ok(objListDto);
        }

        [HttpGet("TrailsInNationalPark/{npId:int}")]
        public IActionResult GetTrailsInNationalPark(int npId)
        {
            var obj = trailRepo.GetTrailsInNationalPark(npId);
            var objDto = mapper.Map<ICollection<TrailDto>>(obj);
            return Ok(objDto);
        }

        [HttpGet("{id:int}", Name = "GetTrail")]
        public IActionResult GetTrail(int id)
        {
            var obj = trailRepo.GetTrail(id);
            if (obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<TrailDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreateTrail([FromBody] TrailCreateDto trailDto)
        {
            if (trailDto == null)
            {
                return BadRequest(ModelState);
            }

            if (trailRepo.TrailExists(trailDto.Name))
            {
                ModelState.AddModelError("", "National Park Already Exists!");
                return StatusCode(StatusCodes.Status404NotFound, ModelState);
            }

            if (ModelState.IsValid)
            {
                var trailObj = mapper.Map<Trail>(trailDto);
                var created = trailRepo.CreateTrail(trailObj);
                if (!created)
                {
                    ModelState.AddModelError("", $"There is an error while saving the record {trailObj.Name}");
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
                }
                else
                {
                    return CreatedAtRoute("GetTrail", new { id = trailObj.Id }, trailObj);
                }

            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateTrail")]
        public IActionResult UpdateTrail(int id, [FromBody] TrailUpdateDto trailDto)
        {
            if (trailDto == null || id != trailDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                var trailObj = mapper.Map<Trail>(trailDto);
                var updated = trailRepo.UpdateTrail(trailObj);
                if (!updated)
                {
                    ModelState.AddModelError("", $"There is an error while update the record {trailObj.Name}");
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
                }
                else
                {
                    return NoContent();
                }

            }
            else
            {
                return BadRequest(ModelState);
            }

        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteTrail(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trailObj = trailRepo.GetTrail(id);
            bool deleted = trailRepo.DeleteTrail(trailObj);
            if (!deleted)
            {
                ModelState.AddModelError("", $"There is an error while delete the record {trailObj.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
    }
}
