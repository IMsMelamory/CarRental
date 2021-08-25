using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;


namespace CarRental
{
    class Program
    {
        static void Main(string[] args)
        {
            var manager1 = new Manager() { LastName = "Иванов", Name = "Иван", SecondLastName = "Иванович", BDay = DateTime.Parse("11.12.1987"),} ;
            var manager2 = new Manager(){LastName = "Иванова",Name = "Ирина",SecondLastName = "Ивановна",BDay = DateTime.Parse("10.02.1989"),};
            
            Console.WriteLine("Введите фамилию менеджера");
            var inputManager = Console.ReadLine();
            if (inputManager.ToLower() == "иванов" || inputManager.ToLower() == "иванова")
            {
                var thisManager = new Manager();
                if (inputManager.ToLower() == "иванов")
                {
                    thisManager = manager1;
                }
                else
                {
                    thisManager = manager2;
                }
                while (true)
                {
                    Console.WriteLine("Введите: 1 - добавить клиента; 2 - удалить клиента; 3 - добавить машину; 4 - удалить машину; 5 - поиск клиента; 6 - поиск машины; end - выход");
                    var inputString = Console.ReadLine();
                    var parseResult = int.TryParse(inputString, out var rez);
                    if (rez == 1)
                    {
                        Console.WriteLine("Введите фамилию");
                        var LastName = Console.ReadLine();
                        Console.WriteLine("Введите Имя");
                        var Name = Console.ReadLine();
                        Console.WriteLine("Введите Отчество");
                        var SecondLastName = Console.ReadLine();
                        Console.WriteLine("Введите полную дату рождения");
                        var BDay = Console.ReadLine();
                        Console.WriteLine("Введите номер удостоверения");
                        var NumberDriverLicense = Console.ReadLine();
                        var user = new Client();
                        user.AddClient(LastName, Name, SecondLastName, BDay, NumberDriverLicense, thisManager);
                    }
                    else if (rez == 2)
                    {
                        Console.WriteLine("Введите номер удостоверения");
                        var NumberDriverLicense = Console.ReadLine();
                        var user = new Client();
                        user.RemoveClient(NumberDriverLicense);
                    }
                    else if (rez == 3)
                    {
                        Console.WriteLine("Введите номер машины");
                        var NumberCar = Console.ReadLine();
                        Console.WriteLine("Введите модель машины");
                        var ModelCar = Console.ReadLine();
                        Console.WriteLine("Введите цвет машины");
                        var ColorCar = Console.ReadLine();
                        Console.WriteLine("Введите год выпуска");
                        var DataRelease = Console.ReadLine();
                        Console.WriteLine("Введите доступна ли машина: true\false");
                        var Availability = Console.ReadLine();
                        Console.WriteLine("Введите стоимость аренды в день");
                        var DayPrice = Console.ReadLine();
                        var car = new Cars();
                        car.AddCar(NumberCar, ModelCar, ColorCar, DataRelease, Availability, int.Parse(DayPrice));
                    }
                    else if (rez == 4)
                    {
                        Console.WriteLine("Введите номер машины");
                        var NumberCar = Console.ReadLine();
                        var car = new Cars();
                        car.RemoveCar(NumberCar);
                    }
                    else if (rez == 5)
                    {
                        Console.WriteLine("Поиск по: 1- Фамилия; 2 - Имя; 3 - Отчество; 4 - Водительское удостоверение");
                        var RezFindClient = Console.ReadLine();
                        var parseResultFindClient = int.TryParse(RezFindClient, out var rezfindclient);
                        if (rezfindclient == 1)
                        {
                            Console.WriteLine("Введите фамилию клиента");
                            var Find = Console.ReadLine();
                            var FindClient = new Client();
                            var FindClientRez = FindClient.GetClient().FindAll(client => client.LastName == Find).ToList();
                            foreach (Client client in FindClientRez)
                            {
                                Console.WriteLine(client.LastName + " " + client.Name + " " + client.SecondLastName + " " + client.BDay + " " + client.NumberDriversLicence);
                            }
                        }
                        else if (rezfindclient == 2)
                        {
                            Console.WriteLine("Введите имя клиента");
                            var Find = Console.ReadLine();
                            var FindClient = new Client();
                            var FindClientRez = FindClient.GetClient().FindAll(client => client.Name == Find).ToList();
                            foreach (Client client in FindClientRez)
                            {
                                Console.WriteLine(client.LastName + " " + client.Name + " " + client.SecondLastName + " " + client.BDay + " " + client.NumberDriversLicence);
                            }
                        }
                        else if (rezfindclient == 3)
                        {
                            Console.WriteLine("Введите отчество клиента");
                            var Find = Console.ReadLine();
                            var FindClient = new Client();
                            var FindClientRez = FindClient.GetClient().FindAll(client => client.SecondLastName == Find).ToList();
                            foreach (Client client in FindClientRez)
                            {
                                Console.WriteLine(client.LastName + " " + client.Name + " " + client.SecondLastName + " " + client.BDay + " " + client.NumberDriversLicence);
                            }
                        }
                        else if (rezfindclient == 4)
                        {
                            Console.WriteLine("Введите номер водительского удостоверения клиента");
                            var Find = Console.ReadLine();
                            var FindClient = new Client();
                            var FindClientRez = FindClient.GetClient().FindAll(client => client.NumberDriversLicence == Find).ToList();
                            foreach (Client client in FindClientRez)
                            {
                                Console.WriteLine(client.LastName + " " + client.Name + " " + client.SecondLastName + " " + client.BDay + " " + client.NumberDriversLicence);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Такого клиента не существует!!!");
                        }
                    }
                    else if (rez == 6)
                    {
                        Console.WriteLine("Поиск по: 1- Номер машины; 2 - Марка машины; 3 - Цвет машины; 4 - Годвыпуска; 5 - Наличие: true\false;");
                        var RezFindCar = Console.ReadLine();
                        var parseResultFindCar = int.TryParse(RezFindCar, out var rezfindcar);
                        if (rezfindcar == 1)
                        {
                            Console.WriteLine("Введите номер машины");
                            var Find = Console.ReadLine();
                            var FindCarRez = new Cars();
                            var FindCar = FindCarRez.GetCars().FindAll(car => car.NumberCar == Find).ToList();
                            foreach (Cars car in FindCar)
                            {
                                Console.WriteLine(car.NumberCar + " " + car.ModelCar + " " + car.ColorCar + " " + car.DateRelease + " " + car.DayPrice + car.Availability);
                            }
                        }
                        else if (rezfindcar == 2)
                        {
                            Console.WriteLine("Введите марку машины");
                            var Find = Console.ReadLine();
                            var FindCarRez = new Cars();
                            var FindCar = FindCarRez.GetCars().FindAll(car => car.ModelCar == Find).ToList();
                            foreach (Cars car in FindCar)
                            {
                                Console.WriteLine(car.NumberCar + " " + car.ModelCar + " " + car.ColorCar + " " + car.DateRelease + " " + car.DayPrice + car.Availability);
                            }
                        }
                        else if (rezfindcar == 3)
                        {
                            Console.WriteLine("Введите цвет машины");
                            var Find = Console.ReadLine();
                            var FindCarRez = new Cars();
                            var FindCar = FindCarRez.GetCars().FindAll(car => car.ColorCar == Find).ToList();
                            foreach (Cars car in FindCar)
                            {
                                Console.WriteLine(car.NumberCar + " " + car.ModelCar + " " + car.ColorCar + " " + car.DateRelease + " " + car.DayPrice + car.Availability);
                            }
                        }
                        else if (rezfindcar == 4)
                        {
                            Console.WriteLine("Введите год выпуска машины");
                            var Find = Console.ReadLine();
                            var FindCarRez = new Cars();
                            var FindCar = FindCarRez.GetCars().FindAll(car => car.DateRelease == Find).ToList();
                            foreach (Cars car in FindCar)
                            {
                                Console.WriteLine(car.NumberCar + " " + car.ModelCar + " " + car.ColorCar + " " + car.DateRelease + " " + car.DayPrice + car.Availability);
                            }
                        }
                        else if (rezfindcar == 5)
                        {
                            Console.WriteLine("Введите true или false ");
                            var Find = Console.ReadLine();
                            var FindCarRez = new Cars();
                            var FindCar = FindCarRez.GetCars().FindAll(car => car.Availability == bool.Parse(Find)).ToList();
                            foreach (Cars car in FindCar)
                            {
                                Console.WriteLine(car.NumberCar + " " + car.ModelCar + " " + car.ColorCar + " " + car.DateRelease + " " + car.DayPrice + car.Availability);
                            }
                        }
                        else
                        {
                            Console.WriteLine("Такой машины не существует!!!");
                        }
                    }
                    else
                    {
                        if (inputString.ToLower() == "end")
                        {
                            break;
                        }
                        Console.WriteLine("Что-то ничего не могу сделать...Ввведите корректное число!!!");
                    }
                }
                
            }
            else
            {
                Console.WriteLine("Такого менеджера не существует!!!");
            }
            


        }
    }
}
