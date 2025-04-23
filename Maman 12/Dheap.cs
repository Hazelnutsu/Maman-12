using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;




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
            PrintColored("You have " + n + " elements\n", ConsoleColor.Blue);
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