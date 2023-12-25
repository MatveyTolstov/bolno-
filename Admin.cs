using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Fear_and_Pain;
using Microsoft.VisualBasic.FileIO;

internal class Admin : ICRUD
{
    private List<UserBase> users;
    private string name;

    public Admin(List<UserBase> users, string name) {
        this.users = users;
        this.name = name;
    }
    public void Create()
    {
        Console.Clear();
        Console.WriteLine($"Добрый день, {name}!");
        Console.WriteLine("Ваша роль админ");
        Console.WriteLine("Esc - назад, S - сохранить | 0 - адм., 1 - менеджер, 2 - ск - менеджер, 3 - кассир, 4 - бухг.");
        Console.WriteLine("------------------------");

        int id = users.Max(u => u.id) + 1;
        Console.WriteLine($"  ID: {id} (присваивается автоматически)");
        Console.WriteLine("  Логин: ");
        Console.WriteLine("  Пароль: ");
        Console.WriteLine("  Роль: ");

        UserBase? user = null;
        Str strelka = new Str(5, 7);
        string login = "";
        string password = "";
        string role = "";

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
                    ProcessSubmit(strelka,ref login, ref password, ref role);
                    break;
                case (ConsoleKey)Keys.Save:
                    ProcessSave(ref id ,ref login, ref password, ref role, ref user);
                    break;
            }

            key = Console.ReadKey(true).Key;
        }

        Convertor.Jsonser<List<UserBase>>(users, "users.json");
    }

    private void ProcessSubmit(Str strelka, ref string login, ref string password, ref string role)
    {
        int i = strelka.GetIndex();
        if (i == 0)
        {
            Console.SetCursorPosition(9, 5);
            login = Vvod.GetValue(login);
        }
        else if (i == 1)
        {
            Console.SetCursorPosition(10, 6);
            password = Vvod.GetValue(password);
        }
        else if (i == 2)
        {
            Console.SetCursorPosition(8, 7);
            role = Vvod.GetValue(role);
        }
    }

    private void ProcessSave(ref int id ,ref string login, ref string password, ref string role, ref UserBase? user)
    {
        if (string.IsNullOrEmpty(login))
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Логин не должен быть пустым. ");
            return;
        }
        if (string.IsNullOrEmpty(password))
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Пароль не должен быть пустым.");
            return;
        }
        if (!int.TryParse(role, out int role_id) || !Enum.IsDefined(typeof(Roles), role_id))
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Неверная роль.    ");
            return;
        }

        Roles user_role = (Roles)role_id;

        if (user == null)
        {
            user = new UserBase(id, login, password, user_role);
            users.Add(user);
        }
        else
        {
            user.login = login;
            user.password = password;
            user.roles = user_role;
        }

        Console.SetCursorPosition(0, 8);
        Console.WriteLine("Сохранено.                  ");
    }
    public void Update(int index)
    {
        UserBase user = users[index];
        Console.Clear();
        Console.WriteLine($"Добрый день, {name}!");
        Console.WriteLine("Ваша роль админ");
        Console.WriteLine("Esc - назад, S - сохранить | 0 - адм., 1 - менеджер, 2 - ск - менеджер, 3 - кассир, 4 - бухг.");
        Console.WriteLine("------------------------");

        DisplayUserInfo(user);

        Str strelka = new Str(5, 7);
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
                    ProcessSubmit(strelka, ref user);
                    break;
                case (ConsoleKey)Keys.Save:
                    ProcessSave(ref user);
                    break;
            }

            key = Console.ReadKey(true).Key;
        }

        Convertor.Jsonser<List<UserBase>>(users, "users.json");
    }

    private void DisplayUserInfo(UserBase user)
    {
        int id = user.id;
        string login = user.login;
        string password = user.password;
        string role = ((int)user.roles).ToString();
        Console.WriteLine($"  ID: {id} (присваивается автоматически)");
        Console.WriteLine($"  Логин: {login}");
        Console.WriteLine($"  Пароль: {password}");
        Console.WriteLine($"  Роль: {role}");
    }

    private void ProcessSubmit(Str strelka, ref UserBase user)
    {
        int i = strelka.GetIndex();
        if (i == 0)
        {
            Console.SetCursorPosition(9, 5);
            user.login = Vvod.GetValue(user.login);
        }
        else if (i == 1)
        {
            Console.SetCursorPosition(10, 6);
            user.password = Vvod.GetValue(user.password);
        }
        else if (i == 2)
        {
            Console.SetCursorPosition(8, 7);
            string roleValue = ((int)user.roles).ToString();
            roleValue = Vvod.GetValue(roleValue);
            if (int.TryParse(roleValue, out int role_id) && Enum.IsDefined(typeof(Roles), role_id))
            {
                user.roles = (Roles)role_id;
            }
            else
            {
                Console.SetCursorPosition(0, 8);
                Console.WriteLine("Неверная роль.    ");
            }
        }
    }

    private void ProcessSave(ref UserBase user)
    {
        if (string.IsNullOrEmpty(user.login))
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Логин не должен быть пустым. ");
            return;
        }
        if (string.IsNullOrEmpty(user.password))
        {
            Console.SetCursorPosition(0, 8);
            Console.WriteLine("Пароль не должен быть пустым.");
            return;
        }

        
        Console.SetCursorPosition(0, 8);
        Console.WriteLine("Сохранено.                  ");
    }

    public void Delete(int index)
    {
        users.RemoveAt(index);
        Convertor.Jsonser(users, "users.json");
    }
    public void Read(int index)
    {
        DrawUsers(index);

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
    private string GetRoleName(Roles roles)
    {
        string roleName = "";
        switch (roles)
        {
            case Roles.Admin:
                roleName = "Администратор";
                break;
            case Roles.Buhgalter:
                roleName = "Бухгалтер";
                break;
            case Roles.Personal_Manager:
                roleName = "Менеджер персонала";
                break;
            case Roles.Warehouse_Manager:
                roleName = "Склад менеджер";
                break;
            case Roles.Kassir:
                roleName = "Кассир";
                break;
            default:
                break;
        }

        return roleName;
    }
    private void DrawMenu()
    {
        Console.Clear();
        Console.WriteLine($"Добро пожаловать, {name}");
        Console.WriteLine("Ваша роль админ");
        Console.WriteLine("F1 - создать запись, Enter - Перейти к записи");
        foreach (var user in users)
        {
            string role = GetRoleName(user.roles);
            Console.WriteLine($"  {user.id}  {user.login}  {user.roles}");
        }
    }
    private void DrawUsers(int index)
    {
        Console.Clear();
        UserBase user = users[index];
        Console.WriteLine($"Добро пожаловать, {name}");
        Console.WriteLine("Ваша роль админ");
        Console.WriteLine("Esc - назад, Del - удалить, R - редактировать");
        Console.WriteLine($"  ID: {user.id}");
        Console.WriteLine($"  Логин: {user.login}");
        Console.WriteLine($"  Пароль: {user.password}");
        string role = GetRoleName(user.roles);
        Console.WriteLine($"  Роль: {(int)user.roles} {role}");
    }
    public void Start()
    {
        DrawMenu();
        Str str = new(4, 4 + users.Count - 1);
        ConsoleKey key = Console.ReadKey(true).Key;
        while (key != (ConsoleKey)Keys.Close)
        {
            switch (key)
            {
                case (ConsoleKey)Keys.Down:
                    str.Next();
                    break;
                case (ConsoleKey)Keys.Up:
                    str.Prev();
                    break;
                case (ConsoleKey)Keys.Submit:
                    ProcessSubmit(str);
                    break;
                case (ConsoleKey)Keys.Create:
                    ProcessCreate(str);
                    break;
            }

            key = Console.ReadKey(true).Key;
        }
    }

    private void ProcessSubmit(Str str)
    {
        int index = str.GetIndex();
        Read(index);
        DrawMenu();
        str.SetMax(4 + users.Count - 1);
        str.Show(-1);
    }

    private void ProcessCreate(Str str)
    {
        Create();
        DrawMenu();
        str.SetMax(4 + users.Count - 1);
        str.Show(-1);
    }


}
