namespace Game
{
    public static class Settings
    {
        private const string MainFolder = "Settings/";
        private const string InputFolder = "Input/";

        public static string Input(string filename)
        {
            return MainFolder + InputFolder + filename;
        }
    }
}