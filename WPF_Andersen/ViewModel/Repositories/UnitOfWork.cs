using System;
using ViewModel.Repositories.Base;

namespace ViewModel.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DatabaseContext _context;

        public UnitOfWork()
        {
            _context = new DatabaseContext();
            ClientRepository = new ClientRepository(_context);
            
        }
        public UnitOfWork(IClientRepository clientRepository)
        {
            ClientRepository = clientRepository;
            
        }
        public IClientRepository ClientRepository { get; set; }
    }
}
