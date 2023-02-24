using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Enums
{
    public enum OrderStatus
    {
        [Display(Name = "Создан")] Created = 1,
        [Display(Name = "Продиагностирован")] Diagnosed = 2,
        [Display(Name = "В работе")] InWork = 3,
        [Display(Name = "Закрыт по работам")] ClosedByWork = 4,
        [Display(Name = "Выполнен")] Completed = 5,
        [Display(Name = "Отказано клиентом")] RegectedByClient = 6,
    }
}
