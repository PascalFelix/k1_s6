using K1_S6.Events;
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


        public SimplexDataManager()
        {

        }

        public void AddConstrain(string constrain)
        {
            _constainList.Add(constrain);
        }

        public void SetObjectiveFunction(string objectiveFunction)
        {
            _ObjectiveFunction = objectiveFunction;
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
