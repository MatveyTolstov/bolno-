using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    internal interface ICRUD
    {
        public void Create();

        public void Update(int index);

        public void Delete(int index);

        public void Read(int index);
    }
}
