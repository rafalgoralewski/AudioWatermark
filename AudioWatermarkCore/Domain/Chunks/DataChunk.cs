using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkCore.Domain.Chunks
{
    public class DataChunk
    {
        private byte[] chunkID;
        private byte[] chunkSize;
        private List<byte> soundData;

        public DataChunk()
        {
            chunkID = new byte[4];
            chunkSize = new byte[4];
            soundData = new List<byte>();
        }

        public DataChunk(DataChunk data)
        {
            chunkID = new byte[4];
            chunkSize = new byte[4];
            soundData = new List<byte>();
            data.ChunkID.CopyTo(this.chunkID, 0);
            data.ChunkSize.CopyTo(this.chunkSize, 0);
            soundData = new List<byte>(data.SoundData);
        }

        public byte[] ChunkID
        {
            get { return chunkID; }
            set { chunkID = value; }
        }

        public byte[] ChunkSize
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
