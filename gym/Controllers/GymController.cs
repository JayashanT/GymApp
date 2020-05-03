using AutoMapper;
using gym.Dtos;
using gym.Entity;
using gym.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GymController:Controller
    {
        private ICommonRepository<Gym> _gymRepository;
        public GymController(ICommonRepository<Gym> gymRepository)
        {
            _gymRepository = gymRepository;
        }

        [HttpPost]
        public IActionResult AddGym([FromBody] Gym gym)
        {
           // var GymToAdd = Mapper.Map<Gym>(gym);
            _gymRepository.Add(gym);
            var Result = _gymRepository.Save();
            if (!Result) throw new ApplicationException("Error: Create Gym Not Done");
            return Ok(Mapper.Map<GymDto>(gym));

        }

        [HttpGet]
        public IActionResult GetAllGyms()
        {
            var AllGyms = _gymRepository.GetAll().ToList();
            var AllGymDto = AllGyms.Select(x => Mapper.Map<GymDto>(x));

            return Ok(AllGymDto);
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetGymById(int id)
        {
            var Result = _gymRepository.Get(x => x.Id == id).FirstOrDefault();
            if (Result == null) throw new ApplicationException("User Not Found");
            return Ok(Mapper.Map<GymDto>(Result));
        }

        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteGym(int id)
        {
            var Find = _gymRepository.Get(x => x.Id == id).FirstOrDefault();
            if (Find == null) return NotFound();
            _gymRepository.Remove(Find);
            var Result = _gymRepository.Save();

            if (Result) return Ok("Gym "+Find.Name+" deleted sucessfully");
            return BadRequest();
        }

        [HttpPut]
        public IActionResult UpdateGym([FromBody] Gym gym)
        {
            var Find = _gymRepository.Get(x => x.Id == gym.Id).FirstOrDefault();
            if (Find == null) return NotFound();

            _gymRepository.Update(gym);
            var Result = _gymRepository.Save();
            if (!Result) throw new ApplicationException("Error: Create Gym Not Done");
            return Ok(Mapper.Map<GymDto>(gym));

        }

        [HttpPost]
        [Route("UpdateAllowedMembers")]
        public IActionResult IncreaseMembers(int id,int memberAmount)
        {
            var Find = _gymRepository.Get(x => x.Id == id).FirstOrDefault();
            if (Find == null) return NotFound();

            //GymDto GymToUpdate = Mapper.Map<GymDto>(Find);
            Find.AllowedMembers = memberAmount;

            _gymRepository.Update(Mapper.Map<Gym>(Find));
            var Result = _gymRepository.Save();

            if(Result) return Ok(Find);
            return BadRequest();
            
        }
    }
}
