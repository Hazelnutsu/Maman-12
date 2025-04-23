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
            buildHeap = '1',

            ChangeD = '2',
           
            extractMax = '3',
           
            insert = '4',

            printHeap = '5',

            exit = '6'
        }

        static Dheap heap; 
        //Pattern for any signed numeric input and white spaces
        static readonly Regex inputPattern = new Regex(@"^\s*[+-]?\d+(?:\s+[+-]?\d+)*\s*$");
        /// <summary>
        /// Main function that starts the program.
        /// </summary>
        public static void Main(string[] args)
        {
            HandleStatus();
        }


       

        /// <summary>
        /// This function HandleStatus:
        /// handles the user input. 
        /// </summary>     
        public static void HandleStatus()
        {
            bool flag = true;
            string msg = "Please pick one of the following options:" +
                "\n 1. Build heap" +
                "\n 2. Change d" +
                "\n 3. Extract max" +
                "\n 4. Insert" +
                "\n 5. Print heap" +
                "\n 6. Exit";

            Console.WriteLine(msg);
            while (flag)
            {
                char inputKey = Console.ReadLine().Trim()[0];
                Console.WriteLine();
                Status status = (Status)inputKey;

                switch (status)
                {
                    case Status.buildHeap:
                        if(heap != null)
                        {
                            PrintColored("A heap was already created", ConsoleColor.Red);
                            break;
                        }

                        PrintColored("Please write your heap values as integers (Seperated by a space): ", ConsoleColor.Blue);                        
                        string userInput = Console.ReadLine();
                        if (!inputPattern.IsMatch(userInput))
                        {
                            PrintColored("[ERROR]: You have to enter integers only. Please try again.", ConsoleColor.Red);
                            break;
                        }

                        //Pattern for replacing any white space with 1 single white space.
                        userInput = Regex.Replace(userInput, @"\s+", " ").Trim();
                        string[] userInputArray = userInput.Split(' ');                        
                        List<int> input = new List<int>();

                        foreach (string num in userInputArray)
                        {                           
                            input.Add(int.Parse(num));                            
                        }

                        bool validList = true;
                        foreach(int num in input)
                        {
                            if(num > 9999 || num < -9999)
                            {
                                validList = false;
                                break;
                            }
                        }

                        if (!validList)
                        {
                            PrintColored("[ERROR] Values have to be between -9999 and 9999", ConsoleColor.Red);
                            break;
                        }

                        
                        if (input.Count == 0 || input.Count > 1000)
                        {
                            PrintColored("[ERROR] input has to be more than 0 and less than 1000 elements", ConsoleColor.Red);
                            break;
                        }

                        
                        int d = ValidInput(true, "Enter the d value: ");                                                                                                                                                   
                        heap = new Dheap(input, d);
                        PrintColored("The heap has been built", ConsoleColor.Green);
                        heap.PrintHeap();

                        break;

                    case Status.ChangeD:
                    case Status.extractMax:
                    case Status.insert:
                    case Status.printHeap:

                        if (heap == null)
                        {
                            PrintColored("[ERROR] You have to create a heap first", ConsoleColor.Red);
                            break;
                        }

                        switch (status)
                        {

                            case Status.ChangeD:
                                int newD = ValidInput(true, "Enter the d value");
                                heap.ChangeD(newD);                                
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
                                
                                int x = ValidInput(false, "Enter value: ");
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
                        PrintColored("[ERROR]: Please pick one of the options 1-6", ConsoleColor.Red);
                        break;


                }
                Console.WriteLine("\n" + msg);
            }
        }

        /// <summary>
        /// The function ValidInput:
        /// Asks the user for input of a value either to insert into the heap or the d value of the heap.
        /// Checks whether the input is valid based on the case that called it:
        /// for a d value checks that its a positive integer and for an insert value checks whether its an integer with a max of 4 digits.
        /// </summary>
        /// <param name="hasToBePositive">Checks whether the input needs to be positive or not.</param>
        /// <returns>The value from the user input as an integer.</returns>
        public static int ValidInput(bool hasToBePositive, string msg)
        {
            int x = 0;
            bool flag = true;
            string value;
            while (flag)
            {
                PrintColored(msg, ConsoleColor.Blue);
                value = Console.ReadLine();
                if (int.TryParse(value, out x))
                {

                    if (!hasToBePositive)
                    {
                        if (x > 9999 || x < -9999)
                        {
                            PrintColored("[ERROR] Values have to be between -9999 and 9999", ConsoleColor.Red);
                            continue;
                        }
                        else
                            break;
                    }
                    else if (hasToBePositive && x > 0)
                        break;
                    else
                    {
                        PrintColored("[ERROR] Value has to be a positive integer", ConsoleColor.Red);
                    }
                    
                }
                else
                {
                    PrintColored("[ERROR] Value has to be an integer", ConsoleColor.Red);
                }
                
            }
            return x;
        }
                
        /// <summary>
        /// The function:
        /// Prints a string with an input color.
        /// </summary>
        /// <param name="message">The string that you wish to print in a different color.</param>
        /// <param name="color">The color you wish to change to.</param>
        public static void PrintColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }


}