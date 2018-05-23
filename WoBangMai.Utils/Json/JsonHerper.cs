using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

namespace WoBangMai.Utils
{
    public static class JsonHerper
    {

        /// <summary>
        /// 序列化Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(this object target)
        {
            T result = (T)target;
            DataContractJsonSerializer json = new DataContractJsonSerializer(result.GetType());
            using (MemoryStream stream = new MemoryStream())
            {
                json.WriteObject(stream, result);
                return Encoding.UTF8.GetString(stream.ToArray());
            }
        }

        /// <summary>
        /// 反序列化Json
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(this string jsonString)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(T));
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.UTF8.GetBytes(jsonString)))
            {
                return (T)serializer.ReadObject(ms);
            }
        }

        /// <summary>
        /// 转化时间格式
        /// </summary>
        public class DateTimeConverter : DateTimeConverterBase
        {
            private static IsoDateTimeConverter dtConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                return dtConverter.ReadJson(reader, objectType, existingValue, serializer);
            }
            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                dtConverter.WriteJson(writer, value, serializer);
            }
        }


        public static string ConvertToJson(object obj)
        {
            System.Web.Script.Serialization.JavaScriptSerializer javascriptserializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            return javascriptserializer.Serialize(obj);
        }


    }

    public class LimitPropsContractResolver : DefaultContractResolver
    {
        string[] props = null;
        bool retain;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="props">传入的属性数组</param>
        /// <param name="retain">true:表示props是需要保留的字段  false：表示props是要排除的字段</param>
        public LimitPropsContractResolver(string[] props, bool retain = true)
        {
            //指定要序列化属性的清单
            this.props = props;

            this.retain = retain;
        }
        protected override IList<JsonProperty> CreateProperties(Type type,
        MemberSerialization memberSerialization)
        {
            IList<JsonProperty> list =
            base.CreateProperties(type, memberSerialization);
            //只保留清单有列出的属性
            return list.Where(p =>
            {
                if (retain)
                    return props.Contains(p.PropertyName);
                else
                    return !props.Contains(p.PropertyName);
            }).ToList();
        }
    }
}
