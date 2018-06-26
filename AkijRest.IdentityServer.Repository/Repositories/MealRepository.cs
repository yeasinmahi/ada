using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models;
using AkijRest.IdentityServer.Repository.Models.Hr;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class MealRepository : IMealRepository
    {
        private readonly HrDbContext _hrDbContext;

        public MealRepository()
        {
            _hrDbContext = new HrDbContext();
        }
        public List<MealDto> Get()
        {
            List<MealDto> mealDtos = new List<MealDto>();

            if (_hrDbContext != null)
            {
                List<tblDay> tblDays
                    = _hrDbContext.tblDays.ToList();

                foreach (tblDay tblDay in tblDays)
                {
                    MealDto dto = new MealDto();

                    dto.Id = tblDay.intDayOffId;
                    dto.DayName = tblDay.strDayName;
                    dto.MenuList = tblDay.strMenuList;

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
            if (_hrDbContext != null)
            {
                if (!String.IsNullOrWhiteSpace(day))
                {
                    tblDay tblDay = _hrDbContext.tblDays.FirstOrDefault(x => x.strDayName.Equals(day));
                    if (tblDay != null)
                    {
                        MealDto dto = new MealDto
                        {
                            Id = tblDay.intDayOffId,
                            DayName = tblDay.strDayName,
                            MenuList = tblDay.strMenuList

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