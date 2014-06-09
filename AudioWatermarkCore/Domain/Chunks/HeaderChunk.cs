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
        private List<byte> others;

        public HeaderChunk()
        {
            rawData = new byte[38];
            others = new List<byte>();
        }

        public HeaderChunk(HeaderChunk header)
        {
            rawData = new byte[38];
            others = new List<byte>();
            header.RawData.CopyTo(this.rawData, 0);
            this.others = header.others.Select(x => x).ToList();
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

        public List<byte> Others
        {
            get { return others; }
            set { others = value; }
        }

        public byte[] Channels
        {
            get { return rawData.Skip(22).Take(2).ToArray(); }
        }

        public byte[] SampleRate
        {
            get { return rawData.Skip(24).Take(4).ToArray(); }
        }

        public byte[] BPS
        {
            get { return rawData.Skip(34).Take(2).ToArray(); }
        }
    }
}
