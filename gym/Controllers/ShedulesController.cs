using gym.Entity;
using gym.Repositories;
using gym.Services;
using gym.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShedulesController:Controller
    {
       // private ICommonRepository<Shedule> _sheduleRepository;
        private ISheduleService _sheduleService;

        public ShedulesController(ICommonRepository<Shedule> sheduleRepository,ISheduleService sheduleService)
        {
           // _sheduleRepository = sheduleRepository;
            _sheduleService = sheduleService;
            
        }

        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAllShedulesByAMember(int memberId)
        {
            return Ok(_sheduleService.GetAllSchedulesOfMember(memberId));
        }

        [HttpPost]
        [Route("createShedule")]
        public IActionResult CreateShedule(SheduleVM sheduleVM)
        {
            return Ok(_sheduleService.CreateNewShedule(sheduleVM));
        }

        [HttpPost]
        [Route("deleteShedule/{id}")]
        public IActionResult DeleteShedule(int id)
        {
            return Ok(_sheduleService.DeleteShedule(id));
        }

        public IActionResult UpdateShedule(SheduleVM sheduleVM)
        {
            return null;
        }
    }
}
