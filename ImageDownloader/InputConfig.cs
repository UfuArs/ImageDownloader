using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloader
{
    internal class InputConfig
    {
        /*private int _count;
        private int _parallelism;
        private string _savePath = "";
        private string _randomImageUrl = "";
        */
        public int Count { get; set; }
        public int Parallelism { get; set; }
        public string SavePath { get; set; }
        public string RandomImageUrl { get; set; }
    }
}
