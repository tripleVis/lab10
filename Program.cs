using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace lab10
{
	class Program
	{
		static void Main()
		{
			var plant = new Plant("Малина", Color.Green);//создание экземпляра
			plant.Add(new Leaf(Color.Yellow));//добавление элемента
			plant.Remove(plant.Leaves[2]);//удаление элемента
			Console.WriteLine("Кол-во листьев в кусте по цветам");
			var groups = plant.Leaves.GroupBy(item => item.Color);//сортировка по цвету
			foreach (var group in groups)
			{
				Console.WriteLine($"{group.Key}: {group.Count()}");
			}

			var rndm = new Random();//cоздание объекта для генерации чисел
			var hs = new HashSet<int>();//инициализация коллекции
			for (int i = 0; i < 10; i++)
			{
				hs.Add(rndm.Next(0, 100));//заполняем коллекцию случайными числами
			}
			Console.WriteLine("Элементы в коллекции HashSet:");
			Print(hs);
			Console.WriteLine();

			int n;
			do
			{
				Console.Write("Введите кол-во элементов для удаления: ");
			} while (!int.TryParse(Console.ReadLine(), out n) || n < 0 || n > hs.Count);
			for (int i = 0; i < n; i++)
			{
				using var enumerator = hs.GetEnumerator();
				if (enumerator.MoveNext())
					hs.Remove(enumerator.Current);
			}
			Console.WriteLine("Элементы в коллекции HashSet:");//вывод
			Print(hs);
			Console.WriteLine();

			hs.Add(20);//добавление элемента
			Console.WriteLine("Добавлен элемент 20");
			Console.WriteLine("Элементы в коллекции HashSet:");
			Print(hs);
			Console.WriteLine();

			var list = new List<int>(hs);
			Console.WriteLine("Элементы коллекции List, построенной на основе HashSet:");
			Print(list);
			Console.WriteLine();

			do
			{
				Console.Write("Введите элемент для поиска (пустая строка - выход): ");
				if (int.TryParse(Console.ReadLine(), out int item))//если удаётся преобразовать в int
					Console.WriteLine(list.Contains(item)
						? "Заданное значение есть во второй коллекции"
						: "Заданного значения нет во второй коллекции");
				else
					break;
				Console.WriteLine();
			} while (true);

			var observable = new ObservableCollection<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };//инициализируем коллекцию
			observable.CollectionChanged += ObservableOnCollectionChanged;//оповещение о изменении в коллекции

			observable.Add(11);
			observable.Remove(2);

			static void Print(IEnumerable<int> hs)
			{
				foreach (var item in hs)
				{
					Console.Write(item + " ");//вывод списка
				}
				Console.WriteLine();
			}
        }

		static void ObservableOnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
		{
			switch (e.Action)
			{
				case NotifyCollectionChangedAction.Add:
					Console.WriteLine("Добавлено: ");
					foreach (var item in e.NewItems)
					{
						Console.Write(item + " ");
					}
					break;
				case NotifyCollectionChangedAction.Remove:
					Console.WriteLine("Удалено: ");
					foreach (var item in e.OldItems)
					{
						Console.Write(item + " ");
					}
					break;
				default:
					Console.WriteLine("Необрабатываемое действие с изменением коллекции");
					break;
			}
			Console.WriteLine();
		}
	}
}