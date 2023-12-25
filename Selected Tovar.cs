using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    public class Selected_Tovar: Tovar
    {
        public int selectedCount = 0;

        public Selected_Tovar(int id, string name, float price, int count, int selectedCount = 0)
        {
            this.id = id;
            this.name = name;
            this.price = price;
            this.count = count;
            this.selectedCount = selectedCount;
        }
    }
}
