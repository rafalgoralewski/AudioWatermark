using AudioWatermarkCore.Domain.Chunks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkCore.Domain
{
    public class WAVFile
    {
        private HeaderChunk header;
        private FormatChunk format;
        private FactChunk fact;
        private List<DataChunk> data;

        public WAVFile()
        {
            Header = new HeaderChunk();
            Format = new FormatChunk();
            Fact = new FactChunk();
            Data = new List<DataChunk>();
        }

        public HeaderChunk Header
        {
            get { return header; }
            set { header = value; }
        }

        public FormatChunk Format
        {
            get { return format; }
            set { format = value; }
        }

        public FactChunk Fact
        {
            get { return fact; }
            set { fact = value; }
        }

        public List<DataChunk> Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
