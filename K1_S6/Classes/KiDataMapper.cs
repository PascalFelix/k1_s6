using K1_S6.Events;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6.Classes
{
    class KiDataMapper
    {
        public delegate void QuestionEventHandler(Object sender, QuestionEventArgs e);
        public event QuestionEventHandler Question;
        protected virtual void OnQuestion(QuestionEventArgs e)
        {
            QuestionEventHandler handler = Question;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        public FileReader FileReader { get; set; }

        private SimplexDataManager _SimplexDataManager = new SimplexDataManager();

        public SimplexDataManager SimplexDataManager { get { return _SimplexDataManager; } }


        public KiDataMapper(FileReader fileReader)
        {
            FileReader = fileReader;


        }

        private bool AreObjectsSubscribed()
        {
            return Question.GetInvocationList().Length < 1 ? false : true;
        }

        public void Start()
        {
            try
            {
                if (!AreObjectsSubscribed())
                {
                    throw new Exception("No objects are subscribed to Question");
                }

                foreach(var data in FileReader)
                {
                    var QuestionArgs = new QuestionEventArgs() { Data = data, Question = "Ist folgende Zeile die Objective Function(0)," +
                        " ein Constain(1) oder zu ignorieren(2)" };

                    OnQuestion(QuestionArgs);
                    if(QuestionArgs.QuestionResult == null)
                    {
                        throw new Exception("No result");
                    }
                    else
                    {
                        SimplexDataManager.SetData(QuestionArgs.QuestionResult);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }

        public void Test()
        {
            OnQuestion(new QuestionEventArgs { Data = "test", Question = "Test2" });
        }



    }
}
