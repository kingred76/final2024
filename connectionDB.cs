
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace finalProjet
{
    public class ConnextionDb
    {
        private readonly string ConnectionString = ("Server=cours.cegep3r.info;Database=a2024_420335ri_eq14;Uid=1286834;Pwd=1286834");

        private MySqlConnection con;

        private static ConnextionDb instance;

        public static ConnextionDb GetInstance()
        {
            if (instance == null)
            {
                return new ConnextionDb();
            }
            return instance;
        }

        public ConnextionDb()
        {
            con = new MySqlConnection(ConnectionString);
        }



        public string? AdminLogIn(string userName, string pwd)
        {

            string encriptedpwd = EncryptionHelper.Encrypt(pwd);

            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM admin WHERE nom='{userName}' AND password='{encriptedpwd}'";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                return r["id"].ToString();
            }
            return null;
        }

        public string? UserName(string userName)
        {



            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM clients WHERE Id='{userName}' ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                return r["prenom"].ToString() + " " + r["nom"].ToString();
            }
            return null;
        }

        public string? AdminName(string userName)
        {



            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM admin WHERE Id='{userName}' ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                return r["nom"].ToString();
            }
            return null;
        }

        public string? UserLogIn(string userName)
        {



            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM clients WHERE Id='{userName}' ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                return r["Id"].ToString();
            }
            return null;
        }

        public List<Activiter> GetAllActiviter()
        {
            List<Activiter> resultas = new List<Activiter>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "SELECT * FROM activites";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                float.TryParse(r["prix_organisation"].ToString(), out float prix);
                float.TryParse(r["prix_clients"].ToString(), out float prixClients);
                resultas.Add(new Activiter
                {
                    Nom = r["nom"].ToString(),
                    Type = r["type"].ToString(),
                    CoutUnitaire = prix,
                    CoutClient = prixClients,

                });
            }

            var result = r.ToString();

            r.Close();
            con.Close();
            return resultas;
        }

        public List<SeancesItem> GetAllSeance(string activiter)
        {
            List<SeancesItem> resultas = new List<SeancesItem>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM seances WHERE nom_activite='{activiter}'";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                var date = r.GetDateTime("date");
                
                resultas.Add(new SeancesItem
                {
                    Id = int.Parse(r["Id"].ToString()),
                    Heure = r["heure"].ToString(),
                    PlaceDispo = int.Parse(r["place_disponible"].ToString()),
                    PlaceMax = int.Parse(r["place_maximum"].ToString()),
                    Date = date,


                });
            }

            var result = r.ToString();

            r.Close();
            con.Close();
            return resultas;
        }



    }
}
