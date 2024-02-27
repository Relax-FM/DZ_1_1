using System;

namespace DZ_1_1
{
    class Programm
    {
        static void Main(string[] args)
        {
            StatesFab.SetParams(5, 50, 100);
            StatesFab.SetStatesParam("data.txt");
            StatesFab.Play();
            StatesFab.PrintCounter("out_data_counter.txt");
            StatesFab.PrintCounterDel("out_data_counter_del.txt");
            StatesFab.PrintTransition("out_data_transition.txt");
        }
    }
}