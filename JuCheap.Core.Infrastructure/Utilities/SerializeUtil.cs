using System.IO;
using System.Xml.Serialization;

namespace JuCheap.Core.Infrastructure.Utilities
{
    /// <summary>
    /// 序列化
    /// </summary>
    public class SerializeUtil
    {
        /// <summary>
        /// 从某一XML文件反序列化到某一类型
        /// </summary>
        /// <param name="filePath">待反序列化的XML文件名称</param>
        /// <returns></returns>
        public static T DeserializeFromXml<T>(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var fs = new FileStream(filePath, FileMode.Open);
                    using (var reader = new StreamReader(fs))
                    {
                        var xs = new XmlSerializer(typeof(T));
                        T ret = (T)xs.Deserialize(reader);
                        return ret;
                    }
                }
            }
            catch
            {
                //igorne
            }
            return default(T);
        }
    }
}
