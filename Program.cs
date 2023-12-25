using System;
using System.Data;

namespace Fear_and_Pain
{
    class Program
    {
        static List<UserBase> users;
        static List<InfoSotrudnik> sotrudniki;
        static List<Tovar> tovari;
        static List<Money> moneys;
        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            LoadFiles();
            while (true)
            {
                authorization auth = new authorization(users);
                UserBase auth_user = auth.Login();
                Console.Clear();
                InfoSotrudnik? auth_sotrudnik = sotrudniki.Find(s => s.user_id == auth_user.id);
                string name = auth_sotrudnik == null ? auth_user.login : auth_sotrudnik.I_name;
                switch (auth_user.roles)
                {
                    case Roles.Admin:
                        Admin admin = new Admin(users, name);
                        admin.Start();
                        break;
                    case Roles.Personal_Manager:
                        Personnel_Manager manager = new Personnel_Manager(sotrudniki, name);
                        manager.Start();
                        break;
                    case Roles.Warehouse_Manager:
                        Warehouse_manager storeManager = new Warehouse_manager(tovari, name);
                        storeManager.Start();
                        break;
                    case Roles.Kassir:
                        Kassir kassir = new Kassir(moneys, name);
                        kassir.Start();
                        break;
                    case Roles.Buhgalter:
                        Buhgalter buhgalter = new Buhgalter(moneys, name);
                        buhgalter.Start();
                        break;
                }
            }
        }

        static void LoadFiles()
        {
            List<UserBase>? loaded_users = Convertor.Jsonviser<List<UserBase>>("users.json");
            if (loaded_users == null)
            {
                loaded_users = new List<UserBase>();
                UserBase admin = new UserBase(0, "admin", "password", Roles.Admin);
                loaded_users.Add(admin);
                Convertor.Jsonser<List<UserBase>>(loaded_users, "users.json");
            }
            users = loaded_users;

            List<InfoSotrudnik>? loaded_sotrudniki = Convertor.Jsonviser<List<InfoSotrudnik>>("sotrudniki.json");
            if (loaded_sotrudniki == null)
            {
                loaded_sotrudniki = new List<InfoSotrudnik>();
                Convertor.Jsonser<List<InfoSotrudnik>>(loaded_sotrudniki, "sotrudniki.json");
            }
            sotrudniki = loaded_sotrudniki;

            List<Tovar>? loaded_tovari = Convertor.Jsonviser<List<Tovar>>("tovari.json");
            if (loaded_tovari == null)
            {
                loaded_tovari = new List<Tovar>();
                Convertor.Jsonser<List<Tovar>>(loaded_tovari, "tovari.json");
            }
            tovari = loaded_tovari;

            List<Money>? loaded_moneys = Convertor.Jsonviser<List<Money>>("zapisi.json");
            if (loaded_moneys == null)
            {
                loaded_moneys = new List<Money>();
                Convertor.Jsonser<List<Money>>(loaded_moneys, "zapisi.json");
            }
            moneys = loaded_moneys;
        }
    }

}