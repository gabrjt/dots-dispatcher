using System;

namespace DOTS.Tests.Editor
{
    public static class TestsExtensions
    {
        public static double GetFrameTimeSpanInMilliseconds(int frameRate)
        {
            var timeSpan = new TimeSpan(0, 0, 1);

            return timeSpan.TotalMilliseconds / frameRate;
        }
    }
}