using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.Compression
{
    class MyCompressorStream : Stream
    {
        private static readonly int m_compress = 1;
        private static readonly int m_decompress = 2;

        private const int m_BufferSize = 100;
        private byte[] m_bytesReadFromStream;
        private Queue<byte> m_queue;
        private MyMaze3DCompressor m_CompressorStream;
        private Stream m_io;
        private int m_mode;
        private FileStream fileInStream;

        /// <summary>
        /// constructor for myCompressor stream
        /// </summary>
        /// <param name="io">stream io</param>
        /// <param name="mode">mode of compress or decompress mode</param>
        public MyCompressorStream(Stream io, int mode)
        {
            this.m_io = io;
            this.m_mode = mode;
            m_bytesReadFromStream = new byte[m_BufferSize];
            m_queue = new Queue<byte>();
            m_CompressorStream = new MyMaze3DCompressor();
        }

        /// <summary>
        /// constructor of my compressor stream
        /// </summary>
        /// <param name="io">mode of compress or decompress mode</param>
        public MyCompressorStream(Stream io)
        {
            this.m_io = io;
            this.m_mode = 1;
            m_bytesReadFromStream = new byte[m_BufferSize];
            m_queue = new Queue<byte>();
            m_CompressorStream = new MyMaze3DCompressor();
        }



        /// <summary>
        /// get the mode - compress - 1 or decompress - 2
        /// </summary>
        public int Mode
        {
            get { return m_mode; }
            set { m_mode = value; }
        }

        /// <summary>
        /// get the compress
        /// </summary>
        public static int Compress
        {
            get
            {
                return m_compress;
            }
        }

        /// <summary>
        /// get the decompress
        /// </summary>
        public static int Decompress
        {
            get
            {
                return m_decompress;
            }
        }
        /// <summary>
        /// check if can read
        /// </summary>
        public override bool CanRead
        {
            get { return m_io.CanRead; }
        }

        /// <summary>
        /// check if can seek
        /// </summary>
        public override bool CanSeek
        {
            get { return m_io.CanSeek; }
        }

        /// <summary>
        /// check if can write
        /// </summary>
        public override bool CanWrite
        {
            get { return m_io.CanWrite; }
        }

        /// <summary>
        /// flush the io
        /// </summary>
        public override void Flush()
        {
            m_io.Flush();
        }

        /// <summary>
        /// read from the Disk
        /// </summary>
        /// <param name="buffer">buffer of bytes to read to</param>
        /// <param name="offset">count the offset</param>
        /// <param name="count">coount of how much</param>
        /// <returns>the bytecount in the read</returns>
        public override int Read(byte[] buffer, int offset, int count)
        {
            if (m_mode == m_compress)
            {
                // do it yourself...
                int r = 0;
                while (m_queue.Count < count && (r = m_io.Read(m_bytesReadFromStream, 0, m_BufferSize)) != 0)
                {
                    byte[] data = new byte[r];
                    for (int i = 0; i < r; data[i] = m_bytesReadFromStream[i], i++) ;
                    byte[] compressed = m_CompressorStream.compress(data);
                    foreach (byte b in compressed)
                    {
                        m_queue.Enqueue(b);
                    }
                }
                int bytesCount = Math.Min(m_queue.Count, count);
                for (int i = 0; i < bytesCount; i++)
                {
                    buffer[i + offset] = m_queue.Dequeue();
                }
                return bytesCount;
                //return -1;
            }
            else if (m_mode == m_decompress)
            {
                int r = 0;
                while (m_queue.Count < count && (r = m_io.Read(m_bytesReadFromStream, 0, m_BufferSize)) != 0)
                {
                    byte[] data = new byte[r];
                    for (int i = 0; i < r; data[i] = m_bytesReadFromStream[i], i++) ;
                    byte[] decompressed = m_CompressorStream.decompress(data);
                    foreach (byte b in decompressed)
                    {
                        m_queue.Enqueue(b);
                    }
                }
                int bytesCount = Math.Min(m_queue.Count, count);
                for (int i = 0; i < bytesCount; i++)
                {
                    buffer[i + offset] = m_queue.Dequeue();
                }
                MyMaze3DCompressor.counter = 0;
                return bytesCount;
            }
            return 0;
        }
        /// <summary>
        /// write to the Disk
        /// </summary>
        /// <param name="buffer">buffer of bytes to write for</param>
        /// <param name="offset">offset of the count</param>
        /// <param name="count">count of the write</param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            if (m_mode == m_compress)
            {
                byte[] data = new byte[count];
                for (int i = 0; i < count; data[i] = buffer[i + offset], i++) ;
                byte[] compressed = m_CompressorStream.compress(data);
                m_io.Write(compressed, 0, compressed.Length);
            }
            else
                 if (m_mode == m_decompress)
            {
                // do it yourself....
                byte[] dataForDecompress = new byte[count];
                for (int i = 0; i < count; dataForDecompress[i] = buffer[i + offset], i++) ;
                byte[] decompressed = m_CompressorStream.decompress(dataForDecompress);
                m_io.Write(decompressed, 0, decompressed.Length);
            }
        }

        #region NotInUse
        /// <summary>
        /// get the seek
        /// </summary>
        /// <param name="offset">offset of the seek</param>
        /// <param name="origin">seek Origion of the seek</param>
        /// <returns>return exception</returns>
        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// set the length
        /// </summary>
        /// <param name="value">param to set to</param>
        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// get the length
        /// </summary>
        public override long Length
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// get the position
        /// </summary>
        public override long Position
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
        #endregion

    }
}
