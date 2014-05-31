using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkCore.Domain.Chunks
{
    public class HeaderChunk
    {
        private string chunkId;
        private uint fileLength;
        private string type;

        public string ChunkId
        {
            get { return chunkId; }
            set { chunkId = value; }
        }

        public uint FileLength
        {
            get { return fileLength; }
            set { fileLength = value; }
        }

        public string Type
        {
            get { return type; }
            set { type = value; }
        }
    }
}
