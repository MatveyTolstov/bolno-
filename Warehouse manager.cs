using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    public class Warehouse_manager
    {
        
        List<Tovar> tovari;
        private string name;
        public Warehouse_manager(List<Tovar> tovari, string name)
        {
            this.tovari = tovari;
            this.name = name;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {name}!");
            Console.WriteLine("Ваша роль склад-менеджер");
            Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
            Console.WriteLine("------------------------");
            foreach (var tovar in tovari)
            {
                Console.WriteLine($"  {tovar.id} - {tovar.name}, {tovar.count}шт., {tovar.price}руб.");
            }
        }

        private void DrawTovar(int index)
        {
            Tovar tovar = tovari[index];
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {name}!");
            Console.WriteLine("Ваша роль склад-менеджер");
            Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {tovar.id}");
            Console.WriteLine($"  Наименование: {tovar.name}");
            Console.WriteLine($"  Цена: {tovar.price}");
            Console.WriteLine($"  Количество: {tovar.count}");
        }

        public void Start()
        {
            DrawMenu();
            Str strelka = new(4, 4 + tovari.Count - 1);
            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)Keys.Close)
            {
                switch (key)
                {
                    case (ConsoleKey)Keys.Down:
                        strelka.Next();
                        break;
                    case (ConsoleKey)Keys.Up:
                        strelka.Prev();
                        break;
                    case (ConsoleKey)Keys.Submit:
                        Read(strelka.GetIndex());
                        DrawMenu();
                        strelka.SetMax(4 + tovari.Count - 1);
                        strelka.Show(-1);
                        break;
                    case (ConsoleKey)Keys.Create:
                        Create();
                        DrawMenu();
                        strelka.SetMax(4 + tovari.Count - 1);
                        strelka.Show(-1);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Create()
        {
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {name}!");
            Console.WriteLine("Ваша роль склад-менеджер");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id;
            if (tovari.Count > 0)
            {
                id = tovari.Max(s => s.id) + 1;
            }
            else
            {
                id = 0;
            }
            Console.WriteLine($"  ID: {id} (присваивается автоматически)");
            Console.WriteLine("  Наименование: ");
            Console.WriteLine("  Цена: ");
            Console.WriteLine("  Количество: ");
            Tovar? tovar = null;
            Str strelka = new(5, 7);
            string t_name = "";
            string price = "";
            string count = "";

            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)Keys.Close)
            {
                switch (key)
                {
                    case (ConsoleKey)Keys.Down:
                        strelka.Next();
                        break;
                    case (ConsoleKey)Keys.Up:
                        strelka.Prev();
                        break;
                    case (ConsoleKey)Keys.Submit:
                        int index = strelka.GetIndex();

                        if (index == 0)
                        {
                            Console.SetCursorPosition(16, 5);
                            t_name = Vvod.GetValue(t_name);
                        }
                        else if (index == 1)
                        {
                            Console.SetCursorPosition(8, 6);
                            price = Vvod.GetValue(price);
                        }
                        else if (index == 2)
                        {
                            Console.SetCursorPosition(14, 7);
                            count = Vvod.GetValue(count);
                        }
                        break;
                    case (ConsoleKey)Keys.Save:
                        if (t_name == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Наименование не может быть пустым.");
                            break;
                        }

                        if (price == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Цена не может быть пустой.        ");
                            break;
                        }
                        float price_f;
                        try
                        {
                            price_f = float.Parse(price);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Неверный формат цены.             ");
                            break;
                        }

                        if (count == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Количество не может быть пустым.  ");
                            break;
                        }
                        int count_i;
                        try
                        {
                            count_i = int.Parse(count);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Неверный формат количества.       ");
                            break;
                        }


                        if (tovar == null)
                        {
                            tovar = new Tovar(id, t_name, price_f, count_i);
                            tovari.Add(tovar);
                        }
                        else
                        {
                            tovar.name = t_name;
                            tovar.price = price_f;
                            tovar.count = count_i;
                        }
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Сохранено.                        ");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Convertor.Jsonser<List<Tovar>>(tovari, "tovari.json");
        }

        public void Read(int index)
        {
            DrawTovar(index);

            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)Keys.Close)
            {
                switch (key)
                {
                    case (ConsoleKey)Keys.Delete:
                        Delete(index);
                        return;
                    case (ConsoleKey)Keys.Update:
                        Update(index);
                        DrawTovar(index);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        public void Update(int index)
        {
            Tovar tovar = tovari[index];
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {name}!");
            Console.WriteLine("Ваша роль склад-менеджер");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id = tovar.id;
            string t_name = tovar.name;
            string price = tovar.price.ToString();
            string count = tovar.count.ToString();
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine($"  Наименование: {t_name}");
            Console.WriteLine($"  Цена: {price}");
            Console.WriteLine($"  Количество: {count}");
            Str strelka = new(5, 7);


            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)Keys.Close)
            {
                switch (key)
                {
                    case (ConsoleKey)Keys.Down:
                        strelka.Next();
                        break;
                    case (ConsoleKey)Keys.Up:
                        strelka.Prev();
                        break;
                    case (ConsoleKey)Keys.Submit:
                        int s_index = strelka.GetIndex();

                        if (s_index == 0)
                        {
                            Console.SetCursorPosition(16, 5);
                            t_name = Vvod.GetValue(t_name);
                        }
                        else if (s_index == 1)
                        {
                            Console.SetCursorPosition(8, 6);
                            price = Vvod.GetValue(price);
                        }
                        else if (s_index == 2)
                        {
                            Console.SetCursorPosition(14, 7);
                            count = Vvod.GetValue(count);
                        }
                        break;
                    case (ConsoleKey)Keys.Save:
                        if (t_name == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Наименование не может быть пустым.");
                            break;
                        }

                        if (price == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Цена не может быть пустой.        ");
                            break;
                        }
                        float price_f;
                        try
                        {
                            price_f = float.Parse(price);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Неверный формат цены.             ");
                            break;
                        }

                        if (count == "")
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Количество не может быть пустым.  ");
                            break;
                        }
                        int count_i;
                        try
                        {
                            count_i = int.Parse(count);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 8);
                            Console.WriteLine("Неверный формат количества.       ");
                            break;
                        }

                        tovar.name = t_name;
                        tovar.price = price_f;
                        tovar.count = count_i;

                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Сохранено.                        ");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Convertor.Jsonser<List<Tovar>>(tovari, "tovari.json");
        }

        public void Delete(int index)
        {
            tovari.RemoveAt(index);
            Convertor.Jsonser(tovari, "tovari.json");
        }
    }
}

