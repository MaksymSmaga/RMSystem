using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMSystem.Models;
using RMSystem.Services;
using System.Collections.Generic;

namespace RMSystem.Pages.Risks
{
    public class RisksModel : PageModel
    {   // ����������� db ��� ������ � ��� ������ ������ ������ RisksModel.
        public readonly IRiskRepository _db;

        // ������� �������� ������ <Risk> Risks.
        public IEnumerable<Risk> Risks { get; set; }

        // ��������� ������ OnGet ��������.
        [BindProperty (SupportsGet = true)]
        public string Search { get; set; }


        // �������� � ����������� ������ db ����������� � IRiskRepository.
        public RisksModel(IRiskRepository db)
        {
            _db = db;
        }

        /// <summary>
        /// ����� �������� ���� ������ Risks � ������ @Model.Risks.
        /// </summary>
        public void OnGet()
        {
            Risks = _db.Search(Search);
        }
    }
}
