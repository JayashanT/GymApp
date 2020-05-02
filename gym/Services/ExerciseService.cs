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
    public class ExerciseService : IExerciseService
    {
        private ICommonRepository<Exercise> _exerciseRepository;
        private ICommonRepository<Image> _imageRepository;
        public ExerciseService(ICommonRepository<Exercise> exerciseRepository, ICommonRepository<Image> imageRepository)
        {
            _exerciseRepository = exerciseRepository;
            _imageRepository = imageRepository;
        }

        public Exercise Add(ExerciseVM exerciseVM)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {


                    ExerciseDto exerciseDto = new ExerciseDto()
                    {
                        Type = exerciseVM.Type,
                        Name = exerciseVM.Name,
                    };

                    Exercise exerciseToAdd = Mapper.Map<Exercise>(exerciseDto);
                    Console.WriteLine(exerciseToAdd);
                    _exerciseRepository.Add(exerciseToAdd);
                    bool result = _exerciseRepository.Save();
                    if (exerciseVM.Images!=null)
                    {
                        foreach (var image in exerciseVM.Images)
                        {
                            ImageDto imageDto = new ImageDto()
                            {
                                Url = image.Url,
                                ExerciseId = Mapper.Map<Exercise>(exerciseToAdd).Id,
                            };
                            Image imageToAdd = Mapper.Map<Image>(imageDto);
                            _imageRepository.Add(imageToAdd);
                            _imageRepository.Save();
                        }

                    }
                    
                    scope.Complete();
                    return Mapper.Map<Exercise>(exerciseToAdd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }

        }

        public bool DeleteExercise(int exerciseId)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    Exercise exercise = _exerciseRepository.Get(x => x.Id == exerciseId).FirstOrDefault();
                    List<Image> images = _imageRepository.Get(x => x.ExerciseId == exerciseId).ToList();

                    foreach (var image in images)
                    {
                        _imageRepository.Remove(image);
                        _imageRepository.Save();
                    }
                    _exerciseRepository.Remove(exercise);
                    _exerciseRepository.Save();
                    scope.Complete();
                    return true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(new Exception(e.Message));
                return false;
            }
        }

        public bool UpdateExercise(ExerciseVM exerciseVM)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    var excerciseInDb = _exerciseRepository.Get(x => x.Id == exerciseVM.Id).FirstOrDefault();

                    if (excerciseInDb != null)
                    {
                        ExerciseDto exerciseDto = new ExerciseDto()
                        {
                            Id = exerciseVM.Id,
                            Name = exerciseVM.Name,
                            Type = exerciseVM.Type
                        };
                        Exercise exerciseToUpdate = Mapper.Map<Exercise>(exerciseDto);
                        _exerciseRepository.Update(exerciseToUpdate);
                        bool Result = _exerciseRepository.Save();

                        foreach (var image in exerciseVM.Images)
                        {
                            var eventInDb = _imageRepository.Get(x => x.Id == image.Id).FirstOrDefault();
                            if (eventInDb != null)
                            {
                                ImageDto imageDto = new ImageDto()
                                {
                                    Id = image.Id,
                                    Url = image.Url,
                                    ExerciseId = image.ExerciseId
                                };
                                Image imageTOAdd = Mapper.Map<Image>(imageDto);
                                _imageRepository.Update(imageTOAdd);
                                _imageRepository.Save();

                            }
                            else
                            {
                                ImageDto imageDto = new ImageDto()
                                {
                                    Url = image.Url,
                                    ExerciseId = image.ExerciseId,
                                };
                                Image imageTOAdd = Mapper.Map<Image>(imageDto);
                                _imageRepository.Update(imageTOAdd);
                                _imageRepository.Save();

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
