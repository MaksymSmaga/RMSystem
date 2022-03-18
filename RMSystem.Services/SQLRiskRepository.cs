using Microsoft.EntityFrameworkCore;
using RMSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMSystem.Services
{
    public class SQLRiskRepository : IRiskRepository
    {
        private readonly RMSystemContext _context;

        // Внедрение зависимости работы с базой данных
        public SQLRiskRepository(RMSystemContext context)
        {
            _context = context;
        }
        public Risk Add(Risk newRisk)
        {
            // Добавление нового риска в БД.
            //_context.Risks.Add(newRisk);
            //_context.SaveChanges();
            _context.Database.ExecuteSqlRaw("spAddNewRisk {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}",
                                                           newRisk.Iso,
                                                           newRisk.PhotoPath,
                                                           newRisk.Process,
                                                           newRisk.Deverity,
                                                           newRisk.Likelihood,
                                                           newRisk.Treatment,
                                                           newRisk.Category,
                                                           newRisk.Description,
                                                           newRisk.Cause,
                                                           newRisk.Source,
                                                           newRisk.PhaseManagement,
                                                           newRisk.Equipment
                                                           );
            return newRisk;
        }

        public Risk Delete(int id)
        {
            // Удаление о риска в БД.
            var delRisk = _context.Risks.Find(id);
            if (delRisk != null)
                _context.Risks.Remove(delRisk);
            _context.SaveChanges();
            return delRisk;
        }

        /// <summary>
        /// Реализация метода IRiskRepository в MockRiskRepository.
        /// Возвращает счетный лист-список всех рисков из репозитория.
        /// </summary>
        /// <returns>IEnumerable<Risk></returns>
        public IEnumerable<Risk> GetAllRisks()
        {
            // return _context.Risks;
            // Прямой метод запроса в SQL

            return _context.Risks
                .FromSqlRaw<Risk>("SELECT * FROM Risks")
                .ToList();
        }

        /// <summary>
        /// Возвращает риск из репозитория.
        /// </summary>
        /// <returns>Risk</returns>
        public Risk GetRisk(int id)
        {
            // return _context.Risks.Find(id);
            // НЕ ИСПОЛЬЗОВАТЬ ИНТЕРПОЛЯЦИЮ СТРОК ЗДЕСЬ!!!


            return _context.Risks
                .FromSqlRaw<Risk>("spGetRiskById {0}", id)
                .ToList()
                .FirstOrDefault();
        }

        /// <summary>
        /// Группируем риски по департаментам.
        /// Создать новые екземпляры моделей и занести в лист.
        /// </summary>
        /// <returns>IEnumerable<ProcessCount></returns>
        public IEnumerable<ProcessCount> RiskCountBYProcess(Process? process)
        {
            // Получаем массив данных.
            IEnumerable<Risk> query = _context.Risks;

            // Делаем выборку рисков.
            if (process.HasValue)
                query = query.Where(r => r.Process == process.Value);

            // Группируем риски по департаментам.
            return query.GroupBy(r => r.Process)

                // Создать новые екземпляры моделей и занести в лист.
                .Select(p => new ProcessCount()
                {
                    Process = p.Key.Value,
                    // Счет колличества рисков в процессе.
                    Count = p.Count()
                }).ToList();
        }

        /// <summary>
        /// Поиск рисков.
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IEnumerable<Risk> Search(string search)
        {
            // Если пустой запрос вернуть все риски.
            if (string.IsNullOrEmpty(search))
                return _context.Risks;
            return _context.Risks
                .Where(r => r.Process.ToString().Contains(search)
                         || r.Description.Contains(search)
                         || r.Threat >= Convert.ToInt32(search));
        }


        public Risk Update(Risk updatedRisk)
        {
            var risk = _context.Risks.Attach(updatedRisk);
            risk.State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            _context.SaveChanges();
            // Возвращаем обновленный Risk.
            return updatedRisk;
        }
    }
}
