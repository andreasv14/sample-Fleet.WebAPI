using AutoMapper;
using Fleet.Api.Exceptions;
using Fleet.Api.Factories;
using Fleet.Api.Services;
using Fleet.Domain;
using Fleet.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Fleet.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainersController : ControllerBase
    {
        #region Private fields

        private readonly IContainerService _containerService;
        private readonly IContainerModelFactory _containerModelFactory;
        private readonly IMapper _mapper;

        #endregion

        #region Constructors

        public ContainersController(
            IContainerService containerService,
            IContainerModelFactory containerModelFactory,
            IMapper mapper)
        {
            _containerService = containerService;
            _containerModelFactory = containerModelFactory;
            _mapper = mapper;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// GET: api/container
        /// 
        /// Gets all containers. 
        /// </summary>
        /// <returns>Returns OK and list of all containers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContainerDto>>> GetAllAsync()
        {
            try
            {
                var containers = await _containerService.GetAllAsync();

                var dtoContainers = _mapper.Map<ContainerDto[]>(containers);

                return Ok(dtoContainers);
            }
            catch (System.Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        /// <summary>
        /// GET api/container/{containerId}
        /// 
        /// Get specific container using containerId.
        /// </summary>
        /// <param name="containerId">Container Id.</param>
        [HttpGet("{containerId:int}")]
        public async Task<ActionResult<ContainerDto>> GetByIdAsync(int containerId)
        {
            try
            {
                var container = await _containerService.GetByIdAsync(containerId);

                var dtoContainer = _mapper.Map<ContainerDto>(container);

                return Ok(dtoContainer);
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
        /// POST api/container?containerName={containerName}
        /// 
        /// Add new container.
        /// </summary>
        /// <param name="containerName">Container name.</param>
        [HttpPost("add")]
        public async Task<ActionResult> AddAsync(string containerName)
        {
            try
            {
                var newContainer = _containerModelFactory.Create(containerName);

                await _containerService.AddAsync(newContainer);

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
        /// PUT api/container/
        /// 
        /// Updates specific container.
        /// </summary>
        /// <param name="updatedContainer">Container DTO model.</param>
        [HttpPut("update")]
        public async Task<ActionResult> UpdateAsync([FromBody] ContainerDto updatedContainer)
        {
            try
            {
                var container = _mapper.Map<Container>(updatedContainer);

                await _containerService.UpdateAsync(container);

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

        /// DELETE api/container/{containerId}
        /// 
        /// Remove selected container.
        /// </summary>
        /// <param name="id">Container Id</param>
        [HttpDelete("remove/{containerId:int}")]
        public async Task<ActionResult> RemoveByIdAsync(int containerId)
        {
            try
            {
                await _containerService.RemoveAsync(containerId);

                return Ok("Successfully removed");
            }
            catch (ObjectNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (System.Exception e)
            {
                return BadRequest(e);
            }
        }

        #endregion
    }
}
