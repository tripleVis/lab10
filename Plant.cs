using System;
using System.Collections;
using System.Linq;

namespace lab10
{
	class Plant : IList
	{
		public string Title { get; private set; }//инициализация свойств
		public string Type { get; private set; }
		public Color Color { get; private set; }
		public Leaf[] Leaves { get; private set; }
		public int Size { get; private set; }

		public int Count => Leaves.Length;

		public object this[int index] { get => Leaves[index]; set => Leaves[index] = value as Leaf; }

		public Plant(string title, string color, string type = "Bush", int leafsAmt = 256)
		{
			Title = title;
			Type = type;
			Color = GetColor(color);
			Size = leafsAmt;
			Leaves = new Leaf[Size];

		}

		public Plant(string title, Color color, string type = "Bush", int leafsAmt = 256)
		{
			Title = title;
			Type = type;
			Color = color;
			Size = leafsAmt;
			Leaves = new Leaf[Size];
			for (int i = 0; i < 128; i++)
			{
				Add(new Leaf());
			}
		}

		public bool IsFixedSize => true;

		public bool IsReadOnly => true;

		public bool IsSynchronized => throw new NotImplementedException();

		public object SyncRoot => throw new NotImplementedException();

		//добавление элемента
		public int Add(object leaf)
		{
			Leaves = Leaves.Append(leaf as Leaf).ToArray();
			return Leaves.Length - 1;
		}

		public void Remove(Leaf leaf)//удаление элемента
		{
			Leaves = Leaves.Where(item => item != leaf).ToArray();
		}

		public void Clear()//очистка
		{
			Leaves = new Leaf[Size];
		}

		public bool Contains(object value)//содержит ли
		{
			return Leaves.Contains(value as Leaf);
		}

		public int IndexOf(object value)
		{
			var leaf = value as Leaf;
			for (int i = 0; i < Leaves.Length; i++)
			{
				if (Leaves[i] == leaf)
					return i;
			}
			return -1;
		}

		public void Insert(int index, object value)//добавление элемента
		{
			if (index > Leaves.Length)
				throw new ArgumentOutOfRangeException();
			var newArr = new Leaf[Size];
			for (int i = 0, j = 0; i < Leaves.Length + 1; i++, j++)
			{
				if (i == index)
				{
					newArr[i] = value as Leaf;
					j--;
					continue;
				}
				newArr[i] = Leaves[j];
			}
			Leaves = newArr;
		}

		public void Remove(object value)
		{
			var newArr = new Leaf[Size];
			for (int i = 0, j = 0; i < Leaves.Length; i++, j++)
			{
				if (Leaves[i] == (value as Leaf))
				{
					j++;
					continue;
				}
				newArr[i] = Leaves[j];
			}
			Leaves = newArr;
		}

		public void RemoveAt(int index)//удалить элемент по индексу
		{
			if (index > Leaves.Length)
				throw new ArgumentOutOfRangeException();
			var newArr = new Leaf[Size];
			for (int i = 0, j = 0; i < Leaves.Length; i++, j++)
			{
				if (i == index)
					j++;
				newArr[i] = Leaves[j];
			}
			Leaves = newArr;
		}

		public void CopyTo(Array array, int index) => throw new NotImplementedException();

		public IEnumerator GetEnumerator() => Leaves.GetEnumerator();


		public static Color GetColor(string color)
		{
			return color.ToLower().Replace('ё', 'е') switch
			{
				"зеленый" => Color.Green,
				"желтый" => Color.Yellow,
				"красный" => Color.Red,
				"коричневый" => Color.Brown,
				_ => Color.Unknown,
			};
		}
	}

	class Leaf
	{
		public Color Color { get; private set; }//свойство

		public Leaf(Color color)//конструктор
		{
			Color = color;
		}

		public Leaf(string color)
		{
			Color = Plant.GetColor(color);
		}

		public Leaf()
		{
			var rndm = new Random();
			Color = (Color)rndm.Next(1, 5);
		}
	}

	enum Color //перечисление
	{
		Unknown,
		Green,
		Yellow,
		Red,
		Brown
	}
}
