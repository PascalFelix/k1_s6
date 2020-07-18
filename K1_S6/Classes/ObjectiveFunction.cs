using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class ObjectiveFunction
    {

        public string sObjectiveFunction { get; set; }

        public double[] LeftSideData { get; set; }

        public double[] SchlupfVariablen { get; set; }

        public int TotalConstrains { get; set; }

        public double RightSideValue = 0;

      //  public int PivotColumnIndex { get; set; }

        public ObjectiveFunction(string objectiveFunction, int maxConstrains)
        {
            sObjectiveFunction = objectiveFunction;
            TotalConstrains = maxConstrains;
            Work();
          //  PivotColumnIndex = GetPivotColumnIndex();
        }

        protected void Work()
        {
            var splitedData = sObjectiveFunction.Split('+');
            LeftSideData = new double[splitedData.Length];
            int i = 0;
            foreach (var item in splitedData)
            {
                LeftSideData[i] = double.Parse(getVariableValue(item));
                i++;
            }
            SchlupfVariablen = new double[TotalConstrains];

        }

        public double GetPivotColumnValue()
        {
            return LeftSideData[GetPivotColumnIndex()];
        }

        public int GetPivotColumnIndex()
        {
            int i = 0;
            int index = 0;
            double temp = 0;
            foreach (var item in LeftSideData)
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


        protected string getVariableName(string cell)
        {
            return cell.Split('*')[1];
        }

        protected string getVariableValue(string cell)
        {
            return cell.Split('*')[0];
        }

        public bool isAllNegativ()
        {
            foreach (var item in LeftSideData)
            {
                if(item > 0)
                {
                    return false;
                }
            }
            foreach (var item in SchlupfVariablen)
            {
                if (item > 0)
                {
                    return false;
                }
            }
            //if(RightSideData > 0)
            //{
            //    return false;
            //}
            return true;
        }

        public void ReduceToZero(AdvancedConstrain pivotConstrain, int pivotIndex)
        {
            var multiplier = LeftSideData[pivotIndex];

            int i = 0;
            foreach (var item in LeftSideData)
            {
                LeftSideData[i] = LeftSideData[i] - (pivotConstrain.leftSideData[i] * multiplier);
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
