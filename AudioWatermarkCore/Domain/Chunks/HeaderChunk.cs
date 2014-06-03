using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkCore.Domain.Chunks
{
    public class HeaderChunk
    {
        private byte[] rawData;

        public HeaderChunk()
        {
            rawData = new byte[12];
        }

        public HeaderChunk(HeaderChunk header)
        {
            header.RawData.CopyTo(this.rawData, 0);
        }

        public byte[] ChunkId
        {
            get { return rawData.Take(4).ToArray(); }
        }

        public byte[] FileLength
        {
            get { return rawData.Skip(4).Take(4).ToArray(); }
        }

        public byte[] Type
        {
            get { return rawData.Skip(8).Take(4).ToArray(); }
        }

        public byte[] RawData
        {
            get { return rawData; }
            set { rawData = value; }
        }
    }
}
