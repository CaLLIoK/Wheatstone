using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Square
{
    class WheatStone
    {
        private class Cortege
        {
            public int FIndex { get; set; }
            public int SIndex { get; set; }
            public Cortege() { }
        }

        private char[,] FillRusMatrix(char[,] matrix, string key)
        {
            char[] keyArray = key.ToCharArray();
            Program.ForRusMethod(keyArray);
            string changedKey = new string(keyArray);
            changedKey = changedKey.Replace(" ", "");
            changedKey = new String(changedKey.Distinct().ToArray());

            for (int index = 0; index < changedKey.Length; index++)
            {
                if (changedKey[index] != 'ё')
                {
                    matrix[index / matrix.GetLength(1), index % matrix.GetLength(1)] = changedKey[index];
                }
                else
                {
                    matrix[index / matrix.GetLength(1), index % matrix.GetLength(1)] = 'е';
                }

            }
            changedKey = changedKey.ToLower();
            char[] keyLikeCharArray = changedKey.ToCharArray();
            int size = changedKey.Length;
            for (char symbol = 'а'; symbol <= 'я'; ++symbol)
            {
                if (symbol == 'ё')
                {
                    ++symbol;
                }

                if (Array.FindIndex(keyLikeCharArray, (a) => a == symbol) == -1)
                {
                    matrix[size / matrix.GetLength(1), size % matrix.GetLength(1)] = symbol;
                    ++size;
                }
            }
            return matrix;
        }

        private char[,] FillEngMatrix(char[,] matrix, string key)
        {
            char[] keyArray = key.ToCharArray();
            Program.ForEngMethod(keyArray);
            string changedKey = new string(keyArray);
            changedKey = changedKey.Replace(" ", "");
            changedKey = new String(changedKey.Distinct().ToArray());

            for (int index = 0; index < changedKey.Length; index++)
            {
                if (changedKey[index] != 'j')
                {
                    matrix[index / matrix.GetLength(1), index % matrix.GetLength(1)] = changedKey[index];
                }
                else
                {
                    matrix[index / matrix.GetLength(1), index % matrix.GetLength(1)] = 'i';
                }

            }
            changedKey = changedKey.ToLower();
            char[] keyCharArray = changedKey.ToCharArray();
            int size = changedKey.Length;
            for (char symbol = 'a'; symbol <= 'z'; ++symbol)
            {
                if (symbol == 'j')
                {
                    ++symbol;
                }

                if (Array.FindIndex(keyCharArray, (a) => a == symbol) == -1)
                {
                    matrix[size / matrix.GetLength(1), size % matrix.GetLength(1)] = symbol;
                    ++size;
                }
            }
            return matrix;
        }

        public string RusEncrypt(string text)
        {
            text = text.ToLower();

            string result = "";

            if (text.Length % 2 != 0)
            {
                text += 'я';
            }
            int size = text.Length / 2;
            int position = 0;
            char[,] bigram = new char[size, 2];

            for (int firstIndex = 0; firstIndex < size; firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < 2; secondIndex++)
                {
                    bigram[firstIndex, secondIndex] = text[position];
                    position++;
                }
            }

            Console.Write("Введите левый ключ: ");
            string leftKey = Convert.ToString(Console.ReadLine());
            Console.Write("Введите правый ключ: ");
            string rightKey = Convert.ToString(Console.ReadLine());
            char[,] firstMatrix = new char[8, 4];
            firstMatrix = FillRusMatrix(firstMatrix, leftKey);

            Console.WriteLine("\n\t_____Первая  матрица_____");
            for (int firstIndex = 0; firstIndex < firstMatrix.GetLength(0); firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < firstMatrix.GetLength(1); secondIndex++)
                {
                    Console.Write("\t" + firstMatrix[firstIndex,secondIndex]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\t_________________________");

            char[,] secondMatrix = new char[8, 4];
            secondMatrix = FillRusMatrix(secondMatrix, rightKey);
            Console.WriteLine("\n\t_____Вторая  матрица_____");
            for (int firstIndex = 0; firstIndex < secondMatrix.GetLength(0); firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < secondMatrix.GetLength(1); secondIndex++)
                {
                    Console.Write("\t" + secondMatrix[firstIndex, secondIndex]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\t_________________________");

            char[,] cryptBigram = new char[size, 2];
            int step = 0;
            while (step < size)
            {
                Cortege cortege1 = FindIndex(bigram[step, 0], firstMatrix);
                Cortege cortege2 = FindIndex(bigram[step, 1], secondMatrix);

                cryptBigram[step, 0] = secondMatrix[cortege1.FIndex, cortege2.SIndex];
                cryptBigram[step, 1] = firstMatrix[cortege2.FIndex, cortege1.SIndex];

                step++;
            }

            for (int firstIndex = 0; firstIndex < size; firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < 2; secondIndex++)
                {
                    result += cryptBigram[firstIndex, secondIndex].ToString();
                }
                result += " ";
            }
            return result;
        }

        public string RusDecrypt(string text)
        {
            string result = "";

            text = text.ToLower();
            int length = text.Length / 2;
            int position = 0;

            char[,] bigram = new char[length, 2];
            char[,] cryptBigram = new char[length, 2];

            for (int firstIndex = 0; firstIndex < length; firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < 2; secondIndex++)
                {
                    bigram[firstIndex, secondIndex] = text[position];
                    position++;
                }
            }

            Console.Write("Введите левый ключ: ");
            string leftKey = Convert.ToString(Console.ReadLine());
            Console.Write("Введите правый ключ: ");
            string rightKey = Convert.ToString(Console.ReadLine());
            char[,] firstMatrix = new char[8, 4];
            firstMatrix = FillRusMatrix(firstMatrix, leftKey);

            Console.WriteLine("\t_____Первая  матрица_____");
            for (int firstIndex = 0; firstIndex < firstMatrix.GetLength(0); firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < firstMatrix.GetLength(1); secondIndex++)
                {
                    Console.Write("\t" + firstMatrix[firstIndex, secondIndex]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\t_________________________");

            char[,] secondMatrix = new char[8, 4];
            secondMatrix = FillRusMatrix(secondMatrix, rightKey);
            Console.WriteLine("\n\t_____Вторая  матрица_____");
            for (int firstIndex = 0; firstIndex < secondMatrix.GetLength(0); firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < secondMatrix.GetLength(1); secondIndex++)
                {
                    Console.Write("\t" + secondMatrix[firstIndex, secondIndex]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\t_________________________");

            int step = 0;
            while (step < length)
            {
                Cortege cortege1 = FindIndex(bigram[step, 0], secondMatrix);
                Cortege cortege2 = FindIndex(bigram[step, 1], firstMatrix);

                cryptBigram[step, 0] = firstMatrix[cortege1.FIndex, cortege2.SIndex];
                cryptBigram[step, 1] = secondMatrix[cortege2.FIndex, cortege1.SIndex];

                step++;
            }

            for (int firstIndex = 0; firstIndex < length; firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < 2; secondIndex++)
                {
                    result += cryptBigram[firstIndex, secondIndex].ToString();
                }
            }
            return result;
        }

        public string EngEncrypt(string text)
        {
            text = text.ToLower();

            string result = "";

            if (text.Length % 2 != 0)
            {
                text += 'z';
            }
            int length = text.Length / 2;
            int position = 0;
            char[,] bigram = new char[length, 2];
            char[,] cryptBigram = new char[length, 2];

            for (int firstIndex = 0; firstIndex < length; firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < 2; secondIndex++)
                {
                    bigram[firstIndex, secondIndex] = text[position];
                    position++;
                }
            }

            Console.Write("Input left key: ");
            string leftKey = Convert.ToString(Console.ReadLine());
            Console.Write("Input right key: ");
            string rightKey = Convert.ToString(Console.ReadLine());
            char[,] firstMatrix = new char[5, 5];
            firstMatrix = FillEngMatrix(firstMatrix, leftKey);

            Console.WriteLine("\t__________First  Matrix__________");
            for (int firstIndex = 0; firstIndex < firstMatrix.GetLength(0); firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < firstMatrix.GetLength(1); secondIndex++)
                {
                    Console.Write("\t" + firstMatrix[firstIndex, secondIndex]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\t_________________________________");

            char[,] secondMatrix = new char[5, 5];
            secondMatrix = FillEngMatrix(secondMatrix, rightKey);
            Console.WriteLine("\n\t__________Second  Matrix_________");
            for (int firstIndex = 0; firstIndex < secondMatrix.GetLength(0); firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < secondMatrix.GetLength(1); secondIndex++)
                {
                    Console.Write("\t" + secondMatrix[firstIndex, secondIndex]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\t_________________________________");

            int step = 0;
            while (step < length)
            {
                Cortege cortege1 = FindIndex(bigram[step, 0], firstMatrix);
                Cortege cortege2 = FindIndex(bigram[step, 1], secondMatrix);

                cryptBigram[step, 0] = secondMatrix[cortege1.FIndex, cortege2.SIndex];
                cryptBigram[step, 1] = firstMatrix[cortege2.FIndex, cortege1.SIndex];

                step++;
            }

            for (int firstIndex = 0; firstIndex < length; firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < 2; secondIndex++)
                {
                    result += cryptBigram[firstIndex, secondIndex].ToString();
                }
                result += " ";
            }
            return result;
        }

        public string EngDecrypt(string text)
        {
            string result = "";

            text = text.ToLower();
            int length = text.Length / 2;
            int position = 0;

            char[,] bigram = new char[length, 2];
            char[,] cryptBigram = new char[length, 2];

            for (int firstIndex = 0; firstIndex < length; firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < 2; secondIndex++)
                {
                    bigram[firstIndex, secondIndex] = text[position];
                    position++;
                }
            }

            Console.Write("Input left key: ");
            string leftKey = Convert.ToString(Console.ReadLine());
            Console.Write("Input right key: ");
            string rightKey = Convert.ToString(Console.ReadLine());
            char[,] firstMatrix = new char[5, 5];
            firstMatrix = FillEngMatrix(firstMatrix, leftKey);

            Console.WriteLine("\t__________First  Matrix__________");
            for (int firstIndex = 0; firstIndex < firstMatrix.GetLength(0); firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < firstMatrix.GetLength(1); secondIndex++)
                {
                    Console.Write("\t" + firstMatrix[firstIndex, secondIndex]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\t_________________________________");

            char[,] second_matrix = new char[5, 5];
            second_matrix = FillEngMatrix(second_matrix, rightKey);
            Console.WriteLine("\n\t__________Second  Matrix__________");
            for (int firstIndex = 0; firstIndex < second_matrix.GetLength(0); firstIndex++)
            {
                for (int seconIndex = 0; seconIndex < second_matrix.GetLength(1); seconIndex++)
                {
                    Console.Write("\t" + second_matrix[firstIndex, seconIndex]);
                }
                Console.WriteLine();
            }
            Console.WriteLine("\t_________________________________");

            int step = 0;
            while (step < length)
            {
                Cortege cortege1 = FindIndex(bigram[step, 0], second_matrix);
                Cortege cortege2 = FindIndex(bigram[step, 1], firstMatrix);

                cryptBigram[step, 0] = firstMatrix[cortege1.FIndex, cortege2.SIndex];
                cryptBigram[step, 1] = second_matrix[cortege2.FIndex, cortege1.SIndex];

                step++;
            }

            for (int firstIndex = 0; firstIndex < length; firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < 2; secondIndex++)
                {
                    result += cryptBigram[firstIndex, secondIndex].ToString();
                }
            }
            return result;
        }

        private Cortege FindIndex(char symbol, char[,] matrix)
        {
            Cortege cortege = new Cortege();

            for (int firstIndex = 0; firstIndex < matrix.GetLength(0); firstIndex++)
            {
                for (int secondIndex = 0; secondIndex < matrix.GetLength(1); secondIndex++)
                {
                    if (symbol == matrix[firstIndex, secondIndex])
                    {
                        cortege.FIndex = firstIndex;
                        cortege.SIndex = secondIndex;
                        return cortege;
                    }
                }
            }

            return null;
        }
    }
}
