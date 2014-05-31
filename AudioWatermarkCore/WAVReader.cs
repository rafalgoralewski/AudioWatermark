using AudioWatermarkCore.Domain;
using AudioWatermarkCore.Domain.Chunks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkCore
{
    public class WAVReader
    {
        public WAVFile ReadFile(string fileName)
        {
            WAVFile result = new WAVFile();
            string rawData;

            using (StreamReader reader = new StreamReader(fileName))
            {
                rawData = reader.ReadToEnd();
            }

            // Header
            result.Header = new HeaderChunk()
            {
                ChunkId = rawData.Substring(0, 4), 
                FileLength = stringLETo32uInt(rawData.Substring(4,4)), 
                Type = rawData.Substring(8, 4)
            };
                
            // Chunks
            int i = 12;
            while (i < result.Header.FileLength)
            {
                string chunkId = rawData.Substring(i, 4);
                uint chunkSize = stringLETo32uInt(rawData.Substring(i + 4, 4));
                uint fullChunkSize = chunkSize + 8;

                // Format
                if (chunkId.Equals("fmt "))
                {
                    result.Format = new FormatChunk()
                    {
                        ChunkId = chunkId,
                        ChunkSize = chunkSize,
                        FormatTag = stringLETo16uInt(rawData.Substring(i + 8, 2)),
                        Channels = stringLETo16uInt(rawData.Substring(i + 10, 2)),
                        SamplesPerSec = stringLETo32uInt(rawData.Substring(i + 12, 4)),
                        AvgBytesPerSec = stringLETo32uInt(rawData.Substring(i + 16, 4)),
                        BlockAlign = stringLETo16uInt(rawData.Substring(i + 20, 2)),
                        BitsPerSample = stringLETo32uInt(rawData.Substring(i + 22, 4))
                    };
                }

                // Fact
                if (chunkId.Equals("fact"))
                {
                    result.Fact = new FactChunk()
                    {
                        ChunkID = chunkId,
                        ChunkSize = chunkSize,
                        NumSamples = stringLETo32uInt(rawData.Substring(i + 8, 4))
                    };
                }

                // Data
                if (chunkId.Equals("data"))
                {
                    DataChunk data = new DataChunk()
                    {
                        ChunkID = chunkId,
                        ChunkSize = chunkSize,
                        Data = rawData.Skip(i + 8).Select(c => (int) c).ToList()
                    };
                }

                i += int.Parse(fullChunkSize.ToString());
            }
            

            return result;
        }

        private uint stringLETo32uInt(string input)
        {
            byte[] array = Encoding.ASCII.GetBytes(input.ToArray());                
            return BitConverter.ToUInt32(array, 0);
        }

        private ushort stringLETo16uInt(string input)
        {
            byte[] array = Encoding.ASCII.GetBytes(input.ToArray());
            return BitConverter.ToUInt16(array, 0);
        }
    }
}
