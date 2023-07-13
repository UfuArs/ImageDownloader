// See https://aka.ms/new-console-template for more information
using ImageDownloader;
using System.ComponentModel;
using System.Drawing;
using System.Net;
using System.Net.Sockets;


Console.WriteLine("App Started..");
int i = 0;

InputConfig config = ConfigLoader.Load();

Downloader downloader = new Downloader(config);
downloader.RefreshScreen += Downloader_RefreshScreen;

Console.CancelKeyPress += delegate (object? sender, ConsoleCancelEventArgs e)
{
    e.Cancel = true;
    downloader.CancelJobs();
    Console.WriteLine("App Canceled..");
};

void Downloader_RefreshScreen(object? sender, EventArgs e)
{
    i++;
    Console.WriteLine("Downloaded: " + i.ToString());
}

downloader.Start();
Console.WriteLine("Download Completed...");
Console.ReadLine();
