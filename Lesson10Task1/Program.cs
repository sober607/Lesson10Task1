using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;
using System.Runtime.Serialization.Formatters.Soap;


namespace Lesson10Task1
{
    class Program
    { 
        static void Main(string[] args)
        {
             XmlSerializer serialiser = new XmlSerializer(typeof(User));

            User userinstance = new User()
            {
                Name = "Vasya Pupkin",
                Age = 20,
                Login = "peterson47",
                Password = "qwerty",
                characteristics = {"bold", "tall" }

            };

            User2 userinstance2 = new User2() // - для soap сериалиацзии
            {
                Name = "Vasya Pupkin",
                Age = 20,
                Login = "peterson47",
                Password = "qwerty",
                

            };

            using (FileStream stream = new FileStream("Serialization.xml", FileMode.Create, FileAccess.Write, FileShare.Read))
            { 

                serialiser.Serialize(stream, userinstance);

            Console.WriteLine("Класс сериализован методом XmlSerializer в Serialization.xml");
            }
            // ------------- BINARY SERIALIZATION
            FileStream stream2 = File.Create("BinarySerialization.dat");

            BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(stream2, userinstance);
            stream2.Close();
            Console.WriteLine("Класс сериализован бинарно в BinarySerialization.dat");

            //--------------SOAP SERIALIZATION
             using (FileStream soapstream = File.Create("SOAPSerialization.xml"))
            {
            SoapFormatter soapFormatter = new SoapFormatter();
                soapFormatter.Serialize(soapstream, userinstance2);


             }
            Console.WriteLine("Класс сериализован в формате SOAP в SOAPSerialization.xml");

            Console.ReadLine();
        }
    }

    [Serializable]
    [XmlRoot("UserData")]
    public class User
    {
        [XmlAttribute("UserName")]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Login { get; set; }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string password = "111111";

        public List<string> characteristics = new List<string>();
    }


    // - класс для SOAP сериализации
    [Serializable]
    public class User2
    {
        [XmlAttribute("UserName")]
        public string Name { get; set; }

        public int Age { get; set; }

        public string Login { get; set; }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private string password = "111111";

    }
}
