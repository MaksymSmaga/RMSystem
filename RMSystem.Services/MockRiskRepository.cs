using RMSystem.Models;
using RMSystem.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RMSystem.Services
{
    // Создаем тестовый псевдорепозиторий.
    // Наследуем и реализуем интерфейс IRiskRepository.
    public class MockRiskRepository : IRiskRepository
    {
        // Внутерунний буфер-лист для работы с ним внутри класса MockRiskRepository.
        private readonly List<Risk> _riskList;

        public MockRiskRepository()
        {
            // Инициализириуем лист псевдозначениями. 
            _riskList = new List<Risk>()
            {
                new Risk()
                {
                    Id = 0, Iso =Iso.Environmental, PhotoPath = "risk.png",  Process = Process.HR, Deverity = Deverity.Medium,
                    Likelihood = Likelihood.Might, Treatment = Treatment.Archived, Category = Category.Default,
                    Description = "Risk management is the process of identifying, assessing and controlling " +
                    "threats to an organization's capital and earnings.", Cause = Cause.CrashDB,
                    Source = Source.Investigation, PhaseManagement = PhaseManagement.Realization, Equipment = Equipment.Pump
                },

                new Risk()
                {
                    Id = 1, Iso =Iso.HealthSafety, PhotoPath = "risk.png",  Process = Process.IT, Deverity = Deverity.Medium,
                    Likelihood = Likelihood.Might, Treatment = Treatment.Archived, Category = Category.Default,
                    Description = "Risk identification can result from passively stumbling across vulnerabilities or through " +
                    "implemented tools and control processes that raise red flags when there are potential identified risks.", Cause = Cause.CrashDB,
                    Source = Source.Investigation, PhaseManagement = PhaseManagement.Realization, Equipment = Equipment.Pump
                },

                new Risk()
                {
                    Id = 2, Iso =Iso.HealthSafety, PhotoPath = "risk.png",  Process = Process.Payroll, Deverity = Deverity.Critical,
                    Likelihood = Likelihood.Might, Treatment = Treatment.Archived, Category = Category.Default,
                    Description = "Thus, a risk management program should be intertwined with organizational strategy.",
                    Cause = Cause.CrashDB,
                    Source = Source.Investigation, PhaseManagement = PhaseManagement.Realization, Equipment = Equipment.Pump
                },

                new Risk()
                {
                    Id = 3, Iso =Iso.Quality, PhotoPath = "risk.png",  Process = Process.HR, Deverity = Deverity.Low,
                    Likelihood = Likelihood.Might, Treatment = Treatment.Archived, Category = Category.Default,
                    Description = "The formidable task is to then determine  which risks fit within the organizations" +
                    " risk appetite and which require additional controls and actions before they are acceptable.", Cause = Cause.CrashDB,
                    Source = Source.Investigation, PhaseManagement = PhaseManagement.Realization, Equipment = Equipment.Pump
                },

                new Risk()
                {
                    Id = 4, Iso =Iso.Environmental, PhotoPath = "risk.png",  Process = Process.IT, Deverity = Deverity.Critical,
                    Likelihood = Likelihood.Might, Treatment = Treatment.Archived, Category = Category.Default,
                    Description = "Every organization faces the risk of unexpected, harmful events that can cost " +
                    "it money or cause it to close.", Cause = Cause.CrashDB,
                    Source = Source.Investigation, PhaseManagement = PhaseManagement.Realization, Equipment = Equipment.Pump
                },

                new Risk()
                {
                    Id = 5, Iso =Iso.HealthSafety, PhotoPath = "risk.png",  Process = Process.Payroll, Deverity = Deverity.Low,
                    Likelihood = Likelihood.Might, Treatment = Treatment.Archived, Category = Category.Default,
                    Description = "Risk management has perhaps never been more important than it is now. The risks modern " +
                    "organizations face have grown more complex, fueled by the rapid pace of globalization.", Cause = Cause.CrashDB,
                    Source = Source.Investigation, PhaseManagement = PhaseManagement.Realization, Equipment = Equipment.Pump
                },
                new Risk()
                {
                    Id = 6, Iso =Iso.Environmental, PhotoPath = "risk.png",  Process = Process.HR, Deverity = Deverity.Medium,
                    Likelihood = Likelihood.Might, Treatment = Treatment.Archived, Category = Category.Default,
                    Description = "Businesses made rapid adjustments to the threats posed by the pandemic. But, going forward " +
                    "they are grappling with novel risks, including how or whether to bring employees back to the office and " +
                    "what should be done to make their supply chains less vulnerable to crises.", Cause = Cause.CrashDB,
                    Source = Source.Investigation, PhaseManagement = PhaseManagement.Realization, Equipment = Equipment.Pump
                },

                new Risk()
                {
                    Id = 7, Iso =Iso.HealthSafety, PhotoPath = "risk.png",  Process = Process.IT, Deverity = Deverity.Medium,
                    Likelihood = Likelihood.Might, Treatment = Treatment.Archived, Category = Category.Default,
                    Description = "Financial vs. nonfinancial industries. In discussions of risk management, many experts " +
                    "note that at companies that are heavily regulated and whose business is risk, managing risk is a formal " +
                    "function.", Cause = Cause.CrashDB,
                    Source = Source.Investigation, PhaseManagement = PhaseManagement.Realization, Equipment = Equipment.Pump
                },

                new Risk()
                {
                    Id = 8, Iso =Iso.HealthSafety, PhotoPath = "risk.png",  Process = Process.IT, Deverity = Deverity.Critical,
                    Likelihood = Likelihood.Might, Treatment = Treatment.Archived, Category = Category.Default,
                    Description = "Banks and insurance companies, for example, have long had large risk departments typically " +
                    "headed by a chief risk officer (CRO), a title still relatively uncommon outside of the financial industry.",
                    Cause = Cause.CrashDB,
                    Source = Source.Investigation, PhaseManagement = PhaseManagement.Realization, Equipment = Equipment.Pump
                }

            };
        }

        /// <summary>
        /// Добавление риска.
        /// </summary>
        /// <param name="newRisk"></param>
        /// <returns> Risk </returns>
        public Risk Add(Risk newRisk)
        {
            // Расчитываем следующий Id нового риска.
            newRisk.Id = _riskList.Max(r => r.Id) + 1;
            _riskList.Add(newRisk);
            return newRisk;
        }

        /// <summary>
        ///  Удалить Риск.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Risk</returns>
        public Risk Delete(int id)
        {
            Risk riskForDel = _riskList.FirstOrDefault(r => r.Id == id);
            if (riskForDel != null)
                _riskList.Remove(riskForDel);
            return riskForDel;
        }

        /// <summary>
        /// Реализация метода IRiskRepository в MockRiskRepository.
        /// Возвращает счетный лист-список всех рисков из репозитория.
        /// </summary>
        /// <returns>IEnumerable<Risk></returns>
        public IEnumerable<Risk> GetAllRisks()
        {
            return _riskList;
        }

        /// <summary>
        /// Реализация метода IRiskRepository в MockRiskRepository.
        /// Возвращает риск из репозитория.
        /// </summary>
        /// <returns>Risk</returns>
        public Risk GetRisk(int id)
        {                                   // вернуть r, где r.Id == id 
            return _riskList.FirstOrDefault(r => r.Id == id);
        }


        /// <summary>
        /// Группируем риски по департаментам.
        /// Создать новые екземпляры моделей и занести в лист.
        /// </summary>
        /// <returns>IEnumerable<ProcessCount></returns>
        public IEnumerable<ProcessCount> RiskCountBYProcess(Process? process)
        {
            // Получаем массив данных.
            IEnumerable<Risk> query = _riskList;

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
                return _riskList;
            return _riskList
                .Where(r => r.Process.ToString().Contains(search)
                         || r.Description.Contains(search) || r.Threat >= Convert.ToInt32(search));
        }

        public Risk Update(Risk updatedRisk)
        {
            // Получаем екземпляр Risk из листа по id.
            Risk risk = _riskList.FirstOrDefault(r => r.Id == updatedRisk.Id);

            // Переприсваивание занчений свойст не нулевому Risk.
            if (risk != null)
            {
                risk.Description = updatedRisk.Description;
                risk.Iso = updatedRisk.Iso;
                risk.Process = updatedRisk.Process;
                risk.Deverity = updatedRisk.Deverity;
                risk.Likelihood = updatedRisk.Likelihood;
                risk.Treatment = updatedRisk.Treatment;
                risk.Category = updatedRisk.Category;
                risk.Cause = updatedRisk.Cause;
                risk.Source = updatedRisk.Source;
                risk.PhaseManagement = updatedRisk.PhaseManagement;
                risk.Equipment = updatedRisk.Equipment;
            }

            // Возвращаем обновленный Risk.
            return risk;
        }
    }
}
