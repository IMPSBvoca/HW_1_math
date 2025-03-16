using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HM1_1_math_
{

    class Program
    {

        //Реализовал в соответствии с требованием задания(можно было бы и без них, как мне кажется, просто обращаться к фактическому значению метода, без enum, но, м.б. я не очень прав)
        enum MathOperation
        {
            StandardDeviation = 1,
            Variance,
            Median,
            Mode
        }
        //Надеюсь нет логических ошибок в написании методов
        //Решил вынести рассчёт среднего арифметического, чтобы не дублировать код
        public static double Mean(double[] userNumberArray)
        {
            double summ = 0;
            foreach(double item in userNumberArray)
            {
                summ += item;
            }
            return (summ / userNumberArray.Length);
        }
        public static double StandardDeviation(double[] userNumberArray)
        {
            return Math.Sqrt(Variance(userNumberArray));
        }
        public static double Variance(double[] userNumberArray) 
        {
            double mean = Mean(userNumberArray);
            double summVariance = 0;
            foreach(double item in userNumberArray)
            {
                summVariance += (item - mean)*(item - mean);
            }
            return (summVariance / userNumberArray.Length);
        }
        public static double Median(double[] userNumberArray)
        {
            Array.Sort(userNumberArray);
            //Т.е. я говорю, что если у меня нечётное кол-во элементов в массиве userNumberArray - верни центральное значение (Length / 2)
            if (userNumberArray.Length % 2 != 0)
            {
                return userNumberArray[userNumberArray.Length / 2];
            }
            //А если чётное - верни среднее 2 элементов: (центр - 1) и центр. Вроде для всех случаев это будет корректно. Если нет - обсудим потом.
            else
            {
                return (userNumberArray[(userNumberArray.Length / 2) - 1] + userNumberArray[userNumberArray.Length/2] / 2);
            }
                
        }
        public static double Mode(double[] userNumbersArray)
        {
            /*
            (Так, тут я немного запутался, но вышло следующее: 
            1) Мы сортируем наш массив
            2) создаем переменную, в которую помещаем первое число массива (то, которое и будем проверять)
            3) после этого создаем каунтера для текущего значения и для максимального значения
            4) создаем переменную моды, в которую присваивается текущее число (после прохождения проверки)
            */

            Array.Sort(userNumbersArray);
            double currentNumber = userNumbersArray[0];
            int currentCount = 1;
            int maxCount = 1;
            double mode = currentNumber;

            //С int = 0 нет смысла начинать, мы при объявлении уже учли элемент[0], он в currentNumber и его счётчик в currentCount, поэтому стартуем со 2 элемента
            for (int i = 1; i < userNumbersArray.Length; i++)
            {
                //Если наш 1 элемент == 0 элементу, то накидываем в каунтер
                if (userNumbersArray[i] == currentNumber)
                {
                    currentCount++;
                }
                //Если же false, тогда проверяем значения каунтеров
                else
                {
                    // Если наш текущий каунтер прошёл несколько раз if из контекста выше - обновляем maxКаунтер, а в моду присваиваем значение, которое сейчас проверяли (если его каунтер выше того, что был ранее)
                    if (currentCount > maxCount)
                    {
                        maxCount = currentCount;
                        mode = currentNumber;
                    }
                    // После этого в любом случае переходим к следующему числу, обновляя текущее значение и каунтер
                    currentNumber = userNumbersArray[i];
                    currentCount = 1;
                }
            }

            // Выйдя из цикла проверяем наши каунтеры и, в зависимости от полученного результата, возвращаем моду
            if(currentCount > maxCount)
            {
                mode = currentCount;
            }
            // Пропишем условие при котором наш массив не имеет моды, поскольку каунтер не поднимался выше 1 (иначе в выводе всегда будет currentCount)
            if(maxCount == 1)
            {
                mode = 0;
            }

            return mode;
        }

        static void Main(string[] args)
        {
            string userInput;
            //Просим пользователя ввести нужные значения
            //Вкину валидацию на ввод, чтобы была (а то сам несколько раз без значений запускал программу и получал ошибки...
            //Я понимаю, что можно ещё валидацию на ввод других символов добавить, но это уже очень далеко за пределами ТЗ (надеюсь).
            do
            {
                Console.Write("Введите любое количество чисел через через запятую: ");
                userInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(userInput))
                {
                    Console.WriteLine("Для выполнения программы необходимо ввести хотя бы 1 значение. Попробуйте ещё раз!");
                }
            } while (string.IsNullOrWhiteSpace(userInput));
            
            //Преобразовываем ввод в массив string'ов
            string[] userInputArray = userInput.Split(',');
            //Преобразуем массив string'ов в double (чтобы при выводе чисел с плавающей запятой не съедалась точка).
            double[] userNumbersArray = new double[userInputArray.Length];
            //Цикл для присвоения значений в созданный массив (скобки оставляю, мне так проще)
            for(int i = 0; i < userInputArray.Length; i++)
            {
                userNumbersArray[i] = double.Parse(userInputArray[i]);
            }
            //Далее просим пользователя выбрать операцию, согласно нашему Enum
            Console.Write("Выберите операцию: 1 - Среднеквадратичное отклоение, 2 - Дисперсия, 3 - Медиана, 4 - Мода : ");
            int userChoice = int.Parse(Console.ReadLine());
            //Далее используем обращаемся к Enum через условный оператор (решил цикл while приклеить, чтобы каждый раз не начинать сначала)
            switch ((MathOperation)userChoice)
            {
                case MathOperation.StandardDeviation:
                    if (userNumbersArray.Length == 1)
                    {
                        Console.WriteLine($"Было передано лишь {userNumbersArray.Length} значение, в связи с чем для него невозможно вычислить среднеквадратичное отклонение!");
                    }
                    else
                    {
                        Console.WriteLine($"Среднеквадратичное отклоенние указанного списка значений = {StandardDeviation(userNumbersArray)}");
                    }
                    break;
                case MathOperation.Variance:
                    if (userNumbersArray.Length == 1)
                    {
                        Console.WriteLine($"Было передано лишь {userNumbersArray.Length} значение, в связи с чем для него невозможно вычислить дисперсию отклонение!");
                    }
                    else
                    {
                        Console.WriteLine($"Дисперсия указанного списка значений = {Variance(userNumbersArray)}");
                    }
                    break;
                case MathOperation.Median:
                    Console.WriteLine($"Медиана указанного списка значения = {Median(userNumbersArray)}");
                    break;
                case MathOperation.Mode:
                    if(Mode(userNumbersArray) == 0)
                    {
                        Console.WriteLine("Все значения уникальны, для данного набора чисел невозможно найти моду!");
                    }
                    else
                    {
                        Console.WriteLine($"Мода указанного списка значения = {Mode(userNumbersArray)}");
                    }
                    break;
                default:
                    Console.WriteLine("Данного метода нет");
                    break;
            }
            Console.ReadLine();
        }
    }
}
