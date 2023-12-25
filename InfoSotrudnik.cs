using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    internal class InfoSotrudnik
    {
        public int id;
        public string surname;
        public string I_name;
        public string midname;
        public DateTime birthday;
        public string passport;
        public string dolzhnost;
        public float zarplata;
        public int? user_id;

        public InfoSotrudnik(int id, string surname, string I_name, DateTime birthday, string passport, string dolzhnost, float zarplata, int? user_id = null,  string midname = "")
        {
            this.id = id;
            this.surname = surname;
            this.I_name = I_name;
            this.midname = midname;
            this.birthday = birthday;
            this.passport = passport;
            this.dolzhnost = dolzhnost;
            this.zarplata = zarplata;
            this.user_id = user_id;
        }
    }
}
