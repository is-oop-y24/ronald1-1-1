using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReportsAPI.Contexts;
using ReportsAPI.Services;
using ReportsDAL;

namespace ReportsAPI.Controllers
{

    
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeeController : ControllerBase
    {

        private EmployeeService _service;

        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }


        [HttpGet("{id}")]
        public ActionResult<EmployeeModel> GetById(int id)
        {
            return _service.GetById(id);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteById(int id)
        {
            return _service.DeleteById(id);
        }
        
        [HttpGet]
        public ActionResult<List<EmployeeModel>> Get()
        {
            return _service.GetAll();
        }

        [HttpPost]
        public ActionResult Post([FromForm] EmployeeModel employee)
        {
            return _service.AddEmployee(employee);
        }
           
    }
}