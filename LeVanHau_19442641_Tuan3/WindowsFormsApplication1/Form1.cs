using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BenhNhan;
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using System.Xml.Serialization;
using System.IO;
namespace WindowsFormsApplication1
{
    
    public partial class Form1 : Form
    {
        
        public Form1()
        {
  
            InitializeComponent();
            Init();
            
        }

        public void Init()
        {
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
        private void button3_Click(object sender, EventArgs e)
        {
            String mabn = textmsbn.Text;
            String cmnd = textcmnd.Text;
            String hoten = texthoten.Text;
            String diachi = textdiachi.Text;
            Person bn = new Person(mabn, cmnd, hoten, diachi);

            /*string xml = new XMLObjectConverter<Person>().object2XML(bn);*/
           
            IConnectionFactory factory = new
            ConnectionFactory("tcp://localhost:61616");
            //tạo connection
            IConnection con = factory.CreateConnection("admin", "admin");
            con.Start();//nối tới MOM
                        //tạo session
            ISession session = con.CreateSession(AcknowledgementMode.AutoAcknowledge);
            //tạo producer
            IObjectMessage xml = session.CreateObjectMessage(bn);
            ActiveMQQueue destination = new ActiveMQQueue("thanthidet");
            IMessageProducer producer = session.CreateProducer(destination);
            producer.Send(xml);
            
            //shutdown
            session.Close();
            con.Close();
            Console.WriteLine(xml);
        }
    }
}
