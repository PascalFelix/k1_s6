using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class NewConstrain : BasicLine
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
        public NewConstrain(string input, int numberOfConstrains) : base(input)
        {
            NumberOfConstrains = numberOfConstrains;
        }
        public NewConstrain(double[] array, int numberOfConstrains) : base(array)
        {
            NumberOfConstrains = numberOfConstrains;
        }
        public void GenerateSlackVariable(int currentPos)
        {
            SlackVarialbes = new double[NumberOfConstrains];
            SlackVarialbes[currentPos] = 1;
        }
        public void ReduceToZero(NewConstrain pivotConstrain, int pivotIndex)
        {
            var multiplier = leftSideArray[pivotIndex];

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
        public void ReduceToPivotColumnValueOne(int pivotColumnIndex)
        {
            var pivotValue = leftSideArray[pivotColumnIndex];
            if (pivotValue == 1 || pivotValue == 0)
            {
                return;
            }
            else
            {
                int i = 0;
                foreach (var item in leftSideArray)
                {
                    leftSideArray[i] = leftSideArray[i] / pivotValue;
                    i++;
                }
                i = 0;
                foreach (var item in SlackVarialbes)
                {
                    SlackVarialbes[i] = SlackVarialbes[i] / pivotValue;
                    i++;
                }
                RightSideValue = RightSideValue / pivotValue;
            }
        }
    }
}
