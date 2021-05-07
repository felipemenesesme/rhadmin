using System.Net;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace Api.Helpers
{
    public class JSONHelper
    {
        public static string GetJSONString(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            WebResponse response = request.GetResponse();

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader reader = new StreamReader(stream, Encoding.UTF8);

                return reader.ReadToEnd();
            }
        }

        public static T GetObjectFromJSONString<T>(string json) where T : new()
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));

                return (T)serializer.ReadObject(stream);
            }
        }

        public static T[] GetArrayFromJSONString<T>(string json) where T : new()
        {
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json)))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T[]));

                return (T[])serializer.ReadObject(stream);
            }
        }
    }
}