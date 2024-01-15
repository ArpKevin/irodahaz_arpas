using System.Text;

namespace irodahaz_arpas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Iroda> irodak = new List<Iroda>();
            StreamReader sr = new(@"..\..\..\src\irodahaz.txt", encoding: Encoding.UTF8);

            int emeletIndex = 1;

            while (!sr.EndOfStream)
            {
                irodak.Add(new(sr.ReadLine(), emeletIndex));
                emeletIndex++;
            }


            Console.ReadKey();
        }
    }
}