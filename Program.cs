using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebAPIClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            try
            {
                MediathekClient client = new MediathekClient{title = "Sturm der Liebe", channel = "ndr"};
                var response = await client.query();
                // Console.WriteLine(response);
                foreach (var res in response.result.results)
                {
                    Console.WriteLine(res.title);
                    Console.WriteLine(res.channel);
                    Console.WriteLine(res.topic);
                    Console.WriteLine(res.timestamp);
                    Console.WriteLine(res.url_video_hd);
                    Console.WriteLine();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        
    }
}
