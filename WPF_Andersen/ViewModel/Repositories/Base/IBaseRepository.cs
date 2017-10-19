using System.Collections;
using System.Collections.Generic;

namespace ViewModel.Repositories.Base
{
    public interface IBaseRepository<T> where T : class
    {
        /// <summary>
        /// Получить весь список объектов
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> GetList();
        /// <summary>
        /// Получить объект
        /// </summary>
        /// <param name="id">Индетификатор</param>
        /// <returns></returns>
        T GetItem(int id);
        void Create(T item); // создание объекта
        void Update(T item); // обновление объекта
        void Delete(int id); // удаление объекта по id
    }
}
