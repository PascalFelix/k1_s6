using K1_S6.Classes;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace K1_S6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Test");
            var Reader = new FileReader("KI_5.txt");
            Reader.ReadFile();

            //foreach(var test in Reader)
            //{
            //    Console.WriteLine(test);
            //}

            var Mapper = new KiDataMapper(Reader);
            Mapper.Question += Mapper_Question;
            Mapper.Start();
            Mapper.SimplexDataManager.Calculate();

            Console.ReadLine();
        }

        public static int temp = 0;
        public static string[] answers = {"2", "0", "2", "1", "1", "1"};

        private static void Mapper_Question(object sender, Events.QuestionEventArgs e)
        {
            Console.WriteLine(e.Question);
            Console.WriteLine(e.Data);
           
            int awnser = int.Parse(answers[temp]);
            temp++;
            e.GenerateResult(awnser);
        }
    }
}
