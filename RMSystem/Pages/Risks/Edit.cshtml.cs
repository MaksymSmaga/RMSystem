using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RMSystem.Models;
using RMSystem.Services;
using System;
using System.IO;

namespace RMSystem.Pages.Risks
{
    public class EditModel : PageModel
    {
        // ���������� ����������� ��� ������ � ��� ������ ������ ������ EditModel.
        private readonly IRiskRepository _riskRepository;

        // ���������� ������ � ������.
        public readonly IWebHostEnvironment _webHostEnviroment;

        // �������� ����������� � ����������� ������ ������ EditModel.

        public EditModel(IRiskRepository riskRepository, IWebHostEnvironment webHostEnviroment)
        {
            _riskRepository = riskRepository;
            _webHostEnviroment = webHostEnviroment;
        }

        // �������� �������� �� ���� Post ������� �������� �����������.
        [BindProperty]
        // ����������� �������� Risk ������ EditModel.
        public Risk Risk { get; set; }

        // �������� �������� �� ���� Post ������� �������� �����������.
        [BindProperty]

        // �������� ��� �������� ����.
        public IFormFile Photo { get; set; }

        // �������� �������� �� ���� Post ������� �������� �����������.
        [BindProperty]

        // ��� �������� ������� ���������.
        public bool Notify { get; set; }

        // �������� ���������.
        public string Message { get; set; }

        /// <summary>
        /// ���������������� �� �������� �������� ID.
        /// ������ �������� ����������.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> IActionResult �������� </returns>
        public IActionResult OnGet(int? id)
        {
            // ��� ��� id nullble �������� ��������.
            if (id.HasValue)
                Risk = _riskRepository.GetRisk(id.Value);
            else
                Risk = new Risk();

            if (Risk == null)
                return RedirectToPage("/NotFound");
            return Page();
        }

        // �������� ������ OnPost - ��� �������� � ��������� �� ������.
        public IActionResult OnPost()
        {
            // �������� ���������� ������.
            if (ModelState.IsValid)
            {
                // ���������� ����.
                if (Photo != null)
                {
                    if (Risk.PhotoPath != null)
                    {   // �������� ������ ���� � ����.
                        string filePath = Path.Combine(_webHostEnviroment.WebRootPath, "images", Risk.PhotoPath);
                        // ������� ����.
                        System.IO.File.Delete(filePath);
                    }
                    // ��������� ����.
                    Risk.PhotoPath = UploadFile();
                }


                if (Risk.Id > 0)
                {
                    // ��������� Risk.
                    Risk = _riskRepository.Update(Risk);

                    // ������� ����� �������� ������(���������) � ������ ������������� TempData.
                    // TempData - Dictionary,  "SuccesMassage" - ���� ��������.
                    TempData["SuccesMassage"] = $"Update {Risk.Description} is successfull!";
                }
                else
                { 
                    // ���������� Risk.
                    Risk = _riskRepository.Add(Risk);

                    // ������� ����� �������� ������(���������) � ������ ������������� TempData.
                    // TempData - Dictionary,  "SuccesMassage" - ���� ��������.
                    TempData["SuccesMassage"] = $"Adding {Risk.Description} is successfull!";

                }
                // ��������������� �� �������� Risks.
                return RedirectToPage("Risks");
            }
            return Page();
        }

        // ����� ����� �������� ������ ���������� � OnPost.
        public void OnPostUpdateNotifRef(int id)
        {
            if (Notify)
                Message = "Thanks for norification";
            else
                Message = "Norificftion is turned off";

            // ��������� ������ ������� �������� ID.
            Risk = _riskRepository.GetRisk(id);

        }

        // �������� �����.
        private string UploadFile()
        {
            string uniqeFileName = null;

            if (Photo != null)
            {
                // ���� �����.
                string uploadsFolder = Path.Combine(_webHostEnviroment.WebRootPath, "images");
                // ������������� ��� �����.
                uniqeFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                // ����������� ����.
                string filePath = Path.Combine(uploadsFolder, uniqeFileName);

                // using ��� ����������� ��������..
                using (var fs = new FileStream(filePath, FileMode.Create))

                {   //  �������� ���� ������� �� ������.
                    Photo.CopyTo(fs);
                }
            }
            // ���������� ���������� ��� �����.
            return uniqeFileName;
        }
    }
}
