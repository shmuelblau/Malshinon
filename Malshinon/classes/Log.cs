namespace Malshinon.classes
{
    public static class Log
    {
        public static string file_name =@"log.csv";

        public static void write(string str)
        {
            File.AppendAllText(file_name, str +"\r\n");
            
        }

        public static void AddPeople(People p)

        {

            string str = $"{DateTime.Now}  Add new People fname = {p.FirstName}  lname {p.LastName}";
            write(str);

        }


        public static void AddReport(IntelReport i )
        {
            string str = $"{DateTime.Now}  new report from {i.ReporterId} about {i.TargetId}";
            write(str);

        }

        public static void request(string type)
        {
            string str = $"{DateTime.Now}  new {type} request";
            write(str);

        }

        public static List<string> read(string str)
        {
            return File.ReadAllLines(file_name).ToList();
        }

        

        


    }
}
