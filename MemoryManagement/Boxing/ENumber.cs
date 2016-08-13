using System;

namespace Boxing
{
    struct ENumber : IEquatable<ENumber>
    {
        public int Value { get; set; }

        public bool Equals(ENumber other)
        {
            return Value == other.Value;
        }
    }
}
