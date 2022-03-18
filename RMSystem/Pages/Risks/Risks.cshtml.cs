using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMSystem.Models;
using RMSystem.Services;
using System.Collections.Generic;

namespace RMSystem.Pages.Risks
{
    public class RisksModel : PageModel
    {   // Внутерунний db для работы с ним внутри класса модели RisksModel.
        public readonly IRiskRepository _db;

        // Создаем свойство модели <Risk> Risks.
        public IEnumerable<Risk> Risks { get; set; }

        // Указываем работу OnGet методами.
        [BindProperty (SupportsGet = true)]
        public string Search { get; set; }


        // Передаем в конструктор класса db приведенную к IRiskRepository.
        public RisksModel(IRiskRepository db)
        {
            _db = db;
        }

        /// <summary>
        /// Метод передает лист Рисков Risks в модель @Model.Risks.
        /// </summary>
        public void OnGet()
        {
            Risks = _db.Search(Search);
        }
    }
}
