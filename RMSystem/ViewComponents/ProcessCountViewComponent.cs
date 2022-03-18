using Microsoft.AspNetCore.Mvc;
using RMSystem.Models;
using RMSystem.Services;


namespace RMSystem.ViewComponents
{
    public class ProcessCountViewComponent : ViewComponent
    {
        private readonly IRiskRepository _riskRepository;

        public ProcessCountViewComponent(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }

        /// <summary>
        ///  Подсчет рисков по процессу.
        /// </summary>
        /// <returns></returns>
        public IViewComponentResult Invoke(Process? process = null)
        {
            // Получаем количество рисков в процессе.
            var result = _riskRepository.RiskCountBYProcess(process);

            // Возвращаем MVC представление с result.
            return View(result);
        }
    }
}
