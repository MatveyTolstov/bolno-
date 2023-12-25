using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.VisualBasic.FileIO;

namespace Fear_and_Pain
{
    internal class Buhgalter
    {
        List<Money> moneys;
        private string name;
        public Buhgalter(List<Money> moneys, string name) {
            this .moneys = moneys;
            this.name = name;
        }
        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
            Console.WriteLine("------------------------");
            float total = 0;
            foreach (var money in moneys)
            {
                if (money.pribavka)
                {
                    total += money.sum;
                }
                else
                {
                    total -= money.sum;
                }

                string date = money.time.ToString("dd.MM.yyyy");
                string type = money.pribavka ? "Прибавка" : "Вычет";
                Console.WriteLine($"  {money.id} - {money.title}, {date}, {money.sum}руб., {type}");
            }
            Console.WriteLine("------------------------");
            Console.WriteLine($"Итого: {total}руб.");
        }


        private void DrawZapis(int index)
        {
            Money money = moneys[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {money.id}");
            Console.WriteLine($"  Название: {money.title}");
            Console.WriteLine($"  Сумма: {money.sum}");
            string date = money.time.ToString("dd.MM.yyyy");
            Console.WriteLine($"  Дата: {date}");
            Console.WriteLine($"  Прибавка?: {money.pribavka}");
        }
        public void Start()
        {
            DrawMenu();
            Str strelka = new Str(4, 4 + moneys.Count - 1);
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
                    case (ConsoleKey)Keys.Minus:
                        Read(strelka.GetIndex());
                        DrawMenu();
                        strelka.SetMax(4 + moneys.Count - 1);
                        strelka.Show(-1);
                        break;
                    case (ConsoleKey)Keys.Create:
                        Create();
                        DrawMenu();
                        strelka.SetMax(4 + moneys.Count - 1);
                        strelka.Show(-1);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }
        public void Create()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль бухгалтер");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");

            int id = GetNextMoneyId();
            Console.WriteLine($"  ID: {id} (присваивается автоматически)");
            Console.WriteLine("  Название: ");
            Console.WriteLine("  Сумма: ");
            Console.WriteLine("  Дата: ");
            Console.WriteLine("  Прибавка?: ");

            Money? moneys = null;
            Str strelka = new Str(5, 8);
            string z_name = "";
            string sum = "";
            string date = "";
            string prihod = "";

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
                        ProcessSubmit(strelka, ref z_name, ref sum, ref date, ref prihod);
                        break;
                    case (ConsoleKey)Keys.Save:
                        ProcessSave(z_name, sum, date, prihod, id);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

        private int GetNextMoneyId()
        {
            if (moneys.Count > 0)
            {
                return moneys.Max(s => s.id) + 1;
            }
            else
            {
                return 0;
            }
        }

        private void ProcessSubmit(Str strelka, ref string z_name, ref string sum, ref string date, ref string prihod)
        {
            int index = strelka.GetIndex();
            switch (index)
            {
                case 0:
                    Console.SetCursorPosition(12, 5);
                    z_name = Vvod.GetValue(z_name);
                    break;
                case 1:
                    Console.SetCursorPosition(9, 6);
                    sum = Vvod.GetValue(sum);
                    break;
                case 2:
                    Console.SetCursorPosition(8, 7);
                    date = Vvod.GetValue(date);
                    break;
                case 3:
                    Console.SetCursorPosition(13, 8);
                    prihod = Vvod.GetValue(prihod);
                    break;
            }
        }

        private void ProcessSave(string z_name, string sum, string date, string prihod, int id)
        {
            if (string.IsNullOrEmpty(z_name))
            {
                Console.SetCursorPosition(0, 9);
                Console.WriteLine("Название не должно быть пустым.");
                return;
            }

            if (string.IsNullOrEmpty(sum))
            {
                Console.SetCursorPosition(0, 9);
                Console.WriteLine("Сумма не должна быть пустой.   ");
                return;
            }

            if (!float.TryParse(sum, out float sum_f))
            {
                Console.SetCursorPosition(0, 9);
                Console.WriteLine("Неверная сумма суммы.        ");
                return;
            }

            DateTime date_dt;
            if (string.IsNullOrEmpty(date) || !DateTime.TryParse(date, out date_dt))
            {
                date_dt = DateTime.Now;
            }

            if (!bool.TryParse(prihod, out bool prihod_b))
            {
                Console.SetCursorPosition(0, 9);
                Console.WriteLine("Неверный формат прибавки.     ");
                return;
            }

            Money newMoney = new Money(id, z_name, sum_f, date_dt, prihod_b);
            moneys.Add(newMoney);

            Console.SetCursorPosition(0, 9);
            Console.WriteLine("Сохранено.                  ");
        }

        public void Read(int index)
        {

            ConsoleKey consoleKey = Console.ReadKey(true).Key;
            while (consoleKey != (ConsoleKey)Keys.Close)
            {
                switch (consoleKey)
                {
                    case (ConsoleKey)Keys.Delete:
                        Delete(index);
                        return;

                    case (ConsoleKey)Keys.Update:
                        Update(index);

                        break;
                }
            }
        }
        public void Delete(int index)
        {
            moneys.RemoveAt(index);
            Convertor.Jsonser(moneys, "moneys.json");
        }

        public void Update(int index)
        {
            Money money = moneys[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Бухгалтер");
            Console.WriteLine("Esc - назад, S - сохранить");

            int id = money.id;
            string title = money.title;
            string sum = money.sum.ToString();
            string time = money.time.ToString();
            string pribavka = money.pribavka.ToString();
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine($"  Название: {title}");
            Console.WriteLine($"  Сумма: {sum}");
            Console.WriteLine($"  Дата: {time}");
            Console.WriteLine($"  Прибавка?: {pribavka}");

            Str str = new Str(5, 8);

            ConsoleKey consoleKey = Console.ReadKey(true).Key;

            while(consoleKey != (ConsoleKey)Keys.Close)
            {
                switch (consoleKey)
                {
                    case (ConsoleKey)Keys.Down:
                        str.Next();
                        break;

                    case (ConsoleKey)Keys.Up:
                        str.Prev(); 
                        break;
                    case (ConsoleKey)Keys.Submit:
                        int Sindex = str.GetIndex();

                        if (Sindex >= 0)
                        {
                            Console.SetCursorPosition(12, 5);
                            title = Vvod.GetValue(title);
                        }
                        else if (Sindex == 1)
                        {
                            Console.SetCursorPosition(9, 6);
                            sum = Vvod.GetValue(time);
                        }
                        else if (Sindex == 2)
                        {
                            Console.SetCursorPosition(8, 7);
                            time = Vvod.GetValue(time);
                        }
                        else if (Sindex == 3)
                        {
                            Console.SetCursorPosition (13, 8);
                            pribavka = Vvod.GetValue(pribavka);
                        }
                        break;
                    case (ConsoleKey)Keys.Save:
                        if (title == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Название не должно быть пустым.");
                            break;
                        }

                        if (sum == "")
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Сумма не должно быть пустой.   ");
                            break;
                        }
                        float sum_float;
                        try
                        {
                            sum_float = float.Parse(sum);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат суммы.        ");
                            break;
                        }

                        DateTime timeDate;
                        if (time == "")
                        {
                            timeDate = DateTime.Now;
                        }
                        else
                        {
                            try
                            {
                                timeDate = DateTime.Parse(time);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 9);
                                Console.WriteLine("Неверный формат даты.         ");
                                break;
                            }
                        }

                        bool pribavka_bool;
                        if (pribavka.ToLower() == "false")
                        {
                            pribavka_bool = false;
                        }
                        else if (pribavka.ToLower() == "true")
                        {
                            pribavka_bool = true;
                        }
                        else
                        {
                            Console.SetCursorPosition(0, 9);
                            Console.WriteLine("Неверный формат прибавки.     ");
                            break;
                        }


                        money.title = title;
                        money.sum = sum_float;
                        money.time = timeDate;
                        money.pribavka = pribavka_bool;
                        Console.SetCursorPosition(0, 8);
                        Console.WriteLine("Сохранено.                    ");
                        break;
                }
                consoleKey = Console.ReadKey(true).Key;



            }

            Convertor.Jsonser<List<Money>>(moneys, "moneys.json");


        }
            
        

    }   


}
