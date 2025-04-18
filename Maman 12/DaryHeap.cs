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
    internal class DaryHeap
    {
        private List<int> input;
        private int d;
        private static bool Empty = true;
        
        public DaryHeap(List<int> intArray, int d)
        {
            this.input = intArray;
            this.d = d;
            this.BuildHeap(this.input, this.d);
            DaryHeap.Empty = false;
        }

        public void BuildHeap(List<int> input, int d)
        {
            int n = this.input.Count;
            for (int i = (int) Math.Floor((n-1) / (double)d); i >= 0; i--)
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
            int n = this.input.Count;
            int largest = this.input[0];
            swap(0, n - 1);
            input.RemoveAt(n - 1);
            Max_Heapify(0);
            return largest;
        }
        
        public void InsertX(int x)
        {
            input.Add(x);
            int n = this.input.Count;
            fix_Insert(n-1);

        }
        public void fix_Insert(int i)
        {
            if(i <= 0) { return; }
            int parent = Parent(i);
            if (input[i] > input[parent])
            {
                swap(i, parent);
                fix_Insert(parent);
            }

        }

        public void PrintHeap()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nThe Heap is: ");
            Console.ResetColor();
            int n = this.input.Count;
            Console.WriteLine("You have " + n + " elements\n");
            int kfola = 0;
            int i = 0;
            while (i < n)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Depth Number: " + kfola + ".\t");
                Console.ResetColor();
                int amount = (int) Math.Pow(this.d, kfola);
                while(amount > 0)
                {
                    if(i < n)
                    {
                        Console.Write(this.input[i] + " ");
                        i++;
                    }
                    amount--;
                }
                kfola++;
                Console.WriteLine();
            }
        }

        //Check in the future
        private void Max_Heapify(int i)
        {
            int largest = i;
            int n = this.input.Count;
            for(int k = i * this.d + 1; k < (i * this.d) + d+1; k++)
            {
                if(k >= n) { break; }
                if (input[k] > input[largest])
                {
                    largest = k;
                }
            }

            if(largest != i)
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
            return this.input.Count;
        }

        public static bool isEmpty()
        {
            return DaryHeap.Empty;
        }
        public void PrintTree()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nThe Heap is: ");
            Console.ResetColor();
            int n = this.input.Count;
            Console.WriteLine("You have " + n + " amount of elements\n");

        }
    }
}
