using System;

namespace OtusHW2
{
    internal class Game : IGame
    {
        protected readonly int _minNum, _maxNum, _tryCount;
        protected int _currentTry = 1;
        protected int _desiredNum = 0;

        public Game(int minNum = 1, int maxNum = 100, int tryCount = 7)
        {
            _minNum = minNum;
            _maxNum = maxNum;
            _tryCount = tryCount;
        }

        public virtual void StartGame()
        {
            _desiredNum = new Random().Next(_minNum, _maxNum + 1);

            Console.WriteLine($"\nЯ загадал число от {_minNum} до {_maxNum}.");
            Console.WriteLine($"У тебя {_tryCount} попыток.\n");

            Play();
        }

        protected virtual void Play()
        {
            while (_currentTry <= _tryCount)
            {
                int guessNum = GetIntFromUser($"{_currentTry}-я попытка: ");

                if (guessNum == _desiredNum)
                {
                    PrintColoredMessage($"ВЕРНО! Умница! Это {_desiredNum}\nУгадал всего за {_currentTry} попыток.", isSuccess: true);
                    break;
                }
                else
                {
                    HandleWrongGuess(guessNum);
                }
                _currentTry++;
            }
            FinishGame();
        }

        protected virtual void HandleWrongGuess(int guessNum)
        {
            if (_currentTry < _tryCount)
            {
                string message = guessNum > _desiredNum ? GetHigherMessage() : GetLowerMessage();
                PrintColoredMessage(message, isSuccess: false);
            }
            else
            {
                PrintColoredMessage($"Увы! Это было число {_desiredNum}. В следующий раз тебе повезет.", isSuccess: false);
            }
        }

        protected virtual string GetHigherMessage()
        {
            string[] messages = { "Нет, мое число меньше.", "О нет, мое число меньше." };
            return messages[new Random().Next(messages.Length)];
        }

        protected virtual string GetLowerMessage()
        {
            string[] messages = { "Нет, мое число больше.", "О нет, мое число больше." };
            return messages[new Random().Next(messages.Length)];
        }

        protected virtual void FinishGame()
        {
            Console.WriteLine("\nИгра окончена.");
            Console.WriteLine("\n(Пожалуйстапожалуйста поставьте нам 5 звездочек в гугл плей!)\n");
        }

        private static void PrintColoredMessage(string message, bool isSuccess)
        {
            ConsoleColor initialBkgColor = Console.BackgroundColor;
            ConsoleColor initialFrgColor = Console.ForegroundColor;
            if (isSuccess)
            {
                Console.BackgroundColor = ConsoleColor.Green;
                Console.ForegroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.WriteLine("\n" + message + "\n");
            Console.BackgroundColor = initialBkgColor;
            Console.ForegroundColor = initialFrgColor;
        }

        private int GetIntFromUser(string message)
        {
            do
            {
                Console.Write(message);
                string inputString = Console.ReadLine()!;
                if (int.TryParse(inputString, out int result))
                {
                    if (result > _maxNum)
                    {
                        Console.WriteLine($"\tЧисло должно быть меньше или равно {_maxNum}");
                    }
                    else if (result < _minNum)
                    {
                        Console.WriteLine($"\tЧисло должно быть больше или равно {_minNum}");
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    Console.WriteLine("\tЭто даже не число!");
                }
                Console.WriteLine("\tПопробуй еще раз.");
            } while (true);
        }
    }
}
