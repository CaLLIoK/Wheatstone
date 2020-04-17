using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Square
{
    class Program
    {
        public static void ForRusMethod(char[] text)
        {
            for (int i = 0; i < text.Length; ++i)
            {
                if (text[i] == 'ё')
                {
                    text[i] = 'е';
                }
                else
                {
                    if ((text[i] < 'а' || text[i] > 'я') && (text[i] < 'А' || text[i] > 'Я'))
                    {
                        text[i] = ' ';
                    }
                }
            }
        }

        public static void ForEngMethod(char[] text)
        {
            for (int i = 0; i < text.Length; ++i)
            {
                if (text[i] == 'j')
                {
                    text[i] = 'i';
                }
                else
                {
                    if ((text[i] < 'a' || text[i] > 'z') && (text[i] < 'A' || text[i] > 'Z'))
                    {
                        text[i] = ' ';
                    }
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine(new string('*', 21) + " Шифрование. Двойной квадрат Уинстона " + new string('*', 21));
            Console.WriteLine("\n|Выберите язык, на котором будет осуществляться зашифровка и расшифровка|");
            Console.WriteLine("\n\t\t\tБуква A(ф) - Английский");
            Console.WriteLine("\t\t\tБуква R(к) - Русский \n" + new string('_', 72));
            Console.Write("Ваш выбор: ");
            ConsoleKey letter = Console.ReadKey().Key;
            if (letter == ConsoleKey.A)
            {
                Console.Write("\n1. Encrypt \n2. Decrypt \nYour choice: ");
                int choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Write("\nInput your message: ");
                        string messageEng = Console.ReadLine();

                        if (String.IsNullOrEmpty(messageEng))
                        {
                            Console.WriteLine("You didn't enter the text.");
                            return;
                        }

                        char[] array = messageEng.ToCharArray();
                        ForEngMethod(array);
                        string newString = new string(array);
                        newString = newString.Replace(" ", "");
                        WheatStone wheatStone = new WheatStone();

                        string result_txt = wheatStone.EngEncrypt(newString);
                        Console.WriteLine("\nYour encrypted message: " + result_txt.ToUpper());
                        break;

                    case 2:
                        Console.Write("Input your cipher: ");
                        string message = Console.ReadLine();

                        if (String.IsNullOrEmpty(message))
                        {
                            Console.WriteLine("You didn't enter the text.");
                            return;
                        }

                        char[] arrayDec = message.ToCharArray();
                        ForEngMethod(arrayDec);
                        string str = new string(arrayDec);
                        str = str.Replace(" ", "");
                        WheatStone wheatstone = new WheatStone();

                        string result = wheatstone.EngDecrypt(str);
                        Console.WriteLine("\nYour decrypted message: " + result.ToUpper());
                        break;

                    default:
                        Console.WriteLine("You're mistaken:(");
                        break;
                }
            }
            else
            {
                if (letter == ConsoleKey.R)
                {
                    Console.Write("\n1. Зашифровать \n2. Расшифровать \nВаш выбор: ");
                    int choice = int.Parse(Console.ReadLine());
                    switch (choice)
                    {
                        case 1:
                            Console.Write("\nВведите ваше сообщение: ");
                            string messageRus = Console.ReadLine();

                            if (String.IsNullOrEmpty(messageRus))
                            {
                                Console.WriteLine("Вы не ввели текст.");
                                return;
                            }

                            char[] array = messageRus.ToCharArray();
                            ForRusMethod(array);
                            string newString = new string(array);
                            newString = newString.Replace(" ", "");
                            WheatStone wheatStone = new WheatStone();

                            string result_txt = wheatStone.RusEncrypt(newString);
                            Console.WriteLine("\nВаше зашифрованное сообщение: " + result_txt.ToUpper());
                            break;

                        case 2:
                            Console.Write("Введите ваш шифр: ");
                            string message = Console.ReadLine();

                            if (String.IsNullOrEmpty(message))
                            {
                                Console.WriteLine("Вы не ввели текст.");
                                return;
                            }

                            char[] arrayDec = message.ToCharArray();
                            ForRusMethod(arrayDec);
                            string str = new string(arrayDec);
                            str = str.Replace(" ", "");
                            WheatStone wheatstone = new WheatStone();

                            string result = wheatstone.RusDecrypt(str);
                            Console.WriteLine("\nВаше расшифрованное сообщение: " + result.ToUpper());
                            break;

                        default:
                            Console.WriteLine("Вы ошиблись:(");
                            break;
                    }
                }
            }
        }
    }
}