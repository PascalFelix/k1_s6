using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class SimplexCalculator
    {
        protected SimplexLineCollection SimplexLineCollection = null;
        public SimplexCalculator(SimplexLineCollection simplexLineCollection)
        {
            SimplexLineCollection = simplexLineCollection;
        }


        protected void GenerateOptimumTablou()
        {
            do
            {
                var pivotIndex = SimplexLineCollection.objectiveFunction.GetPivotColumnIndex();
                SimplexLineCollection.DetectPivotConstrain(pivotIndex);
                SimplexLineCollection.PivotConstrain.ReduceToPivotColumnValueOne(pivotIndex);
                SimplexLineCollection.ReduceConstrainsToZero(pivotIndex);
                SimplexLineCollection.objectiveFunction.ReduceToZero(SimplexLineCollection.PivotConstrain, pivotIndex);


            } while (!SimplexLineCollection.objectiveFunction.isAllNegativ());
        }

        protected void CalcResult()
        {
            int varCount = SimplexLineCollection.objectiveFunction.BasicArray.Length - 1;
            double[] output = new double[varCount];
            for (int i = 0; i < varCount; i++)
            {
                try
                {
                    int OneIndex = 0;
                    int j = 0;
                    foreach (var item in SimplexLineCollection.constrains)
                    {
                        if (item.leftSideArray[i] == 1)
                        {
                            OneIndex = j;
                        }
                        else if (item.leftSideArray[i] != 0)
                        {
                            output[i] = 0;
                            throw new Exception("asdf");
                        }
                        j++;
                    }
                    j = 0;

                    output[i] = SimplexLineCollection.constrains[OneIndex].RightSideValue;

                }
                catch (Exception e)
                {
                    //do nothing
                }
            }
        }

        public void Work()
        {
            GenerateOptimumTablou();
            CalcResult();
        }

    }
}
