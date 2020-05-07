using AutoMapper;
using gym.Dtos;
using gym.Entity;
using gym.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace gym.Services
{
    public class EventRecordService : IEventRecordService
    {
        private ICommonRepository<EventRecord> _eventRecordRepository;
        public EventRecordService(ICommonRepository<EventRecord> eventRecordRepository)
        {
            _eventRecordRepository = eventRecordRepository;
        }

        public EventRecordDto Add(EventRecordDto eventRecord)
        {
            EventRecordDto  recordDto= new EventRecordDto()
            {
                Date=DateTime.Now,
                Weight=eventRecord.Weight,
                EventId=eventRecord.EventId
            };
            EventRecord RecordToAdd = Mapper.Map<EventRecord>(recordDto);
            _eventRecordRepository.Add(RecordToAdd);
            var Result = _eventRecordRepository.Save();

            if (Result)
                return Mapper.Map<EventRecordDto>(RecordToAdd);
            else
                return null;
        }

        public EventRecordDto Update(EventRecordDto eventRecord)
        {
            var Record = _eventRecordRepository.Get(x => x.Id == eventRecord.Id).SingleOrDefault();

            if (Record == null)
                return null;

            EventRecordDto recordDto = new EventRecordDto()
            {
                Date = DateTime.Now,
                Weight = eventRecord.Weight,
                EventId = eventRecord.EventId
            };
            EventRecord RecordToUpdate = Mapper.Map<EventRecord>(recordDto);
            _eventRecordRepository.Update(RecordToUpdate);
            var Result = _eventRecordRepository.Save();

            if (Result)
                return Mapper.Map<EventRecordDto>(RecordToUpdate);
            else
                return null;
        }

        public IEnumerable<EventRecordDto> GetAllRecordsById(int id)
        {
            var allRecordsOfAEvent = _eventRecordRepository.Get(x => x.EventId == id).OrderByDescending(x => x.Date).ToList();
            var allRecordsOfAEventData = allRecordsOfAEvent.Select(x => Mapper.Map<EventRecordDto>(x));
            return allRecordsOfAEventData;
        }


    }
}
