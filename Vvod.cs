using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fear_and_Pain
{
    internal class Vvod
    {
        public static string GetValue(string current, bool secure = false)
        {
            Console.CursorVisible = true;
            int left = Console.CursorLeft;
            int top = Console.CursorTop;
            int maxLenght = Console.WindowWidth - 1;
            Console.SetCursorPosition(left + current.Length, top);
            string value = current;

            ConsoleKeyInfo key = Console.ReadKey(true);

            while (key.Key != (ConsoleKey)Keys.Submit)
            {
                if (key.Key == (ConsoleKey)Keys.Back && value.Length > 0)
                {
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    Console.Write(" ");
                    Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
                    value = value.Remove(value.Length - 1, 1);
                }
                else if (char.IsLetterOrDigit(key.KeyChar) && value.Length + left < maxLenght)
                {
                    Console.Write(secure ? "*" : key.KeyChar);
                    value += key.KeyChar;
                }

                key = Console.ReadKey(true);
            }

            Console.CursorVisible = false;
            return value;
        }
    }
}
