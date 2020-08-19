using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace Mushrooms
{
    [Table("Mushrooms")]
    public class Mushroom
    {
        [PrimaryKey]
        public int ID { get; set; }
        public string CommonName { get; set; }
        public string LatinName { get; set; }
        public bool HasStem { get; set; } //true, false, both
        public string Underside { get; set; } //gills, tubes, teeth, porous
        public string UndersideColor { get; set; }
        public string Veil { get; set; } //Universal, Ring, None
        public string CapColor { get; set; }
        public bool FieldGuide { get; set; }
        public bool HasScales { get; set; } //true, false, both
        public string Edibility { get; set; }
        public string ImageName { get; set; }

        public Mushroom() { }
        
        public Mushroom(SQLiteHandler dbHandler, string commonName, string latinName, bool hasStem, string underside, string undersideColor, string veil, string capColor, bool fieldGuide, bool hasScales, string edibility)
        {
            CommonName = commonName;
            LatinName = latinName;
            HasStem = hasStem;
            Underside = underside;
            UndersideColor = undersideColor;
            CapColor = capColor;
            FieldGuide = fieldGuide;
            HasScales = hasScales;
            Edibility = edibility;
            Veil = veil;

            string[] latins = latinName.Split(' ');
            ImageName = latins[0] + "_" + latins[1];
        }
    }
}

