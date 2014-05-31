using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkCore.Domain.Chunks
{
    public class FormatChunk
    {
        private string chunkId;
        private uint chunkSize;
        private ushort formatTag;
        private ushort channels;
        private uint samplesPerSec;
        private uint avgBytesPerSec;
        private ushort blockAlign;
        private uint bitsPerSample;

        public string ChunkId
        {
            get { return chunkId; }
            set { chunkId = value; }
        }

        public uint ChunkSize
        {
            get { return chunkSize; }
            set { chunkSize = value; }
        }

        public ushort FormatTag
        {
            get { return formatTag; }
            set { formatTag = value; }
        }

        public ushort Channels
        {
            get { return channels; }
            set { channels = value; }
        }

        public uint SamplesPerSec
        {
            get { return samplesPerSec; }
            set { samplesPerSec = value; }
        }

        public uint AvgBytesPerSec
        {
            get { return avgBytesPerSec; }
            set { avgBytesPerSec = value; }
        }

        public ushort BlockAlign
        {
            get { return blockAlign; }
            set { blockAlign = value; }
        }

        public uint BitsPerSample
        {
            get { return bitsPerSample; }
            set { bitsPerSample = value; }
        }
    }
}
