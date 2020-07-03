using K1_S6.Classes;
using System;
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

            Console.ReadLine();
        }

        private static void Mapper_Question(object sender, Events.QuestionEventArgs e)
        {
            Console.WriteLine(e.Question);
            Console.WriteLine(e.Data);

            int awnser = int.Parse(Console.ReadLine());
            e.GenerateResult(awnser);
        }
    }
}
