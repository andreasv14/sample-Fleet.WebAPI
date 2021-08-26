using Fleet.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fleet.Client.WebApp.Data
{
    public class ContainerDataService
    {
        //public async Task<IEnumerable<ContainerDetailDto>> GetAll()
        //{
        //    return await Task.FromResult(new List<ContainerDetailDto>
        //    {
        //        new ContainerDetailDto {Id = 1, Name = "Container A"},
        //        new ContainerDetailDto {Id = 2, Name = "Container B"},
        //        new ContainerDetailDto {Id = 3, Name = "Container C"}
        //    });
        //}

        public IEnumerable<ContainerDto> GetAll()
        {
            return new List<ContainerDto>
            {
                new ContainerDto {ContainerId = 1, Name = "Container A"},
                new ContainerDto {ContainerId = 2, Name = "Container B"},
                new ContainerDto {ContainerId = 3, Name = "Container C"}
            };
        }
    }
}
