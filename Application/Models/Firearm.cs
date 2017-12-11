using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class Firearm
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string FirearmType  { get; set; }
        public string AmmoType { get; set; }
        public int BarrelLength { get; set; }
        public string Manufacturer { get; set; }

        public Firearm()
        {

        }

        public Firearm(int id, string name, string firearmType, string ammoType, int barrelLength, string manufacturer)
        {
            this.ID = id;
            this.Name = name;
            this.FirearmType = firearmType;
            this.AmmoType = ammoType;
            this.BarrelLength = barrelLength;
            this.Manufacturer = manufacturer;
            
        }

    }
}
