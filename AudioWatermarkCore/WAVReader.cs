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

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                //result.Header.RawData = reader.ReadBytes(12);
                result.Header.Quick = reader.ReadBytes(106);
                result.Data.ChunkID = reader.ReadBytes(4);
                result.Data.ChunkSize = reader.ReadBytes(4);

                result.Data.SoundData = reader.ReadBytes(result.Data.ChunkSize.ToInt()).ToList();
            }

            // Chunks
            //int i = 12;
            //while (i < result.Header.FileLength.ToInt())
            //{
            //    uint chunkId = stringLETo32uInt(rawData.Substring(i, 4));
            //    uint chunkSize = stringLETo32uInt(rawData.Substring(i + 4, 4));
            //    uint fullChunkSize = chunkSize + 8;

            //    // Format "fmt "
            //    if (chunkId.Equals(0x20746d66))
            //    {
            //        result.Format = new FormatChunk()
            //        {
            //            ChunkId = chunkId,
            //            ChunkSize = chunkSize,
            //            FormatTag = stringLETo16uInt(rawData.Substring(i + 8, 2)),
            //            Channels = stringLETo16uInt(rawData.Substring(i + 10, 2)),
            //            SamplesPerSec = stringLETo32uInt(rawData.Substring(i + 12, 4)),
            //            AvgBytesPerSec = stringLETo32uInt(rawData.Substring(i + 16, 4)),
            //            BlockAlign = stringLETo16uInt(rawData.Substring(i + 20, 2)),
            //            BitsPerSample = stringLETo32uInt(rawData.Substring(i + 22, 4))
            //        };
            //    }

            //    // Fact "fact"
            //    if (chunkId.Equals(0x00)) //ignore
            //    {
            //        result.Fact = new FactChunk()
            //        {
            //            ChunkID = chunkId,
            //            ChunkSize = chunkSize,
            //            NumSamples = stringLETo32uInt(rawData.Substring(i + 8, 4))
            //        };
            //    }

            //    // SoundData "data"
            //    if (chunkId.Equals(0x61746164))
            //    {
            //        DataChunk data = new DataChunk()
            //        {
            //            ChunkID = chunkId,
            //            ChunkSize = chunkSize,
            //            SoundData = rawData.Skip(i + 8).Select(c => (byte) c).ToList()
            //        };

            //        result.Data = data;
            //        break;
            //    }

            //    i += int.Parse(fullChunkSize.ToString());
            //}            

            return result;
        }

        public void SaveFile(WAVFile file, string filename)
        {
            using (BinaryWriter stream = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                //stream.Write(file.Header.ChunkId);
                //stream.Write(file.Header.FileLength);
                //stream.Write(file.Header.Type);

                //stream.Write(file.Format.ChunkId);
                //stream.Write(file.Format.ChunkSize);
                //stream.Write(file.Format.FormatTag);
                //stream.Write(file.Format.Channels);
                //stream.Write(file.Format.SamplesPerSec);
                //stream.Write(file.Format.AvgBytesPerSec);
                //stream.Write(file.Format.BlockAlign);
                //stream.Write(file.Format.BitsPerSample);
                
                //stream.Write(file.Fact.ChunkID);
                //stream.Write(file.Fact.ChunkSize);
                //stream.Write(file.Fact.NumSamples);


                ///////////quick
                stream.Write(file.Header.Quick);

                stream.Write(file.Data.ChunkID);
                stream.Write(file.Data.ChunkSize);

                foreach (var sample in file.Data.SoundData)
                {
                    stream.Write(sample);
                }

                stream.Flush();
            }
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
