using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.RegularExpressions;
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
            buildHeap = '1',

            /// <summary>
            /// Change the value of d in the d-ary heap.
            /// </summary>
            ChangeD = '2',

            /// <summary>
            /// Extract the maximum element from the heap.
            /// </summary>
            extractMax = '3',

            /// <summary>
            /// insert a new element into the heap.
            /// </summary>
            insert = '4',

            /// <summary>
            /// Print the current heap.
            /// </summary>
            printHeap = '5',

            /// <summary>
            /// exit the program.
            /// </summary>
            exit = '6'
        }

        static Dheap heap; 
        //pattern for any signed numeric input and white spaces
        static readonly Regex inputPattern = new Regex(@"^\s*[+-]?\d+(?:\s+[+-]?\d+)*\s*$");
        /// <summary>
        /// Main function that starts the program.
        /// </summary>
        public static void Main(string[] args)
        {
            HandleStatus();
        }


       

        /// <summary>
        /// This function:
        /// handles the user input. 
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
                    case Status.buildHeap:
                        
                        PrintColored("Please write your heap values as integers (Seperated by a space): \n", ConsoleColor.Blue);                        
                        string userInput = Console.ReadLine();
                        if (!inputPattern.IsMatch(userInput))
                        {
                            PrintColored("[ERROR]: You have to enter integers only. Please try again.", ConsoleColor.Red);
                            break;
                        }
                        //pattern for replacing any white space with 1 single white space
                        userInput = Regex.Replace(userInput, @"\s+", " ").Trim();
                        string[] userInputArray = userInput.Split(' ');
                        List<int> input = new List<int>();
                        foreach (string num in userInputArray)
                        {
                            input.Add(int.Parse(num));
                        }

                        if (input.Count == 0 || input.Count > 1000)
                        {
                            PrintColored("[ERROR] input has to be more than 0 and less than 1000 elements", ConsoleColor.Red);
                            break;
                        }

                        int d;
                        bool dValid = false;
                        while (!dValid)
                        {
                            PrintColored("Please enter the d value: ", ConsoleColor.Blue);
                            string dInput = Console.ReadLine();
                            if (int.TryParse(dInput, out d))
                            {
                                if (d <= 0)
                                {
                                    PrintColored("[ERROR] d value has to be a positive integer", ConsoleColor.Red);
                                    continue;
                                }
                                dValid = true;
                                break;
                            }
                            else
                            {
                                PrintColored("[ERROR] d value has to be a positive integer", ConsoleColor.Red);                                
                            }
                        }
                        
                        
                        
                        
                            //int d = int.TryParse()
                            //Maman12.heap = new Dheap(input, d);

                            PrintColored("The heap has been built", ConsoleColor.Green);

                       // heap.PrintHeap();


                        break;

                    case Status.ChangeD:
                    case Status.extractMax:
                    case Status.insert:
                    case Status.printHeap:

                        if (Dheap.IsEmpty())
                        {
                            PrintColored("[ERROR]: You have to create an heap first", ConsoleColor.Red);
                            Thread.Sleep(2000);
                            break;
                        }

                        switch (status)
                        {

                            case Status.ChangeD:                                
                                int newD = CreateD();
                                Maman12.heap.ChangeD(newD);                                
                                PrintColored("The d value was ChangeD to: " + newD, ConsoleColor.Green);
                                heap.PrintHeap();

                                break;

                            case Status.extractMax:                                
                                if (heap.GetLength() == 0)
                                {
                                    PrintColored("[ERROR]: The heap is empty. ", ConsoleColor.Red);
                                    break;
                                }
                                int max = heap.ExtractMax();
                                PrintColored("The max value is: " + max, ConsoleColor.Green);
                                heap.PrintHeap();
                                break;

                            case Status.insert:                                
                                PrintColored("Enter value: ", ConsoleColor.Blue);
                                int x = 0;
                                string value = Console.ReadLine();
                                while (!IsValidInputNumber(value))
                                {
                                    Console.WriteLine();
                                    PrintColored("Enter value: ", ConsoleColor.Blue);
                                    value = Console.ReadLine();

                                }
                                x = int.Parse(value);
                                heap.InsertX(x);
                                
                                PrintColored("The value " + x + " was inserted to the heap successfully.", ConsoleColor.Green);
                                heap.PrintHeap();
                                break;

                            case Status.printHeap:                                
                                heap.PrintHeap();
                                break;
                        }
                        break;

                    case Status.exit:
                        //exit program
                        flag = false;
                        break;

                    default:
                        PrintColored("[ERROR]: Please enter a valid integer", ConsoleColor.Red);
                        break;


                }
                Console.WriteLine("\n" + s1);
            }
        }

        /// <summary>
        /// The function receives an array of strings and an array of integers. 
        /// The function checks for every given value if its valid and that the amount of elements is  <= 1000.
        /// The function also checks if a heap was already created before to validate creation.
        /// Furthermore, the array is being created and the values are being parsed to integers.
        /// </summary>
        /// <param name="stringInput"></param>        
        /// <returns> true if the user input is valid, false otherwise. </returns>
        //public static bool IsValidHeap(string[] stringInput, int[] input)
        //{
        //    if (!Dheap.IsEmpty())
        //    {
        //        PrintColored("[ERROR]: You have already created an heap", ConsoleColor.Red);
        //        return false;
        //    }
        //    if (stringInput.Length > 1000)
        //    {
        //        PrintColored("[ERROR]: You exceeded the amount of elements an heap can store (1000). Please try again.", ConsoleColor.Red);
        //        return false;
        //    }
        //    //int i = 0;
        //    //foreach (string item in stringInput)
        //    //{
        //    //    //Checks for a given value if it is valid using the "IsValidInputNumber" function.
        //    //    if (!IsValidInputNumber(item))
        //    //    {
        //    //        return false;
        //    //    }
        //    //    int number = int.Parse(item);
        //    //    input[i] = number;
        //    //    i++;
        //    //}
        //    //return true;
        //}


        /// <summary>
        /// The function checks if the input string is a valid integers.
        /// </summary>
        /// <param name="num">String representing a number.</param>
        /// <returns> true if the string represents a valid integer, false otherwise. </returns>
        public static bool IsValidInputNumber(string num)
        {
            if (!IsNumeric(num))
            {
                PrintColored("[ERROR]: You have to enter integers only. Please try again.", ConsoleColor.Red);
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
        /// The functions checks whether it's an integer.
        /// </summary>
        /// <returns> true if the string represents an integer, false otherwise. </returns>
        public static bool IsNumeric(string value)
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
        /// The function handles invalid input by checking if it's an integer.
        /// </summary>
        /// <returns> The amount of integers the userInput contains</returns>
        public static int CountElement(string userInput)
        {

            int i = 0;
            int j = 0;
            while (i < userInput.Length)
            {

                if (i < userInput.Length && userInput[i] != ' ')
                {
                    while (i < userInput.Length && userInput[i] != ' ')
                    {
                        if (userInput[i] == '.')
                        {
                            PrintColored("[ERROR]: You have to enter integers only. Please try again.", ConsoleColor.Red);
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
        /// The function removes the spaces from the string and returns the amount integers in the string.
        /// The function handles invalid input and checks if the input is a number.
        /// </summary>
        /// <returns> The length of the array representing the heap. </returns>
        public static int RemoveSpaces(string[] stringInput, string userInput)
        {
            int i = 0;
            int j = 0;
            while (i < userInput.Length)
            {
                string str = "";
                while (i < userInput.Length && userInput[i] != ' ')
                {
                    //A stopping condition for a string with a double value. 
                    if (userInput[i] == '.')
                    {
                        return 0;
                    }
                    str += userInput[i];
                    i++;
                }
                if (str != "")
                {
                    stringInput[j] = str;
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

        public static int CreateD()
        {
            int d = 0;
            while (d <= 0)
            {

                PrintColored("\nPlease enter the d value:", ConsoleColor.Blue);
                string d_string = Console.ReadLine();

                if (!IsNumeric(d_string) || int.Parse(d_string) <= 0)
                {
                    PrintColored("[ERROR]: The D value has to be an integers greater than 0", ConsoleColor.Red);
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