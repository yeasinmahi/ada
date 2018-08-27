using System;
using System.Collections.Generic;
using System.Linq;
using AkijRest.IdentityServer.Repository.Dtos;
using AkijRest.IdentityServer.Repository.Helpers.DbHelpers;
using AkijRest.IdentityServer.Repository.Models.Global;
using AkijRest.IdentityServer.Repository.Repositories.Interfaces;

namespace AkijRest.IdentityServer.Repository.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly GlobalDbContext _context;

        public TaskRepository()
        {
            _context = new GlobalDbContext();
        }
        public List<TaskDto> Get()
        {
            List<TaskDto> taskDtos = new List<TaskDto>();

            if (_context != null)
            {
                List<tblTaskRecord> tasks = _context.tblTaskRecords.ToList();

                foreach (tblTaskRecord task in tasks)
                {
                    TaskDto dto = GeDto(task);

                    taskDtos.Add(dto);
                }

                return taskDtos;
            }

            throw new Exception();
        }

        public TaskDto Get(int id)
        {
            if (_context != null)
            {
                tblTaskRecord task = _context.tblTaskRecords.FirstOrDefault(x => x.intID.Equals(id));
                TaskDto dto = GeDto(task);
                return dto;
            }
            throw new Exception();
        }
        public List<TaskDto> GetByEnroll(int enroll)
        {
            List<TaskDto> dtos = new List<TaskDto>();
            if (_context != null)
            {
                List<tblTaskRecord> tasks = _context.tblTaskRecords.Where(x => x.intEnroll.Equals(enroll)).ToList();
                foreach (tblTaskRecord task in tasks)
                {
                    TaskDto dto = GeDto(task);
                    dtos.Add(dto);
                }
                return dtos;
            }
            throw new Exception();
        }

        private TaskDto GeDto(tblTaskRecord task)
        {
            TaskDto dto = new TaskDto();

            dto.Id = task.intID;
            dto.Enroll = task.intEnroll;
            dto.Date = task.dteDate;
            dto.KeyPoint = task.strKeyPoint;
            dto.Status = task.strStatus;
            dto.Remarks = task.strRemarks;
            dto.StartDate = task.dteStartTime;
            dto.EndDate = task.dteEndTime;
            dto.DeadLine = task.dteDeadLine;
            dto.CompletionPercentage = task.intComplete;
            dto.Type = task.strType;
            dto.EmailSendTime = task.dteEmailSend;

            return dto;
        }
    }
}