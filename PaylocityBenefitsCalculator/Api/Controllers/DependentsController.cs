using Api.Dtos.Dependent;
using Api.Interfaces;
using Api.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentRepository _dependentRepository;
    private readonly IMapper _mapper;

    public DependentsController(IDependentRepository dependentRepository, IMapper mapper)
    {
        _dependentRepository = dependentRepository;
        _mapper = mapper;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        var dependent = await _dependentRepository.GetById(id);

        if (dependent == null)
        {
            return NotFound();
        }

        return Ok(new ApiResponse<GetDependentDto>()
        {
            Data = _mapper.Map<GetDependentDto>(dependent)
        });
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        var dependents = await _dependentRepository.GetAll();


        return Ok(new ApiResponse<List<GetDependentDto>>()
        {
            Data = _mapper.Map<List<GetDependentDto>>(dependents)
        });
    }
}
