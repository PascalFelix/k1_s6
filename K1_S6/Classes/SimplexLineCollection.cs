using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    enum OptimisingType
    {
        min,
        max
    }

    class SimplexLineCollection
    {

        public List<NewConstrain> constrains = new List<NewConstrain>();
        public NewConstrain PivotConstrain = null;
        public NewObjectiveFunction objectiveFunction = null;

        public double[,] TransponatedArray = null;

        public SimplexLineCollection(NewObjectiveFunction newObjectiveFunction, List<NewConstrain> newConstrains, OptimisingType type)
        {
            constrains = newConstrains;
            objectiveFunction = newObjectiveFunction;
            TransponateIfNeeded(type);
            int i = 0;
            foreach (var item in constrains)
            {
                item.GenerateSlackVariable(i);
                i++;
            }
            objectiveFunction.GenerateSlackVariable();
        }


        public void DetectPivotConstrain(int pivotColumnIndex)
        {
            double temp = 0;
            foreach (var item in constrains)
            {
                var value = item.DivideRightSideValueByPivotCloumnIndexValue(pivotColumnIndex);
                if (value < temp || temp == 0)
                {
                    temp = value;
                    PivotConstrain = item;
                }
            }
        }

        public void ReduceConstrainsToZero(int pivotColumnIndex)
        {
            foreach (var item in constrains)
            {
                if (item == PivotConstrain)
                {
                    continue;
                }
                item.ReduceToZero(PivotConstrain, pivotColumnIndex);

            }
        }










        public void TransponateIfNeeded(OptimisingType type)
        {
            if (type == OptimisingType.min)
            {
                Transpose();
            }
        }
        private void Transpose()
        {
            int size = constrains.Count + 1;

            TransponatedArray = new double[objectiveFunction.BasicArray.Length, size];
            double[,] temp = new double[size, objectiveFunction.BasicArray.Length];

            for (int i = 0; i < constrains.Count; i++)
            {
                for (int j = 0; j < constrains[i].BasicArray.Length; j++)
                {
                    temp[i, j] = constrains[i].BasicArray[j];
                }
            }
            for (int i = 0; i < objectiveFunction.BasicArray.Length; i++)
            {
                temp[size - 1, i] = objectiveFunction.BasicArray[i];
            }
            TransponatedArray = Transpose(temp);
            

            for (int i = 0; i < objectiveFunction.BasicArray.Length - 1; i++)
            {
                double[] tmp = new double[objectiveFunction.BasicArray.Length];
                for (int j = 0; j < size; j++)
                {
                    tmp[j] = TransponatedArray[i, j];
                }
                if(i > constrains.Count - 1)
                {
                    constrains.Add(new NewConstrain(tmp, constrains.Count + 1));
                }
                constrains[i].SetBasicArray(tmp);

            }
            double[] tmp2 = new double[objectiveFunction.BasicArray.Length];
            for (int j = 0; j < size; j++)
            {
                tmp2[j] = TransponatedArray[size - 1, j];
            }
            objectiveFunction.SetBasicArray(tmp2);
            objectiveFunction.NumberOfConstrains = constrains.Count;
        }
        private double[,] Transpose(double[,] matrix)
        {
            int w = matrix.GetLength(0);
            int h = matrix.GetLength(1);

            double[,] result = new double[h, w];

            for (int i = 0; i < w; i++)
            {
                for (int j = 0; j < h; j++)
                {
                    result[j, i] = matrix[i, j];
                }
            }

            return result;
        }

    }

}
