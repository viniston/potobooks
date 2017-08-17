using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Photo.Business.Utilities.ObjectSerializer
{
	/// <summary>
	/// A class that provides with generic binary serialization and deserialization methods
	/// </summary>
	public static class ObjectSerializer
	{
		#region Public Methods

		/// <summary>
		/// Binary serializing for the passed object
		/// </summary>
		/// <param name="obj"></param>
		/// <returns>byte[]</returns>
		public static byte[] Serialize(object obj)
		{
			BinaryFormatter bf = new BinaryFormatter();
			MemoryStream ms = new MemoryStream();
			bf.Serialize(ms, obj);
			ms.Seek(0, SeekOrigin.Begin);
			return ms.ToArray();
		}

		/// <summary>
		/// Binary deserialization of the passed byte array into an object
		/// </summary>
		/// <param name="bytes"></param>
		/// <returns>object</returns>
		public static object Deserialize(byte[] bytes)
		{
			MemoryStream ms = new MemoryStream(bytes);
			BinaryFormatter bf = new BinaryFormatter();
			ms.Position = 0;
			return bf.Deserialize(ms);
		}
		
		#endregion
	}
}