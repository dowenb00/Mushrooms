using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using SQLite;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Mushrooms
{
    [Table("FoundMushrooms")]
    public class FoundMushroom
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string GPS { get; set; }
        public DateTime TimeStamp { get; set; }
        public int MushroomID { get; set; }
        //public Image SpecimenPhoto { get; set; }
        public string DateString { get; set; }
        public string Photo { get; set; }
        public bool Identified { get; set; }
        public string Notes { get; set; }
        [Ignore]
        public Mushroom mushroom { get; set; }        
        [Ignore]
        public string CommonName { get; set; }


        public FoundMushroom() { }

        public FoundMushroom(string gps, DateTime timeStamp, int mushroomID, bool identified)
        {
            GPS = gps;
            TimeStamp = timeStamp;
            MushroomID = mushroomID;
            DateString = timeStamp.ToShortDateString();
            Identified = identified;
        }

    }
}
