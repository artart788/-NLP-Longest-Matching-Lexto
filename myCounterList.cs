using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LextoConsole
{
    class myCounterList
    {
        public List<myCounter> MyCounters { get; set; } = new List<myCounter>();

        public void AddElement(string s)
        {
            int indexx = 0;
            int isDup = 0;
            foreach (var VARIABLE in MyCounters)
            {
                if (VARIABLE.words == s)
                {
                    isDup = 1;
                    MyCounters[indexx].count ++;
                    break;
                }
                ++indexx;
            }
            if (isDup != 1)
            {
                MyCounters.Add(new myCounter()
                {
                    count = 1,
                    words = s
                });
            }
            
        }

        public string getReport()
        {
            List<myCounter> export = MyCounters.OrderByDescending(o => o.count).ToList();
            string s = "";
            foreach (var myCounter in export)
            {
                s += "Word: " + myCounter.words + " | Count: " + myCounter.count + "\n";
            }

            return s;
        }
    }
}
