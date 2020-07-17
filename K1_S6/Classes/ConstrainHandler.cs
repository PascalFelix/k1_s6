using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class ConstrainHandler
    {
        protected List<Constrain> Constrains = null;
        protected List<AdvancedConstrain> AdvancedConstrains = new List<AdvancedConstrain>();

        public AdvancedConstrain PivotConstrain = null;

        protected int MaxNumberOfVariables = 0;

        public ConstrainHandler(List<Constrain> constrains)
        {
            Constrains = constrains;
            MaxNumberOfVariables = CalculateNumberOfVariables();
            CreateListOfAdvancedConstrains();
        }

        protected void CreateListOfAdvancedConstrains()
        {
            int maxConstrains = Constrains.Count;
            int i = 0;
            foreach (var item in Constrains)
            {
                AdvancedConstrains.Add(new AdvancedConstrain(item.sConstrain, maxConstrains, MaxNumberOfVariables, i));
                i++;
            }


        }

        protected int CalculateNumberOfVariables()
        {
            int temp = 0;
            foreach (var item in Constrains)
            {
                if (item.GetNumberOfVariables() > temp)
                {
                    temp = item.GetNumberOfVariables();
                }

            }
            return temp;
        }

        public void DetectPivotConstrain(int pivotColumnIndex)
        {
            double temp = 0;
            foreach (var item in AdvancedConstrains)
            {
                item.DivideRightSideValueByPivotCloumnIndexValue(pivotColumnIndex);
                if(item.RightsideOperatorValue > temp)
                {
                    temp = item.RightsideOperatorValue;
                    PivotConstrain = item;
                }
            }
        }

    }
}
