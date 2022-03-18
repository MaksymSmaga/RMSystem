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
        // Внутренний репозиторий для работы с ним внутри класса модели EditModel.
        private readonly IRiskRepository _riskRepository;

        // Переменная работы с хостом.
        public readonly IWebHostEnvironment _webHostEnviroment;

        // Передаем репозиторий в конструктор класса модели EditModel.

        public EditModel(IRiskRepository riskRepository, IWebHostEnvironment webHostEnviroment)
        {
            _riskRepository = riskRepository;
            _webHostEnviroment = webHostEnviroment;
        }

        // Свойство доступно во всех Post методах текущего контроллера.
        [BindProperty]
        // Декларируем свойство Risk модели EditModel.
        public Risk Risk { get; set; }

        // Свойство доступно во всех Post методах текущего контроллера.
        [BindProperty]

        // Свойство для хранения фото.
        public IFormFile Photo { get; set; }

        // Свойство доступно во всех Post методах текущего контроллера.
        [BindProperty]

        // Бул Свойство отсылки сообщение.
        public bool Notify { get; set; }

        // Свойство сообщение.
        public string Message { get; set; }

        /// <summary>
        /// Переадресовывает на страницу согласно ID.
        /// Только получает информацию.
        /// </summary>
        /// <param name="id"></param>
        /// <returns> IActionResult страницу </returns>
        public IActionResult OnGet(int? id)
        {
            // Так как id nullble проверим значение.
            if (id.HasValue)
                Risk = _riskRepository.GetRisk(id.Value);
            else
                Risk = new Risk();

            if (Risk == null)
                return RedirectToPage("/NotFound");
            return Page();
        }

        // Название только OnPost - Для отправки и обработки на сервер.
        public IActionResult OnPost()
        {
            // Проверка валидности модели.
            if (ModelState.IsValid)
            {
                // Сохранение фото.
                if (Photo != null)
                {
                    if (Risk.PhotoPath != null)
                    {   // Получаем полный путь к фото.
                        string filePath = Path.Combine(_webHostEnviroment.WebRootPath, "images", Risk.PhotoPath);
                        // Удаляем фото.
                        System.IO.File.Delete(filePath);
                    }
                    // Выгружаем фото.
                    Risk.PhotoPath = UploadFile();
                }


                if (Risk.Id > 0)
                {
                    // Обновляем Risk.
                    Risk = _riskRepository.Update(Risk);

                    // Простой метод передачи данных(сообщений) в другое представление TempData.
                    // TempData - Dictionary,  "SuccesMassage" - ключ значения.
                    TempData["SuccesMassage"] = $"Update {Risk.Description} is successfull!";
                }
                else
                { 
                    // Добавление Risk.
                    Risk = _riskRepository.Add(Risk);

                    // Простой метод передачи данных(сообщений) в другое представление TempData.
                    // TempData - Dictionary,  "SuccesMassage" - ключ значения.
                    TempData["SuccesMassage"] = $"Adding {Risk.Description} is successfull!";

                }
                // Перенаправление на страницу Risks.
                return RedirectToPage("Risks");
            }
            return Page();
        }

        // Любой метод отправки должен начинается с OnPost.
        public void OnPostUpdateNotifRef(int id)
        {
            if (Notify)
                Message = "Thanks for norification";
            else
                Message = "Norificftion is turned off";

            // Заполняем модель данными согласно ID.
            Risk = _riskRepository.GetRisk(id);

        }

        // Выгрузка файла.
        private string UploadFile()
        {
            string uniqeFileName = null;

            if (Photo != null)
            {
                // Путь файла.
                string uploadsFolder = Path.Combine(_webHostEnviroment.WebRootPath, "images");
                // Уникализируем имя файла.
                uniqeFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                // Комбинируем путь.
                string filePath = Path.Combine(uploadsFolder, uniqeFileName);

                // using для автоочистки ресурсов..
                using (var fs = new FileStream(filePath, FileMode.Create))

                {   //  Копируем файл потоком на сервер.
                    Photo.CopyTo(fs);
                }
            }
            // Возвращаем уникальное имя файла.
            return uniqeFileName;
        }
    }
}
