using System;
using System.Collections.Generic;

public class Car
{
    private string brand;
    private string model;
    private string number;
    private string color;
    private bool isRunning;
    private int speed;
    private int gear;

    public Car(string brand, string model, string number, string color)
    {
        this.brand = brand;
        this.model = model;
        this.number = number;
        this.color = color;
        isRunning = false;
        speed = 0;
        gear = 0;
    }

    public void Start()
    {
        if (gear == 0 || gear == 1)
        {
            isRunning = true;
        }
        else
        {
            Console.WriteLine("Нельзя завести машину при включенной передаче.");
            isRunning = false;
        }
    }

    public void Stop()
    {
        isRunning = false;
        speed = 0;
    }

    public void Accelerate(int speedIncrease)
    {
        if (!isRunning)
        {
            Console.WriteLine("Нельзя увеличить скорость, машина не заведена.");
        }
        else if (gear == 0)
        {
            Console.WriteLine("Нельзя увеличить скорость при включенной нейтральной передаче.");
        }
        else
        {
            speed += speedIncrease;
            Console.WriteLine($"Скорость увеличена на {speedIncrease} км/ч и составляет {speed} км/ч");
        }
    }

    public void Brake(int speedDecrease)
    {
        if (!isRunning)
        {
            Console.WriteLine("Нельзя притормаживать, машина не заведена.");
        }
        else
        {
            speed -= speedDecrease;
            if (speed < 0)
            {
                speed = 0;
            }
            Console.WriteLine($"Скорость уменьшена на {speedDecrease} км/ч и составляет {speed} км/ч");
        }
    }

    public void ChangeGear(int newGear)
    {
        if (!isRunning)
        {
            Console.WriteLine("Нельзя изменить передачу, машина не заведена.");
        }
        else
        {
            bool validGear = false;

            if (newGear == 0)
            {
                gear = 0;
                validGear = true;
            }
            else if ((newGear == 1 && speed >= 0 && speed <= 30) ||
                     (newGear == 2 && speed >= 20 && speed <= 50) ||
                     (newGear == 3 && speed >= 40 && speed <= 70) ||
                     (newGear == 4 && speed >= 60 && speed <= 90) ||
                     (newGear == 5 && speed >= 80 && speed <= 120))
            {
                gear = newGear;
                validGear = true;
            }

            if (!validGear)
            {
                Console.WriteLine("Нельзя установить передачу при текущей скорости.");
                isRunning = false;
                speed = 0;
            }
            else
            {
                Console.WriteLine($"Передача установлена на {newGear}");
            }
        }
    }

    public string GetStatus()
    {
        return $"Машина {brand} {model}, Номер: {number}, Цвет: {color}, " +
               $"Скорость: {speed} км/ч, Передача: {gear}, Состояние: {(isRunning ? "Заведена" : "Заглушена")}";
    }

    public string GetBrandModel()
    {
        return $"{brand} {model}";
    }
}

class Program
{
    static void Main()
    {
        List<Car> cars = new List<Car>
        {
            new Car("Toyota", "Camry", "AB123CD", "Синий"),
            new Car("BMW", "X5", "XY789ZZ", "Черный"),
            new Car("Ford", "Mustang", "FG456EF", "Красный")
        };

        Console.WriteLine("Выберите машину:");

        for (int i = 0; i < cars.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {cars[i].GetBrandModel()}");
        }

        int selectedCarIndex = int.Parse(Console.ReadLine()) - 1;

        if (selectedCarIndex < 0 || selectedCarIndex >= cars.Count)
        {
            Console.WriteLine("Некорректный выбор машины.");
            return;
        }

        Car selectedCar = cars[selectedCarIndex];

        while (true)
        {
            Console.WriteLine($"Выбрана машина: {selectedCar.
                GetBrandModel()}");
            Console.WriteLine("Выберите действие:");
            Console.WriteLine("1. Завести машину");
            Console.WriteLine("2. Заглушить машину");
            Console.WriteLine("3. Газануть");
            Console.WriteLine("4. Притормозить");
            Console.WriteLine("5. Переключить передачу");
            Console.WriteLine("6. Выйти");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    selectedCar.Start();
                    Console.WriteLine($"Машина {selectedCar.GetBrandModel()} завелась");
                    break;

                case 2:
                    selectedCar.Stop();
                    Console.WriteLine($"Машина {selectedCar.GetBrandModel()} остановилась");
                    break;

                case 3:
                    Console.WriteLine("Введите скорость для увеличения:");
                    int speedIncrease = int.Parse(Console.ReadLine());
                    selectedCar.Accelerate(speedIncrease);
                    break;

                case 4:
                    Console.WriteLine("Введите скорость для уменьшения:");
                    int speedDecrease = int.Parse(Console.ReadLine());
                    selectedCar.Brake(speedDecrease);
                    break;

                case 5:
                    Console.WriteLine("Введите новую передачу (от 0 до 5):");
                    int newGear = int.Parse(Console.ReadLine());
                    selectedCar.ChangeGear(newGear);
                    break;

                case 6:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Недопустимое действие. Попробуйте снова.");
                    break;
            }

            Console.WriteLine($"Состояние машины: {selectedCar.GetStatus()}");
        }
    }
}