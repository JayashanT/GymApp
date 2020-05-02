using System.Collections.Generic;
using gym.Dtos;
using gym.ViewModels;

namespace gym.Services
{
    public interface ISheduleService
    {
        SheduleDto CreateNewShedule(SheduleVM sheduleVM);
        bool DeleteShedule(int SheduleId);
        IEnumerable<SheduleDto> GetAllSchedulesOfMember(int memberId);
        bool UpdateShedule(SheduleVM sheduleVM);
    }
}