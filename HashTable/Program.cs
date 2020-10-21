using GoodsClassLibrary;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace MyQueue
{
    public class Program
    {
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            var hashtable = CreateRandomHastableOfGoods(5);
            ShowHashtable(hashtable);
            
            Console.WriteLine("Добавление объекта");
            var newToy = new Toy().CreateRandom();
            hashtable.Add("Новый объект - игрушка", newToy);
            ShowHashtable(hashtable);
            Console.WriteLine("Удаление объекта");
            hashtable.Remove("Новый объект - игрушка");
            ShowHashtable(hashtable);


            Console.WriteLine("\nЗапрос 1. Найти игрушки с минимальной и максимальной ценами.");
           
            Toy minToy = (Toy)FindMinPriceToy(hashtable);
            Toy maxToy = (Toy)FindMaxPriceToy(hashtable);
            if (minToy == null) Console.WriteLine("В коллекции нет игрушек");
            else
            {
                Console.WriteLine($"Игрушка с минимальной ценой: {minToy.GetName()}. Цена:{minToy.Price}");
                Console.WriteLine($"Игрушка с максимальной ценой: {maxToy.GetName()}. Цена:{maxToy.Price}");
            }

            Console.WriteLine("\nЗапрос 2. Найти продукт с ближайшей датой истечения срока годности.");
            ShowTheClosestExpirationDate(hashtable);

            Console.WriteLine("\nЗапрос 3. Посчитать количество объектов типа MilkFood в коллекции.");
            Console.WriteLine($"Объектов заданного типа - {CountObjectsOfType(hashtable, typeof(MilkFood))}");

            Console.WriteLine("Поиск объекта по цене");
            Goods objectToFind = new Food("Хлеб", 20, 100, new DateTime(2021, 4, 26));
            hashtable.Add("Хлеб", new Food("Хлеб", 30, 100, new DateTime(2021, 4, 26)));
            FindObjectWithPrice(hashtable, objectToFind);

            Console.WriteLine("\nДемонстрация клонирования коллекции");
            Console.WriteLine("\nОригинальная коллекция");
            ShowHashtable(hashtable); 
            Hashtable clone = (Hashtable)hashtable.Clone();
            Console.WriteLine("\nКоллекция-клон");
            ShowHashtable(clone);

        }

        static void ShowHashtable(Hashtable ht)
        {
            foreach (DictionaryEntry de in ht)
            {
                var goods = de.Value as Goods;
                Console.WriteLine($"Ключ: {de.Key}  Значение: {goods.GetName()}");
            }
        }

        static Hashtable CreateRandomHastableOfGoods(int length)
        {
            Hashtable goodsHashtable = new Hashtable();
            for (int i = 0; i < length; i++)
            {
                var goods = CreateRandomGoods(rnd);
                goodsHashtable.Add(goods.Name + i, goods);
                
            }
            return goodsHashtable;
        }

        static Goods FindMinPriceToy(Hashtable ht)
        {
            ICollection values = ht.Values;
            double minPrice = double.MaxValue;
            Toy minToy = null;

            foreach (var item in values)
            {
                Goods g = item as Goods;
                if (g is Toy && g.Price < minPrice)
                {
                    minPrice = g.Price;
                    minToy = (Toy)g;
                }
            }
            return minToy;
        }

        static Goods FindMaxPriceToy(Hashtable ht)
        {
            ICollection values = ht.Values;
            double maxPrice = double.MinValue;
            Toy maxToy = null;

            foreach (var item in values)
            {
                Goods g = item as Goods;
                if (g is Toy && g.Price > maxPrice)
                {
                    maxPrice = g.Price;
                    maxToy = (Toy)g;
                }
            }
            return maxToy;
        }

        static void ShowTheClosestExpirationDate(Hashtable ht)
        {
            ICollection values = ht.Values;
            Food closestFood = null;
            foreach (var i in values)
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

        static int CountObjectsOfType(Hashtable ht, Type type)
        {
            ICollection values = ht.Values;
            int count = 0;
            foreach (var i in values)
            {
                if (i.GetType() == type) count++;
            }
            return count;
        }

        static void FindObjectWithPrice(Hashtable ht, Goods objectToFind)
        {
            
                ICollection values = ht.Values;
                Goods[] goodsArr = new Goods[values.Count];
                values.CopyTo(goodsArr, 0);
                int index = Array.BinarySearch(goodsArr, objectToFind, new PriceComparer());
                
            if (index != -1)
            {
                Goods objectFound = goodsArr[index];
                Console.WriteLine($"Объект найден: {objectFound.GetName()}");
            }
            else Console.WriteLine("Заданный объект отсутствует");
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
    }
}
