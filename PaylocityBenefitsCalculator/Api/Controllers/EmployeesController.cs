﻿using Api.Dtos.Employee;
using Api.Interfaces;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public EmployeesController(IEmployeeRepository employeeRepository, IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }


    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        var employee = await _employeeRepository.GetById(id);

        if (employee == null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse<GetEmployeeDto>()
        {
            Data = _mapper.Map<GetEmployeeDto>(employee)
        });
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        //task: use a more realistic production approach
        var employees = await _employeeRepository.GetAll();

        return Ok(new ApiResponse<List<GetEmployeeDto>>()
        {
            Data = _mapper.Map<List<GetEmployeeDto>>(employees),
            Success = true
        });
    }
}
