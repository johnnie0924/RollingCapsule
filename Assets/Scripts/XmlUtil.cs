using System.IO;
using System.Xml;
using System.Xml.Serialization;

public class XmlUtil {

	public static void Seialize<T>(string filename, T data) {
		using ( var stream = new FileStream(filename, FileMode.Create) ) {
			var serializer = new XmlSerializer(typeof(T));
			serializer.Serialize(stream, data);
		}
	}
	
	public static T Deserialize<T>(string filename) {
		using ( var stream = new FileStream(filename, FileMode.Open) ) {
			var serializer = new XmlSerializer(typeof(T));
			return (T)serializer.Deserialize(stream);
		}
	}
}