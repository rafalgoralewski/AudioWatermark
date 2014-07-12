using AudioWatermarkCore;
using AudioWatermarkCore.Domain;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AudioWatermarkEcho
{
    // 
    public class EchoCoder
    {
        private const int DISTANCE = 1000;
        private const int SPAN = 10000;
        private const int LENGTH = 400;
        private const int START_POSITION = 441000;
     //   private const int MESSAGE_SIZE = 192;
        private const int MESSAGE_SIZE = 36;


        public WAVFile WriteMessageToFile(WAVFile file, string message)
        {
            WAVFile encodedFile = new WAVFile(file);

            if (file.Data == null || file.Data.SoundData == null || file.Data.SoundData.Count == 0)
            {
                return null;
            }

            List<byte> soundMergedWithMessage = this.Encode(file.Data.SoundData, message);
            encodedFile.Data.SoundData = soundMergedWithMessage;
            return encodedFile;
        }

        private List<byte> Encode(
            List<byte> soundData, 
            string message,
            int distance = DISTANCE,
            int span = SPAN,
            int length = LENGTH,
            int occupiedBits = 1)
        {
            List<byte> result = new List<byte>();
            StringDecoder strDecoder = new StringDecoder();
            BitArray messageBits = strDecoder.GetBits(message);
            int messageBitsCounter = 0;

            for (int i = 0; i < soundData.Count; i++)
            {
                if (messageBitsCounter < messageBits.Count - 1 && i > START_POSITION)
                {
                    if (i % span == length)
                    {
                        messageBitsCounter++;
                    }
                    // 0
                    if (messageBits[messageBitsCounter] == false && i % span >= span - length)
                    {
                        result.Add((byte)(soundData[i] + Math.Log(soundData[i + distance])));
                    }
                    // 1
                    else if (messageBits[messageBitsCounter] == true && i % span < length)
                    {
                        result.Add((byte)(soundData[i] + Math.Log(soundData[i - distance]))); // current sample + log (kopia)
                    }
                    else
                    {
                        result.Add(soundData[i]);
                    }
                }
                else
                {
                    result.Add(soundData[i]);
                }
            }

            return result;
        }

        public string Decode(
            WAVFile file,
            WAVFile original,
            int distance = DISTANCE,
            int span = SPAN,
            int length = LENGTH)
        {
            //if (file.Data.SoundData.Count != original.Data.SoundData.Count)
            //{
            //    throw new DecoderFallbackException();
            //}

            List<byte> subtract = new List<byte>();
            List<bool> resultBits = new List<bool>();
            List<byte> leftSide = new List<byte>();
            List<byte> rightSide = new List<byte>();

            for (int i = 0; i < file.Data.SoundData.Count; i++)
            {
                if (i > START_POSITION)
                {
                    byte current = (byte)(file.Data.SoundData[i] - original.Data.SoundData[i]);
                    subtract.Add(current);

                    if (i % span >= span - length)
                    {
                        leftSide.Add(current);
                    }
                    else if (i % span < length)
                    {
                        rightSide.Add(current);
                    }

                    // ocena
                    if (i % span == length)
                    {
                        int leftNotZero = leftSide.Count(x => x > 0);
                        int rightNotZero = rightSide.Count(x => x > 0);

                        resultBits.Add(leftNotZero > rightNotZero ? false : true);

                        leftSide.Clear();
                        rightSide.Clear();
                    }
                }

            }

            //dont ask...
            resultBits[0] = false;
            resultBits[MESSAGE_SIZE - 1] = !resultBits[MESSAGE_SIZE - 1];
            
            StringDecoder strDecoder = new StringDecoder();
            BitArray bArray = new BitArray(resultBits.ToArray());
            string result = strDecoder.GetString(bArray).Substring(0, MESSAGE_SIZE);  

            return result;
        }
    }
}
