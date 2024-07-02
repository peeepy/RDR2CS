namespace RDR2CS
{
    using System;
    using static RDONativesWrapper.JoaatUtil;

    // Type aliases
    using BOOL = System.Int32;

    // You can also define structs, enums, and other shared types here
    public struct Vector3(float x, float y, float z)
    {
        public readonly float X = x;
        public readonly float Y = y;
        public readonly float Z = z;
    }
}