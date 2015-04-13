using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication
{
    /// <summary>
    /// V0.1.1
    /// </summary>
    class Program
    {


        static void Main(string[] args)
        {
            doo("http://dwz.cn/create.php");
            System.Threading.Thread.Sleep(-1);
        }

        static async void doo(string url)
        {
            //设置HttpClientHandler的AutomaticDecompression
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };

            //创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient(handler))
            {
                //使用FormUrlEncodedContent做HttpContent
                var content = new FormUrlEncodedContent(new Dictionary<string, string>()
                {
                    {"url", "http://www.cnblogs.com/fish-li/archive/2011/11/20/2256385.html"}
                });

                //await异步等待回应
                var response = await http.PostAsync(url, content);

                var contacts = await response.Content.ReadAsStringAsync(); 

                var ret = JsonConvert.DeserializeObject<ResultModel>(contacts);

                //await异步读取最后的JSON（注意此时gzip已经被自动解压缩了，因为上面的AutomaticDecompression = DecompressionMethods.GZip）
                Console.WriteLine();
            }
        }
    }

    public class ResultModel
    {
        public string tinyurl { get; set; }
        public string status { get; set; }
        public string longurl { get; set; }
        public string err_msg { get; set; }
    }
}
