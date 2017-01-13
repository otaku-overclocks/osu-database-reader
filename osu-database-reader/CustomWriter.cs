using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace osu_database_reader
{
    public class CustomWriter : BinaryWriter
    {
        public CustomWriter(Stream output) : base(output)
        {
        }

        public override void Write(byte[] buffer)
        {
            Write(buffer.Length);
            base.Write(buffer);
        }

        public override void Write(string value)
        {
            if (value.Equals(string.Empty))
            {
                Write((byte) 0x00);
                return;
            }
            else
            {
                Write((byte)0x0B);
            }
            base.Write(value);
        }

        public void Write(DateTime value)
        {
            Write(value.ToUniversalTime().Ticks);
        }

        public void Write(Dictionary<Mods, double> value)
        {
            Write(value.Count);
            foreach (var modPair in value)
            {
                Write((byte)0x08);
                Write((int)modPair.Key);
                Write((byte)0x0D);
                Write(modPair.Value);
            }
        }

        public void Write(List<TimingPoint> value)
        {
            Write(value.Count);
            foreach (var timingPoint in value)
            {
                Write(timingPoint);
            }
        }

        public void Write(TimingPoint value)
        {
            Write(value.Time);
            Write(value.Speed);
            Write(value.NotInherited);
        }
    }
}
