using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BenhNhan
{

    [Serializable]
    public class Person
    {
        public string Msbn { get; set; }
        public string Cmnd { get; set; }
        public string Hoten { get; set; }
        
        public string Diachi { get; set; }
        public Person() { }
        public Person(string mssv, string cmnd, string hoten , string diachi)
        {
            this.Msbn = mssv;  Cmnd = cmnd; Hoten = hoten; Diachi = diachi;
        }
        public override string ToString()
        {
            return Msbn + "\t" + Cmnd + "\t" + Hoten + "\t" + Diachi;
        }
    }
}
