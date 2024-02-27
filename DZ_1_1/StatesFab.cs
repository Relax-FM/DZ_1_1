using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace DZ_1_1
{
    internal class StatesFab
    {
        private static int StatesCount = 0;
        private static int IterCount = 0;
        private static int StepCount = 0;
        private static Random random = new Random(); 

        private static State NowState;

        private static List<State> States = new List<State>();

        private static List<int> ListOfTransition = new List<int>();
        private static List<int> ListOfCounter = new List<int>();

        private static List<List<int>> ListOfListOfTransition = new List<List<int>>();
        private static List<List<int>> ListOfListOfCounter = new List<List<int>>();

        public static void SetParams(int statesCount, int iterCount, int stepCount)
        {
            StatesCount = statesCount;
            IterCount = iterCount;
            StepCount = stepCount;
        }

        public static void SetStatesParam(string path) 
        {
            int counter = 0;
            using (StreamReader sr = new StreamReader(@$"{path}"))
            {
                try
                {
                    string line = sr.ReadLine();
                    while (line != null)
                    {
                        counter++;
                        if (counter > StatesCount)
                        {
                            throw new Exception("Неправильно введены настройки");
                        }
                        
                        List<string> words = new List<string>(line.Replace('.', ',').Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries));
                        List<double> param = new List<double>();

                        double checker1 = 0.0;
                        int checker2 = 0;
                        foreach (string word in words)
                        {
                            checker1 += double.Parse(word);
                            checker2++;
                            param.Add(double.Parse(word));
                        }
                        if (checker1 != 1.0)
                        {
                            throw new Exception("Неправильные параметры вероятности (!=1)");
                        }
                        if(checker2 != StatesCount)
                        {
                            throw new Exception($"Неправильные параметры вероятности (!={StatesCount})");
                        }

                        States.Add(new State(param));

                        line = sr.ReadLine();
                    }
                }
                catch (Exception ex) 
                {
                    Console.WriteLine(ex.Message);
                }

            }
            PrintAll();

        }

        private static void GenStartPosition()
        {
            int startPose = random.Next(0, StatesCount);
            Console.WriteLine($"\nСтартовая позиция: {startPose}");
            NowState = States[startPose];
            // NowState.Print();

        }

        private static void Step()
        {
            int genNum = random.Next(0, 100);
            int newNowState = NowState.GetNextState(genNum);
            NowState = States[newNowState];
            Console.WriteLine($"Новая позиция: {newNowState}");
        }

        public static void Play()
        {
            for (int i = 0; i < IterCount; i++)
            {
                ListOfCounter.Clear();
                ListOfTransition.Clear();
                // Отчистили листы


                // Создали лист с StatesCount счетчиками
                for (int j = 0; j < StatesCount; j++)
                {
                    ListOfCounter.Add(0);
                }

                // Рандомно выбрали начальную позицию и записали ее во все списки
                GenStartPosition();
                ListOfCounter[NowState.GetID()]++;
                ListOfTransition.Add(NowState.GetID());

                for (int j = 0; j < StepCount; j++)
                {
                    Step();
                    ListOfCounter[NowState.GetID()]++;
                    ListOfTransition.Add(NowState.GetID());
                }

                // Запомнили листы в листы листов
                ListOfListOfCounter.Add(new List<int>(ListOfCounter));
                ListOfListOfTransition.Add(new List<int>(ListOfTransition));
            }

        }

        public static void PrintCounter(string path)
        {
            using (StreamWriter file = new StreamWriter(@$"{path}"))
            {
                foreach (List<int> listofcounter in ListOfListOfCounter)
                {
                    foreach (int counter in listofcounter)
                    {
                        file.Write(counter);
                        file.Write('\t');
                    }
                    file.WriteLine();
                }
            }
        }

        public static void PrintCounterDel(string path)
        {
            using (StreamWriter file = new StreamWriter(@$"{path}"))
            {
                foreach (List<int> listofcounter in ListOfListOfCounter)
                {
                    foreach (int counter in listofcounter)
                    {
                        double del = (double)(counter) / 100.0;
                        file.Write(del);
                        file.Write('\t');
                    }
                    file.WriteLine();
                }
            }
        }

        public static void PrintTransition(string path)
        {
            using (StreamWriter file = new StreamWriter(@$"{path}"))
            {
                int count = 0;
                foreach (List<int> listoftransition in ListOfListOfTransition)
                {
                    file.WriteLine(count);
                    count++;
                    int x = 0;
                    foreach (int transition in listoftransition)
                    {
                        file.Write(x);
                        file.Write('\t');
                        file.Write(transition);
                        file.WriteLine();
                        x++;
                    }
                    file.WriteLine();
                }
            }
        }

        public static void PrintAll() 
        {
            foreach (State state in States)
            {
                state.Print();
            }

        }
    }
}
