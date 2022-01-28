using System;

namespace Gripper.WebClient
{
    /// <summary>
    /// A data structure to provide settings to polling operations.
    /// </summary>
    public struct PollSettings
    {
        /// <summary>
        /// Defines the period of the polling operation. That is, the length of one cycle in milliseconds.
        /// </summary>
        public int PeriodMs;

        /// <summary>
        /// Defines the total timeout period of the polling operation in milliseconds.
        /// </summary>
        public int TimeoutMs;

        public PollSettings(int periodMs, int timeoutMs)
        {
            PeriodMs = periodMs;
            TimeoutMs = timeoutMs;
        }

        public override bool Equals(object? obj)
        {
            return obj is PollSettings other &&
                   PeriodMs == other.PeriodMs &&
                   TimeoutMs == other.TimeoutMs;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PeriodMs, TimeoutMs);
        }

        public void Deconstruct(out int periodMs, out int timeoutMs)
        {
            periodMs = PeriodMs;
            timeoutMs = TimeoutMs;
        }

        public static implicit operator (int PeriodMs, int TimeoutMs)(PollSettings value)
        {
            return (value.PeriodMs, value.TimeoutMs);
        }

        public static implicit operator PollSettings((int PeriodMs, int TimeoutMs) value)
        {
            return new PollSettings(value.PeriodMs, value.TimeoutMs);
        }

        public static PollSettings ElementDetectionLong => (100, 30_000);
        public static PollSettings ElementDetectionDefault => (100, 10_000);
        public static PollSettings ElementDetectionShort => (100, 3_000);
        public static PollSettings FrameDetectionDefault => (1_500, 15_000);
        public static PollSettings FrameDetectionLong => (5_000, 30_000);
    }
}
