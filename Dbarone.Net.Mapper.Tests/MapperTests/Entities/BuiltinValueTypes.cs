public class BuiltinValueTypes {
    public bool BoolValue { get; set; }
    public byte ByteValue { get; set; }
    public sbyte SByteValue { get; set; }
    public char CharValue { get; set; }
    public decimal DecimalValue { get; set; }
    public double DoubleValue { get; set; }
    public float FloatValue { get; set; }
    public int IntValue { get; set; }
    public uint UIntValue { get; set; }
    public nint NIntValue { get; set; }
    public nuint NUIntValue { get; set; }
    public long LongValue { get; set; }
    public ulong ULongValue { get; set; }
    public short ShortValue { get; set; }
    public ushort UShortValue { get; set; }

    public static BuiltinValueTypes CreateMin() {
        return new BuiltinValueTypes
        {
            BoolValue = false,
            ByteValue = byte.MinValue,
            SByteValue = sbyte.MinValue,
            CharValue = char.MinValue,
            DecimalValue = decimal.MinValue,
            DoubleValue = double.MinValue,
            FloatValue = float.MinValue,
            IntValue = int.MinValue,
            UIntValue = uint.MinValue,
            NIntValue = nint.MinValue,
            NUIntValue = nuint.MinValue,
            LongValue = long.MinValue,
            ULongValue = ulong.MinValue,
            ShortValue = short.MinValue,
            UShortValue = ushort.MinValue
        };
    }
    public static BuiltinValueTypes CreateMax() {
        return new BuiltinValueTypes
        {
            BoolValue = true,
            ByteValue = byte.MaxValue,
            SByteValue = sbyte.MaxValue,
            CharValue = char.MaxValue,
            DecimalValue = decimal.MaxValue,
            DoubleValue = double.MaxValue,
            FloatValue = float.MaxValue,
            IntValue = int.MaxValue,
            UIntValue = uint.MaxValue,
            NIntValue = nint.MaxValue,
            NUIntValue = nuint.MaxValue,
            LongValue = long.MaxValue,
            ULongValue = ulong.MaxValue,
            ShortValue = short.MaxValue,
            UShortValue = ushort.MaxValue
        };
    }
}