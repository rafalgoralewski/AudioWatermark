using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioWatermarkCore;
using AudioWatermarkCore.Domain;
using System.Collections;

namespace AudioWatermarkLSB
{
    public class LSBCoder
    {
        public WAVFile WriteMessageToFile(WAVFile file, string message)
        {
            // brak kopiarki
            WAVFile encodedFile = new WAVFile(file);

            if (file.Data == null || file.Data.SoundData == null || file.Data.SoundData.Count == 0)
            {
                return null;
            }

            List<byte> soundMergedWithMessage = this.Encode(file.Data.SoundData, message);
            encodedFile.Data.SoundData = soundMergedWithMessage;
            return encodedFile;
        }

        private List<byte> Encode(List<byte> soundData, string message, int occupiedBits = 1)
        {
            List<byte> result = new List<byte>();
            StringDecoder strDecoder = new StringDecoder();

            BitArray messageBits = strDecoder.GetBits(message);
            //string tmp = strDecoder.GetString(messageBits);

            int counter = 0;
            foreach (var soundbit in soundData)
            {
                byte newSoundBit = soundbit;
                if (messageBits[counter] == true && newSoundBit % 2 == 0)
                {
                    newSoundBit = byte.Parse((newSoundBit + 1).ToString()); //0 -> 1
                }
                else if (messageBits[counter] == false && newSoundBit % 2 == 1)
                {
                    newSoundBit = byte.Parse((newSoundBit - 1).ToString()); //1 -> 0
                }

                result.Add(newSoundBit);

                counter = counter >= messageBits.Count - 1 ?
                        0 :
                        counter + 1;
            }

            return result;
        }
    }
}
