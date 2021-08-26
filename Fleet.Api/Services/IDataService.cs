using Fleet.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fleet.Api.Services
{
    /// <summary>
    /// An interface for basic CRUD operations.
    /// </summary>
    /// <typeparam name="TBusinessModel">Business model.</typeparam>
    public interface IDataService<TBusinessModel> where TBusinessModel : BaseModel
    {
        /// <summary>
        /// Add new business model into the collection.
        /// </summary>
        /// <param name="model">Business model.</param>
        /// <exception cref="System.NullReferenceException">Model is null.</exception>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">Model data are not valid.</exception>
        void Add(TBusinessModel model);

        /// <summary>
        /// Add new business model into the collection asynchronous.
        /// </summary>
        /// <param name="model">Business model.</param>
        /// <exception cref="System.NullReferenceException">Model is null.</exception>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">Model data are not valid.</exception>
        Task AddAsync(TBusinessModel model);

        /// <summary>
        /// Add new list of models into the collection.
        /// </summary>
        /// <param name="models">List of business models.</param>
        /// <exception cref="System.ComponentModel.DataAnnotations.ValidationException">Models data are not valid.</exception>
        void AddRange(IEnumerable<TBusinessModel> models);

        /// <summary>
        /// Update selected model with a new name.
        /// </summary>
        /// <param name="model">Update business model.</param>
        /// <exception cref="Exceptions.ObjectNotFoundException">Model is not found.</exception>
        void Update(TBusinessModel model);

        /// <summary>
        /// Update selected model with a new name asynchronous.
        /// </summary>
        /// <param name="model">Update business model.</param>
        /// <exception cref="Exceptions.ObjectNotFoundException">Model is not found.</exception>
        Task UpdateAsync(TBusinessModel updatedModel);

        /// <summary>
        /// Remove business model from the collection using Id.
        /// </summary>
        /// <param name="id">Business model Id.</param>
        /// <exception cref="Exceptions.ObjectNotFoundException">Buseinss model is not found.</exception>
        void Remove(int id);

        /// <summary>
        /// Remove business model from the collection using Id asynchronous.
        /// </summary>
        /// <param name="id">Business model Id.</param>
        /// <exception cref="Exceptions.ObjectNotFoundException">Buseinss model is not found.</exception>
        Task RemoveAsync(int id);

        /// <summary>
        /// Remove list of business models from the collection using their Ids.
        /// </summary>
        /// <param name="ids">List of business models Ids.</param>
        /// <exception cref="Exceptions.ObjectNotFoundException">Business model is not found.</exception>
        void RemoveRange(IEnumerable<int> ids);

        /// <summary>
        /// Get specific business model using their Id.
        /// </summary>
        /// <param name="id">Business model Id.</param>
        /// <exception cref="Exceptions.ObjectNotFoundException">Business model is not found.</exception>
        /// <returns>Business model.</returns>
        TBusinessModel GetById(int id);

        /// <summary>
        /// Get specific business model using their Id.
        /// </summary>
        /// <param name="id">Business model Id.</param>
        /// <exception cref="Exceptions.ObjectNotFoundException">Business model is not found.</exception>
        /// <returns>Business model.</returns>
        Task<TBusinessModel> GetByIdAsync(int id);

        /// <summary>
        /// Get a list of business models using a list of ids.
        /// </summary>
        /// <param name="ids">List of model Ids.</param>
        /// <returns></returns>
        IEnumerable<TBusinessModel> GetByIds(IEnumerable<int> ids);

        /// <summary>
        /// Get a list of business models using a list of ids async.
        /// </summary>
        /// <param name="ids">List of model Ids.</param>
        /// <returns></returns>
        Task<IEnumerable<TBusinessModel>> GetByIdsAsync(IEnumerable<int> ids);

        /// <summary>
        /// Get all business models from collection.
        /// </summary>
        /// <returns>List of all business models.</returns>
        IEnumerable<TBusinessModel> GetAll();

        /// <summary>
        /// Get all business models from collection async.
        /// </summary>
        /// <returns>List of all business models.</returns>
        Task<IEnumerable<TBusinessModel>> GetAllAsync();
    }
}
