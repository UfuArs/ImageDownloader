using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloader
{
    public class DownloadCompletedEventArgs : EventArgs
    {
        public int Index { get; set; }

    }
}
