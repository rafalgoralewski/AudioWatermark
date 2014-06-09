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
        private DataChunk data;

        public WAVFile()
        {
            Header = new HeaderChunk();
            Data = new DataChunk();
        }

        public WAVFile(WAVFile file)
        {
            Header = new HeaderChunk(file.Header);
            Data = new DataChunk(file.Data);
        }

        public HeaderChunk Header
        {
            get { return header; }
            set { header = value; }
        }

        public DataChunk Data
        {
            get { return data; }
            set { data = value; }
        }
    }
}
