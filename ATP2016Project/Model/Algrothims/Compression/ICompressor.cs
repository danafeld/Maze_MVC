using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.Compression
{
    interface ICompressor
    {
        /// <summary>
        /// get the compress array in bytes
        /// </summary>
        /// <param name="data">array of bytes</param>
        /// <returns>compress array  of maze in bytes</returns>
        byte[] compress(byte[] data);
        /// <summary>
        /// get the decompress array in bytes
        /// </summary>
        /// <param name="data">array of bytes</param>
        /// <returns>decompress array  of maze in bytes</returns>
        byte[] decompress(byte[] data);
    }
}
