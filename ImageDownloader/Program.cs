// See https://aka.ms/new-console-template for more information
using ImageDownloader;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;


Console.WriteLine("App Started..");

InputConfig config = ConfigLoader.Load();

Downloader downloader = new Downloader(config);
downloader.RefreshScreen += Downloader_RefreshScreen;

Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
{
    e.Cancel = true;
    downloader.CancelJobs();
    Console.WriteLine("App Canceled..");
};

Console.WriteLine("Downloading {0} images ({1} parallel downloads at most)", config.Count, config.Parallelism);
Console.Write("Downloaded Image Count : 0");

void Downloader_RefreshScreen(object? sender, DownloadCompletedEventArgs e)
{
    int maxProgressChunk = config.Count.ToString().Length;
    Console.SetCursorPosition(25, Console.CursorTop);
    Console.Write(e.Index);
    Console.SetCursorPosition(25, Console.CursorTop);
}

downloader.Start();
Console.WriteLine("Download Completed...");
Console.ReadLine();
