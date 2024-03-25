﻿namespace TheWhiskyRealm.Infrastructure.Data.Common;

/// <summary>
/// Provides methods for accessing and manipulating data in the database.
/// </summary>
public interface IRepository
{
    /// <summary>
    /// Retrieves all records of type T from the database.
    /// </summary>
    /// <returns>An IQueryable representing the queryable expression tree.</returns>
    IQueryable<T> All<T>() where T : class;

    /// <summary>
    /// Retrieves all records of type T from the database without tracking changes.
    /// </summary>
    /// <returns>An IQueryable representing the queryable expression tree without change tracking.</returns>
    IQueryable<T> AllReadOnly<T>() where T : class;


    /// <summary>
    /// Asynchronously adds the specified entity to the database.
    /// </summary>
    /// <param name="entity">The entity to add to the database.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    Task AddAsync<T>(T entity) where T : class;

    /// <summary>
    /// Asynchronously retrieves an entity of type T from the database based on the specified id.
    /// </summary>
    /// <param name="id">The id of the entity to retrieve.</param>
    /// <returns>Returns the retrieved entity or null if not founded.</returns>
    Task<T?> GetByIdAsync<T>(object id) where T : class;

    /// <summary>
    /// Deletes the specified entity from the database.
    /// </summary>
    /// <param name="entity">The entity to delete from the database.</param>
    void Delete<T>(T entity) where T : class;

    /// <summary>
    /// Asynchronously saves all changes made in the database context.
    /// </summary>
    /// <returns>Returns the number of affected rows.</returns>
    Task<int> SaveChangesAsync();
}