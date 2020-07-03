using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Events
{
    class QuestionResult
    {

        public string Data { get; }

        private int _AnswerType = 0;
        public int AnswerType
        {
            get
            {
                return _AnswerType;
            }
            set
            {
                _AnswerType = value;
                if (value < 0 || value > 2)
                {
                    throw new Exception("_AnswerType out of range");
                }
            }
        }
        public QuestionResult(string data)
        {
            Data = data;
        }
    }
}
