namespace ViewModel.Repositories.Base
{
     public interface IUnitOfWork
    {
        IClientRepository ClientRepository { get; set; }
    }
}
