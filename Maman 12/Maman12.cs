using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;




namespace Maman_12
{
    internal class Maman12
    {
        static DaryHeap heap; //Our global variable.
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

            

            Console.WriteLine(s1);

            while (flag)
            {
               

                char inputKey = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                

                switch (inputKey)
                {
                    case '1':
                        //build heap function call
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("Please write your heap array values(Spaces seperate the numbers): ");
                        Console.ResetColor();
                        Console.WriteLine();
                        string array = Console.ReadLine();
                        string[] string_Input = array.Split(' ');
                        List<int> input = new List<int>();

                        if (!isValid(string_Input, input)){
                            break;
                        }
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("\nPlease enter the d value: ");
                        Console.ResetColor();
                        int d = int.Parse(Console.ReadLine());
                        Console.WriteLine();

                        Maman12.heap = new DaryHeap(input, d);
                        
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("The heap has been built");
                        Console.ResetColor();

                        heap.PrintHeap();


                        break;
                    
                    case '2':
                    case '3':
                    case '4':
                    case '5':

                        if (DaryHeap.isEmpty())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("You have to create a heap first");
                            Console.ResetColor();

                            Thread.Sleep(2000);

                            break;
                        }

                        switch (inputKey) {

                            case '2':
                                //change d function call
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("Please enter the new d value: ");
                                Console.ResetColor();
                                int newD = int.Parse(Console.ReadLine());
                                Maman12.heap.Change_d(newD);
                                //Change Console Color.
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("The d value was changed to: " + newD);
                                Console.ResetColor();
                                heap.PrintHeap();

                                break;

                            case '3':
                                //extract max function call
                                int max = heap.ExtractMax();
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("The max value is: " + max);
                                Console.ResetColor();
                                heap.PrintHeap();
                                break;

                            case '4':
                                //insert node function call
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("Enter value: ");
                                Console.ResetColor();
                                int x = int.Parse(Console.ReadLine());
                                heap.InsertX(x);

                                //Change Console Color
                                Console.ForegroundColor = ConsoleColor.Green;
                                Console.WriteLine("The value " + x + " was inserted to the heap");
                                Console.ResetColor();
                                heap.PrintHeap();
                                break;

                            case '5':
                                //print heap function call
                                string s2 = "Choose from the following options:" +
                                    "\n 1. Print heap as a triangle" +
                                    "\n 2. Print heap as a tree";
                                Console.WriteLine(s2);
                                char inputKey2 = Console.ReadKey().KeyChar;
                                Console.WriteLine("\n");
                                switch (inputKey2)
                                {
                                    case '1':
                                        
                                        heap.PrintHeap();
                                        break;
                                    case '2':

                                        heap.PrintTree();
                                        break;
                                    default:
                                        Console.WriteLine("Please enter a valid number");
                                        break;
                                }
                                break;
                            }
                        break;

                    case '6':
                        //exit program
                        flag = false;
                        break;

                    default:
                        Console.WriteLine("Please enter a valid number");
                        break;
                    

                }
                Console.WriteLine("\n" + s1);
            }


        }
        public static bool isValid(string[] string_Input, List<int> input)
        {
            if (!DaryHeap.isEmpty())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You have already created an heap");
                Console.ResetColor();
                return false;
            }
            if (string_Input.Length > 1000)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("You exceeded the amount of elements an heap can store(1000). Please try again.");
                Console.ResetColor();
                return false;
            }
            foreach (string item in string_Input)
            {
                if (!isNumeric(item))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You have to enter numbers only. Please try again.");
                    Console.ResetColor();
                    input.Clear();
                    return false;
                }
                int number = int.Parse(item);

                if (number > 9999)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("You exceeded the maximum value of an element(9999). Please try again.");
                    Console.ResetColor();
                    input.Clear();
                    return false;
                }
                input.Add(number);
            }
            return true;
        }
        public static bool isNumeric(string value)
        {
            //Check this. Specificly "out _"
            return double.TryParse(value, out _);
        }
         
    }
    
    
}
