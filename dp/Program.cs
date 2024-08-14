using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("Добро пожаловать в волшебный лес!");
        Console.Write("Введите ваше имя, герой: ");
        string playerName = Console.ReadLine();
        Console.WriteLine($"Приветствую, {playerName}! Твоя цель - найти сокровище, скрытое в лесу.");

        
        int health = 100; // Здоровье игрока
        List<string> resources = new List<string>(); 

       
        char[,] forest = new char[5, 5]; // Карта леса 5x5
        InitializeForest(forest);

        // Размещениею сокровища в случайнойй позиции
        Random random = new Random();
        int treasureRow = random.Next(0, 5);
        int treasureCol = random.Next(0, 5);
        forest[treasureRow, treasureCol] = 'T';

        bool foundTreasure = false;
        int playerRow = 0, playerCol = 0; // Начальная позиция игрока

        
        while (health > 0 && !foundTreasure)
        {
            Console.WriteLine($"\nТекущее здоровье: {health}");
            Console.WriteLine("Введите команду (вверх/вниз/влево/вправо):");
            string command = Console.ReadLine().ToLower();

            // Движение. игрока по картеt
            switch (command)
            {
                case "вверх": if (playerRow > 0) playerRow--; break;
                case "вниз": if (playerRow < 4) playerRow++; break;
                case "влево": if (playerCol > 0) playerCol--; break;
                case "вправо": if (playerCol < 4) playerCol++; break;
                default: Console.WriteLine("Неверная команда!"); continue;
            }

            // Проверка позиции игрока
            if (playerRow == treasureRow && playerCol == treasureCol)
            {
                Console.WriteLine("Поздравляем! Вы нашли сокровище!");
                foundTreasure = true;
            }
            else
            {
                
                GenerateRandomEvent(ref health, resources);
            }

            
            DisplayForest(forest, playerRow, playerCol);
        }

        if (health <= 0)
        {
            Console.WriteLine("К сожалению, вы потеряли все здоровье и не смогли найти сокровище.");
        }
        else if (foundTreasure)
        {
            Console.WriteLine("Вы успешно завершили приключение с найденными ресурсами:");
            foreach (var resource in resources)
            {
                Console.WriteLine($"- {resource}");
            }
        }
    }

    static void InitializeForest(char[,] forest)
    {
        for (int i = 0; i < forest.GetLength(0); i++)
        {
            for (int j = 0; j < forest.GetLength(1); j++)
            {
                forest[i, j] = '.'; // Пустые клетки леса
            }
        }
    }

    static void GenerateRandomEvent(ref int health, List<string> resources)
    {
        Random random = new Random();
        int eventCode = random.Next(1, 5);

        switch (eventCode)
        {
            case 1:
                Console.WriteLine("Вы нашли зелье здоровья! Ваше здоровье восстановлено на 20 единиц.");
                health += 20;
                resources.Add("Зелье здоровья");
                break;
            case 2:
                Console.WriteLine("Вы попали в ловушку! Потеряно 30 единиц здоровья.");
                health -= 30;
                break;
            case 3:
                Console.WriteLine("Вы нашли магический кристалл! Он может пригодиться.");
                resources.Add("Магический кристалл");
                break;
            case 4:
                Console.WriteLine("Волшебные ягоды добавили вам энергии.");
                health += 10;
                resources.Add("Волшебные ягоды");
                break;
        }
    }

    static void DisplayForest(char[,] forest, int playerRow, int playerCol)
    {
        for (int i = 0; i < forest.GetLength(0); i++)
        {
            for (int j = 0; j < forest.GetLength(1); j++)
            {
                if (i == playerRow && j == playerCol)
                {
                    Console.Write("P "); // Игрок
                }
                else if (forest[i, j] == 'T')
                {
                    Console.Write(". "); // Сокровище, скрытое на карте
                }
                else
                {
                    Console.Write(forest[i, j] + " ");
                }
            }
            Console.WriteLine();
        }
    }
}
