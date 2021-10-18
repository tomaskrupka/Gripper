﻿using System;

namespace Gripper.Authenticated.Browser
{
    public struct PollSettings
    {
        public int PeriodMs;
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

        public static PollSettings Default => (100, 30_000);
    }
}