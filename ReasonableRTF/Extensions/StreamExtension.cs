using ReasonableRTF.Helper;

namespace ReasonableRTF.Extensions
{
	/// <summary>
	/// Contains Extension Methods for <see cref="Stream"/>s.
	/// </summary>
	internal static class StreamExtension
	{
		/// <summary>
		/// Reads exactly bytesToRead out of stream, unless it is out of bytes.
		/// </summary>
		/// <exception cref="IOException"></exception>
		internal static void ReadAll(this Stream stream, byte[] buffer, int bytesToRead)
		{
			// NOTE: .NET versions 7 and later have a built-in method for this (ReadExactly), but earlier versions
			// and Framework require a custom-implemented one.
#if NET7_0_OR_GREATER
			stream.ReadExactly(buffer, 0, bytesToRead);
#else
			int bytesLeftToRead = bytesToRead;

			int totalBytesRead = 0;

			while (bytesLeftToRead > 0)
			{
				int bytesRead = stream.Read(buffer, totalBytesRead, bytesLeftToRead);
				if (bytesRead == 0) ThrowHelper.IOException("Unexpected end of stream.");

				totalBytesRead += bytesRead;
				bytesLeftToRead -= bytesRead;
			}
#endif
		}

		/// <summary>
		/// Converts the <paramref name="stream"/> to an <see cref="byte"/>[].
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/> to convert.</param>
		/// <returns>Returns the <see cref="byte"/>[] of the <paramref name="stream"/>.</returns>
		/// <exception cref="IOException"></exception>
		internal static byte[] ToBytes(this Stream stream)
		{
			byte[] bytes;
			if (stream is MemoryStream ms)
			{
				// call to throw if to large > 2GB
				_ = ms.GetLength(true);
				bytes = GetMemoryStreamBytes(ms);
			}
			else if (stream is FileStream fs)
			{
				// this logic differs a little bit from ReasonableRTF since it uses only the readable length
				// and don't assume the stream is at the beginning.
				int length = fs.GetLength(false);
				bytes = new byte[length];
				fs.ReadAll(bytes, length);
			}
			else
			{
				using (ms = new MemoryStream())
				{
					stream.CopyTo(ms);
					// call to throw if to large
					_ = ms.GetLength(true);
					bytes = GetMemoryStreamBytes(ms);
				}
			}
			return bytes;
		}

		/// <summary>
		/// Gets the remaining length of the <paramref name="stream"/>.
		/// </summary>
		/// <param name="stream">The <see cref="Stream"/>, which Length should be returned.</param>
		/// <param name="ignorePosition">Specifies if the Position in the <paramref name="stream"/> won't be used to calculate the remaining length.</param>
		/// <returns>Returns the remaining length of the <paramref name="stream"/>.</returns>
		/// <exception cref="IOException"></exception>
		internal static int GetLength(this Stream stream, bool ignorePosition)
		{
			long readableLength = stream.Length;
			if (!ignorePosition)
			{
				readableLength -= stream.Position;
			}
			if (readableLength > int.MaxValue)
			{
				ThrowHelper.IOException("Stream length was over 2 gigabytes. This is not supported.");
			}
			return (int)readableLength;
		}

		/// <summary>
		/// Gets the <see cref="byte"/>[] of the <paramref name="ms"/>.
		/// </summary>
		/// <param name="ms">The <see cref="MemoryStream"/>, which should be converted.</param>
		/// <returns>Returns the <paramref name="ms"/> as <see cref="byte"/>[].</returns>
		internal static byte[] GetMemoryStreamBytes(this MemoryStream ms)
		{
			// We don't support "virtual lower bounds" on arrays - only upper. I mean we could support lower, but
			// that's an extra bounds check on every access and all, so meh.
			if (ms.TryGetBuffer(out ArraySegment<byte> buffer) && buffer is { Array: not null, Offset: 0 })
			{
				return buffer.Array;
			}
			else
			{
				return ms.ToArray();
			}
		}
	}
}
