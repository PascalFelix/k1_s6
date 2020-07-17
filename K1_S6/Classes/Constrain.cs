using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    enum OperatorType
    {
        Greater,
        Less,
        none
    }

    class Constrain
    {



        public string sConstrain { get; set; }

        protected string[] _Array { get; set; }

        public double RightsideOperatorValue { get; set; }

        public OperatorType OperatorType = OperatorType.none;

        private int _calculatedNumberOfVariables = 0;

        public Constrain(string constrain)
        {
            sConstrain = constrain;
        }

        /*
         * false = greater
         * true = less
         */
        private OperatorType DetectGreaterOrLess()
        {
            if (sConstrain.Contains(">="))
            {
                this.OperatorType = OperatorType.Greater;

            }
            else if (sConstrain.Contains("<="))
            {
                this.OperatorType = OperatorType.Less;
            }
            else
            {
                this.OperatorType = OperatorType.none;
            }
            return this.OperatorType;
        }

        private string[] SplitByOperator()
        {
            var temp = DetectGreaterOrLess();
            if (temp == OperatorType.Less)
            {
                return sConstrain.Split("<=".ToCharArray());
            }
            else if (temp == OperatorType.Greater)
            {
                return sConstrain.Split(">=".ToCharArray());
            }
            else
            {
                throw new Exception("SplitByOperator");
            }
        }

        private void SetRightsideOperatorValue(string value)
        {
            double temp = 0;
            if (double.TryParse(value, out temp))
            {
                RightsideOperatorValue = temp;
            }
            else
            {
                throw new Exception("Right side operator value couldnt be cast to double");
            }
        }

        public string[] ToArray()
        {
            if (_Array == null)
            {
                var splitedConstrain = SplitByOperator();
                SetRightsideOperatorValue(splitedConstrain[2]);

                var constrainLeftSideValues = splitedConstrain[0].Split('+');

                string[] returnData = new string[constrainLeftSideValues.Length + 1];

                int i = 0;
                foreach (var item in constrainLeftSideValues)
                {
                    returnData[i] = item;
                    i++;
                }
                returnData[i] = RightsideOperatorValue.ToString();

                //_Array = new string[returnData.Length];
                _Array = returnData;

            }

            return _Array;
        }

        protected string getVariableName(string cell)
        {
            return cell.Split('*')[1];
        }

        protected string getVariableValue(string cell)
        {
            return cell.Split('*')[0];
        }

        public int GetNumberOfVariables()
        {
            if (_calculatedNumberOfVariables == 0)
            {
                var variables = new HashSet<string>();
                for (int i = 0; i < ToArray().Length - 1; i++)
                {
                    variables.Add(getVariableName(ToArray()[i]));
                    _calculatedNumberOfVariables = variables.Count;
                }
            }
            return _calculatedNumberOfVariables;
        }

    }
}
