using System.Collections.Generic;

namespace Dispatcher.Tests.Editor
{
    internal readonly struct ValueTestDataComparer : IComparer<ValueTestData>
    {
        public int Compare(ValueTestData x, ValueTestData y)
        {
            return x.Value.CompareTo(y.Value);
        }
    }
}