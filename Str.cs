using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    internal class Str
    {
        public int currentPos = 0;
        private int maxPos;
        private int minPos;

        public Str(int minPos, int maxPos)
        {
            Console.SetCursorPosition(0, 0);
            this.minPos = minPos;
            this.maxPos = maxPos;

            currentPos = minPos;
            Show(-1);
        }

        public void Show(int prevPos)
        {
            if (minPos > maxPos)
            {
                return;
            }

            if (prevPos >= 0)
            {
                Console.SetCursorPosition(0, prevPos);
                Console.WriteLine("  ");
            }

            Console.SetCursorPosition(0, currentPos);
            Console.WriteLine("->");

            int viewPosition = Math.Max(currentPos - minPos, 0);
            Console.SetCursorPosition(0, viewPosition);
        }

        public void Next()
        {
            int prevPos = currentPos;
            currentPos += 1;
            currentPos = currentPos > maxPos ? minPos : currentPos;
            Show(prevPos);
        }
        public void Prev()
        {
            int prevPos = currentPos;
            currentPos -= 1;
            currentPos = currentPos < minPos ? maxPos : currentPos;
            Show(prevPos);
        }

        public int GetIndex()
        {
            return currentPos - minPos;
        }

        public void SetMax(int maxPos)
        {
            this.maxPos = maxPos;
            currentPos = currentPos > maxPos ? minPos : currentPos;
        }

        public void SetMin(int minPos)
        {
            this.minPos = minPos;
            currentPos = currentPos < minPos ? maxPos : currentPos;
        }
    }
}

