using Fleet.Api.Exceptions;
using Fleet.Api.Services;
using Fleet.Api.Test.Helpers;
using Fleet.Data;
using Fleet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xunit;

namespace Fleet.Api.Test.Services
{
    /// <summary>
    /// Classes to help to create a new data service and a new dbcontext to include this new data service into the DbSet.
    /// </summary>
    #region Helpers

    interface IDataServiceTest : IDataService<ModelTest>
    {
    }

    class DataServiceTest : BaseDataService<ModelTest>, IDataServiceTest
    {
        public DataServiceTest(FleetDbContext context) : base(context)
        {
        }
    }

    #endregion

    public class BaseDataServiceTests
    {
        #region Private fields

        private readonly DataServiceTest _dataService;

        #endregion

        #region Constructors

        public BaseDataServiceTests()
        {
            _dataService = new DataServiceTest(DataSourceHelper.GetSetupInMemoryDbContext());
        }

        #endregion

        #region Tests

        [Fact]
        public void Add_AddNewModelWithValidData_AddModelIntoTheCollection()
        {
            //Arrange 
            var newModel = new ModelTest() { Id = 1, Name = "Model A" };

            // Act
            _dataService.Add(newModel);

            // Assert
            Assert.True(_dataService.GetAll().Any());
        }

        [Fact]
        public void Add_AddNewModelAndObjectIsNull_ThrowsNullReferenceException()
        {
            //Arrange 
            ModelTest newModel = null;


            // Act and Assert 
            Assert.Throws<NullReferenceException>(() => _dataService.Add(newModel));
        }

        [Fact]
        public void Add_AddNewModelAndNameIsEmpty_ThrowsValidationException()
        {
            //Arrange 
            var newModel = new ModelTest() { Id = 1 };

            // Act and Assert 
            Assert.Throws<ValidationException>(() => _dataService.Add(newModel));
        }

        [Fact]
        public void AddRange_AddNewModelsWithValidModels_AddNewModelsIntoTheCollection()
        {
            //Arrange 
            var modelsToBeAdded = new List<ModelTest>()
            {
                new ModelTest() { Id = 1, Name = "Model A" },
                new ModelTest() { Id = 2, Name = "Model B" }
            };

            // Act
            _dataService.AddRange(modelsToBeAdded);

            // Assert
            Assert.True(_dataService.GetAll().Any());
        }

        [Fact]
        public void Update_AddNewModelWithValidData_AddModelIntoTheCollection()
        {
            //Arrange 
            var modelId = 1;
            var modelName = "Model A";

            var newModel = new ModelTest() { Id = modelId, Name = modelName };

            // Act
            _dataService.Add(newModel);

            newModel.Name = "Model B";
            _dataService.Update(newModel);

            // Assert
            var selectedModel = _dataService.GetById(modelId);

            Assert.True(selectedModel.Name == newModel.Name);
        }

        [Fact]
        public void AddRange_AddNewModelsFirstModelsNameIsNull_AddNewModelsIntoTheCollection()
        {
            //Arrange 
            var modelsToBeAdded = new List<ModelTest>
            {
                new ModelTest() { Id = 1 },
                new ModelTest() { Id = 2, Name = "Model B" }
            };

            // Act and Assert 
            Assert.Throws<ValidationException>(() => _dataService.AddRange(modelsToBeAdded));
        }

        [Fact]
        public void Remove_RemoveModelThatExist_RemoveModelFromCollection()
        {
            //Arrange 

            var modelId = 1;

            var newModel = new ModelTest() { Id = modelId, Name = "Model A" };

            //Act
            _dataService.Add(newModel);

            //Act and Assert 
            _dataService.Remove(modelId);

            Assert.False(_dataService.GetAll().Any());
        }

        [Fact]
        public void Remove_RemoveModelThatDoesNotExist_ThrowsObjectNotFoundException()
        {
            //Arrange 
            var modelId = 2;
            var newModel = new ModelTest() { Id = 1, Name = "Model A" };

            //Act
            _dataService.Add(newModel);

            //Act and Assert 
            Assert.Throws<ObjectNotFoundException>(() => _dataService.Remove(modelId));
        }

        [Fact]
        public void RemoveRange_RemoveModelsThatExist_RemoveModelsFromCollection()
        {
            //Arrange 
            var modelsToBeAdded = new List<ModelTest>()
            {
                new ModelTest() { Id = 1, Name = "Model A" },
                new ModelTest() { Id = 2, Name = "Model B" }
            };

            //Act
            _dataService.AddRange(modelsToBeAdded);

            _dataService.RemoveRange(new int[] { 1, 2 });

            //Assert 
            Assert.False(_dataService.GetAll().Any());
        }

        [Fact]
        public void RemoveRange_RemoveModelsAndLastModelIdDoesNotExist_ThrowsObjectNotFoundException()
        {
            //Arrange 
            var modelsToBeAdded = new List<ModelTest>()
            {
                new ModelTest() { Id = 1, Name = "Model A" },
                new ModelTest() { Id = 2, Name = "Model B" }
            };

            //Act
            _dataService.AddRange(modelsToBeAdded);

            //Act and Assert 
            Assert.Throws<ObjectNotFoundException>(() => _dataService.RemoveRange(new int[] { 1, 2, 3 }));
        }

        [Fact]
        public void GetById_GetModelUsingId_ReturnModel()
        {
            //Arrange 
            var modelsToBeAdded = new List<ModelTest>()
            {
                new ModelTest() { Id = 1, Name = "Model A" },
                new ModelTest() { Id = 2, Name = "Model B" }
            };

            // Act
            _dataService.AddRange(modelsToBeAdded);

            var model = _dataService.GetById(1);

            // Assert
            Assert.True(model != null);
        }

        [Fact]
        public void GetById_GetModelUsingNotExistingId_ThrowsObjectNotFoundException()
        {
            //Arrange 
            var modelsToBeAdded = new List<ModelTest>()
            {
                new ModelTest() { Id = 1, Name = "Model A" },
                new ModelTest() { Id = 2, Name = "Model B" }
            };

            // Act
            _dataService.AddRange(modelsToBeAdded);

            // Assert
            Assert.Throws<ObjectNotFoundException>(() => _dataService.GetById(3));
        }

        [Fact]
        public void GetAll_GetAllModels_ReturnsListOfModels()
        {
            //Arrange 
            var modelsToBeAdded = new List<ModelTest>()
            {
                new ModelTest() { Id = 1, Name = "Model A" },
                new ModelTest() { Id = 2, Name = "Model B" }
            };

            // Act
            _dataService.AddRange(modelsToBeAdded);

            var models = _dataService.GetAll();

            // Assert
            Assert.True(models.Any());
            Assert.True(models.Count() == modelsToBeAdded.Count());
        }

        #endregion
    }
}
