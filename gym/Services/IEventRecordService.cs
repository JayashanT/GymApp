using System.Collections.Generic;
using gym.Dtos;

namespace gym.Services
{
    public interface IEventRecordService
    {
        EventRecordDto Add(EventRecordDto eventRecord);
        IEnumerable<EventRecordDto> GetAllRecordsById(int id);
        EventRecordDto Update(EventRecordDto eventRecord);
    }
}