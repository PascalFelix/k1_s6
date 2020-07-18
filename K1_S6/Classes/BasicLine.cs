using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    enum OperatorTypes
    {
        Greater,
        Less,
        none
    }

    class BasicLine
    {
        public double[] BasicArray = null;
        public void SetBasicArray(double[] newBasicArray)
        {
            BasicArray = newBasicArray;
            SetRightSideValue(newBasicArray[newBasicArray.Length - 1]);
            var newArray = new double[newBasicArray.Length - 1];
            Array.Copy(newBasicArray, 0, newArray, 0, newArray.Length);
            leftSideArray = newArray;
        }

        public double[] leftSideArray = null;
        public double RightSideValue = 0;
        private void SetRightSideValue(string Value)
        {
            RightSideValue = double.Parse(Value);
        }
        private void SetRightSideValue(double Value)
        {
            RightSideValue = Value;
        }

        private string InputLine = "";
        private OperatorTypes OperatorType = OperatorTypes.none;

    

        public BasicLine(string Input)
        {
            InputLine = Input;
            SetBasicArray(GenerateBasicArray());
        }

        public BasicLine(double[] array)
        {
            SetBasicArray(array);
        }


        public double[] GenerateBasicArray()
        {

            var splitedConstrain = SplitByOperator();

            if (splitedConstrain.Length >= 2)
            {
                SetRightSideValue(splitedConstrain[2]);
            }

            var constrainLeftSideValues = splitedConstrain[0].Split('+');

            double[] returnData = new double[constrainLeftSideValues.Length + 1];

            int i = 0;
            foreach (var item in constrainLeftSideValues)
            {
                returnData[i] = double.Parse(getVariableValue(item));
                i++;
            }
            returnData[i] = double.Parse(RightSideValue.ToString());

            return returnData;
        }


        protected string getVariableName(string cell)
        {
            return cell.Split('*')[1];
        }

        protected string getVariableValue(string cell)
        {
            return cell.Split('*')[0];
        }

        private OperatorTypes DetectGreaterOrLess()
        {
            if (InputLine.Contains(">="))
            {
                this.OperatorType = OperatorTypes.Greater;

            }
            else if (InputLine.Contains("<="))
            {
                this.OperatorType = OperatorTypes.Less;
            }
            else
            {
                this.OperatorType = OperatorTypes.none;
            }
            return this.OperatorType;
        }

        private string[] SplitByOperator()
        {
            var temp = DetectGreaterOrLess();
            if (temp == OperatorTypes.Less)
            {
                return InputLine.Split("<=".ToCharArray());
            }
            else if (temp == OperatorTypes.Greater)
            {
                return InputLine.Split(">=".ToCharArray());
            }
            else
            {
                return new string[1] { InputLine };
            }
        }
        public double DivideRightSideValueByDouble(double value)
        {
            return RightSideValue / value;
        }

        public double DivideRightSideValueByPivotCloumnIndexValue(int index)
        {
            return DivideRightSideValueByDouble(leftSideArray[index]);
        }


    }
}
