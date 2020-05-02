using AutoMapper;
using gym.Dtos;
using gym.Entity;
using gym.Repositories;
using gym.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace gym.Services
{
    public class SheduleService : ISheduleService
    {
        private ICommonRepository<Shedule> _sheduleRepository;
        private ICommonRepository<Event> _eventRepository;
        public SheduleService(ICommonRepository<Shedule> sheduleRepository, ICommonRepository<Event> eventRepository)
        {
            _sheduleRepository = sheduleRepository;
            _eventRepository = eventRepository;
        }

        public IEnumerable<SheduleDto> GetAllSchedulesOfMember(int memberId)
        {
            var allShedules = _sheduleRepository.Get(x => x.MemberId == memberId).OrderByDescending(x => x.CreatedAt);
            var allShedulesdata = allShedules.Select(x => Mapper.Map<SheduleDto>(x));
            return allShedulesdata;
        }

        public SheduleDto CreateNewShedule(SheduleVM sheduleVM)
        {
            try
            {
                using(TransactionScope scope=new TransactionScope())
                {
                    SheduleDto sheduleDto = new SheduleDto()
                    {
                        CreatedAt = DateTime.Now,
                        Name=sheduleVM.Name,
                        MemberId = sheduleVM.MemberId
                    };
                    Shedule sheduleToAdd = Mapper.Map<Shedule>(sheduleDto);
                    _sheduleRepository.Add(sheduleToAdd);
                    bool Result = _sheduleRepository.Save();

                    foreach (var workoutEvent in sheduleVM.Events)
                    {
                        EventDto eventDto = new EventDto()
                        {
                            NoOfSet = workoutEvent.NoOfSet,
                            Repition = workoutEvent.Repition,
                            BreakTime = workoutEvent.BreakTime,
                            SheduleId = Mapper.Map<Shedule>(sheduleToAdd).Id,
                            ExerciseId = workoutEvent.ExerciseId
                        };
                        Event eventToAdd = Mapper.Map<Event>(eventDto);
                        _eventRepository.Add(eventToAdd);
                        _eventRepository.Save();
                    }
                    scope.Complete();
                    return Mapper.Map<SheduleDto>(sheduleToAdd);
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(new Exception(e.Message));
                return null;
            }
            
        }

        public bool DeleteShedule(int SheduleId)
        {
            try
            {
                using(TransactionScope scope=new TransactionScope())
                {
                    Shedule shedule = _sheduleRepository.Get(x => x.Id == SheduleId).FirstOrDefault();
                    List<Event> Events = _eventRepository.Get(x => x.SheduleId == SheduleId).ToList();

                    foreach(var workout in Events)
                    {
                        _eventRepository.Remove(workout);
                        _eventRepository.Save();
                    }
                    _sheduleRepository.Remove(shedule);
                    _sheduleRepository.Save();
                    scope.Complete();
                    return true;
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(new Exception(e.Message));
                return false;
            }
        }

        public bool UpdateShedule(SheduleVM sheduleVM)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var sheduleInDB = _sheduleRepository.Get(x => x.Id == sheduleVM.Id).FirstOrDefault();

                    if (sheduleInDB != null)
                    {
                        SheduleDto sheduleDto = new SheduleDto()
                        {
                            Id = sheduleVM.Id,
                            Name=sheduleVM.Name,
                            CreatedAt = DateTime.Now,
                            MemberId = sheduleVM.MemberId
                        };
                        Shedule sheduleToUpdate = Mapper.Map<Shedule>(sheduleDto);
                        _sheduleRepository.Update(sheduleToUpdate);
                        bool Result = _sheduleRepository.Save();

                        foreach (var workoutEvent in sheduleVM.Events)
                        {
                            var eventInDb = _eventRepository.Get(x => x.Id == workoutEvent.Id).FirstOrDefault();
                            if (eventInDb != null)
                            {
                                EventDto eventDto = new EventDto()
                                {
                                    Id = workoutEvent.Id,
                                    NoOfSet = workoutEvent.NoOfSet,
                                    Repition = workoutEvent.Repition,
                                    BreakTime = workoutEvent.BreakTime,
                                    SheduleId = Mapper.Map<Shedule>(sheduleInDB).Id,
                                    ExerciseId = workoutEvent.ExerciseId
                                };
                                Event eventToAdd = Mapper.Map<Event>(eventDto);
                                _eventRepository.Update(eventToAdd);
                                _eventRepository.Save();
                                
                            }
                            else
                            {
                                EventDto eventDto = new EventDto()
                                {
                                    NoOfSet = workoutEvent.NoOfSet,
                                    Repition = workoutEvent.Repition,
                                    BreakTime = workoutEvent.BreakTime,
                                    SheduleId = Mapper.Map<Shedule>(sheduleInDB).Id,
                                    ExerciseId = workoutEvent.ExerciseId
                                };
                                Event eventToAdd = Mapper.Map<Event>(eventDto);
                                _eventRepository.Add(eventToAdd);
                                _eventRepository.Save();
                                
                            }


                        }
                        scope.Complete();
                        return true;
                    }
                    else return false; 
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
            }
        }

}
