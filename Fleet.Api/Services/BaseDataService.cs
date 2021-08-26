using Fleet.Api.Exceptions;
using Fleet.Data;
using Fleet.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fleet.Api.Services
{
    public abstract class BaseDataService<TBusinessModel> : IDataService<TBusinessModel> where TBusinessModel : BaseModel
    {
        #region Private fields

        private readonly FleetDbContext _context;
        private readonly DbSet<TBusinessModel> _entitySource;

        #endregion

        #region Constructors

        protected BaseDataService(FleetDbContext context)
        {
            _context = context;
            _entitySource = context.Set<TBusinessModel>();
        }

        #endregion

        #region Public methods

        public void Add(TBusinessModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Model cannot be null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ValidationException("Model name property cannot be null or empty");
            }

            _entitySource.Add(model);

            _context.SaveChanges();
        }

        public virtual async Task AddAsync(TBusinessModel model)
        {
            if (model == null)
            {
                throw new NullReferenceException("Model cannot be null");
            }

            if (string.IsNullOrEmpty(model.Name))
            {
                throw new ValidationException("Model name property cannot be null or empty");
            }

            await _entitySource.AddAsync(model);

            await _context.SaveChangesAsync();
        }

        public void AddRange(IEnumerable<TBusinessModel> models)
        {
            if (models.Any(x => string.IsNullOrEmpty(x.Name)))
            {
                throw new ValidationException("Model name property cannot be null or empty");
            }

            _entitySource.AddRange(models);

            _context.SaveChanges();
        }

        public void Update(TBusinessModel updatedModel)
        {
            _entitySource.Update(updatedModel);

            _context.SaveChanges();
        }

        public async Task UpdateAsync(TBusinessModel updatedModel)
        {
            _entitySource.Update(updatedModel);

            await _context.SaveChangesAsync();
        }

        public void Remove(int id)
        {
            var selectedModel = _entitySource.FirstOrDefault(model => model.Id == id);
            if (selectedModel == null)
            {
                throw new ObjectNotFoundException($"Model with Id {id} is not found");
            }

            _entitySource.Remove(selectedModel);

            _context.SaveChanges();
        }

        public async Task RemoveAsync(int id)
        {
            var selectedModel = await GetByIdAsync(id);
            if (selectedModel == null)
            {
                throw new ObjectNotFoundException($"Model with Id {id} is not found");
            }

            _entitySource.Remove(selectedModel);

            await _context.SaveChangesAsync();
        }

        public void RemoveRange(IEnumerable<int> ids)
        {
            var modelsToBeRemoved = new List<TBusinessModel>();

            foreach (var id in ids)
            {
                var selectedModel = _entitySource.FirstOrDefault(model => model.Id == id);
                if (selectedModel == null)
                {
                    throw new ObjectNotFoundException($"Model with Id {id} is not found");
                }

                modelsToBeRemoved.Add(selectedModel);
            }

            _entitySource.RemoveRange(modelsToBeRemoved);

            _context.SaveChanges();
        }

        public TBusinessModel GetById(int id)
        {
            var selectedModel = _entitySource.FirstOrDefault(model => model.Id == id);
            if (selectedModel == null)
            {
                throw new ObjectNotFoundException($"Model with Id {id} is not found");
            }

            return selectedModel;
        }

        public virtual async Task<TBusinessModel> GetByIdAsync(int id)
        {
            var selectedModel = await _entitySource.FindAsync(id);
            if (selectedModel == null)
            {
                throw new ObjectNotFoundException($"Model with Id {id} is not found");
            }

            return selectedModel;
        }


        public IEnumerable<TBusinessModel> GetAll()
        {
            return _entitySource.ToList();
        }

        public virtual async Task<IEnumerable<TBusinessModel>> GetAllAsync()
        {
            return await _entitySource.ToListAsync();
        }

        public IEnumerable<TBusinessModel> GetByIds(IEnumerable<int> ids)
        {
            var selectedContainers = new List<TBusinessModel>();

            foreach (var id in ids)
            {
                var currentModel = GetById(id);

                selectedContainers.Add(currentModel);
            }

            return selectedContainers;
        }

        public async Task<IEnumerable<TBusinessModel>> GetByIdsAsync(IEnumerable<int> ids)
        {
            var selectedContainers = new List<TBusinessModel>();

            foreach (var id in ids)
            {
                var currentModel = await GetByIdAsync(id);

                selectedContainers.Add(currentModel);
            }

            return selectedContainers;
        }

        #endregion
    }
}
