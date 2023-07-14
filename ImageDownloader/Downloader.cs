using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloader
{
    internal class Downloader
    {
        private InputConfig _config;

        static bool Locker;
        static int _downloadedCount = 0;
        static int _remainDownloadCount = 0;
        static int _maxCountFlag = 0;
        private bool _isCancelJobs = false;

        public delegate void DownloadCompleted(int count);
        public event EventHandler<DownloadCompletedEventArgs> RefreshScreen;

        public Downloader(InputConfig config)
        {
            this._config = config;
            _remainDownloadCount = this._config.Count;
            Locker = false;
        }
        public void Start()
        {
            if (!Directory.Exists(this._config.SavePath))
            {
                Directory.CreateDirectory(this._config.SavePath);
            }

            while (_remainDownloadCount > 0)
            {
                if (!Locker)
                {
                    Locker = true;
                    int maxItemCount = _remainDownloadCount < this._config.Parallelism ? _downloadedCount + _remainDownloadCount : _downloadedCount + this._config.Parallelism;
                    Parallel.For(_downloadedCount, maxItemCount, number =>
                    {
                        DownloadFile(number);
                    });
                }
            }
        }

        private string GetFileName(int number)
        {
            number++;
            return this._config.SavePath + "\\" + number.ToString() + ".png";
        }
        private void DownloadFile(int number)
        {
            using (var webClient = new WebClient())
            {
                webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(WebClient_DownloadFileCompleted);
                webClient.DownloadFileAsync(new Uri(_config.RandomImageUrl), GetFileName(number));

                if (this._isCancelJobs)
                {
                    webClient.CancelAsync();
                    webClient.Dispose();
                }
            }
            while (Locker) ;
        }

        public void CancelJobs()
        {
            this._isCancelJobs = true;
            _remainDownloadCount = 0;
            Locker = true;
            Directory.Delete(this._config.SavePath, true);
        }
        private void WebClient_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

            if (!e.Cancelled)
            {
                _downloadedCount++;
                _remainDownloadCount--;
                _maxCountFlag++;
            }

            if (_maxCountFlag == this._config.Parallelism)
            {
                _maxCountFlag = 0;
                Locker = false;
            }
            else
            {
                Locker = true;
            }

            RefreshScreen?.Invoke(this, new DownloadCompletedEventArgs { Index = _downloadedCount});
        }
    }
}
