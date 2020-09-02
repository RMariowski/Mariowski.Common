using Mariowski.Common.DataTypes;

namespace Mariowski.Common.UnitTests.DataTypes
{
    public class VObj : ValueObject<VObj>
    {
        private readonly string _value;

        public VObj(string value)
        {
            _value = value;
        }

        public override bool Equals(VObj other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return _value == other._value;
        }

        public override int GetHashCode()
            => _value.GetHashCode();
    }
}