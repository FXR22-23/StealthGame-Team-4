namespace GuardControl
{
    public static class PatrolsCounter
    {
        private static int _counter;

        public static int Get()
        {
            return _counter;
        }

        public static void Reset()
        {
            _counter = 0;
        }

        public static void Increment()
        {
            _counter++;
        }
    }
}
