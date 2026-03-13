using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;

namespace LB.Utility.Random;

public sealed class PseudoRNG
{

    public static PseudoRNG Initialize()
    {
        return Initialize(null);
    }
    public static PseudoRNG Initialize(UInt64 seed)
    {
        Instance = new(seed);
        return Instance;
    }
    public static PseudoRNG Initialize(String? seed)
    {
        Instance = seed == null ? new() : new(seed);
        return Instance;
    }

    public static PseudoRNG? Instance { get; private set; }

    private static UInt64 CreateRandomSeed()
    {
        Span<Byte> bytes = stackalloc byte[8];
        RandomNumberGenerator.Fill(bytes);
        return BitConverter.ToUInt64(bytes);
    }

    private static UInt64 SeedFromString(String seed)
    {
        if (String.IsNullOrWhiteSpace(seed))
            throw new ArgumentException($"{nameof(seed)} cannot be empty.");

        var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(seed));

        return BitConverter.ToUInt64(bytes, 0);
    }

    #region Members
    private UInt64 _state;
    private readonly UInt64 _inc;
    #endregion

    #region Properties
    public String Seed { get; }
    #endregion

    #region Constructors
    public PseudoRNG(String? seed = null) : this(String.IsNullOrWhiteSpace(seed) ? CreateRandomSeed() : SeedFromString(seed))
    {
    }

    /// <summary>
    /// Creates a PCG32 RNG.
    /// </summary>
    /// <param name="seed">Initial state seed.</param>
    /// <param name="sequence">Stream selector (must be odd internally).</param>
    public PseudoRNG(UInt64 seed, UInt64 sequence = 54u)
    {
        Seed = seed.ToString();
        _state = 0;
        _inc = (sequence << 1) | 1UL; // must be odd

        NextUInt32();          // advance once
        _state += seed;
        NextUInt32();          // advance again
    }
    #endregion

    #region Generators
    /// <summary>
    /// Returns a uniformly distributed Int32 (full range).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int32 NextInt32()
        => unchecked((Int32)(NextUInt32() ^ 0x8000_0000u));

    /// <summary>
    /// Returns an Int32 in [0, maxExclusive).
    /// </summary>
    public Int32 NextInt32(Int32 maxExclusive)
        => NextInt32(0, maxExclusive);

    /// <summary>
    /// Returns an Int32 in [minInclusive, maxExclusive).
    /// </summary>
    public Int32 NextInt32(Int32 minInclusive, Int32 maxExclusive)
    {
        if (minInclusive >= maxExclusive)
            throw new ArgumentOutOfRangeException(nameof(maxExclusive),
                "maxExclusive must be greater than minInclusive.");

        // Map signed interval into an unsigned interval while preserving ordering.
        uint uMin = unchecked((uint)minInclusive) ^ 0x8000_0000u;
        uint uMax = unchecked((uint)maxExclusive) ^ 0x8000_0000u;

        uint range = uMax - uMin; // valid because uMin < uMax

        // No need for range==0 handling here; min<max guarantees range>0.
        uint threshold = (uint)(-range % range);

        while (true)
        {
            uint r = NextUInt32();
            if (r >= threshold)
            {
                uint u = uMin + (r % range);
                return unchecked((Int32)(u ^ 0x8000_0000u));
            }
        }
    }

    /// <summary>
    /// Returns a uniformly distributed Int64 (full range).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Int64 NextInt64()
        => unchecked((Int64)(NextUInt64() ^ 0x8000_0000_0000_0000UL));

    /// <summary>
    /// Returns an Int64 in [0, maxExclusive).
    /// </summary>
    public Int64 NextInt64(Int64 maxExclusive)
        => NextInt64(0, maxExclusive);

    /// <summary>
    /// Returns an Int64 in [minInclusive, maxExclusive).
    /// </summary>
    public Int64 NextInt64(Int64 minInclusive, Int64 maxExclusive)
    {
        if (minInclusive >= maxExclusive)
            throw new ArgumentOutOfRangeException(nameof(maxExclusive),
                "maxExclusive must be greater than minInclusive.");

        ulong uMin = unchecked((ulong)minInclusive) ^ 0x8000_0000_0000_0000UL;
        ulong uMax = unchecked((ulong)maxExclusive) ^ 0x8000_0000_0000_0000UL;

        ulong range = uMax - uMin; // valid because uMin < uMax
        ulong threshold = (0UL - range) % range;

        while (true)
        {
            ulong r = NextUInt64();
            if (r >= threshold)
            {
                ulong u = uMin + (r % range);
                return unchecked((Int64)(u ^ 0x8000_0000_0000_0000UL));
            }
        }
    }

    /// <summary>
    /// Returns a uniformly distributed 32-bit unsigned integer.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt32 NextUInt32()
    {
        UInt64 oldState = _state;
        _state = unchecked(oldState * 6364136223846793005UL + _inc);

        UInt32 xorshifted = (UInt32)(((oldState >> 18) ^ oldState) >> 27);
        Int32 rot = (Int32)(oldState >> 59);

        return (xorshifted >> rot) | (xorshifted << ((-rot) & 31));
    }

    /// <summary>
    /// Returns an Int32 in [0, maxExclusive).
    /// </summary>
    public UInt32 NextUInt32(UInt32 maxExclusive)
    {
        return NextUInt32(0, maxExclusive);
    }

    public UInt32 NextUInt32(UInt32 minInclusive, UInt32 maxExclusive)
    {
        if (minInclusive >= maxExclusive)
            throw new ArgumentOutOfRangeException(nameof(maxExclusive),
                "maxExclusive must be greater than minInclusive.");

        uint range = maxExclusive - minInclusive;

        // Fast path: full range
        if (range == 0)
            return NextUInt32();

        uint threshold = (uint)(-range % range);

        while (true)
        {
            uint r = NextUInt32();
            if (r >= threshold)
                return minInclusive + (r % range);
        }
    }

    /// <summary>
    /// Returns a uniformly distributed 32-bit unsigned integer.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public UInt64 NextUInt64()
    {
        UInt64 hi = NextUInt32();
        UInt64 low = NextUInt32();
        return (hi << 32) | low;
    }

    public UInt64 NextUInt64(UInt64 maxExclusive)
    {
        return NextUInt64(0, maxExclusive);
    }

    /// <summary>
    /// Returns an Int32 in [0, maxExclusive).
    /// </summary>
    public UInt64 NextUInt64(UInt64 minInclusive, UInt64 maxExclusive)
    {
        if (minInclusive >= maxExclusive)
            throw new ArgumentOutOfRangeException(nameof(maxExclusive),
                "maxExclusive must be greater than minInclusive.");

        ulong range = maxExclusive - minInclusive;

        // range == 0 would mean full 2^64 span (only possible if min=0,max=0),
        // but we disallow min>=max, so you won't hit this. Leaving here for completeness.
        if (range == 0)
            return NextUInt64();

        // Remove modulo bias (works due to wraparound in unsigned arithmetic)
        ulong threshold = (0UL - range) % range;

        while (true)
        {
            ulong r = NextUInt64();
            if (r >= threshold)
                return minInclusive + (r % range);
        }
    }

    /// <summary>
    /// Returns a double in [0, 1).
    /// </summary>
    public Double NextDouble()
    {
        // 53 bits of precision
        UInt64 hi = NextUInt32();
        UInt64 lo = NextUInt32();
        UInt64 combined = (hi << 21) ^ lo;

        return (combined & ((1UL << 53) - 1)) / (Double)(1UL << 53);
    }

    public Boolean NextBool() => (NextUInt32() & 1) == 1;

    public void NextBytes(Span<Byte> buffer)
    {
        Int32 i = 0;
        while (i < buffer.Length)
        {
            UInt32 value = NextUInt32();
            for (Int32 b = 0; b < 4 && i < buffer.Length; b++)
            {
                buffer[i++] = (Byte)(value & 0xFF);
                value >>= 8;
            }
        }
    } 
    #endregion
}

