using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    internal class Money
    {
        public int id;
        public string title;
        public float sum;
        public DateTime time;
        public bool pribavka;

        public Money(int id, string title, float sum, DateTime time, bool pribavka)
        {
            this.id = id;
            this.title = title;
            this.sum = sum;
            this.time = time;
            this.pribavka = pribavka;
        }
    }
}
