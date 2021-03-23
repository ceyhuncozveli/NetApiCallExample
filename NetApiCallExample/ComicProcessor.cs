using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace NetApiCallExample
{
    public class ComicProcessor
    {
        //public int MaxComicNumber { get; set; }

        public static async Task<ComicModel> LoadComic(int comicNumber = 0)
        {
            string url = "";

            if (comicNumber > 0)
            {
                url = $"http://xkcdd.com/{ comicNumber }/info.0.json";
            }
            else
            {
                url = "http://xkcdd.com/info.0.json";
            }

            using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    ComicModel comic = await response.Content.ReadAsAsync<ComicModel>();

                    //if (comicNumber == 0)
                    //{
                    //    MaxComicNumber = comic.Num;
                    //}

                    return comic;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
