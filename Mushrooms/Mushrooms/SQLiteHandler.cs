using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using Xamarin.Essentials;

namespace Mushrooms
{
    public class SQLiteHandler
    {
        public SQLiteConnection myDB;

        public SQLiteHandler()
        {
            string dbPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "MushroomDB.db3");
            myDB = new SQLiteConnection(dbPath);
        }

        public void CreateDB()
        {
            myDB.DropTable<Mushroom>();
            myDB.DropTable<FoundMushroom>();

            myDB.CreateTable<Mushroom>();
            myDB.CreateTable<FoundMushroom>();

            List<Mushroom> mrs = new List<Mushroom>
            {
            new Mushroom(this, "Oyster Mushroom", "Pleurotus ostreatus", false, "Gills", "White", "None", "Off-white,Tan,Gray,Blue,Brown", true, false, "Choice edible"),
            new Mushroom(this, "Golden Chanterelle", "Cantharellus cibarius", true, "Gills", "Yellow", "None", "Yellow,Orange", true, false, "Choice edible"),
            new Mushroom(this, "Porcini Bolete", "Boletus edulis", true, "Pores", "Off-white,Yellow", "none", "Red,Brown,Yellow", true, false, "Choice edible"),
            new Mushroom(this, "Destroying Angel", "Amanita virosa", true, "Gills", "White", "Ring", "White", true, true, "Deadly poisonous!"),
            new Mushroom(this, "Fly Agaric", "Amanita muscaria", true, "Gills", "White", "Ring", "Yellow,Orange,Red", true, true, "Poisonous, Psychoactive"),
            new Mushroom(this, "Morel", "Morchella esculenta", true, "N/A", "N/A", "None", "Gray,Black", true, false, "Choice edible"),
            new Mushroom(this, "Maitake (Hen of the Woods)", "Grifola frondosa", false, "Pores", "White", "None", "Variegated,Gray,Brown,Black,Off-white", true, false, "Choice edible"),
             new Mushroom(this, "Chicken of the Woods", "Laetiporus sulphureus", false, "Pores", "Yellow,Orange", "None", "Variegated,Yellow,Orange", true, false, "Choice edible"),
             new Mushroom(this, "Turkey Tail Mushroom", "Trametes versicolor", false, "Pores", "White,Off-white", "none", "Variegated,Brown,Blue,Orange,Red,Purple,Off-white", true, false, "Inedible, medicinal"),
             new Mushroom(this, "False Morel", "Gyromitra esculenta", true, "N/A", "N/A", "Ring", "Red,Brown", true, false, "Poisonous"),
             new Mushroom(this, "Hedgehog Mushroom", "Hydnum repandum", true, "Teeth", "Brown,Tan", "None", "Brown,Tan", true, false, "Edible"),
             new Mushroom(this, "Scarlet Waxcap", "Hygrocybe coccinea", true, "Gills", "Red,Yellow", "None", "Red", true, false, "Edible"),
             new Mushroom(this, "Slippery Jack Bolete", "Suillus luteus", true, "Pores", "Yellow,Brown", "None", "Red,Brown", true, false, "Edible"),
             new Mushroom(this, "Artist's Conk", "Ganoderma applanatum", false, "Pores", "White", "None", "Variegated,Gray,Brown", true, false, "Inedible"),
             new Mushroom(this, "Honey Mushroom", "Armillaria gallica", true, "Gills", "White", "Ring", "Golden-yellow,Brown", true, false, "Edible"),
             new Mushroom(this, "Tent Stakes", "Gomphidius glutinosus", true, "Gills", "White, Brown", "None", "Brown", true, false, "Edible"),
             new Mushroom(this, "Death Cap", "Amanita phalloides", true, "Gills", "White", "Ring", "White,Yellow,Tan", true, false, "Deadly poisonous!"),
             new Mushroom(this, "Chaga Mushroom", "Inonotus obliquus", false, "N/A", "N/A", "None", "Black,Brown,Orange", true, false, "Inedible, medicinal"),
             new Mushroom(this, "Cinnabar Chanterelle", "Cantharellus cinnabarinus", true, "Gills", "Orange,Red", "None", "Red,Orange", true, false, "Choice edible"),
             new Mushroom(this, "Golden Tops", "Psilocybe cubensis", true, "Gills", "Brown", "None", "Brown,Gold,Tan,White", true, false, "Poisonous, psychoactive"),
            new Mushroom(this, "Lion's Mane", "Hericeum erinaceus", false, "Teeth","White", "None", "White", true, false, "Edible, medicinal"),
            new Mushroom(this, "Deadly Galerina", "Galerina marginata", true, "Gills", "Tan,Brown,Rusty-brown", "None", "Tan,Brown", true, false, "Deadly poisonous"),
             new Mushroom(this, "Wood Blewitt Mushroom", "Clitocybe nuda", true, "Gills", "Lilac,Purple", "None", "Lilac,Purple,Brown", true, false, "Choice edible"),
             new Mushroom(this, "Jack 'O Lantern Mushroom", "Omphalotus olearius", true, "Gills", "Yellow,Orange", "None", "Yellow,Orange", true, false, "Poisonous"),
             new Mushroom(this, "Common Puffball", "Lycoperdon perlatum", true, "N/A", "N/A", "None", "White,Gray,Brown", true, true, "Edible when young"),
             new Mushroom(this, "Shaggy Mane Mushroom", "Coprinus comatus", true, "Gills", "White,Black", "Ring", "White,Black,Brown", true, true, "Edible"),
             new Mushroom(this, "Reishi Mushroom", "Ganoderma lucidum", true, "Pores", "White", "None", "Variegated,Red,Rust,White,Yellow", true, false, "Inedible, medicinal")
            };

            foreach (Mushroom m in mrs)
            {
                var maxPk = myDB.Table<Mushroom>().OrderByDescending(c => c.ID).FirstOrDefault();
                m.ID = (maxPk == null ? 1 : maxPk.ID + 1);
                myDB.Insert(m);
            }

        }
        public void AddNewMushroom(FoundMushroom fm)
        {
            Mushroom m = fm.mushroom;
            m.CommonName = "Unknown";
            m.ImageName = "Unknown";
            fm.CommonName = "Unknown";
            var maxPk = myDB.Table<Mushroom>().OrderByDescending(c => c.ID).FirstOrDefault();
            m.ID = (maxPk == null ? 1 : maxPk.ID + 1);
            int what = myDB.Insert(m);

            fm.MushroomID = m.ID;
            var maxPk2 = myDB.Table<FoundMushroom>().OrderByDescending(c => c.ID).FirstOrDefault();
            fm.ID = (maxPk2 == null ? 1 : maxPk2.ID + 1);
            myDB.Insert(fm);
        }
        public void AddFoundMushroom(FoundMushroom fm)
        {
            var maxPk = myDB.Table<FoundMushroom>().OrderByDescending(c => c.ID).FirstOrDefault();
            fm.ID = (maxPk == null ? 1 : maxPk.ID + 1);
            myDB.Insert(fm);
        }
        public List<Mushroom> GetMushroomTable()
        {
            var data = myDB.Table<Mushroom>().ToList();
            return data;
        }
        public List<FoundMushroom> GetFoundMushroomTable()
        {
            var data = myDB.Table<FoundMushroom>().ToList();
            foreach(FoundMushroom fm in data)
            {
                List<Mushroom> m = myDB.Query<Mushroom>("SELECT * FROM Mushrooms WHERE ID = " + fm.MushroomID);
                fm.mushroom = m[0];
                if (fm.Photo == "empty")
                {
                    fm.Photo = m[0].ImageName;
                }
                fm.CommonName = m[0].CommonName;
            }
            return data;
        }
        public List<Mushroom> GetMushroom(string query)
        {
            List<Mushroom> result = new List<Mushroom>();
            List<Mushroom> ms = myDB.Table<Mushroom>().ToList();
            foreach (Mushroom m in ms)
            {
                if (m.FieldGuide)
                {
                    if (query.Equals(m.CommonName.ToLower()) || query.Equals(m.LatinName.ToLower()))
                    {
                        result.Add(m);
                    }
                }
            }
            return result;
        }
        public List<Mushroom> Identify(Mushroom m)
        {
            List<Mushroom> results = new List<Mushroom>();

            if (!(m.CapColor == null && m.Underside == null && m.UndersideColor == null && m.Veil == null))
            {
                string query = "SELECT * FROM Mushrooms WHERE";
                if (m.CapColor != null)
                {
                    query += " CapColor LIKE '%" + m.CapColor + "%' AND";
                }
                if (m.Underside != null)
                {
                    query += " Underside = '" + m.Underside + "' AND";
                }
                if (m.UndersideColor != null)
                {
                    query += " UndersideColor LIKE '%" + m.UndersideColor + "%' AND";
                }
                if (m.Veil != null)
                {
                    query += " Veil = '" + m.Veil + "' AND";
                }
                query += " HasStem = " + m.HasStem + " AND";
                query += " HasScales = " + m.HasScales + " AND";

                query = query.Substring(0, query.Length - 3);
                results = myDB.Query<Mushroom>(query);
            }            
            return results;
        }
    }
}

