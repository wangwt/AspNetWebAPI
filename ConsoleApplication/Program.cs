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
    class Program
    {

          //curl -X POST \
          //-H "X-AVOSCloud-Application-Id: hsxrrj2ebcqypzq535wt30bsqjdm9xta4uyjsrcz80r1iopr" \
          //-H "X-AVOSCloud-Application-Key: u5f9ekwbl4joqazrbre90d3txh1mqggavlnv0c4sf7r7bkpk" \
          //-H "Content-Type: application/json" \
          //-d '{"mobilePhoneNumber": "186xxxxxxxx"}' \
          //https://leancloud.cn/1.1/requestSmsCode

        static void Main(string[] args)
        {
            //HttpClient client = new HttpClient();
            //client.BaseAddress = new Uri("https://leancloud.cn/");
            //client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //client.DefaultRequestHeaders.Add("x-avoscloud-application-id", "hsxrrj2ebcqypzq535wt30bsqjdm9xta4uyjsrcz80r1iopr");
            //client.DefaultRequestHeaders.Add("x-avoscloud-application-key", "u5f9ekwbl4joqazrbre90d3txh1mqggavlnv0c4sf7r7bkpk");
            ////HTTP Post请求 
            //var gizmo = new { username = "cooldude22", password = "p_n7!-e8", phone = "415-392-0202", test = "trest" }; //new { score=1337,playerName="Sean Plott",cheatMode=false};
            //Uri gizmoUri = null;

            //HttpResponseMessage response =  client.PostAsJsonAsync("1.1/users", gizmo).Result;

            //if (response.IsSuccessStatusCode)
            //{
            //    gizmoUri = response.Headers.Location;
            //    Console.WriteLine("添加成功");
            //}
            //else
            //{
            //    //var products = response.Content.ReadAsAsync<IEnumerable<ErrorModel>>().Result; 
            //    Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            //}
            dooPost();
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine("时间："+i);
                Thread.Sleep(1000);
            }
               
            Console.WriteLine("完了");
        }

        /// <summary>
        /// HttpClient实现Post请求
        /// </summary>
        static async void dooPost()
        {
            string url = "http://developer.51cto.com/art/201407/445547.htm";
            //设置HttpClientHandler的AutomaticDecompression
            var handler = new HttpClientHandler() { AutomaticDecompression = DecompressionMethods.GZip };
         
            //创建HttpClient（注意传入HttpClientHandler）
            using (var http = new HttpClient(new HtmlTextHandler()))
            {
                
                //await异步等待回应
                try
                {
                    var response = await http.GetStringAsync(url);

                    Console.WriteLine(response);
                   
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

        }
    }

    class HtmlTextHandler : HttpClientHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);

            var contentType = response.Content.Headers.ContentType;
            contentType.CharSet = await getCharSetAsync(response.Content);

            return response;
        }

        private async Task<string> getCharSetAsync(HttpContent httpContent)
        {
            var charset = httpContent.Headers.ContentType.CharSet;
            if (!string.IsNullOrEmpty(charset))
                return charset;

            var content = await httpContent.ReadAsStringAsync();
            var match = Regex.Match(content, @"charset=(?<charset>.+?)""", RegexOptions.IgnoreCase);
            if (!match.Success)
                return charset;

            return match.Groups["charset"].Value;
        }
    }

    public class ErrorModel
    {
        public string code { get; set; }
        public string error { get; set; }
    }
}
