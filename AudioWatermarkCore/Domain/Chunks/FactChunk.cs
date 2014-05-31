﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkCore.Domain.Chunks
{
    public class FactChunk
    {
        private string chunkID;
        private uint chunkSize;
        private uint numSamples;

        public string ChunkID
        {
            get { return chunkID; }
            set { chunkID = value; }
        }

        public uint ChunkSize
        {
            get { return chunkSize; }
            set { chunkSize = value; }
        }

        public uint NumSamples
        {
            get { return numSamples; }
            set { numSamples = value; }
        }
    }
}
