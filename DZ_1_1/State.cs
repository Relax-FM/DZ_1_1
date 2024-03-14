using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DZ_1_1
{
    internal class State
    {
        private static int Count = 0;
        private int id = 0;
        private List<double> Probabilities;
        private List<int> ProbabilitiesInt = new List<int>();

        public State(List<double> probabilities)
        {
            try
            {
                id = Count;
                Probabilities = new List<double>(probabilities);

                ProbabilitiesInt.Add(0);
                for (int i = 0; i < Probabilities.Count; i++)
                {
                    ProbabilitiesInt.Add(((int)(Probabilities[i] * 100.0)) + ProbabilitiesInt[i]);
                }
                if (ProbabilitiesInt.Count != Probabilities.Count + 1)
                {
                    throw new Exception("Ошибка в создании State");
                }


                Count++;
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
        }

        public int GetNextState(int rnd)
        {
            int nextStep = -1;
            try
            {
                for (int i = ProbabilitiesInt.Count - 1; i > 0; i--)
                {
                    if (rnd < ProbabilitiesInt[i]) nextStep = i - 1;
                }
                if ((rnd < 0) || (nextStep == -1))
                {
                    throw new Exception("Ошибка GetNextState");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
            }
            // Console.WriteLine(nextStep);
            return nextStep;
        }

        public int GetNextState_v2(int rnd)
        {
            int nextStep = -1;
            int nowSum = 0;
            try
            {
                for (int i = 0; i < Probabilities.Count; i++)
                {
                    double res = (Probabilities[i] * 1000.0);
                    int testing = (int)res;
                    int konets = testing % 10;
                    if (konets >= 8)
                    {
                        testing = (testing / 10) + 1;
                    }
                    else
                    {
                        testing = testing / 10;
                    }
                    nowSum += testing;
                    if (rnd < nowSum)
                    {
                        nextStep = i;
                        break;
                    }
                }
                if ((rnd < 0) || (nextStep == -1) || (rnd>=100))
                {
                    throw new Exception("Ошибка GetNextState");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            // Console.WriteLine(nextStep);
            return nextStep;
        }

        public int GetID()
        {
            return id;
        }

        public void Print()
        {
            Console.WriteLine($"State : {id}\nParams : ");
            Console.WriteLine($"Sum : {Probabilities.Sum()}");
            for (int i = 0; i < Probabilities.Count; i++)
            {
                Console.Write($"{i} : {Probabilities[i]}  ");
            }
            Console.WriteLine();
        }
    }
}
