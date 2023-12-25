using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    internal class Kassir
    {
        List<Selected_Tovar> tovari;
        List<Money> moneys;
        private string name;

        public Kassir(
            List<Money> moneys, string name)
        {
            LoadTovari();
            this.moneys = moneys;
            this.name = name;
        }

        private void LoadTovari()
        {
            List<Selected_Tovar>? tovari = Convertor.Jsonviser<List<Selected_Tovar>>("tovari.json");
            if (tovari == null)
            {
                tovari = new List<Selected_Tovar>();
            }
            this.tovari = tovari;
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый пожаловать, {name}!");
            Console.WriteLine("Ваша роль кассир");
            Console.WriteLine("Enter - Перейти к записи, S - подтвердить покупку");
            Console.WriteLine("------------------------");
            float total = 0;
            foreach (var tovar in tovari)
            {
                total += tovar.price * tovar.selectedCount;
                Console.WriteLine($"  {tovar.id} - {tovar.name}, {tovar.price}руб. | {tovar.selectedCount}");
            }
            Console.WriteLine("------------------------");
            Console.WriteLine($"Итого: {total}руб.");
        }

        private void DrawTovar(int index)
        {
            Selected_Tovar tovar = tovari[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль кассир");
            Console.WriteLine("Esc - назад");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {tovar.id} (присваивается автоматически)");
            Console.WriteLine($"  Наименование: {tovar.name}");
            Console.WriteLine($"  Цена: {tovar.price}");
            Console.WriteLine($"  Количество на складе: {tovar.count}");
            Console.WriteLine($"  Выбранное количество: {tovar.selectedCount}");
        }

        public void Start()
        {
            DrawMenu();
            Str strelka = new Str(4, 4 + tovari.Count - 1);
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
                        Select(strelka.GetIndex());
                        DrawMenu();
                        strelka.Show(-1);
                        break;
                    case (ConsoleKey)Keys.Save:
                        Submit();
                        LoadTovari();
                        DrawMenu();
                        strelka.Show(-1);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        private void Select(int index)
        {
            DrawTovar(index);
            Selected_Tovar tovar = tovari[index];
            ConsoleKey key = Console.ReadKey(true).Key;
            while (key != (ConsoleKey)Keys.Close)
            {
                switch (key)
                {
                    case (ConsoleKey)Keys.Plus:
                        tovar.selectedCount += 1;
                        if (tovar.selectedCount > tovar.count)
                        {
                            tovar.selectedCount = tovar.count;
                        }
                        break;
                    case (ConsoleKey)Keys.Minus:
                        tovar.selectedCount -= 1;
                        if (tovar.selectedCount < 0)
                        {
                            tovar.selectedCount = 0;
                        }
                        break;
                }
                Console.SetCursorPosition(24, 8);
                Console.Write($"{tovar.selectedCount}                    ");
                key = Console.ReadKey(true).Key;
            }
        }

        private void Submit()
        {
            List<Tovar> tovari = new List<Tovar>();
            foreach (var tovar in this.tovari)
            {
                tovar.count -= tovar.selectedCount;
                tovari.Add(tovar);

                int money_id;
                if (moneys.Count > 0)
                {
                    money_id = moneys.Max(z => z.id) + 1;
                }
                else
                {
                    money_id = 0;
                }
                Money money = new Money(money_id, tovar.name, tovar.selectedCount * tovar.price, DateTime.Now, true);
                moneys.Add(money);
                tovar.selectedCount = 0;
            }

            Convertor.Jsonser<List<Money>>(moneys, "moneys.json");
            Convertor.Jsonser<List<Tovar>>(tovari, "tovari.json");
        }
    }

}

