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

        public int GetID()
        {
            return id;
        }

        public void Print()
        {
            Console.WriteLine($"State : {id}\nParams : \n0 : {Probabilities[0]} 1 : {Probabilities[1]} 2 : {Probabilities[2]} 3 : {Probabilities[3]} 4 : {Probabilities[4]}");
        }
    }
}
