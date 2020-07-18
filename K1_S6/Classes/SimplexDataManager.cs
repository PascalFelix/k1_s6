using K1_S6.Events;
using K1_S6.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class SimplexDataManager
    {
        private string _ObjectiveFunction = "";
        private List<string> _constainList = new List<string>();

        private List<NewConstrain> constrainList = new List<NewConstrain>();
        private NewObjectiveFunction ObjectiveFunction = null;

        private ConstrainHandler ConstrainHandler = null;
        private SimplexCalculator SimplexCalculator = null;

        //min or max; min true, max false
        protected OptimisingType TargetType = OptimisingType.min;

        //protected int _ArrayHeight = 0;
        //protected int ArrayHeight
        //{
        //    get
        //    {
        //        if (_ArrayHeight == 0)
        //        {
        //            ArrayHeight = CalculateArrayHeight();
        //        }

        //        return _ArrayHeight;

        //    }
        //    set
        //    {
        //        _ArrayHeight = value;
        //    }
        //}

        //protected int _ArrayWidth = 0;
        //protected int ArrayWidth
        //{
        //    get
        //    {
        //        if (_ArrayWidth == 0)
        //        {
        //            ArrayWidth = CalculateArrayWidth();
        //        }

        //        return _ArrayWidth;
        //    }
        //    set
        //    {
        //        _ArrayWidth = value;
        //    }
        //}



        public SimplexDataManager()
        {

        }

        //protected int CalculateArrayWidth()
        //{
        //    //todo
        //    return 0;
        //}

        //protected int CalculateArrayHeight()
        //{
        //    //todo
        //    return 0;
        //}


        protected bool isObjectFunctionSet()
        {
            return !String.IsNullOrEmpty(_ObjectiveFunction);
        }
        protected bool AreConstrainsSet()
        {
            return _constainList.Count() > 0 ? true : false;
        }

        protected void CreateConstrains()
        {
            //generate constrain object list
            foreach (var item in _constainList)
            {
                constrainList.Add(new NewConstrain(item, _constainList.Count));
            }
        }
        protected void CreateObjectiveFunctionObject()
        {
            ObjectiveFunction = new NewObjectiveFunction(_ObjectiveFunction, constrainList.Count);
        }

        public void Calculate()
        {
            if (isObjectFunctionSet() && AreConstrainsSet())
            {
                CreateConstrains();
                CreateObjectiveFunctionObject();
                var temp = new SimplexLineCollection(ObjectiveFunction, constrainList, TargetType);

                //ConstrainHandler = new ConstrainHandler(constrainList);
                SimplexCalculator = new SimplexCalculator(temp);
                SimplexCalculator.Work();
                //SimplexCalculator.Work();
            }
        }

        protected string RemoveTrailingSemicolon(string Input)
        {
            return Input.Remove(Input.Length - 1);
        }

        protected string RemoveBeginningPlus(string Input)
        {
            return Input.Substring(1);
        }


        public string RemoveUnessearryStringGarbage(string Input)
        {
            Input = Input.Replace("min:", "").Replace("max:", "").Replace(" ", "");
            Input = RemoveBeginningPlus(Input);
            Input = RemoveTrailingSemicolon(Input);

            return Input;
        }


        public void AddConstrain(string constrain)
        {
            _constainList.Add(RemoveUnessearryStringGarbage(constrain));
        }

        public void SetObjectiveFunction(string objectiveFunction)
        {

            if (objectiveFunction.Contains("min"))
            {
                TargetType = OptimisingType.min;

            }
            else if (objectiveFunction.Contains("max"))
            {
                TargetType = OptimisingType.max;
            }
            else
            {
                throw new UnkonwTargetType(objectiveFunction);
            }
            _ObjectiveFunction = RemoveUnessearryStringGarbage(objectiveFunction);
        }


        public void SetData(QuestionResult questionResult)
        {
            switch (questionResult.AnswerType)
            {
                case 0:
                    SetObjectiveFunction(questionResult.Data);
                    break;

                case 1:
                    AddConstrain(questionResult.Data);
                    break;

                default:
                    break;

            }
        }


    }
}
