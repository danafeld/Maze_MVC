using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATP2016Project.Model.Algrothims.Compression
{
    class MyMaze3DCompressor : ICompressor
    {
        public static int counter = 0;
        /// <summary>
        /// get the compress length
        /// </summary>
        public static int Compress { get; internal set; }
        /// <summary>
        /// get the decompress length
        /// </summary>
        public static int Decompress { get; internal set; }

        /// <summary>
        /// get the compress array in bytes
        /// </summary>
        /// <param name="data">array of bytes</param>
        /// <returns>compress array  of maze in bytes</returns>
        public byte[] compress(byte[] data)
        {
            byte[] pointsAndSizes = getPointsAndSizes(data); // get the sizes and points
            byte toCompare = 1;
            byte counter = 0;
            List<byte> compressed = new List<byte>();
            compressed.AddRange(pointsAndSizes); // add the sizes and points
            if (data[9] == 0)
            {
                counter++;
                toCompare = data[9];
            }
            if (counter == 0)
                compressed.Add((byte)0); // add byte 0
            counter = 0;
            for (int i = 9; i < data.Length; i++)
            {
                if (data[i] == toCompare)
                {
                    if (counter == 255)
                    {
                        compressed.Add(counter);//add 255
                        compressed.Add((byte)0); // add 0
                        counter = 0;
                    }
                    counter++;
                }
                else
                {
                    compressed.Add(counter); // add numbers of show up
                    counter = 0;
                    toCompare = data[i]; // change the data
                    counter++;
                }
            }
            compressed.Add(counter);
            return compressed.ToArray();
        }

        /// <summary>
        /// get the decompress array in bytes
        /// </summary>
        /// <param name="data">array of bytes</param>
        /// <returns>decompress array  of maze in bytes</returns>
        public byte[] decompress(byte[] data)
        {
            int j;
            List<byte> decompressed = new List<byte>();
            if (counter == 0)
            {
                byte[] pointsAndSizes = getPointsAndSizes(data);
                decompressed.AddRange(pointsAndSizes); // add the sizes and goal point and start point
                counter++;
                addTodecompressd(decompressed, data, 9);
            }
            else
            {
                addTodecompressd(decompressed, data, 0);
            }
            return decompressed.ToArray();
        }

        /// <summary>
        /// get the start point and the goal point and the lengths of the maze
        /// </summary>
        /// <param name="data">byte array of start point, goal point, and sizes of array</param>
        /// <returns>byte array of points and sizes</returns>
        private byte[] getPointsAndSizes(byte[] data)
        {
            byte[] pointsAndSizes = new byte[9];
            for (int i = 0; i < 9; i++)
            {
                pointsAndSizes[i] = data[i]; // get the sizes and positions
            }
            return pointsAndSizes;
        }

        /// <summary>
        /// help function add to decompressed
        /// </summary>
        /// <param name="decompressed">list byte decompressed</param>
        /// <param name="data">byte data of 100</param>
        private void addTodecompressd(List<byte> decompressed, byte[] data, int startPoint)
        {
            int j;
            for (int i = startPoint; i < data.Length; i++)
            {
                if (i % 2 == 0)
                {
                    for (j = 0; j < data[i]; j++) // add 1 byte
                    {
                        decompressed.Add((byte)1);
                    }
                }
                else
                {
                    for (j = 0; j < data[i]; j++) // add 0 byte
                    {
                        decompressed.Add((byte)0);
                    }
                }
            }
        }
    }
}
