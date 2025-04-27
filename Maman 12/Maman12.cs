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
        //Pattern for any signed numeric with a maximum of 4 digits input and white spaces.
        static readonly Regex inputPattern = new Regex(@"^\s*[+-]?\d{1,4}(?:\s+[+-]?\d{1,4})*\s*$");
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
                        MakeList();
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
        /// <summary>
        /// The function MakeList:
        /// Handles the user's input for valid values for the d-ary heap.
        /// Calls the constructor and creates the heap.
        /// </summary>
        public static void MakeList()
        {
            bool flag = true; 
            while (flag)
            {
                PrintColored("Please write your heap values as integers (Seperated by a space): ", ConsoleColor.Blue);
                string userInput = Console.ReadLine();
                if (!inputPattern.IsMatch(userInput))
                {
                    PrintColored("[ERROR]: You have to enter integers between -9999 and 9999 only. Please try again.", ConsoleColor.Red);
                    continue;
                }

                
                userInput = Regex.Replace(userInput, @"\s+", " ").Trim();//Pattern for replacing any white space with 1 single white space.
                string[] userInputArray = userInput.Split(' '); //seperating the integers into an array of strings.
                List<int> input = new List<int>();

                foreach (string num in userInputArray)//adding the values of the input to a List and casting them to integers.
                {
                    input.Add(int.Parse(num));
                }

                if (input.Count == 0 || input.Count > 1000)
                {
                    PrintColored("[ERROR] input has to be more than 0 and less than 1000 elements", ConsoleColor.Red);
                    continue;
                }


                int d = ValidInput(true, "Enter the d value: ");
                heap = new Dheap(input, d);//initializing the d-ary heap object.
                PrintColored("The heap has been built", ConsoleColor.Green);
                heap.PrintHeap();
                flag = false;

            }
        }
    }

}

namespace Maman_12
{
    internal class Dheap
    {
        //Class properties
        private List<int> input;
        private int d;

        /// <summary>
        /// Main constructor building the d-ary heap object.
        /// </summary>
        /// <param name="input">The List that represents the d-ary heap.</param>
        /// <param name="d">The number of sons each node has in the d-ary heap.</param>
        public Dheap(List<int> input, int d)
        {
            this.input = input;
            this.d = d;
            BuildHeap();
        }

        /// <summary>
        /// The function BuildHeap:
        /// Creates a max d-ary heap based on the input List of integers using the MaxHeapify function.
        /// </summary>
        public void BuildHeap()
        {
            int n = input.Count;
            int i = (int)Math.Floor((n - 1) / (double)this.d);

            while (i >= 0)
            {
                MaxHeapify(i);
                i--;
            }
        }
        /// <summary>
        /// The function MaxHeapify:
        /// Compares the value at index "i" of the d-ary heap to all of it's sons and swapping them if needed to keep the max heap property.
        /// Stops when the value has reached his correct place.
        /// </summary>
        /// <param name="i">The index of a node in the d-ary heap.</param>
        private void MaxHeapify(int i)
        {
            int largest = i;
            int n = input.Count;
            for (int k = i * d + 1; k < (i * d) + d + 1; k++)
            {
                if (k >= n) { break; }
                if (input[k] > input[largest])
                {
                    largest = k;
                }
            }

            if (largest != i)
            {
                Swap(largest, i);
                MaxHeapify(largest);
            }
        }


        /// <summary>
        /// The function ChangeD:
        /// Changes the d value of the d-ary heap and re-arranges the values so each node will have the new d value of sons using the BuildHeap function.
        /// </summary>
        /// <param name="newD">The new d value of the d-ary heap.</param>
        public void ChangeD(int newD)
        {
            d = newD;
            BuildHeap();
        }

        /// <summary>
        /// The function ExtractMax:
        /// Extracts the maximum value from the d-ary heap and re-arranges the values so it returns to being a max heap using the MaxHeapify function.
        /// </summary>
        /// <returns>The maximum value of the d-ary heap.</returns>
        public int ExtractMax()
        {
            int n = input.Count;
            int largest = input[0];
            Swap(0, n - 1);
            input.RemoveAt(n - 1);
            MaxHeapify(0);
            return largest;
        }


        /// <summary>
        /// The function InsertX:
        /// Inserts a value "x" into the d-ary heap and places it in it's correct place using the MaxHeapify function.
        /// </summary>
        /// <param name="x">The value that gets inserted into the heap.</param>
        public void InsertX(int x)
        {
            int n = input.Count;
            if (n >= 1000)
            {
                PrintColored("Heap is full, you cannot insert more elements.", ConsoleColor.Red);
                return;
            }
            input.Insert(0, x);
            MaxHeapify(0);
        }


        /// <summary>
        /// The function PrintHeap:
        /// Prints the values of the nodes of the d-ary heap printing each level seperatly.
        /// </summary>
        public void PrintHeap()
        {
            int n = input.Count;
            PrintColored("You have " + n + " elements", ConsoleColor.Blue);
            PrintColored("\nThe Heap is: ", ConsoleColor.Green);
            int exponent = 0;
            int i = 0;
            while (i < n)
            {
                PrintColored("Depth Number " + exponent + ":\t", ConsoleColor.Green);
                int amount = (int)Math.Pow(d, exponent);
                while (amount > 0)
                {
                    if (i < n)
                    {
                        Console.Write(input[i] + " ");
                        i++;
                    }
                    amount--;
                }
                exponent++;
                Console.WriteLine();
            }
        }


        /// <summary>
        /// The function Swap:
        /// Swaps between the values of 2 elements in the List.
        /// </summary>
        /// <param name="k">Index of the first value that gets swapped.</param>
        /// <param name="i">Index of the second value that gets swapped</param>
        private void Swap(int k, int i)
        {
            int temp = input[i];
            input[i] = input[k];
            input[k] = temp;
        }

        /// <summary>
        /// The function GetLength:
        /// Returns the length of the List representing the d-ary heap.
        /// </summary>
        /// <returns>The length of the List representing the d-ary heap.</returns>
        public int GetLength()
        {
            return input.Count;
        }


        /// <summary>
        /// The function PrintColored:
        /// Prints a string with an input color.
        /// </summary>
        /// <param name="message">The string that you wish to print in a different color.</param>
        /// <param name="color">The color you wish to change to.</param>
        public void PrintColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}