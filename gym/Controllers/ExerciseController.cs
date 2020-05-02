using AutoMapper;
using gym.Dtos;
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
    public class ExerciseController : Controller
    {
        private ICommonRepository<Exercise> _exerciseRepository;
        private IExerciseService _exerciseService;
        private ICommonRepository<Image> _imageRepository;
        public ExerciseController(ICommonRepository<Exercise> exerciseRepository, IExerciseService exerciseService, ICommonRepository<Image> imageRepository)
        {
            _exerciseRepository = exerciseRepository;
            _imageRepository = imageRepository;
            _exerciseService = exerciseService;
        }

        [HttpPost]
        [Route("AddAExercise")]
        public IActionResult Add([FromBody] ExerciseVM execiseVM)
        {
            return Ok(_exerciseService.Add(execiseVM));
        }

        [HttpPost]
        [Route("DeleteExcerise/{id}")]
        public IActionResult DeleteAExcercise(int Id)
        {
            return Ok(_exerciseService.DeleteExercise(Id));
        }

        [HttpPost]
        [Route("UpdateExcerise")]
        public IActionResult UpdateAExcercise(ExerciseVM execiseVM)
        {
            return Ok(_exerciseService.UpdateExercise(execiseVM));
        }

        [HttpGet]
        [Route("GetAllExcercises")]
        public IActionResult GetallExcercises()
        {
            var allExercises = _exerciseRepository.GetAll().ToList();
            var allExcercisesDto = allExercises.Select(x => Mapper.Map<ExerciseDto>(x));

            return Ok(allExcercisesDto);
        }

       /* [HttpGet]
        [Route("GetExcerciseById/{id}")]
        public IActionResult GetExcerciseTypes(int id)
        {
            return null;
        }*/

        [HttpGet]
        [Route("GetExcerciseById/{id}")]
        public IActionResult GetExcerciseById(int id)
        {
            Exercise excerciseById = _exerciseRepository.Get(x=>x.Id==id).FirstOrDefault();
            List<Image> images = _imageRepository.Get(x => x.ExerciseId == id).ToList();

            return Ok(images);
        }


    }
}
