using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class NewObjectiveFunction : BasicLine
    {
        private int _NumberOfConstrains = 0;
        public double[] SlackVarialbes = null;
        public int NumberOfConstrains
        {
            get
            {
                return _NumberOfConstrains;
            }
            set
            {
                _NumberOfConstrains = value;
            }
        }
        public NewObjectiveFunction(string input, int numberOfConstrains) : base(input)
        {
            NumberOfConstrains = numberOfConstrains;
        }
        public NewObjectiveFunction(double[] array, int numberOfConstrains) : base(array)
        {
            NumberOfConstrains = numberOfConstrains;
        }

        public void GenerateSlackVariable()
        {
            SlackVarialbes = new double[NumberOfConstrains];
        }
        public bool isAllNegativ()
        {
            foreach (var item in leftSideArray)
            {
                if (item > 0)
                {
                    return false;
                }
            }
            foreach (var item in SlackVarialbes)
            {
                if (item > 0)
                {
                    return false;
                }
            }
            return true;
        }
        public int GetPivotColumnIndex()
        {
            int i = 0;
            int index = 0;
            double temp = 0;
            foreach (var item in leftSideArray)
            {
                if (item > temp)
                {
                    temp = item;
                    index = i;
                }
                i++;
            }
            return index;
        }
        public void ReduceToZero(NewConstrain pivotConstrain, int pivotIndex)
        {
            double multiplier = leftSideArray[pivotIndex];

            int i = 0;
            foreach (var item in leftSideArray)
            {
                leftSideArray[i] = leftSideArray[i] - (pivotConstrain.leftSideArray[i] * multiplier);
                i++;
            }
            i = 0;
            foreach (var item in SlackVarialbes)
            {
                SlackVarialbes[i] = SlackVarialbes[i] - (pivotConstrain.SlackVarialbes[i] * multiplier);
                i++;
            }
            RightSideValue = RightSideValue - (pivotConstrain.RightSideValue * multiplier);
        }

    }


}
