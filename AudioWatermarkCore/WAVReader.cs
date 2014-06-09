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
            const int headerLength = 38;
            int dataoffset;

            using (StreamReader reader = new StreamReader(fileName))
            {
                dataoffset = reader.ReadToEnd().IndexOf("data");
            }

            using (BinaryReader reader = new BinaryReader(File.Open(fileName, FileMode.Open)))
            {
                // header
                result.Header.RawData = reader.ReadBytes(headerLength);

                // skip
                result.Header.Others = reader.ReadBytes(dataoffset - headerLength).ToList();
                
                // data
                result.Data.ChunkID = reader.ReadBytes(4);
                result.Data.ChunkSize = reader.ReadBytes(4);
                result.Data.SoundData = reader.ReadBytes(result.Data.ChunkSize.ToInt()).ToList();
            }

            return result;
        }

        public void SaveFile(WAVFile file, string filename)
        {
            using (BinaryWriter stream = new BinaryWriter(File.Open(filename, FileMode.Create)))
            {
                stream.Write(file.Header.RawData);
                stream.Write(file.Header.Others.ToArray());
                stream.Write(file.Data.ChunkID);
                stream.Write(file.Data.ChunkSize);

                foreach (var sample in file.Data.SoundData)
                {
                    stream.Write(sample);
                }

                stream.Flush();
            }
        }
    }
}
