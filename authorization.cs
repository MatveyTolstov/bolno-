using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    internal class authorization
    {
        private List<UserBase> users;

        public authorization(List<UserBase> users)
        {
            this.users = users;
        }

        public UserBase Login()
        {
            Console.Clear();
            UserBase? user = null;
            string login = "";
            string password = "";
            Console.WriteLine("Авторизация");
            Console.WriteLine("-----------");

            Console.WriteLine("  Логин: ");
            Console.WriteLine("  Пароль: ");
            Console.WriteLine("  Авторизоваться");

            Str strelka = new (2, 4);

            while (user == null)
            {
                ConsoleKey key = Console.ReadKey(true).Key;
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
                        if (index == 2)
                        {
                            user = users.Find(u => u.login == login && u.password == password);
                            if (user == null)
                            {
                                Console.SetCursorPosition(0, 5);
                                Console.WriteLine("Неправильный логин или пароль");
                            }
                        }
                        else if (index == 0)
                        {
                            Console.SetCursorPosition(9, 2);
                            login = Vvod.GetValue(login);
                        }
                        else if (index == 1)
                        {
                            Console.SetCursorPosition(10, 3);
                            password = Vvod.GetValue(password, true);
                        }
                        break;
                }
            }

            return user;
        }
    }
}
