using System.ComponentModel.DataAnnotations;

namespace CarService.Domain.Enums
{
    public enum Roles
    {
        [Display(Name = "Администратор")] Admin = 1,
        [Display(Name = "Менеджер")] Manager = 2,
        [Display(Name = "Диагност")] Diagnostician = 3,
        [Display(Name = "Автомеханик")] Mechanic = 4,
    }
}