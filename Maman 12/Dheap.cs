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
        private List<int> input;
        private int d;

        /*
         * The constructor that receives a array of integers and a d value 
         * And creates a maximum d-ary heap.
         */
        public Dheap(List<int> input, int d)
        {
            this.input = input;
            this.d = d;
            BuildHeap();
        }

        /*
         * This method receives an array of integers and an integer d
         * And builds a maximum d-ary heap from the input array.
         */
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

        /* 
		 * This method receives an integer d
		 * And rearranges the heap based on the new d value.
		 */
        public void ChangeD(int newD)
        {
            d = newD;
            BuildHeap();
        }

        /// <summary>
        /// Extracts the maximum element from the heap 
        /// and rearranges the heap accordingly.
        /// </summary>
        /// <returns> The maximum element. </returns>
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
        /// The function receives an integer x, inserts it to the heap
        /// And calls FixInsert to rearrange the heap.
        /// </summary>

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
        /// The function prints each level of the heap in a different line. 
        ///// </summary>
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
        /// The function receives an index i and places the element at that index in its correct location. 
        /// </summary>
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
        /// The function receives 2 indexes and Swap between the values at those indexes.
        /// </summary>
        private void Swap(int k, int i)
        {
            int temp = input[i];
            input[i] = input[k];
            input[k] = temp;
        }

        /// <summary>
        /// The functions returns the length of the heap. 
        /// </summary>
        public int GetLength()
        {
            return input.Count;
        }

        /// <summary>
        /// The function returns true if the heap was not created and false otherwise. 
        /// </summary>


        /// <summary>
        /// The function receives a string and a color, then prints the string in the given color. 
        /// </summary>
        public void PrintColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}