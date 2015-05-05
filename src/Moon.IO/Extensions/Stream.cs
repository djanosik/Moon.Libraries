using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Moon.Threading.Tasks;

namespace Moon.IO
{
    /// <summary>
    /// <see cref="Stream" /> extension methods.
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// Reads all bytes from the stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public static byte[] ReadAllBytes(this Stream input)
        {
            var buffer = new byte[16 * 1024];
            using (var memoryStream = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    memoryStream.Write(buffer, 0, read);
                }
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// Asynchronously reads all bytes from the stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        public static Task<byte[]> ReadAllBytesAsync(this Stream input)
            => input.ReadAllBytesAsync(CancellationToken.None);

        /// <summary>
        /// Asynchronously reads all bytes from the stream.
        /// </summary>
        /// <param name="input">The input stream.</param>
        /// <param name="cancellationToken">
        /// A cancellation token to observe while waiting for the task to complete.
        /// </param>
        public static async Task<byte[]> ReadAllBytesAsync(this Stream input, CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return await new CanceledTask<byte[]>();
            }

            var buffer = new byte[16 * 1024];
            using (var memoryStream = new MemoryStream())
            {
                int read;
                while ((read = await input.ReadAsync(buffer, 0, buffer.Length, cancellationToken)) > 0)
                {
                    await memoryStream.WriteAsync(buffer, 0, read, cancellationToken);
                }
                return memoryStream.ToArray();
            }
        }
    }
}