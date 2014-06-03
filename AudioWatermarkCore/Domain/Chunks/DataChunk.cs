using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkCore.Domain.Chunks
{
    public class DataChunk
    {
        private uint chunkID;
        private uint chunkSize;
        private List<byte> soundData;

        public DataChunk()
        {
            soundData = new List<byte>();
        }

        public DataChunk(DataChunk data)
        {
            chunkID = data.ChunkID;
            chunkSize = data.ChunkSize;
            soundData = new List<byte>(data.SoundData);
        }

        public uint ChunkID
        {
            get { return chunkID; }
            set { chunkID = value; }
        }

        public uint ChunkSize
        {
            get { return chunkSize; }
            set { chunkSize = value; }
        }

        public List<byte> SoundData
        {
            get { return soundData; }
            set { soundData = value; }
        }
    }
}
