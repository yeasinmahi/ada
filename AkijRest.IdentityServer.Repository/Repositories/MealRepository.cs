using System;
using System.Collections.Generic;
using System.Linq;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly IdentityServerDbContext _context;

        public MealRepository()
        {
            _context = new IdentityServerDbContext();
        }
        public List<MealDto> Get()
        {
            List<MealDto> mealDtos = new List<MealDto>();

            if (_context != null)
            {
                List<Meal> meals = _context.Meals.ToList();

                foreach (Meal meal in meals)
                {
                    MealDto dto = new MealDto();

                    dto.Id = meal.Id;
                    dto.DayName = meal.DayName;
                    dto.MenuList = meal.MenuList;
                    dto.AltMenuList = meal.AltMenuList;

                    mealDtos.Add(dto);
                }

                return mealDtos;
            }

            throw new Exception();
        }
        public MealDto Get(int id)
        {
            //TODO: Have To implements
            return new MealDto();
        }
        public MealDto GetByDay(string day)
        {
            if (_context != null)
            {
                if (!String.IsNullOrWhiteSpace(day))
                {
                    Meal meal = _context.Meals.FirstOrDefault(x => x.DayName.Equals(day));
                    if (meal != null)
                    {
                        MealDto dto = new MealDto
                        {
                            Id = meal.Id,
                            DayName = meal.DayName,
                            MenuList = meal.MenuList,
                            AltMenuList = meal.AltMenuList

                        };
                        return dto;
                    }
                    else
                    {
                        //todo: tblDay data not found
                    }

                }
                else
                {
                    //todo: day parm can not be empty
                }

            }
            else
            {
                //todo: context is null
            }
            throw new Exception();
        }
    }
}