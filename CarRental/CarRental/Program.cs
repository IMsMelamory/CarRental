using System;
using CarRental.Providers;
using CarRental.Repositories;

namespace CarRental
{
    class Program
    {
        static void Main(string[] args)
        {
            var carRepository = new CarsRepository(new JsonProvider<Car>("cars.json"));
            var clientsRepository = new ClientsRepository(new JsonProvider<Client>("clients.json"));
            var managersRepository = new Repository<Manager>(new JsonProvider<Manager>("managers.json"));

            while (true)
            {
                Console.WriteLine("Введите: 1 - вход админа, 2 - вход менеджера, exit - выход");
                var InputBegin = Console.ReadLine();
                if (InputBegin == "1")
                {
                    while (true)
                    {
                        Console.WriteLine("Введите: 1 - добавить менеджера, 2 - удалить менеджера, exit - выход");
                        var InputManager = Console.ReadLine();
                        if (InputManager == "1")
                        {
                            Console.WriteLine("Введите фамилию");
                            var LastName = Console.ReadLine();
                            Console.WriteLine("Введите Имя");
                            var Name = Console.ReadLine();
                            Console.WriteLine("Введите Отчество");
                            var SecondLastName = Console.ReadLine();
                            DateTime Day;
                            var BDay = "";
                            while (true)
                            {
                                Console.WriteLine("Введите полную дату рождения");
                                BDay = Console.ReadLine();
                                if (DateTime.TryParse(BDay, out Day) == false)
                                {
                                    Console.WriteLine("Ошибка! ВВедите полную дату рождения");
                                }
                                else
                                {
                                    break;
                                }
                            }
                            AddManager(LastName, Name, SecondLastName, Day);
                        }
                        else if (InputManager == "2")
                        {
                            Console.WriteLine("Введите фамилию");
                            var LastName = Console.ReadLine();
                            RemoveManager(LastName);
                        }
                        else if (InputManager.ToLower() == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Некорерктный ввод");
                        }
                    }
                }
                else if (InputBegin == "2")
                {
                    Console.WriteLine("Введите фамилию менеджера");
                    var inputManager = Console.ReadLine();
                    if (FindManager(inputManager) != null)
                    {
                        var thisManager = FindManager(inputManager);
                        while (true)
                        {
                            Console.WriteLine("Введите: 1 - добавить клиента; 2 - удалить клиента; 3 - добавить машину; 4 - удалить машину; 5 - поиск клиента; 6 - поиск машины; exit - выход");
                            var inputString = Console.ReadLine();
                            switch (inputString)
                            { 
                                case "1":
                                    Console.WriteLine("Введите фамилию");
                                    var LastName = Console.ReadLine();
                                    Console.WriteLine("Введите Имя");
                                    var Name = Console.ReadLine();
                                    Console.WriteLine("Введите Отчество");
                                    var SecondLastName = Console.ReadLine();
                                    var BDay = "";
                                    DateTime Day;
                                    while (true)
                                    {
                                        Console.WriteLine("Введите полную дату рождения");
                                        BDay = Console.ReadLine();
                                        if (DateTime.TryParse(BDay, out Day) == false)
                                        {
                                            Console.WriteLine("Ошибка! ВВедите полную дату рождения");
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    Console.WriteLine("Введите номер удостоверения");
                                    var NumberDriverLicense = Console.ReadLine();
                                    AddClient(LastName, Name, SecondLastName, Day, NumberDriverLicense, thisManager);
                                    break;
                                case "2":
                                    Console.WriteLine("Введите номер удостоверения");
                                    NumberDriverLicense = Console.ReadLine();
                                    RemoveClient(NumberDriverLicense);
                                    break;
                                case "3":
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

                                    var carToAdd = new Car
                                    {
                                        Number = NumberCar,
                                        Model = ModelCar,
                                        Color = ColorCar,
                                        DateRelease = DataRelease,
                                        Availability = bool.TryParse(Availability, out var availability) && availability,
                                        DayPrice = 
                                    };
                                    carRepository.Add(carToAdd);
                                    break;
                                case "4":
                                    Console.WriteLine("Введите номер машины");
                                    NumberCar = Console.ReadLine();
                                    
                                    carRepository.RemoveByCarNumber(NumberCar);
                                    break;
                                case "5":
                                    Console.WriteLine("Поиск по: 1- Фамилия; 2 - Имя; 3 - Отчество; 4 - Водительское удостоверение");
                                    var RezFindClient = Console.ReadLine();
                                    var parseResultFindClient = int.TryParse(RezFindClient, out var rezfindclient);
                                    switch (rezfindclient)
                                    {
                                        case 1:
                                        {
                                            Console.WriteLine("Введите фамилию клиента");
                                            var find = Console.ReadLine();
                                        
                                            foreach (var client in clientsRepository.FindByLastName(find))
                                            {
                                                Console.WriteLine(client.LastName + " " + client.Name + " " + client.SecondLastName + " " + client.BDay + " " + client.NumberDriversLicence);
                                            }

                                            break;
                                        }
                                        case 2:
                                        {
                                            Console.WriteLine("Введите имя клиента");
                                            var find = Console.ReadLine();
                                        
                                            foreach (Client client in clientsRepository.FindByName(find))
                                            {
                                                Console.WriteLine(client.LastName + " " + client.Name + " " + client.SecondLastName + " " + client.BDay + " " + client.NumberDriversLicence);
                                            }

                                            break;
                                        }
                                        case 3:
                                        {
                                            Console.WriteLine("Введите отчество клиента");
                                            var find = Console.ReadLine();
                                            var FindClientRez = GetClients().FindAll(client => client.SecondLastName == find).ToList();
                                            foreach (Client client in FindClientRez)
                                            {
                                                Console.WriteLine(client.LastName + " " + client.Name + " " + client.SecondLastName + " " + client.BDay + " " + client.NumberDriversLicence);
                                            }

                                            break;
                                        }
                                        case 4:
                                        {
                                            Console.WriteLine("Введите номер водительского удостоверения клиента");
                                            var find = Console.ReadLine();
                                            var FindClientRez = GetClients().FindAll(client => client.NumberDriversLicence == find).ToList();
                                            foreach (Client client in FindClientRez)
                                            {
                                                Console.WriteLine(client.LastName + " " + client.Name + " " + client.SecondLastName + " " + client.BDay + " " + client.NumberDriversLicence);
                                            }

                                            break;
                                        }
                                        default:
                                            Console.WriteLine("Такого клиента не существует!!!");
                                            break;
                                    }
                                    break;
                                case "6":
                                    Console.WriteLine("Поиск по: 1- Номер машины; 2 - Марка машины; 3 - Цвет машины; 4 - Годвыпуска; 5 - Наличие: true\false;");
                                    var RezFindCar = Console.ReadLine();
                                    var parseResultFindCar = int.TryParse(RezFindCar, out var rezfindcar);
                                    switch (rezfindcar)
                                    {
                                        case 1:
                                        {
                                            Console.WriteLine("Введите номер машины");
                                            var find = Console.ReadLine();
                                            var FindCar = GetCars().FindAll(car => car.Number == find).ToList();
                                            foreach (var cars in FindCar)
                                            {
                                                Console.WriteLine(cars.Number + " " + cars.Model + " " + cars.Color + " " + cars.DateRelease + " " + cars.DayPrice + cars.Availability);
                                            }

                                            break;
                                        }
                                        case 2:
                                        {
                                            Console.WriteLine("Введите марку машины");
                                            var find = Console.ReadLine();
                                            var FindCar = GetCars().FindAll(car => car.Model == find).ToList();
                                            foreach (var cars in FindCar)
                                            {
                                                Console.WriteLine(cars.Number + " " + cars.Model + " " + cars.Color + " " + cars.DateRelease + " " + cars.DayPrice + cars.Availability);
                                            }

                                            break;
                                        }
                                        case 3:
                                        {
                                            Console.WriteLine("Введите цвет машины");
                                            var find = Console.ReadLine();
                                            var FindCar = GetCars().FindAll(car => car.Color == find).ToList();
                                            foreach (var cars in FindCar)
                                            {
                                                Console.WriteLine(cars.Number + " " + cars.Model + " " + cars.Color + " " + cars.DateRelease + " " + cars.DayPrice + cars.Availability);
                                            }

                                            break;
                                        }
                                        case 4:
                                        {
                                            Console.WriteLine("Введите год выпуска машины");
                                            var find = Console.ReadLine();
                                            var FindCar = GetCars().FindAll(car => car.DateRelease == find).ToList();
                                            foreach (var cars in FindCar)
                                            {
                                                Console.WriteLine(cars.Number + " " + cars.Model + " " + cars.Color + " " + cars.DateRelease + " " + cars.DayPrice + cars.Availability);
                                            }

                                            break;
                                        }
                                        case 5:
                                        {
                                            Console.WriteLine("Введите true или false ");
                                            var find = Console.ReadLine();
                                            var FindCar = GetCars().FindAll(car => car.Availability == bool.Parse(find)).ToList();
                                            foreach (var cars in FindCar)
                                            {
                                                Console.WriteLine(cars.Number + " " + cars.Model + " " + cars.Color + " " + cars.DateRelease + " " + cars.DayPrice + cars.Availability);
                                            }

                                            break;
                                        }
                                        default:
                                            Console.WriteLine("Такой машины не существует!!!");
                                            break;
                                    }
                                    break;
                                case "exit":
                                    break;
                                default:
                                    Console.WriteLine("Что-то ничего не могу сделать...Ввведите корректное число!!!");
                                    break;
                            }
                            if (inputString.ToLower() == "exit")
                            {
                                break;
                            }

                        }
                    }
                    else
                    {
                        Console.WriteLine("Такого менеджера не существует!!!");
                    }
                }
                else if (InputBegin.ToLower() == "exit")
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Введите корректные данные");
                }
            }
            
            

            


        }
    }
}
