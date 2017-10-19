using System;
using System.Collections.Generic;
using System.Linq;
using DAL.Repositories.Base;
using Model.Entities;
using System.Data.Entity;

namespace DAL.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly DatabaseContext _context;
        public ClientRepository(DatabaseContext context)
        {
            _context = context;
        }
        public void Create(Client item)
        {
            _context.Clients.Add(item);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            //Не уверен правильно ли писать через firsOrDefault
            var client = _context.Clients.FirstOrDefault(o => o.Id == id);
            _context.Clients.Remove(client);
            _context.SaveChanges();
        }

        public Client GetItem(int id)
        {
            return _context.Clients.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Client> GetList()
        {
            return _context.Clients.ToList();
        }

        /// <summary>
        /// Обжновление бд
        /// </summary>
        /// <param name="item"></param>
        public void Update(Client item)
        {
            var client = _context.Clients.FirstOrDefault(o => o.Id == item.Id);
            bool isModified = false;

            if (client.Id != item.Id)
            {
                client.Id = item.Id;
                isModified = true;
            }

            if (client.FirstName != item.FirstName)
            {
                client.FirstName = item.FirstName;
                isModified = true;
            }

            if (client.LastName != item.LastName)
            {
                client.LastName = item.LastName;
                isModified = true;
            }

            if (client.Age != item.Age)
            {
                client.Age = item.Age;
                isModified = true;
            }

            if (isModified)
            {
                _context.Entry(client).State = EntityState.Modified;
                _context.SaveChanges();
            }
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
