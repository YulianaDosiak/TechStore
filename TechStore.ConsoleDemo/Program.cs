using System;
using AutoMapper;
using TechStore.DALEF.AutoMapper;
using TechStore.DALEF.Concrete;
using DTO = TechStore.DTO;

class Program
{
    static void Main()
    {
        // 1. Налаштування AutoMapper
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<CategoryMap>();
            cfg.AddProfile<ProductMap>();
            cfg.AddProfile<UserMap>();
            // додати інші профілі
        });

        IMapper mapper = config.CreateMapper();

        // 2. Створення DAL класів
        string connStr = "Server=localhost;Database=TechStore;User Id=sa;Password=MyStr0ngPass123;";
        var categoryDal = new CategoryDALEF(connStr, mapper);

        // 3. Приклади CRUD
        Console.WriteLine("----Створюємо категорію----");
        var newCategory = categoryDal.Create(new DTO.Category { CategoryName = "Смартфони" });
        Console.WriteLine($"Створено: {newCategory.CategoryId} - {newCategory.CategoryName}");

        Console.WriteLine("----Всі категорії----");
        var categories = categoryDal.GetAll();
        foreach (var cat in categories)
        {
            Console.WriteLine($"{cat.CategoryId} - {cat.CategoryName}");
        }

        Console.WriteLine("----Отримати категорію по Id----");
        var catById = categoryDal.GetById(newCategory.CategoryId);
        Console.WriteLine($"{catById.CategoryId} - {catById.CategoryName}");

        Console.WriteLine("----Оновити категорію----");
        catById.CategoryName = "Ноутбуки";
        categoryDal.Update(catById);

        Console.WriteLine("----Видалити категорію----");
        categoryDal.Delete(catById.CategoryId);

        Console.WriteLine("Готово!");
    }
}
