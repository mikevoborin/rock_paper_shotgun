using System;
using System.Security.Cryptography;

namespace rock_paper_scissors
{
    class Program
    {   static bool ParametersValidation(string[] args)
        {
            if ((args.Length < 3) || (args.Length % 2 == 0))
            {
                Console.WriteLine("Program requre more than 2 parameters. Total number of parameters must be odd number");
                return false;
            }
            for(int i=0;i<args.Length;i++)
                for (int j = 0; j < args.Length; j++)
                {
                    if ((args[i] == args[j]) && (i != j))
                    {
                        Console.WriteLine("Program requre unique parameters.");
                        return false;
                    }
                }
            return true;
        }
        static byte[] GenerateBytes(int number)
        {
            var RNG = RandomNumberGenerator.Create();
            byte[] data = new byte[number];
            RNG.GetBytes(data);
            return data;
        }
        static int GenerateMove(string[] args)
        {
            int move = BitConverter.ToInt32(GenerateBytes(sizeof(int)),0);
            if (move < 0)
            {
                move = GenerateMove(args);
            }
            else move = (
                    (move % args.Length) + 1);
            return move;
        }
        static void PrintHMAC(int move, byte[] key)
        {
            HMACSHA256 hmac = new HMACSHA256(key);
            var hash =hmac.ComputeHash(BitConverter.GetBytes(move));
            string sHash=BitConverter.ToString(hash).Replace("-", "");
            Console.WriteLine("HMAC: {0}",sHash);
        }
        static int InteractingWithUser(string[] args)
        {
            Console.WriteLine("Avalable moves");
            for(int i=0; i< args.Length; i++)
            {
                Console.WriteLine("{0} - {1}",i+1,args[i]);
            }
            Console.WriteLine("0 - exit");
            bool notInt;
            do
            {
                Console.Write("Enter your move: ");
                string input = Console.ReadLine();
                int number;
                notInt = false;
                bool sucesess=Int32.TryParse(input, out number);
                if (!sucesess)
                {
                    Console.WriteLine("Please input number");
                    notInt = true;
                }
                if (number > args.Length || number < 0)
                {
                    Console.WriteLine("Please input number in range 0..{0}",args.Length);
                    notInt = true;
                }
                if (!notInt)
                {
                    if (number != 0)
                    {
                        Console.WriteLine("Your move: {0}", args[number - 1]);
                    }
                    return number;
                }
            }
            while (notInt);
            return -1;
        }
        static void ResultPrinting(int playerMove, int gameMove, int gameParameter)
        {
            if (playerMove != gameMove)
            {
                if (playerMove < gameMove)
                {
                    if ((playerMove + gameParameter) >= gameMove)
                    {
                        Console.WriteLine("You lose.");
                    }
                    else
                    {
                        Console.WriteLine("You Win!");
                    }
                }
                else if ((playerMove - gameParameter) <= gameMove)
                {
                    Console.WriteLine("You Win!");
                }
                else
                {
                    Console.WriteLine("You lose.");                    
                }         
            }
            else Console.WriteLine("It is a draw!");
        }

        static void Main(string[] args)
        {
            if (ParametersValidation(args))
            {
                int move = GenerateMove(args);
                byte[] hmackey = GenerateBytes(sizeof(Int64));
                PrintHMAC(move, hmackey);
                var userMove = InteractingWithUser(args);
                if ( userMove!= 0)
                {
                    Console.WriteLine("Computer move: {0}",args[move-1]);
                    ResultPrinting(userMove, move,((args.Length - 1) / 2));
                    string shmackey = BitConverter.ToString(hmackey);
                    Console.WriteLine("hmackey: {0}", shmackey.Replace("-", ""));
                    Console.ReadKey();
                }
            }
            else
            {
                Console.ReadKey();
            }
        }
    }
}
