using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using System.Xml.Serialization;
using System.IO;

namespace ActiveMQSender
{
    
    public class Person
    {
        public long Mssv { get; set; }
        public string Hoten { get; set; }
        public DateTime Ngaysinh { get; set; }
        public Person() { }
        public Person(long mssv, string hoten, DateTime ngaysinh)
        {
            this.Mssv = mssv; Hoten = hoten; Ngaysinh = ngaysinh;
        }
    }
    public class XMLObjectConverter<T>
    {
        public string object2XML(T p)
        {
            string xml = "";
            XmlSerializer ser = new XmlSerializer(typeof(T));
            using (MemoryStream ms = new MemoryStream())
            {
                ser.Serialize(ms, p);
                ms.Position = 0;
                xml = new StreamReader(ms).ReadToEnd();
            }
            return xml;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("sending message. Enter to exit.");
            //tạo connection factory
            IConnectionFactory factory = new
           ConnectionFactory("tcp://localhost:61616");
            //tạo connection
            IConnection con = factory.CreateConnection("admin", "admin");
            con.Start();//nối tới MOM
                        //tạo session
            ISession session = con.CreateSession(AcknowledgementMode.AutoAcknowledge);
            //tạo producer
            ActiveMQQueue destination = new ActiveMQQueue("thanthidet");
            IMessageProducer producer = session.CreateProducer(destination);
            //send message
            //biến đối tượng thành XML document String
            Person p = new Person(1001, "Truong Van COi", new DateTime());
            //string xml = genXML(p).ToLower();
            string xml = new XMLObjectConverter<Person>().object2XML(p);
            Console.WriteLine(xml.ToLower());
            IMessage msg = new ActiveMQTextMessage("Hello Vu NGuyen");
            producer.Send(msg);
            //shutdown
            session.Close();
            con.Close();
            Console.ReadKey();
        }
    }
}
