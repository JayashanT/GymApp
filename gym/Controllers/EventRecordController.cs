using gym.Dtos;
using gym.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Controllers
{
    public class EventRecordController : Controller
    {
        private IEventRecordService _eventRecordService;
        public EventRecordController(IEventRecordService eventRecordService)
        {
            _eventRecordService = eventRecordService;
        }

        [HttpPost]
        public IActionResult Add([FromBody]  EventRecordDto eventRecord)
        {
            var result=_eventRecordService.Add(eventRecord);
            return Ok(result);
        }

        [HttpGet]
        public IActionResult GetById(int eventId)
        {
            return Ok(_eventRecordService.GetAllRecordsById(eventId));
        }

        [HttpPut]
        public IActionResult UpdateRecord([FromBody]  EventRecordDto eventRecord)
        {
            return Ok(_eventRecordService.Update(eventRecord));
        }
    }
}
