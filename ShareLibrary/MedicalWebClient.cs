using Newtonsoft.Json;
using ShareLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ShareLibrary
{
    public class MedicalWebClient
    {
        public string Host { get; private set; } = "http://193.112.19.82:8500";
        //public string Host { get; private set; } = "http://localhost:5000";
        private HttpClient _httpClient = new HttpClient();

        /// <summary>
        /// 提交生理参数到服务器
        /// </summary>
        /// <param name="bioValues"></param>
        /// <returns></returns>
        public async Task<BioValues> PostBioValuesAsync(BioValues bioValues)
        {
            try
            {
                string url = $"{Host}/api/biovalues";

                //json序列化
                string json = JsonConvert.SerializeObject(bioValues);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                //post提交数据到服务端
                HttpResponseMessage response = await _httpClient.PostAsync(url, content);

                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();

                var newBioValues = JsonConvert.DeserializeObject<BioValues>(result);

                return newBioValues;
            }
            catch (Exception ex)
            {
                //return ex.Message;
                throw;
            }
        }

        /// <summary>
        /// 从服务器获取生理参数
        /// </summary>
        /// <returns></returns>
        public async Task<List<BioValues>> GetBioValuesAsync()
        {
            try
            {
                string url = $"{Host}/api/biovalues";

                //从服务端get数据
                HttpResponseMessage response = await _httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string result = await response.Content.ReadAsStringAsync();

                var newBioValuesList = JsonConvert.DeserializeObject<List<BioValues>>(result);

                return newBioValuesList;
            }
            catch (Exception ex)
            {
                //return ex.Message;
                throw;
            }
        }
    }
}
