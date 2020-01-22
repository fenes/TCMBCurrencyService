using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace TCMBCurrencyService.Util
{
    public static class XmlUtil
    {
        public static string ToXML<T>(this T obj)
        {
            using (var stringWriter = new StringWriter(new StringBuilder()))
            {
                var xmlSerializer = new XmlSerializer(obj.GetType());
                xmlSerializer.Serialize(stringWriter, obj);
                return stringWriter.ToString();
            }
        }

        public static bool IsValidXml(string strInput)
        {
            if (!string.IsNullOrEmpty(strInput) && strInput.TrimStart().StartsWith("<"))
                try
                {
                    var doc = XDocument.Parse(strInput);
                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }

            return false;
        }
    }
}