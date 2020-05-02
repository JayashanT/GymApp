using gym.Entity;
using gym.ViewModels;

namespace gym.Services
{
    public interface IExerciseService
    {
        Exercise Add(ExerciseVM exerciseVM);
        bool DeleteExercise(int exerciseId);
        bool UpdateExercise(ExerciseVM exerciseVM);
    }
}