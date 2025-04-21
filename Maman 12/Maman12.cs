using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading;

namespace Maman_12
{
    internal class Maman12
    {
        /// <summary>
        /// Represents the possible user operations on the heap.
        /// </summary>
        enum Status
        {
            /// <summary>
            /// Build the heap from input.
            /// </summary>
            Build_Heap = '1',

            /// <summary>
            /// Change the value of d in the d-ary heap.
            /// </summary>
            Change_d = '2',

            /// <summary>
            /// Extract the maximum element from the heap.
            /// </summary>
            Extract_Max = '3',

            /// <summary>
            /// Insert a new element into the heap.
            /// </summary>
            Insert = '4',

            /// <summary>
            /// Print the current heap.
            /// </summary>
            Print_Heap = '5',

            /// <summary>
            /// Exit the program.
            /// </summary>
            Exit = '6'
        }

        static Dheap heap; //Our global variable.

        /// <summary>
        /// Main function that starts the program.
        /// </summary>
        public static void Main(string[] args)
        {
            HandleStatus();
        }
        /// <summary>
        /// The function handles the status of the program and the user input.
        /// The function also handles the creation of the heap and the d value.
        /// The function also handles the printing, inserting a value and extracting the maximum value. 
        /// </summary>
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
                        string User_Input = Console.ReadLine();
                        int string_input_length = count_Elements(User_Input);
                        string[] string_Input = new string[string_input_length];
                        int length = remove_Spaces(string_Input, User_Input);
                        int[] input = new int[length];

                        //Change the parameter of the function isValidHeap
                        if (string_input_length == 0 || !isValidHeap(string_Input, input) )
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
                                    value = Console.ReadLine();

                                }
                                x = int.Parse(value);
                                heap.InsertX(x);

                                //Change Console Color
                                PrintColored("The value " + x + " was inserted to the heap successfully.", ConsoleColor.Green);
                                heap.PrintHeap();
                                break;

                            case Status.Print_Heap:
                                //print heap function call
                                heap.PrintHeap();
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
        /// <summary>
        /// The function receives an array of strings and an array of integers. 
        /// The function checks for every given value if its valid, checks that the amount of elements is less than than 1000.
        /// The function also checks if a heap was already created before to validate creation.
        /// Furthermore, the array is being created and the values are being parsed to integers.
        /// </summary>
        /// <returns> true if the user input is valid, false otherwise. </returns>
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
                //Checks for a given value if it is valid using the "is_Valid_Input_Number" function.
                if (!is_Valid_Input_Number(item))
                {
                    return false;
                }
                int number = int.Parse(item);
                input[i] = number;
                i++;
            }
            return true;
        }
        /// <summary>
        /// The function receives a string. 
        /// The function checks if the input string is valid.
        /// </summary>
        /// <returns> true if a string is a whole number and a valid number, false otherwise. </returns>
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



        /// <summary>
        /// The function receives a string.
        /// The functions checks whether it's a whole number.
        /// </summary>
        /// <returns> true if a string is a whole number, false otherwise. </returns>
        public static bool isNumeric(string value)
        {
            // Check for any non-digit characters
            foreach (char c in value)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            // Try parsing as integer (to handle things like int overflow)
            return int.TryParse(value, out _);
        }


        /// <summary>
        /// The function receives a string.
        /// The function counts the amount of elements in the string (which are seperated by spaces).
        /// The function handles invalid input and checks if the input is a number.
        /// </summary>
        /// <returns> The amount of numbers the user_input contains</returns>
        public static int count_Elements(string User_Input)
        {
            int i = 0;
            int j = 0;
            while (i < User_Input.Length)
            {

                if (i < User_Input.Length && User_Input[i] != ' ')
                {
                    while (i < User_Input.Length && User_Input[i] != ' ')
                    {
                        if (User_Input[i] == '.')
                        {
                            PrintColored("[ERROR]: You have to enter round numbers only. Please try again.", ConsoleColor.Red);
                            return 0;
                        }
                        i++;
                    }
                    j++;
                }
                i++;
            }
            return j;
        }


        /// <summary>
        /// The function receives a string array and a string.
        /// The function removes the spaces from the string and returns the amount of elements in the string.
        /// The function handles invalid input and checks if the input is a number.
        /// </summary>
        /// <returns> The length of the heap array. </returns>
        public static int remove_Spaces(string[] string_Input, string User_Input)
        {
            int i = 0;
            int j = 0;
            while (i < User_Input.Length)
            {
                string str = "";
                while (i < User_Input.Length && User_Input[i] != ' ')
                {
                    //A stopping condition for a string with a double value. 
                    if (User_Input[i] == '.')
                    {
                        return 0;
                    }
                    str += User_Input[i];
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

        /// <summary>
        /// The function creates the d value.
        /// The function handles invalid input and checks if the input is a number.
        /// </summary>
        /// <returns> The new d value </returns>

        public static int create_D()
        {
            int d = 0;
            while (d <= 0)
            {

                PrintColored("\nPlease enter the d value:", ConsoleColor.Blue);
                string d_string = Console.ReadLine();

                if (!isNumeric(d_string) || int.Parse(d_string) <= 0)
                {
                    PrintColored("[ERROR]: The D value has to be a round number greater than 0", ConsoleColor.Red);
                    continue;
                }
                d = int.Parse(d_string);
                Console.WriteLine();
            }
            return d;
        }


        /// <summary>
        /// The function receives a string and a color, then prints the string in the given color. 
        /// </summary>
        public static void PrintColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }


}