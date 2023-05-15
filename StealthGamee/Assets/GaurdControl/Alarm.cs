namespace GaurdControl
{
    public static class Alarm
    {
        private static bool _myAlarm = false;

        public static bool HasGoneOff()
        {
            return _myAlarm;
        }

        public static void SetAlarm()
        {
            _myAlarm = true;
        }
    }
}
