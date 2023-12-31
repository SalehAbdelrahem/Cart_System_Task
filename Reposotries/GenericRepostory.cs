﻿using Data;
using Microsoft.EntityFrameworkCore;
using Models;

namespace Reposotries
{
    public class GenericRepostory<T> : IGenericRepostory<T>  where T : BaseModel
    {
        Project_Context Context;
        DbSet<T> Table;
        public GenericRepostory(Project_Context context)
        {
            Context = context;
            Table = Context.Set<T>();
        }

        public IEnumerable<T> Get()
        {
            return Table;
        }

        public T GetByID(int id)
        {
            return Table.Find(id);
        }

        public int Add(T entity)
        {
            Table.Add(entity);
            return entity.Id;
        }

        public void Update(T entity)
        {
            Table.Update(entity);
        }

        public void Remove(T entity)
        {
            Table.Remove(entity);
        }


    }
}
