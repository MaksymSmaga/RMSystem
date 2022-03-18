using RMSystem.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace RMSystem.Models
{

    public class Risk
    {
        public int Id { get; set; }

        public DateTime DataCreated = DateTime.Now;

        // Обязательное поле модели.
        [Required]
        public Iso Iso { get; set; }

        public string PhotoPath { get; set; }

        // Обязательное поле модели.
        [Required]
        // Процесс.
        public Process? Process { get; set; }

        // Обязательное поле модели.
        [Required]
        // Тяжесть.
        public Deverity Deverity { get; set; }

        // Обязательное поле модели.
        [Required]
        // Вероятность.
        public Likelihood Likelihood { get; set; }

        // Обработка.
        public Treatment Treatment { get; set; }
        // Обязательное поле модели.

        [Required]
        // Категория.
        public Category? Category { get; set; }

        [MaxLength(500), MinLength(10)]
        // Поле не может быть пустым.
        [Required(ErrorMessage = "Please write an Risk Description")]
        // Описание.
        public string Description { get; set; }
        // Обязательное поле модели.
        [Required]

        // Причина.
        public Cause Cause { get; set; }

        // Обязательное поле модели.
        [Required]

        // Источник.
        public Source Source { get; set; }

        [Required]
        // Этап Управления.
        public PhaseManagement PhaseManagement { get; set; }

        // Обязательное поле модели.

        [Required]
        // Оборудование.
        public Equipment? Equipment { get; set; }
        public int Threat { get { return (int)Deverity * (int)Likelihood; } }
    }

}
