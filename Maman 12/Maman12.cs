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
        enum Status
        {
            Build_Heap = '1',
            Change_d = '2',
            Extract_Max = '3',
            Insert = '4',
            Print_Heap = '5',
            Exit = '6'
        }
        static Dheap heap; //Our global variable.
        public static void Main(string[] args)
        {

            HandleStatus();
        }

        public static void HandleStatus()
        {
            bool flag = true;
            string s1 = "Please pick one of the following options:" +
                "\n 1. Build heap" +
                "\n 2. Change d" +
                "\n 3. Extract max" +
                "\n 4. Insert" +
                "\n 5. Print heap" +
                "\n 6. Exit";

            Console.WriteLine(s1);
            while (flag)
            {
                char inputKey = Console.ReadKey().KeyChar;
                Console.WriteLine("\n");
                Status status = (Status)inputKey;

                switch (status)
                {
                    case Status.Build_Heap:
                        //build heap function call
                        PrintColored("Please write your heap array values(Seperated by a single space): ", ConsoleColor.Blue);
                        Console.WriteLine();
                        string array = Console.ReadLine();
                        int string_input_length = count_Elements(array);
                        string[] string_Input = new string[string_input_length];
                        int length = remove_Spaces(string_Input, array);
                        int[] input = new int[length];

                        //Change the parameter of the function isValidHeap
                        if (!isValidHeap(string_Input, input))
                        {
                            break;
                        }
                        int d = create_D();
                        Maman12.heap = new Dheap(input, d);

                        PrintColored("The heap has been built", ConsoleColor.Green);

                        heap.PrintHeap();


                        break;

                    case Status.Change_d:
                    case Status.Extract_Max:
                    case Status.Insert:
                    case Status.Print_Heap:

                        if (Dheap.isEmpty())
                        {
                            PrintColored("[ERROR]: You have to create an heap first", ConsoleColor.Red);


                            Thread.Sleep(2000);

                            break;
                        }

                        switch (status)
                        {

                            case Status.Change_d:
                                //change d function call
                                int newD = create_D();
                                Maman12.heap.Change_d(newD);
                                //Change Console Color.
                                PrintColored("The d value was changed to: " + newD, ConsoleColor.Green);

                                heap.PrintHeap();

                                break;

                            case Status.Extract_Max:
                                //extract max function call
                                if (heap.getLength() == 0)
                                {
                                    PrintColored("[ERROR]: The heap is empty. ", ConsoleColor.Red);
                                    break;
                                }
                                int max = heap.ExtractMax();
                                PrintColored("The max value is: " + max, ConsoleColor.Green);
                                heap.PrintHeap();
                                break;

                            case Status.Insert:
                                //insert node function call
                                PrintColored("Enter value: ", ConsoleColor.Blue);
                                int x = 0;
                                string value = Console.ReadLine();
                                while (!is_Valid_Input_Number(value))
                                {
                                    Console.WriteLine();
                                    PrintColored("Enter value: ", ConsoleColor.Blue);

                                }
                                x = int.Parse(value);
                                heap.InsertX(x);

                                //Change Console Color
                                PrintColored("The value " + x + " was inserted to the heap successfully.", ConsoleColor.Green);
                                heap.PrintHeap();
                                break;

                            case Status.Print_Heap:
                                //print heap function call
                                string s2 = "Choose from the following options:" +
                                    "\n 1. Print heap as a triangle" +
                                    "\n 2. Print heap as a tree";
                                Console.WriteLine(s2);
                                char inputKey2 = Console.ReadKey().KeyChar;
                                Status status2 = (Status)inputKey2;
                                Console.WriteLine("\n");
                                switch (status2)
                                {
                                    case Status.Build_Heap:

                                        heap.PrintHeap();
                                        break;
                                    case Status.Change_d:

                                        heap.PrintTree();
                                        break;
                                    default:
                                        PrintColored("[ERROR]: Please enter a valid number", ConsoleColor.Red);
                                        break;
                                }
                                break;
                        }
                        break;

                    case Status.Exit:
                        //exit program
                        flag = false;
                        break;

                    default:
                        PrintColored("[ERROR]: Please enter a valid number", ConsoleColor.Red);
                        break;


                }
                Console.WriteLine("\n" + s1);
            }
        }
        public static bool isValidHeap(string[] string_Input, int[] input)
        {
            if (!Dheap.isEmpty())
            {
                PrintColored("[ERROR]: You have already created an heap", ConsoleColor.Red);
                return false;
            }
            if (string_Input.Length > 1000)
            {
                PrintColored("[ERROR]: You exceeded the amount of elements an heap can store (1000). Please try again.", ConsoleColor.Red);
                return false;
            }
            int i = 0;
            foreach (string item in string_Input)
            {
                if (!isNumeric(item))
                {
                    PrintColored("[ERROR]: You have to enter numbers only. Please try again.", ConsoleColor.Red);
                    return false;
                }
                int number = int.Parse(item);

                if (number > 9999 || number < -9999)
                {
                    PrintColored("[ERROR]: You exceeded the maximum value of an element (9999). Please try again.", ConsoleColor.Red);
                    return false;
                }
                input[i] = number;
                i++;
            }
            return true;
        }
        public static bool is_Valid_Input_Number(string num)
        {
            if (!isNumeric(num))
            {
                PrintColored("[ERROR]: You have to enter numbers only. Please try again.", ConsoleColor.Red);
                return false;
            }
            int number = int.Parse(num);
            if (number > 9999 || number < -9999)
            {
                PrintColored("[ERROR]: You exceeded the maximum value of an element (9999). Please try again.", ConsoleColor.Red);
                return false;
            }
            return true;
        }
        public static bool isNumeric(string value)
        {
            //Check this. Specificly "out _"
            return double.TryParse(value, out _);
        }
        public static int count_Elements(string array)
        {
            int i = 0;
            int j = 0;
            while (i < array.Length)
            {
                if (i < array.Length && array[i] != ' ')
                {
                    while (i < array.Length && array[i] != ' ')
                    {
                        i++;
                    }
                    j++;
                }
                i++;
            }
            return j;
        }
        public static int remove_Spaces(string[] string_Input, string array)
        {
            int i = 0;
            int j = 0;
            while (i < array.Length)
            {
                string str = "";
                while (i < array.Length && array[i] != ' ')
                {
                    str += array[i];
                    i++;
                }
                if (str != "")
                {
                    string_Input[j] = str;
                    j++;
                }
                i++;
            }
            return j;
        }

        public static int create_D()
        {
            int d = 0;
            while (d <= 0)
            {

                PrintColored("\nPlease enter the d value:", ConsoleColor.Blue);
                string d_string = Console.ReadLine();
                if (!isNumeric(d_string) || int.Parse(d_string) <= 0)
                {
                    PrintColored("[ERROR]: The D value has to be a number greater than 0", ConsoleColor.Red);
                    continue;
                }
                d = int.Parse(d_string);
                Console.WriteLine();
            }
            return d;
        }
        public static void PrintColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }


}