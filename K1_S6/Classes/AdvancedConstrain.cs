using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class AdvancedConstrain : Constrain
    {
        public int TotalConstrains { get; set; }
        public int TotalVariables { get; set; }
        public int CurrentPosition { get; set; }

        public double[] SchlupfVariablen = null;

        public double[] leftSideData = null;


        public AdvancedConstrain(string constrain, int numberOfTotalConstrains, int numberOfTotalVariables, int currentPosition) : base(constrain)
        {
            TotalConstrains = numberOfTotalConstrains;
            TotalVariables = numberOfTotalVariables;
            CurrentPosition = currentPosition;

            leftSideData = new double[numberOfTotalVariables];
            Array.Clear(leftSideData, 0, numberOfTotalVariables);
            SchlupfVariablen = new double[numberOfTotalConstrains];
            SchlupfVariablen[currentPosition] = 1;
            OrderAndCreateLeftSideDataArray();

        }

        protected void OrderAndCreateLeftSideDataArray()
        {

            for (int i = 0; i < ToArray().Length - 1; i++)
            {
                leftSideData[i] = double.Parse(getVariableValue(ToArray()[i]));

            }
        }

        public int GetHighestValueIndex()
        {
            int i = 0;
            double temp = 0;
            foreach (var item in leftSideData)
            {
                if (item > temp)
                {
                    temp = item;
                }
                i++;
            }
            return i;
        }

        public double DivideRightSideValueByDouble(double value)
        {
            return RightSideValue / value;
        }

        public double DivideRightSideValueByPivotCloumnIndexValue(int index)
        {
            return DivideRightSideValueByDouble(leftSideData[index]);
        }

        public void ReduceToPivotColumnValueOne(int pivotColumnIndex)
        {
            var pivotValue = leftSideData[pivotColumnIndex];
            if (pivotValue == 1 || pivotValue == 0)
            {
                return;
            }
            else
            {
                int i = 0;
                foreach (var item in leftSideData)
                {
                    leftSideData[i] = leftSideData[i] / pivotValue;
                    i++;
                }
                i = 0;
                foreach (var item in SchlupfVariablen)
                {
                    SchlupfVariablen[i] = SchlupfVariablen[i] / pivotValue;
                    i++;
                }
                RightSideValue = RightSideValue / pivotValue;
            }
        }

        public void ReduceToZero(AdvancedConstrain pivotConstrain, int pivotIndex)
        {
            var multiplier = leftSideData[pivotIndex];

            int i = 0;
            foreach (var item in leftSideData)
            {
                leftSideData[i] = leftSideData[i] - (pivotConstrain.leftSideData[i] * multiplier);
                i++;
            }
            i = 0;
            foreach (var item in SchlupfVariablen)
            {
                SchlupfVariablen[i] = SchlupfVariablen[i] - (pivotConstrain.SchlupfVariablen[i] * multiplier);
                i++;
            }
            RightSideValue = RightSideValue - (pivotConstrain.RightSideValue * multiplier);
        }

    }
}
