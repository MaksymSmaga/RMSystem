using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMSystem.Models;
using RMSystem.Services;

namespace RMSystem.Pages.Risks
{
    public class DeleteModel : PageModel
    {
        // Считываем репозиторий для внутренней работы класса.
        private readonly IRiskRepository _riskRepository;

        public DeleteModel(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }

        // Свойство доступное по проекту.
        [BindProperty]
        public Risk Risk { get; set; }

        /// <summary>
        /// Получить риск по id на страницу удаления.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult OnGet(int id)
        {
            Risk = _riskRepository.GetRisk(id);

            if (Risk == null)
                return RedirectToPage("/NotFound");

            return Page();
        }

         /// <summary>
         /// Удаление Риска согласно id переданному в модель.
         /// </summary>
         /// <returns></returns>
        public IActionResult OnPost()
        {
            Risk delRisk = _riskRepository.Delete(Risk.Id);

            if (delRisk == null)
                return RedirectToPage("/NotFound");

            return RedirectToPage("Risks");
        }
    }
}
