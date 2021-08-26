using AutoMapper;
using Fleet.Api.Converters;
using Fleet.Api.Exceptions;
using Fleet.Api.Factories;
using Fleet.Api.Services;
using Fleet.Dtos;
using Fleet.Models;
using Fleet.Models.Enums;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Fleet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransportationsController : ControllerBase
    {
        #region Private fields

        private readonly ITransportationService _transportationService;
        private readonly IContainerService _containerService;
        private readonly ITransportationModelFactory _transportationModelfactory;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public TransportationsController(
            ITransportationService transportationService,
            IContainerService containerService,
            ITransportationModelFactory transportationModelfactory,
            IMapper mapper)
        {
            _transportationService = transportationService;
            _containerService = containerService;
            _transportationModelfactory = transportationModelfactory;
            _mapper = mapper;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// GET: api/transportations
        /// 
        /// Gets all transportations.
        /// </summary>        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransportationDto>>> GetAllAsync()
        {
            try
            {
                var transportations = await _transportationService.GetAllAsync();

                var dtoTransportations = _mapper.Map<TransportationDto[]>(transportations);

                return Ok(dtoTransportations);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }


        /// <summary>
        /// GET: api/transportations/type/{transportationType}
        /// 
        /// Get transportations based on type.
        /// </summary>  
        /// <param name="transportationType">Transportation type.</param>
        /// <returns>Return OK status with list of tranportations.</returns>
        [HttpGet("type/{transportationType}")]
        public async Task<ActionResult<IEnumerable<TransportationDto>>> GetByType(TransportationTypeDto transportationType)
        {
            try
            {
                var type = _mapper.Map<TransportationType>(transportationType);

                var transportations = await _transportationService.GetByTypeAsync(type);

                var dtoTransportations = _mapper.Map<TransportationDto[]>(transportations);

                return Ok(dtoTransportations);
            }
            catch (FormatException formatException)
            {
                return BadRequest(formatException.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// GET api/transportation/{transportationId}
        /// 
        /// Get transportation with Id.
        /// </summary>
        /// <param name="id">Transportation Id.</param>
        [HttpGet("{transportationId:int}")]
        public async Task<ActionResult<TransportationDto>> GetByIdAsync(int transportationId)
        {
            try
            {
                var transportation = await _transportationService.GetByIdAsync(transportationId);

                var dtoTransportation = _mapper.Map<TransportationDto>(transportation);

                return Ok(dtoTransportation);
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// POST api/transportation?type={type}&name={name}&capacity={capacity}
        /// 
        /// Add new transportation.
        /// </summary>
        /// <param name="type">Transportation type.</param>
        /// <param name="name">Name.</param>
        /// <param name="capacity">Capacity.</param>
        [HttpPost("add")]
        public async Task<ActionResult> AddAsync([FromQuery] TransportationTypeDto type, string name, uint capacity)
        {
            try
            {
                var transportationType = _mapper.Map<TransportationType>(type);

                var newTransportation = _transportationModelfactory.Create(transportationType, name, capacity);

                await _transportationService.AddAsync(newTransportation);

                return Ok("Successfully added");
            }
            catch (ValidationException ex)
            {
                //todo: Return validation error action result.
                return BadRequest(ex.Message);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// PUT api/transportation
        /// 
        /// Update transportation.
        /// </summary>
        /// <param name="updatedTransportation">Transportation message.</param>
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync([FromBody] Transportation updatedTransportation)
        {
            try
            {
                await _transportationService.UpdateAsync(updatedTransportation);

                return Ok("Successfully updated");
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// DELETE api/transportation/1
        /// 
        /// Remove selected transportation.
        /// </summary>
        /// <param name="id">Transportation Id</param>
        [HttpDelete("remove/{id:int}")]
        public async Task<ActionResult> RemoveByIdAsync(int id)
        {
            try
            {
                await _transportationService.RemoveAsync(id);

                return Ok("Successfully removed");
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// PUT api/transportation/load
        /// 
        /// Add containers to selected transportation.
        /// </summary>
        /// <param name="transportationLoadContainers">Current transportation and containers to be loaded</param>
        /// <returns></returns>
        [HttpPut("load")]
        public async Task<ActionResult> LoadContainersAsync([FromBody] LoadContainersDto transportationLoadContainers)
        {
            try
            {
                var currentTransportation = await _transportationService.GetByIdAsync(transportationLoadContainers.TransportationId);
                if (currentTransportation == null)
                {
                    return BadRequest($"Transportation with {transportationLoadContainers.TransportationId} cannot be found");
                }

                var containers = await _containerService.GetByIdsAsync(transportationLoadContainers.ContainerIds);

                await _transportationService.LoadAsync(transportationLoadContainers.TransportationId, containers);

                return Ok("Successfully loaded");
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        /// <summary>
        /// GET api/transportations/containers/1
        /// 
        ///  load containers of a current transportation.
        /// </summary>
        /// <param name="transportationId">Transportation Id.</param>
        /// <returns></returns>
        [HttpGet("containers/{transportationId:int}")]
        public async Task<ActionResult<IEnumerable<ContainerDto>>> GetTransportationContainersAsync(int transportationId)
        {
            try
            {
                var loadContainers = await _containerService.GetTransportLoadContainersAsync(transportationId);

                var dtoContainers = _mapper.Map<ContainerDto[]>(loadContainers);

                return Ok(dtoContainers);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion
    }
}
