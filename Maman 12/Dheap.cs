using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Schema;




namespace Maman_12
{
    internal class Dheap
    {
        private int[] input;
        private int d;
        private static bool Empty = true;
        private int heap_Length;

        public Dheap(int[] intArray, int d)
        {
            this.input = new int[1000];
            int i = 0;
            while (i < intArray.Length)
            {
                this.input[i] = intArray[i];
                i++;
            }
            this.heap_Length = i;
            while (i < 1000)
            {
                this.input[i] = 10001;
                i++;
            }
            this.d = d;
            this.BuildHeap(this.input, this.d);
            Dheap.Empty = false;
        }

        public void BuildHeap(int[] input, int d)
        {
            int n = this.heap_Length;
            for (int i = (int)Math.Floor((n - 1) / (double)d); i >= 0; i--)
            {
                this.Max_Heapify(i);
            }
        }


        public void Change_d(int newD)
        {
            this.d = newD;
            BuildHeap(input, this.d);
        }

        public int ExtractMax()
        {
            int n = this.heap_Length;
            int largest = this.input[0];
            swap(0, n - 1);
            input[n - 1] = 10001;
            this.heap_Length = n - 1;
            Max_Heapify(0);
            return largest;
        }

        public void InsertX(int x)
        {
            this.input[heap_Length] = x;
            this.heap_Length++;
            int n = this.heap_Length;
            fix_Insert(n - 1);

        }
        public void fix_Insert(int i)
        {
            if (i <= 0) { return; }
            int parent = Parent(i);
            if (input[i] > input[parent])
            {
                swap(i, parent);
                fix_Insert(parent);
            }

        }

        public void PrintHeap()
        {
            int n = this.heap_Length;
            PrintColored("You have " + n + " elements\n", ConsoleColor.Blue);
            PrintColored("\nThe Heap is: ", ConsoleColor.Green);
            int exponent = 0;
            int i = 0;
            while (i < n)
            {
                PrintColored("Depth Number " + exponent + ":\t", ConsoleColor.Green);
                int amount = (int)Math.Pow(this.d, exponent);
                while (amount > 0)
                {
                    if (i < n)
                    {
                        Console.Write(this.input[i] + " ");
                        i++;
                    }
                    amount--;
                }
                exponent++;
                Console.WriteLine();
            }
        }

        //Check in the future
        private void Max_Heapify(int i)
        {
            int largest = i;
            int n = this.heap_Length;
            for (int k = i * this.d + 1; k < (i * this.d) + d + 1; k++)
            {
                if (k >= n) { break; }
                if (input[k] > input[largest])
                {
                    largest = k;
                }
            }

            if (largest != i)
            {
                swap(largest, i);
                this.Max_Heapify(largest);
            }
        }
        private void swap(int k, int i)
        {
            int temp = this.input[i];
            this.input[i] = this.input[k];
            this.input[k] = temp;
        }

        //returns the parent of a certain son indexed i
        private int Parent(int i)
        {
            return Convert.ToInt32(Math.Floor((i - 1) / (double)d));
        }

        //returns the k-th son of a certain node indexed i
        private int K_son(int i, int k)
        {
            return this.d * i + k;
        }

        public int getLength()
        {
            return this.heap_Length;
        }

        //This returns if a heap was already created. 
        public static bool isEmpty()
        {
            return Dheap.Empty;
        }
        public void PrintTree()
        {
            PrintColored("\nThe Heap is: ", ConsoleColor.Green);
            int n = this.heap_Length;
            Console.WriteLine("You have {0} amount of elements\n", n);

        }
        public void PrintColored(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}