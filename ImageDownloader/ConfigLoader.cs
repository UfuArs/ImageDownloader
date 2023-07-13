using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownloader
{
    internal static class ConfigLoader
    {
        public static InputConfig Load()
        {
            using (StreamReader r = new StreamReader("./Input.json"))
            {
                string json = r.ReadToEnd();
                InputConfig? config = JsonConvert.DeserializeObject<InputConfig>(json);
                if (config is null) return new InputConfig();
                return config;
            }
        }
    }
}
