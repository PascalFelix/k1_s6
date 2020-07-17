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

        public int[] SchlupfVariablen { get; set; }

        public int TotalConstrains { get; set; }

        public int PivotColumnIndex { get; set; }

        public ObjectiveFunction(string objectiveFunction, int maxConstrains)
        {
            sObjectiveFunction = objectiveFunction;
            TotalConstrains = maxConstrains;
            Work();
            PivotColumnIndex = GetPivotColumnIndex();
        }

        protected void Work()
        {
            var splitedData = sObjectiveFunction.Split('+');
            LeftSideData = new double[splitedData.Length];
            int i = 0;
            foreach (var item in splitedData)
            {
                LeftSideData[i] = double.Parse(getVariableName(item));
                i++;
            }
            SchlupfVariablen = new int[TotalConstrains];

        }

        public double GetPivotColumnValue()
        {
            return LeftSideData[PivotColumnIndex];
        }

        protected int GetPivotColumnIndex()
        {
            int i = 0;
            double temp = 0;
            foreach (var item in LeftSideData)
            {
                if (item > temp)
                {
                    temp = item;
                }
                i++;
            }
            return i;
        }


        protected string getVariableName(string cell)
        {
            return cell.Split('*')[1];
        }

        protected string getVariableValue(string cell)
        {
            return cell.Split('*')[0];
        }


    }
}
