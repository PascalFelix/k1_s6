using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class SimplexCalculator
    {
        protected ObjectiveFunction ObjectiveFunction = null;
        protected ConstrainHandler ConstrainHandler = null;
        public SimplexCalculator(ObjectiveFunction objectiveFunction, ConstrainHandler constrainHandler)
        {
            ObjectiveFunction = objectiveFunction;
            ConstrainHandler = constrainHandler;
        }


        protected void GenerateOptimumTablou()
        {
            do
            {
                var pivotIndex = ObjectiveFunction.GetPivotColumnIndex();
                ConstrainHandler.DetectPivotConstrain(pivotIndex);
                ConstrainHandler.PivotConstrain.ReduceToPivotColumnValueOne(pivotIndex);
                ConstrainHandler.ReduceConstrainsToZero(pivotIndex);
                ObjectiveFunction.ReduceToZero(ConstrainHandler.PivotConstrain, pivotIndex);


            } while (!ObjectiveFunction.isAllNegativ());
        }

        public void Work()
        {
            GenerateOptimumTablou();
        }

    }
}
