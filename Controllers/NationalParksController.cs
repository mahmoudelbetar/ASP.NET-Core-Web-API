using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ParkyAPI.Dtos;
using ParkyAPI.Models;
using ParkyAPI.Repository;

namespace ParkyAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NationalParksController : ControllerBase
    {
        private readonly INationalParkRepository npRepo;
        private readonly IMapper mapper;

        public NationalParksController(INationalParkRepository npRepo, IMapper mapper)
        {
            this.npRepo = npRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult GetAllNationalParks()
        {
            var objList = npRepo.GetAllNationalParks();
            var objListDto = mapper.Map<ICollection<NationalParkDto>>(objList);
            return Ok(objListDto);
        }

        [HttpGet("{id:int}", Name = "GetNationalPark")]
        public IActionResult GetNationalPark(int id)
        {
            var obj = npRepo.GetNationalPark(id);
            if(obj == null)
            {
                return NotFound();
            }
            var objDto = mapper.Map<NationalParkDto>(obj);
            return Ok(objDto);
        }

        [HttpPost]
        public IActionResult CreateNationalPark([FromBody] NationalParkDto nationalParkDto)
        {
            if(nationalParkDto == null)
            {
                return BadRequest(ModelState);
            }

            if (npRepo.NationalParkExists(nationalParkDto.Name))
            {
                ModelState.AddModelError("", "National Park Already Exists!");
                return StatusCode(StatusCodes.Status404NotFound, ModelState);
            }

            if (ModelState.IsValid)
            {
                var nationalParkObj = mapper.Map<NationalPark>(nationalParkDto);
                var created = npRepo.CreateNationalPark(nationalParkObj);
                if (!created)
                {
                    ModelState.AddModelError("", $"There is an error while saving the record {nationalParkObj.Name}");
                    return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
                }
                else
                {
                    return CreatedAtRoute("GetNationalPark", new { id = nationalParkObj.Id }, nationalParkObj);
                }
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id:int}", Name = "UpdateNationalPark")]
        public IActionResult UpdateNationalPark(int id, [FromBody] NationalParkDto nationalParkDto)
        {
            if (nationalParkDto == null || id != nationalParkDto.Id)
            {
                return BadRequest(ModelState);
            }

            if (ModelState.IsValid)
            {
                var nationalParkObj = mapper.Map<NationalPark>(nationalParkDto);
                var updated = npRepo.UpdateNationalPark(nationalParkObj);
                if (!updated)
                {
                    ModelState.AddModelError("", $"There is an error while update the record {nationalParkObj.Name}");
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
        public IActionResult DeleteNationalPark(int id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var nationalParkObj = npRepo.GetNationalPark(id);
            bool deleted = npRepo.DeleteNationalPark(nationalParkObj);
            if (!deleted)
            {
                ModelState.AddModelError("", $"There is an error while delete the record {nationalParkObj.Name}");
                return StatusCode(StatusCodes.Status500InternalServerError, ModelState);
            }

            return NoContent();
        }
    }
}
