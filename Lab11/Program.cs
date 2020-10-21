using GoodsClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MyQueue
{
    public class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            MyQueue<Goods> goodsQueue = CreateRandomMyQueueOfGoods(10);
            Console.WriteLine("Перебор очереди");
            foreach (var i in goodsQueue)
            {
                Console.WriteLine(i.GetName());
            }           
            Console.WriteLine($"\nДоступ к элементу очереди:{goodsQueue.Peek().GetName()}");
            Console.WriteLine($"\nИзвлечение элемента очереди:{goodsQueue.Dequeue().GetName()}");
            Console.WriteLine("\nПеребор очереди после извлечения элемента");
            foreach (var i in goodsQueue)
            {
                Console.WriteLine(i.GetName());
            }
            Console.WriteLine("Добавление элемента в очередь");
            goodsQueue.Enqueue(new Food().CreateRandom());
            foreach (var i in goodsQueue)
            {
                Console.WriteLine(i.GetName());
            }
            Console.WriteLine($"\nКоличество элементов в очереди: {goodsQueue.Count()}");
            var itemToFind = goodsQueue.Peek();
            if (goodsQueue.Contains(itemToFind)) Console.WriteLine($"\nВ очереди содержится элемент {itemToFind.GetName()}");
            else Console.WriteLine($"\nВ очереди не содержится элемент {itemToFind.GetName()}");

            Console.WriteLine("\nКопирование в массив");
            Goods[] array = goodsQueue.ToArray();
            Console.WriteLine("\nПеребор массива:");
            foreach (var i in array)
            {
                Console.WriteLine(i.GetName());
            }

            Console.WriteLine("\nКопирование в массив начиная с заданного индекса:");
            array = new Goods[15];
            goodsQueue.CopyTo(array, 5);
            foreach (var i in array)
            {
                if (i == null) Console.WriteLine("null");
                else Console.WriteLine(i.GetName());
            }

            Console.WriteLine("\nКлонирование очереди:");
            var newQueue = (MyQueue<Goods>)goodsQueue.Clone();
            foreach (var i in newQueue)
            {
                Console.WriteLine(i.GetName());
            }
            Console.WriteLine("\nОчищение очереди:");
            goodsQueue.Clear();
            Console.WriteLine(goodsQueue.Peek() == null);
        }



        static MyQueue<Goods> CreateRandomMyQueueOfGoods(int length)
        {
            MyQueue<Goods> goodsList = new MyQueue<Goods>();
            for (int i = 0; i < 10; i++)
            {
                goodsList.Enqueue(CreateRandomGoods(rnd));
            }
            return goodsList;
        }

        static void ShowTheClosestExpirationDate(List<Goods> goodsList)
        {
            Food closestFood = null;
            foreach (var i in goodsList)
            {
                if (i is Food && closestFood == null)
                    closestFood = (Food)i;
                else continue;
                if (i is Food f && f.ExpirationDate < closestFood.ExpirationDate)
                    closestFood = f;
            }
            if (closestFood == null) Console.WriteLine("Объекты типа Food отсутвуют");
            else Console.WriteLine("Продукт с ближайшей датой истечения срока годности:" +
                                   $" {closestFood.Name} - {closestFood.ExpirationDate.ToShortDateString()}");
        }
        
        static Goods CreateRandomGoods(Random rnd)
        {
            int type = rnd.Next(0, 3);

            switch (type)
            {
                case 0: return new Toy().CreateRandom();
                case 1: return new Food().CreateRandom();
                case 2: return new MilkFood().CreateRandom();
                default: throw new Exception("unknown type");
            }
        }

        static Food CreateRandomFood()
        {
            string[] foodNames = { "Мясо", "Лук", "Картофель", "Хлеб" };
            return new Food(foodNames[rnd.Next(0, foodNames.Length - 1)], rnd.Next(50, 100), rnd.Next(1, 100), RandomDate());
        }

        

        public static DateTime RandomDate()
        {
            return new DateTime(rnd.Next(2020, 2022), rnd.Next(1, 12), rnd.Next(1, 30));
        }
    }
}
