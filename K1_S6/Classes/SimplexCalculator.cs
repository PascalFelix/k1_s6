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
            int counter = 0;
            do
            {
                var pivotIndex = SimplexLineCollection.objectiveFunction.GetPivotColumnIndex();
                SimplexLineCollection.DetectPivotConstrain(pivotIndex);
                SimplexLineCollection.PivotConstrain.ReduceToPivotColumnValueOne(pivotIndex);
                SimplexLineCollection.ReduceConstrainsToZero(pivotIndex);
                SimplexLineCollection.objectiveFunction.ReduceToZero(SimplexLineCollection.PivotConstrain, pivotIndex);
                counter++;
                if(counter > 1000)
                {
                    throw new Exception("Tabelle konnte nicht optimiert werden");
                }

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
                    bool OneFound = false;
                    int j = 0;
                    foreach (var item in SimplexLineCollection.constrains)
                    {
                        var rounded = Math.Round(item.leftSideArray[i]);
                        if (rounded == double.Parse(1.ToString()))
                        {
                            OneFound = true;
                            OneIndex = j;
                        }
                        else if (rounded != double.Parse(0.ToString()))
                        {
                            output[i] = 0;
                            throw new Exception("asdf");
                        }
                        j++;
                    }
                    j = 0;
                    if(OneFound)
                    output[i] = SimplexLineCollection.constrains[OneIndex].RightSideValue;
                    OneFound = false;

                }
                catch (Exception e)
                {
                    //do nothing
                }
            }
            int x = 1;
            foreach (var item in output)
            {
                Console.WriteLine("x{0} : {1}", x.ToString(), output[x - 1].ToString());
                x++;
            }
            Console.WriteLine("Objective function right hand side :{0}", SimplexLineCollection.objectiveFunction.RightSideValue.ToString());

        }

        public void Work()
        {
            try
            {
                GenerateOptimumTablou();
                CalcResult();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

    }
}
