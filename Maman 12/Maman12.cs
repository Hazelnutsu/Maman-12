using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Maman_12
{
    internal class Maman12
    {
        public static void Main(string[] args)
        {
            string s1 = "Please pick one of the following options:" +
                "\n 1. Build heap" +
                "\n 2. Change d" +
                "\n 3. Extract max" +
                "\n 4. Insert" +
                "\n 5. Print heap" +
                "\n 6. Exit";

            bool flag = true;

            DaryHeap place_holderHeap;

            Console.WriteLine(s1);

            while (flag)
            {
               

                char inputKey = Console.ReadKey().KeyChar;
                

                switch (inputKey)
                {
                    case '1':
                        //build heap function call
                        Console.Clear();
                        Console.WriteLine("\n Enter values for the heap: ");
                        string inputString = Console.ReadLine();
                        Console.WriteLine("Enter the d value: ");
                        int inputD = int.Parse(Console.ReadLine());
                        string[] inputStringArray = inputString.Split(' ');

                        List<int> intArray = new List<int>();
                        foreach(string num in inputStringArray)
                        {
                            int convertedNum = int.Parse(num);
                            intArray.Add(convertedNum);

                        }
                        place_holderHeap = new DaryHeap(inputD, intArray);


                        break;

                    case '2':
                        //change d function call
                        Console.WriteLine("Enter value: ");
                        break;
                    case '3':
                    //extract max function call

                    case '4':
                        //insert node function call
                        Console.WriteLine("Enter value: ");
                        break;
                    case '5':
                        //print heap function call

                    case '6':
                        //exit program
                        flag = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid number");
                        break;

                }
                Console.WriteLine(s1);
            }


        }
         
    }
    
    
}
