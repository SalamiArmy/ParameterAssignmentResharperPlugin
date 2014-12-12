namespace ParameterAssignment
{
    class Example
    {
        public int ViolationExample(int val)
        {
            if(val > 5)
                val = 5;
            return val;
        }

        public int AcceptableExample(int val)
        {
            var updatedVal = val;
            if (val > 5)
                updatedVal = 5;
            return updatedVal;
        }
    }
}
