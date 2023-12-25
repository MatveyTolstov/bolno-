using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    internal class Personnel_Manager: ICRUD
    {
        List<InfoSotrudnik> sotrudniki;
        private string name;
        public Personnel_Manager(List<InfoSotrudnik> sotrudniki, string name)
        {
            this.sotrudniki = sotrudniki;
            this.name = name;
        }

        public void Create()
        {
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль: Менеджер персонала");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id;
            if (sotrudniki.Count > 0)
            {
                id = sotrudniki.Max(s => s.id) + 1;
            }
            else
            {
                id = 0;
            }
            Console.WriteLine($"  ID: {id} (присваивается автоматически)");
            Console.WriteLine("  Фамилия: ");
            Console.WriteLine("  Имя: ");
            Console.WriteLine("  Отчество: ");
            Console.WriteLine("  День рождения: ");
            Console.WriteLine("  Паспорт: ");
            Console.WriteLine("  Должность: ");
            Console.WriteLine("  Зарплата: ");
            Console.WriteLine("  ID пользователя: ");
            InfoSotrudnik? sotrudnik = null;
            Str strelka = new(5, 12);
            string surname = "";
            string I_name = "";
            string midname = "";
            string birthday = "";
            string passport = "";
            string dolzhnost = "";
            string zarplata = "";
            string user_id = "";

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
                            Console.SetCursorPosition(11, 5);
                            surname = Vvod.GetValue(surname);
                        }
                        else if (index == 1)
                        {
                            Console.SetCursorPosition(7, 6);
                            I_name = Vvod.GetValue(I_name);
                        }
                        else if (index == 2)
                        {
                            Console.SetCursorPosition(12, 7);
                            midname = Vvod.GetValue(midname);
                        }
                        else if (index == 3)
                        {
                            Console.SetCursorPosition(17, 8);
                            birthday = Vvod.GetValue(birthday);
                        }
                        else if (index == 4)
                        {
                            Console.SetCursorPosition(11, 9);
                            passport = Vvod.GetValue(passport);
                        }
                        else if (index == 5)
                        {
                            Console.SetCursorPosition(13, 10);
                            dolzhnost = Vvod.GetValue(dolzhnost);
                        }
                        else if (index == 6)
                        {
                            Console.SetCursorPosition(12, 11);
                            zarplata = Vvod.GetValue(zarplata);
                        }
                        else if (index == 7)
                        {
                            Console.SetCursorPosition(19, 12);
                            user_id = Vvod.GetValue(user_id);
                        }
                        break;
                    case (ConsoleKey)Keys.Save:
                        if (surname == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Фамилия не должен быть пустой.      ");
                            break;
                        }

                        if (I_name == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Имя не должен быть пустым.          ");
                            break;
                        }

                        if (birthday == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("День рождения не должен быть пустым.");
                            break;
                        }
                        DateTime birthday_dt;
                        try
                        {
                            birthday_dt = DateTime.Parse(birthday);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат даты.              ");
                            break;
                        }

                        if (passport == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Пасспорт не должен быть пустым.     ");
                            break;
                        }

                        if (dolzhnost == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Должность не должен быть пустой.    ");
                            break;
                        }

                        if (zarplata == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Зарплата не должен быть пустой.     ");
                            break;
                        }
                        float zarplata_f;
                        try
                        {
                            zarplata_f = float.Parse(zarplata);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат зарплаты.          ");
                            break;
                        }

                        int? user_id_i;
                        if (user_id != "")
                        {
                            try
                            {
                                user_id_i = int.Parse(user_id);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.WriteLine("Неверный формат ID пользователя.   ");
                                break;
                            }
                        }
                        else
                        {
                            user_id_i = null;
                        }

                        if (sotrudnik == null)
                        {
                            sotrudnik = new InfoSotrudnik(id, surname, I_name, birthday_dt, passport, dolzhnost, zarplata_f, user_id_i, midname);
                            sotrudniki.Add(sotrudnik);
                        }
                        else
                        {
                            sotrudnik.surname = surname;
                            sotrudnik.I_name = I_name;
                            sotrudnik.midname = midname;
                            sotrudnik.birthday = birthday_dt;
                            sotrudnik.passport = passport;
                            sotrudnik.dolzhnost = dolzhnost;
                            sotrudnik.zarplata = zarplata_f;
                            sotrudnik.user_id = user_id_i;
                        }
                        Console.SetCursorPosition(0, 13);
                        Console.WriteLine("Сохранено.                         ");

                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Convertor.Jsonser<List<InfoSotrudnik>>(sotrudniki, "sotrudniki.json");
        }
        public void Update(int index)
        {
            InfoSotrudnik sotrudnik = sotrudniki[index];
            Console.Clear();
            Console.WriteLine($"Добрый день, {name}!");
            Console.WriteLine("Ваша роль менеджер персонала");
            Console.WriteLine("Esc - назад, S - сохранить");
            Console.WriteLine("------------------------");
            int id = sotrudnik.id;
            string surname = sotrudnik.surname;
            string I_name = sotrudnik.I_name;
            string midname = sotrudnik.midname;
            string birthday = sotrudnik.birthday.ToString("dd.MM.yyyy");
            string passport = sotrudnik.passport;
            string dolzhnost = sotrudnik.dolzhnost;
            string zarplata = sotrudnik.zarplata.ToString();
            string user_id = sotrudnik.user_id != null ? sotrudnik.user_id.ToString() : "";
            Console.WriteLine($"  ID: {id} (выдается автоматически)");
            Console.WriteLine($"  Фамилия: {surname}");
            Console.WriteLine($"  Имя: {I_name}");
            Console.WriteLine($"  Отчество: {midname}");
            Console.WriteLine($"  День рождения: {birthday}");
            Console.WriteLine($"  Паспорт: {passport}");
            Console.WriteLine($"  Должность: {dolzhnost}");
            Console.WriteLine($"  Зарплата: {zarplata}");
            Console.WriteLine($"  ID пользователя: {user_id}");
            Str strelka = new(5, 12);

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
                            Console.SetCursorPosition(11, 5);
                            surname = Vvod.GetValue(surname);
                        }
                        else if (s_index == 1)
                        {
                            Console.SetCursorPosition(7, 6);
                            I_name = Vvod.GetValue(I_name);
                        }
                        else if (s_index == 2)
                        {
                            Console.SetCursorPosition(12, 7);
                            midname = Vvod.GetValue(midname);
                        }
                        else if (s_index == 3)
                        {
                            Console.SetCursorPosition(17, 8);
                            birthday = Vvod.GetValue(birthday);
                        }
                        else if (s_index == 4)
                        {
                            Console.SetCursorPosition(11, 9);
                            passport = Vvod.GetValue(passport);
                        }
                        else if (s_index == 5)
                        {
                            Console.SetCursorPosition(13, 10);
                            dolzhnost = Vvod.GetValue(dolzhnost);
                        }
                        else if (s_index == 6)
                        {
                            Console.SetCursorPosition(12, 11);
                            zarplata = Vvod.GetValue(zarplata);
                        }
                        else if (s_index == 7)
                        {
                            Console.SetCursorPosition(19, 12);
                            user_id = Vvod.GetValue(user_id);
                        }
                        break;
                    case (ConsoleKey)Keys.Save:
                        if (surname == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Фамилия не должна быть пустой.");
                            break;
                        }

                        if (I_name == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Имя не должно быть пустым.");
                            break;
                        }

                        if (birthday == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("День Рождения не должно быть пустым.");
                            break;
                        }
                        DateTime birthday_dt;
                        try
                        {
                            birthday_dt = DateTime.Parse(birthday);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат даты.");
                            break;
                        }

                        if (passport == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Пасспорт не должен быть пустым.");
                            break;
                        }

                        if (dolzhnost == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Должность не должна быть пустой.");
                            break;
                        }

                        if (zarplata == "")
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Зарплата не должен быть пустой.");
                            break;
                        }
                        float zarplata_surname;
                        try
                        {
                            zarplata_surname = float.Parse(zarplata);
                        }
                        catch
                        {
                            Console.SetCursorPosition(0, 13);
                            Console.WriteLine("Неверный формат зарплаты.");
                            break;
                        }

                        int? user_id_name;
                        if (user_id != "")
                        {
                            try
                            {
                                user_id_name = int.Parse(user_id);
                            }
                            catch
                            {
                                Console.SetCursorPosition(0, 13);
                                Console.WriteLine("Неверный формат ID пользователя.");
                                break;
                            }
                        }
                        else
                        {
                            user_id_name = null;
                        }

                        sotrudnik.surname = surname;
                        sotrudnik.I_name = I_name;
                        sotrudnik.surname = surname;
                        sotrudnik.birthday = birthday_dt;
                        sotrudnik.passport = passport;
                        sotrudnik.dolzhnost = dolzhnost;
                        sotrudnik.zarplata = zarplata_surname;
                        sotrudnik.user_id = user_id_name;

                        Console.SetCursorPosition(0, 13);
                        Console.WriteLine("Сохранено.                  ");
                        break;
                }

                key = Console.ReadKey(true).Key;
            }

            Convertor.Jsonser<List<InfoSotrudnik>>(sotrudniki, "sotrudniki.json");
        }

        public void Read(int index)
        {
            DrawSotrudnik(index);

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
                        DrawSotrudnik(index);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }
        public void Delete(int index)
        {
            sotrudniki.RemoveAt(index);
            Convertor.Jsonser(sotrudniki, "sotrudniki.json");
        }

        private void DrawMenu()
        {
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {name}!");
            Console.WriteLine("Ваша роль менеджер персонала");
            Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
            Console.WriteLine("------------------------");
            foreach (var sotrudnik in sotrudniki)
            {
                string fio = $"{sotrudnik.surname} {sotrudnik.I_name} {sotrudnik.midname}";
                Console.WriteLine($"  {sotrudnik.id} - {fio}, {sotrudnik.dolzhnost}");
            }
        }
        private void DrawSotrudnik(int index)
        {
            InfoSotrudnik sotrudnik = sotrudniki[index];
            Console.Clear();
            Console.WriteLine($"Добро пожаловать, {name}!");
            Console.WriteLine("Ваша роль менеджер персонала");
            Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
            Console.WriteLine("------------------------");
            Console.WriteLine($"  ID: {sotrudnik.id}");
            Console.WriteLine($"  Фамилия: {sotrudnik.surname}");
            Console.WriteLine($"  Имя: {sotrudnik.I_name}");
            Console.WriteLine($"  Отчество: {sotrudnik.midname}");
            string birthday = sotrudnik.birthday.ToString("dd.MM.yyyy");
            Console.WriteLine($"  День рождения: {birthday}");
            Console.WriteLine($"  Паспорт: {sotrudnik.passport}");
            Console.WriteLine($"  Должность: {sotrudnik.dolzhnost}");
            Console.WriteLine($"  Зарплата: {sotrudnik.zarplata}");
            string user_id = sotrudnik.user_id == null ? "" : sotrudnik.user_id.ToString();
            Console.WriteLine($"  ID пользователя: {user_id}");
        }
        public void Start()
        {
            DrawMenu();
            Str strelka = new(4, 4 + sotrudniki.Count - 1);
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
                        strelka.SetMax(4 + sotrudniki.Count - 1);
                        strelka.Show(-1);
                        break;
                    case (ConsoleKey)Keys.Create:
                        Create();
                        DrawMenu();
                        strelka.SetMax(4 + sotrudniki.Count - 1);
                        strelka.Show(-1);
                        break;
                }

                key = Console.ReadKey(true).Key;
            }
        }

    }
}
