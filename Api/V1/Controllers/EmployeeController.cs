using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Api.Data;
using Api.Models;
using Api.V1.Dtos;
using System.Threading.Tasks;
using Api.Helpers;
using System;

namespace Api.Controllers
{
    /// <summary>
    /// 1.0 Controller Version
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository _repo;
        private readonly IMapper _mapper;

        public EmployeeController(IRepository repo, IMapper mapper)
        {
            _repo = repo;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] PageParams pageParams)
        {
            try
            {
                var employees = await _repo.GetAllEmployeesAsync(pageParams);

                //var employeesResult = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

                Response.AddPagination(employees.CurrentPage, employees.PageSize, employees.TotalCount, employees.TotalPages);

                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest($"error: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetByEmployeeId(int id)
        {
            try
            {
                var employee = _repo.GetEmployeeById(id);
                if (employee == null) return NotFound("Employee not found!");

                //var employeeDto = _mapper.Map<EmployeeRegisterDto>(employee);

                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest($"error: {ex.Message}");
            }
        }

        [HttpGet("{id}/monthlycost/{month}/{year}")]
        public IActionResult GetEmployeeMonthlyCost(int id, int month, int year)
        {
            try
            {
                var employee = _repo.GetEmployeeById(id);
                if (employee == null) return NotFound("Employee not found!");

                DateTime[] holydays = DateTimeExtensions.getHolydays(year.ToString());

                int days = DateTimeExtensions.calculeMonth(month, year, holydays);

                int temp = 0;

                int days_off = 0;

                switch (employee.EscalaDeTrabalho)
                {
                    case "5x2":
                        for (int i = 1; i <= days; i++)
                        {
                            temp++;
                            if (temp == 5)
                            {
                                days_off += 2;
                                days -= 2;
                                temp = 0;
                            }
                        }
                        break;
                    case "6x1":
                        for (int i = 1; i <= days; i++)
                        {
                            temp++;
                            if (temp == 6)
                            {
                                days_off += 1;
                                days -= 1;
                                temp = 0;
                            }
                        }
                        break;
                    case "6x2":
                        for (int i = 1; i <= days; i++)
                        {
                            temp++;
                            if (temp == 6)
                            {
                                days_off += 2;
                                days -= 2;
                                temp = 0;
                            }
                        }
                        break;
                    case "12x36":
                        days_off = days / 2;
                        days -= days_off;
                        break;
                    default:
                        return BadRequest("Invalid employee work schedule!");
                }

                return Ok(employee.CustoDiario * days);
            }
            catch (Exception ex)
            {
                return BadRequest($"error: {ex.Message}");
            }
        }

        [HttpGet("{id}/yearlycost/{year}")]
        public IActionResult GetEmployeeYearlyCost(int id, int year)
        {
            try
            {
                var employee = _repo.GetEmployeeById(id);
                if (employee == null) return NotFound("Employee not found!");

                DateTime[] holydays = DateTimeExtensions.getHolydays(year.ToString());

                int days = DateTimeExtensions.calculeYear(year, holydays);

                int temp = 0;

                int days_off = 0;

                switch (employee.EscalaDeTrabalho)
                {
                    case "5x2":
                        for (int i = 1; i <= days; i++)
                        {
                            temp++;
                            if (temp == 5)
                            {
                                days_off += 2;
                                days -= 2;
                                temp = 0;
                            }
                        }
                        break;
                    case "6x1":
                        for (int i = 1; i <= days; i++)
                        {
                            temp++;
                            if (temp == 6)
                            {
                                days_off += 1;
                                days -= 1;
                                temp = 0;
                            }
                        }
                        break;
                    case "6x2":
                        for (int i = 1; i <= days; i++)
                        {
                            temp++;
                            if (temp == 6)
                            {
                                days_off += 2;
                                days -= 2;
                                temp = 0;
                            }
                        }
                        break;
                    case "12x36":
                        days_off = days / 2;
                        days -= days_off;
                        break;
                    default:
                        return BadRequest("Invalid employee work schedule!");
                }

                return Ok(employee.CustoDiario * days);
            }
            catch (Exception ex)
            {
                return BadRequest($"error: {ex.Message}");
            }
        }

        [HttpPost]
        public IActionResult Post(Employee model)
        {
            _repo.Add(model);

            if (_repo.SaveChanges())
            {
                return Ok(model);
            }

            return BadRequest("Employee not registered!");
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, EmployeeRegisterDto model)
        {
            try
            {
                var employee = _repo.GetEmployeeById(id);
                if (employee == null) NotFound("Employee not found!");

                _mapper.Map(model, employee);

                _repo.Update(employee);

                if (_repo.SaveChanges())
                {
                    return Created($"/api/v1/employee/{model.Id}", _mapper.Map<EmployeeDto>(employee));
                }

                return BadRequest("Employee not updated!");
            }
            catch (Exception ex)
            {
                return BadRequest($"error: {ex.Message}");
            }
        }

        // api/employee
        [HttpPatch("{id}")]
        public IActionResult Patch(int id, EmployeePatchDto model)
        {
            var employee = _repo.GetEmployeeById(id);
            if (employee == null) return BadRequest("Employee not found");

            _mapper.Map(model, employee);

            _repo.Update(employee);
            if (_repo.SaveChanges())
            {
                return Created($"/api/employee/{model.Id}", _mapper.Map<EmployeePatchDto>(employee));
            }

            return BadRequest("Employee not updated");
        }

        [HttpPatch("{id}/changeState")]
        public IActionResult changeState(int id, ChangeStateDto changeState)
        {
            var employee = _repo.GetEmployeeById(id);
            if (employee == null) return BadRequest("Employee not found!");

            employee.Ativo = changeState.State;

            _repo.Update(employee);
            if (_repo.SaveChanges())
            {
                var msn = employee.Ativo ? "ativado" : "desativado";
                return Ok(new { message = $"Employee {msn} com sucesso!" });
            }

            return BadRequest("Employee not updated!");
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = _repo.GetEmployeeById(id);
                if (employee == null) return NotFound("Employee not found!");

                _repo.Delete(employee);

                if (_repo.SaveChanges())
                {
                    return Ok("Employee deleted!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"error: {ex.Message}");
            }

            return BadRequest();
        }

    }
}