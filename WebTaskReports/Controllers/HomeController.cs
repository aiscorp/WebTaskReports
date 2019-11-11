using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebTaskReports.Models;

namespace WebTaskReports.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _Configuration;

        private readonly List<Job> _Jobs;

        public HomeController(IConfiguration Configuration)
        {
            _Configuration = Configuration;

            _Jobs = new List<Job>
            {
                new Job { Id=1, Name="Проект asp.net core 3.0 - часть 1", Description="Создаем проект по аналогу на уроке, собираем грабли - из-за core 2.2 не работает часть кода, курим docs.microsoft.com"},
                new Job { Id=2, Name="Паттерны на Itvdn и Metainit - часть 1 - Abstract Factory", Description="Паттерн Абстрактная фабрика (Abstract Factory) предоставляет интерфейс для создания семейств взаимосвязанных объектов с определенными интерфейсами без указания конкретных типов данных объектов."},
                new Job { Id=3, Name="Проект asp.net core 3.0 - часть 2", Description="Ажур... 2,5 бакса в день без баз данных - да ну вас, вы серьезно?"},
                new Job { Id=4, Name="Паттерны на Itvdn и Metainit - часть 2 - Factory Method", Description="Фабричный метод (Factory Method) - это паттерн, который определяет интерфейс для создания объектов некоторого класса, но непосредственное решение о том, объект какого класса создавать происходит в подклассах. "},
                new Job { Id=5, Name="Проект asp.net core 3.0 - часть 3", Description="А что если посмотреть пример с Мастер страницами, Razor, Bootstrap и Авторизацией?? хм..."},
                new Job { Id=6, Name="Паттерны на Itvdn и Metainit - часть 3 - Builder", Description="Строитель (Builder) - шаблон проектирования, который инкапсулирует создание объекта и позволяет разделить его на различные этапы."},
            };
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Jobs()
        {
            return View(_Jobs);
        }

        public IActionResult Job(int id)
        {

            ViewData["Job"] = "Job: " + _Jobs[id].Id;

            return View(_Jobs[id]);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



    }
}