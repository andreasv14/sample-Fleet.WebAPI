using Fleet.Api.Services;
using Fleet.Api.Test.Helpers;
using Fleet.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Fleet.Api.Test.Services
{
    public class TransportationServiceTests
    {
        #region Private fields

        private readonly TransportationService _transportationService;
        private static List<Container> _existingContainers;

        #endregion

        #region Constructors

        public TransportationServiceTests()
        {
            var context = DataSourceHelper.GetSetupInMemoryDbContext();
            SeedData(context);

            _transportationService = new TransportationService(context);
        }

        #endregion

        #region Tests

        [Fact]
        public void LoadAsync_ContainersListIsEmpty_ThrowValidationException()
        {
            //Arrange
            var newContainersToBeLoad = new List<Models.Container>();

            //Act and Assert
            Assert.ThrowsAsync<ValidationException>(() => _transportationService.LoadAsync(1, newContainersToBeLoad));
        }

        [Fact]
        public void LoadAsync_ContainersListIsNotEmpty_AddContainersIntoTheCollection()
        {
            //Arrange
            var transportationId = 1;

            var newContainersToBeLoad = new List<Models.Container>()
            {
                 new Models.Container {Id = 1, Name = "Container 1"},
                 new Models.Container {Id = 2, Name = "Container 2"}
            };

            //Act
            _ = _transportationService.LoadAsync(transportationId, newContainersToBeLoad);

            //Assert
            var transportation = _transportationService.GetById(transportationId);

            Assert.Equal(newContainersToBeLoad, transportation.LoadContainers);
        }

        [Fact]
        public void LoadAsync_TotalContainersExceedCapacity_ThrowsValidationException()
        {
            //Arrange
            var transportationId = 1;

            var newContainersToBeLoad = new List<Models.Container>()
            {
                 new Models.Container {Id = 1, Name = "Container 1"},
                 new Models.Container {Id = 2, Name = "Container 2"},
                 new Models.Container {Id = 3, Name = "Container 3"},
                 new Models.Container {Id = 4, Name = "Container 4"},
                 new Models.Container {Id = 5, Name = "Container 5"}
            };

            //Act and Assert
            Assert.ThrowsAsync<ValidationException>(() => _transportationService.LoadAsync(transportationId, newContainersToBeLoad));
        }

        [Fact]
        public void LoadAsync_TotalContainersDoesNotExceedCapacity_ThrowsValidationException()
        {
            //Arrange
            var transportationId = 1;

            var newContainersToBeLoad = new List<Models.Container>()
            {
                 new Models.Container {Id = 1, Name = "Container 1"},
                 new Models.Container {Id = 2, Name = "Container 2"},
                 new Models.Container {Id = 3, Name = "Container 3"},
                 new Models.Container {Id = 4, Name = "Container 4"},
            };

            //Act
            _ = _transportationService.LoadAsync(transportationId, newContainersToBeLoad);

            //Assert
            var transportation = _transportationService.GetById(transportationId);

            Assert.Equal(newContainersToBeLoad, transportation.LoadContainers);
        }

        [Fact]
        public void LoadAsync_TransportationHasAlreadyContainersAndAddNewContainersAndExceedCapacity_ThrowsValidationException()
        {
            //Arrange
            var transportationId = 1;

            var transportation = _transportationService.GetById(transportationId);

            transportation.LoadContainers.Add(_existingContainers[0]);
            transportation.LoadContainers.Add(_existingContainers[1]);
            _transportationService.Update(transportation);

            var newContainersToBeLoad = new List<Container>()
            {
                _existingContainers[2],
                _existingContainers[3],
                _existingContainers[4],
                _existingContainers[5],
            };

            //Act
            Assert.ThrowsAsync<ValidationException>(() => _transportationService.LoadAsync(transportationId, newContainersToBeLoad));
        }

        [Fact]
        public void LoadAsync_TransportationHasAlreadyContainersAndAddNewContainersAndDoesNotExceedCapacity_AddContainersIntoTransportationLoadContainers()
        {
            //Arrange
            var transportationId = 1;

            var transportation = _transportationService.GetById(transportationId);

            transportation.LoadContainers.Add(_existingContainers[0]);
            transportation.LoadContainers.Add(_existingContainers[1]);
            _transportationService.Update(transportation);

            var newContainersToBeLoad = new List<Container>()
            {
                _existingContainers[2],
                _existingContainers[3],
            };

            //Act
            _ = _transportationService.LoadAsync(transportationId, newContainersToBeLoad);


            Assert.True(transportation.LoadContainers.Count == 4);
        }

        #endregion

        #region Private methods

        private static void SeedData(MockFleetDbContext context)
        {
            var transportations = new List<Transportation>
            {
                new Transportation { Id =1, Name = "Ship A" , Capacity = 4 , Type= Models.Enums.TransportationType.Ship},
                new Transportation { Id =2, Name = "Ship B" , Capacity = 4 , Type= Models.Enums.TransportationType.Ship},
                new Transportation { Id =3, Name = "Ship C" , Capacity = 4 , Type= Models.Enums.TransportationType.Ship},
                new Transportation { Id =4, Name = "Truck A" , Capacity = 2 , Type= Models.Enums.TransportationType.Truck},
                new Transportation { Id =5, Name = "Truck B" , Capacity = 2 , Type= Models.Enums.TransportationType.Truck}
            };

            context.Transportations.AddRange(transportations);

            _existingContainers = new List<Container>
            {
                new Container {Id = 1, Name = "Container 1"},
                new Container {Id = 2, Name = "Container 2"},
                new Container {Id = 3, Name = "Container 3"},
                new Container {Id = 4, Name = "Container 4"},
                new Container {Id = 5, Name = "Container 5"},
                new Container {Id = 6, Name = "Container 6"},
                new Container {Id = 7, Name = "Container 7"},
                new Container {Id = 8, Name = "Container 8"}
            };

            context.Containers.AddRange(_existingContainers);

            context.SaveChanges();
        }

        #endregion
    }
}
