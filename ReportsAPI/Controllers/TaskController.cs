﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsAPI.Contexts;
using ReportsAPI.Services;
using ReportsDAL;

namespace ReportsAPI.Controllers
{


    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    public class TaskController : ControllerBase
    {

        private TaskService _service;

        public TaskController(TaskService service)
        {
            _service = service;
        }


        [HttpGet("{id}")]
        public ActionResult<TaskModel> GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            return _service.DeleteById(id);
        }

        [HttpGet]
        public ActionResult<List<TaskModel>> Get()
        {
            return _service.GetAll();
        }

        [HttpPost]
        public ActionResult Post([FromForm] TaskModel task)
        {
            return _service.AddTask(task);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateTask(int id, [FromForm] string description, [FromForm] TaskModel.StatusType? status)
        {
            if (status != null)
            {
                return _service.UpdateTaskStatus((TaskModel.StatusType) status, id);
            }

            if (!string.IsNullOrEmpty(description))
            {
                return _service.UpdateTaskDescription(description, id);
            }

            return new BadRequestResult();
        }

        [HttpGet("find-by-create/{date}")]
        public ActionResult<List<TaskModel>> GetByCreateDate(string date)
        {
            try
            {
                var dateTime = DateTime.ParseExact(date, "dd.MM.yyyy", null);
                return _service.GetByCreateDate(dateTime);
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("find-by-update/{date}")]
        public ActionResult<List<TaskModel>> GetByUpdateDate(string date)
        {
            try
            {
                var dateTime = DateTime.ParseExact(date, "dd.MM.yyyy", null);
                return _service.GetByUpdateDate(dateTime);
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("find-by-head/{head}")]
        public ActionResult<List<TaskModel>> GetByHead(int head)
        {
            try
            {
                return _service.GetByHead(head);
            }
            catch
            {
                return new BadRequestResult();
            }
        }

        [HttpGet("find-by-status/{status}")]
        public ActionResult<List<TaskModel>> GetByStatus(TaskModel.StatusType status)
        {
            try
            {
                return _service.GetByStatus(status);
            }
            catch
            {
                return new BadRequestResult();
            }
        }
    }
}