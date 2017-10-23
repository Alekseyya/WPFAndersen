using System;
using Model.Entities;

namespace DAL.Repositories.Base
{
    public interface IClientRepository: IBaseRepository<Client>, IDisposable
    {
        bool HasClientOnDatabase(Client client);
    }
}
