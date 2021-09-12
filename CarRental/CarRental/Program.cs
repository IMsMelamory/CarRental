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
            var managersRepository = new ManagersRepository(new JsonProvider<Manager>("managers.json"));

            while (true)
            {
                Console.WriteLine("Введите: 1 - вход админа, 2 - вход менеджера, exit - выход");
                var inputBegin = Console.ReadLine();
                if (inputBegin == "1")
                {
                    while (true)
                    {
                        Console.WriteLine("Введите: 1 - добавить менеджера, 2 - удалить менеджера, exit - выход");
                        var inputManager = Console.ReadLine();
                        if (inputManager == "1")
                        {
                            Console.WriteLine("Введите фамилию");
                            var lastName = Console.ReadLine();
                            Console.WriteLine("Введите Имя");
                            var name = Console.ReadLine();
                            Console.WriteLine("Введите Отчество");
                            var secondLastName = Console.ReadLine();
                            DateTime day;
                            var bDay = "";
                            while (true)
                            {
                                Console.WriteLine("Введите полную дату рождения");
                                bDay = Console.ReadLine();
                                if (DateTime.TryParse(bDay, out day) == false)
                                {
                                    Console.WriteLine("Ошибка! ВВедите полную дату рождения");
                                }
                                else
                                {
                                    break;
                                }
                            }
                            var managerToAdd = new Manager
                            {
                                LastName = lastName,
                                Name = name,
                                SecondLastName = secondLastName,
                                BDay = day,
                            };
                            managersRepository.Add(managerToAdd);
                        }
                        else if (inputManager == "2")
                        {
                            Console.WriteLine("Введите фамилию");
                            var lastName = Console.ReadLine();
                            managersRepository.RemoveByLastNameManager(lastName); 
                        }
                        else if (inputManager.ToLower() == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Некорерктный ввод");
                        }
                    }
                }
                else if (inputBegin == "2")
                {
                    Console.WriteLine("Введите фамилию менеджера");
                    var inputManager = Console.ReadLine();
                    if (managersRepository.FindByLastName(inputManager) != null)
                    {
                        var thisManager = managersRepository.FindByLastName(inputManager);
                        while (true)
                        {
                            Console.WriteLine("Введите: 1 - добавить клиента; 2 - удалить клиента; 3 - добавить машину; 4 - удалить машину; 5 - поиск клиента; 6 - поиск машины; exit - выход");
                            var inputString = Console.ReadLine();
                            switch (inputString)
                            { 
                                case "1":
                                    Console.WriteLine("Введите фамилию");
                                    var lastName = Console.ReadLine();
                                    Console.WriteLine("Введите Имя");
                                    var name = Console.ReadLine();
                                    Console.WriteLine("Введите Отчество");
                                    var secondLastName = Console.ReadLine();
                                    var bDay = "";
                                    DateTime day;
                                    while (true)
                                    {
                                        Console.WriteLine("Введите полную дату рождения");
                                        bDay = Console.ReadLine();
                                        if (DateTime.TryParse(bDay, out day) == false)
                                        {
                                            Console.WriteLine("Ошибка! ВВедите полную дату рождения");
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    Console.WriteLine("Введите номер удостоверения");
                                    var numberDriverLicense = Console.ReadLine();
                                    var clientToAdd = new Client()
                                    {
                                        LastName = lastName,
                                        Name = name,
                                        SecondLastName = secondLastName,
                                        BDay = day,
                                        NumberDriversLicence = numberDriverLicense,
                                    };
                                    clientsRepository.Add(clientToAdd);
                                    //clientToAdd.ClientAssociateManager((Manager)thisManager);
                                    break;
                                case "2":
                                    Console.WriteLine("Введите номер удостоверения");
                                    numberDriverLicense = Console.ReadLine();
                                    clientsRepository.RemoveByNumberDriversLicense(numberDriverLicense);
                                    break;
                                case "3":
                                    Console.WriteLine("Введите номер машины");
                                    var numberCar = Console.ReadLine();
                                    Console.WriteLine("Введите модель машины");
                                    var modelCar = Console.ReadLine();
                                    Console.WriteLine("Введите цвет машины");
                                    var colorCar = Console.ReadLine();
                                    Console.WriteLine("Введите год выпуска");
                                    var dataRelease = Console.ReadLine();
                                    var availabilityBool = false;
                                    while (true)
                                    {
                                        Console.WriteLine("Введите доступна ли машина: true\false");
                                        var availability = Console.ReadLine();
                                        if (bool.TryParse(availability, out availabilityBool) == false)
                                        {
                                            Console.WriteLine("Ошибка! ВВедите true или false");
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    var dayPriceInt = 0;
                                    while (true)
                                    {
                                        Console.WriteLine("Введите стоимость аренды в день");
                                        var dayPrice = Console.ReadLine();
                                        if (int.TryParse(dayPrice, out dayPriceInt) == false)
                                        {
                                            Console.WriteLine("Ошибка! ВВедите целочисленную стоимость");
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }
                                    var carToAdd = new Car
                                    {
                                        Number = numberCar,
                                        Model = modelCar,
                                        Color = colorCar,
                                        DateRelease = dataRelease,
                                        Availability = availabilityBool,
                                        DayPrice = dayPriceInt,
                                    };
                                    carRepository.Add(carToAdd);
                                    break;
                                case "4":
                                    Console.WriteLine("Введите номер машины");
                                    numberCar = Console.ReadLine();
                                    carRepository.RemoveByCarNumber(numberCar);
                                    break;
                                case "5":
                                    Console.WriteLine("Поиск по: 1- Фамилия; 2 - Имя; 3 - Отчество; 4 - Водительское удостоверение");
                                    var rezFindClient = Console.ReadLine();
                                    var parseResultFindClient = int.TryParse(rezFindClient, out var rezfindclient);
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
                                            foreach (var client in clientsRepository.FindBySecondLastName(find))
                                            {
                                                Console.WriteLine(client.LastName + " " + client.Name + " " + client.SecondLastName + " " + client.BDay + " " + client.NumberDriversLicence);
                                            }
                                            break;
                                        }
                                        case 4:
                                        {
                                            Console.WriteLine("Введите номер водительского удостоверения клиента");
                                            var find = Console.ReadLine();
                                            foreach (var client in clientsRepository.FindByNumberDriverLicense(find))
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
                                    var rezFindCar = Console.ReadLine();
                                    var parseResultFindCar = int.TryParse(rezFindCar, out var rezfindcar);
                                    switch (rezfindcar)
                                    {
                                        case 1:
                                        {
                                            Console.WriteLine("Введите номер машины");
                                            var find = Console.ReadLine();
                                            foreach (var cars in carRepository.FindByNumberCar(find))
                                            {
                                                Console.WriteLine(cars.Number + " " + cars.Model + " " + cars.Color + " " + cars.DateRelease + " " + cars.DayPrice + cars.Availability);
                                            }
                                            break;
                                        }
                                        case 2:
                                        {
                                            Console.WriteLine("Введите марку машины");
                                            var find = Console.ReadLine();
                                            foreach (var cars in carRepository.FindByModelCar(find))
                                            {
                                                Console.WriteLine(cars.Number + " " + cars.Model + " " + cars.Color + " " + cars.DateRelease + " " + cars.DayPrice + cars.Availability);
                                            }

                                            break;
                                        }
                                        case 3:
                                        {
                                            Console.WriteLine("Введите цвет машины");
                                            var find = Console.ReadLine();
                                            foreach (var cars in carRepository.FindByColorCar(find))
                                            {
                                                Console.WriteLine(cars.Number + " " + cars.Model + " " + cars.Color + " " + cars.DateRelease + " " + cars.DayPrice + cars.Availability);
                                            }

                                            break;
                                        }
                                        case 4:
                                        {
                                            Console.WriteLine("Введите год выпуска машины");
                                            var find = Console.ReadLine();
                                            foreach (var cars in carRepository.FindByDateRelease(find))
                                            {
                                                Console.WriteLine(cars.Number + " " + cars.Model + " " + cars.Color + " " + cars.DateRelease + " " + cars.DayPrice + cars.Availability);
                                            }

                                            break;
                                        }
                                        case 5:
                                        {
                                            Console.WriteLine("Введите true или false ");
                                            var find = Console.ReadLine();
                                            foreach (var cars in carRepository.FindByAviability(bool.TryParse(find, out var findBool) && findBool))
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
                else if (inputBegin.ToLower() == "exit")
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
