using RMSystem.Models;
using System.Collections.Generic;

namespace RMSystem.Services
{
    // Реализация паттерна IRepository
    public interface IRiskRepository
    {
        // Декларируем функционал работы с репозиторием

        /// <summary>
        /// Получает счетный лист-список всех рисков из репозитория
        /// </summary>
        /// <returns>IEnumerable<Risk></returns>
        IEnumerable<Risk> GetAllRisks();

        /// <summary>
        /// Получает риск из репозитория
        /// </summary>
        /// <returns>Risk by id</returns>
        Risk GetRisk(int id);

        /// <summary>
        /// Обновление Risk.
        /// </summary>
        /// <param name="updatedRisk"></param>
        /// <returns> Risk </returns>
        Risk Update(Risk updatedRisk);

        /// <summary>
        /// Добавить Risk.
        /// </summary>
        /// <param name="updatedRisk"></param>
        /// <returns> Risk</returns>
        Risk Add(Risk newRisk);

        /// <summary>
        /// Удалить Risk.
        /// </summary>
        /// <param name="updatedRisk"></param>
        /// <returns> </returns>
        Risk Delete(int id);

        /// <summary>
        ///  Получает коллекцию рисков процесса.
        /// </summary>
        /// <returns>IEnumerable <ProcessCount></returns>
        IEnumerable<ProcessCount> RiskCountBYProcess(Process? process);

        /// <summary>
        /// Поиск риска.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        IEnumerable<Risk> Search(string search);
    }
}

