using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkCore.Domain.Chunks
{
    public class DataChunk
    {
        private string chunkID;
        private uint chunkSize;
        private List<int> data;

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

        public List<int> Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
