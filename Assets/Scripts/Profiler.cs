using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    internal class Profiler
    {
        public Profiler(string name0)
        {
            //Debug.Log("Hello");
            beginMeasure(name0);
        }

        ~Profiler()
        {
            endMeasure();
        }

        public void measureDeltaTime(string name)
        {
            endMeasure();
            beginMeasure(name);
        }

        private void beginMeasure(string name)
        {
            watch = System.Diagnostics.Stopwatch.StartNew();
            measureName = name;
        }

        private void endMeasure()
        {
            watch.Stop();
            var dt = watch.ElapsedMilliseconds;
            Debug.Log(measureName + " " + (dt).ToString("f6"));
        }

        private System.Diagnostics.Stopwatch watch;
        private string measureName;
    }
}


