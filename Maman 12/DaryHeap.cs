using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Maman_12
{
    internal class DaryHeap
    {
        private List<int> _input;
        private int _d; //number of sons
        int cas;



        public DaryHeap(int d, List<int> intArray)
        {
            _d = d;
            _input = intArray;
        }

        public void BuildHeap()
        {

        }

        
        public void Change_d(int d)
        {

        }

        public void ExtractMax()
        {

        }
        
        public void InsertX(int x)
        {

        }

        public void PrintHeap()
        {

        }
        private void Max_Heapify(int i)
        {

        }

        //returns the parent of a certain son indexed i
        private int Parent(int i)
        {
            double a = i / _d;
           
            return (int)Math.Floor(a);
        }
        
        //returns the k-th son of a certain node indexed i
        private int D_son(int i, int k)
        {
            return _d * i - _d + k + 1;
        }



    }
}
