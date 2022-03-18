
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMSystem.Models;
using RMSystem.Services;

namespace RMSystem.Pages.Risks
{
    public class DetailsModel : PageModel
    {
        // Внутренний лист для работы с ним внутри класса модели DetailsModel.
        private readonly IRiskRepository _riskRepository;

        // Считываем в модель репозиторий.
        public DetailsModel(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }
        // Декларируем поле Risk для возврата значения
        public Risk Risk { get; private set; }

        /// <summary>
        /// Получает Risk и возвращает страницу с ней
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>страницу IActionResult (преадресация, json запросы)</returns>
        public IActionResult OnGet(int id)
        {
            Risk = _riskRepository.GetRisk(id);

            // Перенаправляем на страницу NotFound, если Risk == null
            if (Risk == null)

                return RedirectToPage("/NotFound");

            return Page();
        }
    }
}
