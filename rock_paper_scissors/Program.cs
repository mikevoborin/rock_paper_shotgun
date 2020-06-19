using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        static int GenerateMove()
        {
            //    SecureRandom.ll
            return 0;
        }
        static void ResultPrinting(int playerMove, int gameMove, int gameParameter)
        {
            if (playerMove != gameMove)
            {
                if (playerMove < gameMove)
                {
                    if (playerMove + gameParameter > gameMove)
                    {
                        Console.WriteLine("Player Wins!");
                    }
                    else Console.WriteLine("Game Wins!");
                }
                else if (gameMove + gameParameter > playerMove)
                     {
                         Console.WriteLine("Game Wins!");
                     }
                     else Console.WriteLine("Player Wins!");         
            }
            else Console.WriteLine("It is a draw!");
        }

        static void Main(string[] args)
        {
            if (ParametersValidation(args))
            {
                int move=GenerateMove();
                //if userinput()!=0;
                //ResultPrinting(playerMove,gamemove,((agvs.Length+1)%2));

            }
            else Console.ReadKey();
        }
    }
}
