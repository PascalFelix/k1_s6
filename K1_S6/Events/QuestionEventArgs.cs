using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Events
{
    class QuestionEventArgs : EventArgs
    {
        public string Question { get; set; }
        public string Data { get; set; }

        public QuestionResult QuestionResult { get; set; }

        public void GenerateResult(int answer)
        {
            QuestionResult = new QuestionResult(Data) { AnswerType = answer };
        }

    }
}

