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

        public static Dictionary<string, string[]> fileList = new Dictionary<string, string[]>();
        public static string currentFile = "";
        public static int temp = 0;
        static void Main(string[] args)
        {
            Console.WriteLine("Test");

            fileList.Add("KI_2.txt", new string[] { "2", "0", "2", "1", "1", "1" });
            
          
            fileList.Add("KI_5.txt", new string[] { "2", "0", "2", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" });
            
            fileList.Add("KI_8.txt", new string[] { "2", "0", "2", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" });
            
            fileList.Add("KI_9.txt", new string[] { "2", "0", "2", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" });
            fileList.Add("KI_10.txt", new string[] { "2", "0", "2", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" });
            fileList.Add("KI_15.txt", new string[] { "2", "0", "2", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" });
            fileList.Add("KI_20.txt", new string[] { "2", "0", "2", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" });
            fileList.Add("KI_30.txt", new string[] { "2", "0", "2", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" });

            string[] files = new string[] { "KI_5.txt" };

            foreach (var file in fileList.Keys)
            {
                currentFile = file;
                Console.WriteLine(currentFile);
                var Reader = new FileReader(currentFile);
                Reader.ReadFile();



                var Mapper = new KiDataMapper(Reader);
                Mapper.Question += Mapper_Question;
                Mapper.Start();
                Mapper.SimplexDataManager.Calculate();
                temp = 0;
                Console.WriteLine("-------------------------");
            }


            Console.ReadLine();
        }

        private static void Mapper_Question(object sender, Events.QuestionEventArgs e)
        {
            int awnser = int.Parse(fileList[currentFile][temp]);
            temp++;
            e.GenerateResult(awnser);
        }
    }
}
