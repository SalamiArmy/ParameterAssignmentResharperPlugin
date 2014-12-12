using JetBrains.Annotations;

namespace ParameterAssignment
{
    [UsedImplicitly]
    class Example
    {
        [UsedImplicitly]
        public int ViolationExample(int val)
        {
            if(val > 5)
                val = 5;
            return val;
        }

        [UsedImplicitly]
        public int AcceptableExample(int val)
        {
            var updatedVal = val;
            if (val > 5)
                updatedVal = 5;
            return updatedVal;
        }
    }
}
