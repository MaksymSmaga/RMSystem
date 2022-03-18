
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMSystem.Models;
using RMSystem.Services;

namespace RMSystem.Pages.Risks
{
    public class DetailsModel : PageModel
    {
        // ���������� ���� ��� ������ � ��� ������ ������ ������ DetailsModel.
        private readonly IRiskRepository _riskRepository;

        // ��������� � ������ �����������.
        public DetailsModel(IRiskRepository riskRepository)
        {
            _riskRepository = riskRepository;
        }
        // ����������� ���� Risk ��� �������� ��������
        public Risk Risk { get; private set; }

        /// <summary>
        /// �������� Risk � ���������� �������� � ���
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>�������� IActionResult (������������, json �������)</returns>
        public IActionResult OnGet(int id)
        {
            Risk = _riskRepository.GetRisk(id);

            // �������������� �� �������� NotFound, ���� Risk == null
            if (Risk == null)

                return RedirectToPage("/NotFound");

            return Page();
        }
    }
}
