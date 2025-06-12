namespace Malshinon.classes
{
    public static class Log
    {
        public static string file_name =@"log.csv";

        public static void write(string str)
        {
            File.WriteAllText(file_name, str);
            
        }

        


    }
}
