using System;

namespace Runtime
{
    public class Player
    {
        public static void Main()
        {
            Wallet.Add(1000);
            Wallet.Remove(10);
            Console.WriteLine();
            Console.ReadLine();
            Console.BackgroundColor = ConsoleColor.Red;
            
        }
    }

    public static class Wallet
    {
        public static int Money { get; private set; }
        
        public static void Add(int money)
        {
            Money += money;
        }

        public static void Remove(int money)
        {
            Money -= money;
        }

        public static bool CanRemoveMoney(int money)
        {
            return Money - money >= 0;
        }
    }
    
    class Score
    {
        public static int Value { get; set; }

        public static int SolveDiscriminant()
        {
            return 4 + 5;
        }
        
        public static void Add(string result, int countOfResults)
        {
            bool b = true;
            b = false;

            bool needWin = Value == 20;
            Value++;
            Console.WriteLine(Value);

            if (needWin)
                Console.WriteLine("You win");

            for (int i = 0; i < countOfResults; i++)
            {
                Console.WriteLine(result);
            }
        }
    }
}