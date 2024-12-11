
using Microsoft.UI.Xaml.Controls;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

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


        // Nouvelle méthode : Inscrire un utilisateur à une séance
        public bool RegisterToSeance(int clientId, int seanceId)
        {
            try
            {
                using (var command = new MySqlCommand("ajouter_participant_seance", con))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;

                    
                    command.Parameters.AddWithValue("id_client", clientId);
                    command.Parameters.AddWithValue("id_seance", seanceId);

                   
                    con.Open();
                    command.ExecuteNonQuery();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'inscription : {ex.Message}");
                return false;
            }
            finally
            {
                con.Close();
            }
        }

        public void RegisterToSeance(string idClient, int id_seance)
        {
            
            
                try
                {
                   
                    {
                        con.Open();

                        // Commande pour appeler la procédure stockée
                        using (var command = new MySqlCommand("ajouter_participant_seance", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            // Ajouter les paramètres
                            command.Parameters.AddWithValue("@id_client", idClient);
                            command.Parameters.AddWithValue("@id_seance", id_seance);

                            // Exécuter la commande
                            using (var reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    string message = reader["message"].ToString();
                                    
                                }
                            }
                        }
                    }

                }

                catch (Exception ex)
                {
                    
                }
                con.Close();
            }

        public bool UserActiviter(string userID, string activiter)
        {
            var seances = GetAllSeance(activiter);

            foreach (var seance in seances)
            {

                MySqlCommand commande = new MySqlCommand();
                commande.Connection = con;
                commande.CommandText = $"SELECT * FROM inscription WHERE id_clients='{userID}' AND id_seance='{seance.Id}'";
                con.Open();
                MySqlDataReader r = commande.ExecuteReader();

                if (r.Read())
                {
                    con.Close();
                    return true;
                }
                con.Close();
            }
            con.Close();
            return false;




        }

        public List<Utilisateur> GetAllUser()
        {
            List<Utilisateur> resultas = new List<Utilisateur>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM clients ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            while (r.Read())
            {
                var date = r.GetDateTime("date_naissance");

                resultas.Add(new Utilisateur
                {
                    Id = (r["Id"].ToString()),
                    Nom = r["nom"].ToString(),
                    Prenom = (r["prenom"].ToString()),
                    Adresse = (r["adresse"].ToString()),
                    DateNaissance = date,
                    Age = int.Parse(r["age"].ToString()),


                });
            }

            var result = r.ToString();

            r.Close();
            con.Close();
            return resultas;
        }

        public void DeleteUser(string userId)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"DELETE FROM clients WHERE Id='{userId}' ";
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
        }

        public void AjoutUser(Utilisateur NewUser)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO clients ( nom, prenom, adresse, date_naissance) " +
                           "VALUES (@Nom, @Prenom, @Adresse, @DateNaissance)";
            commande.Parameters.AddWithValue("@Nom", NewUser.Nom);
            commande.Parameters.AddWithValue("@Prenom", NewUser.Prenom);
            commande.Parameters.AddWithValue("@Adresse", NewUser.Adresse);
            commande.Parameters.AddWithValue("@DateNaissance", NewUser.DateNaissance);          
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
        }

        public void ModifierUser(Utilisateur UpdatedUser)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;

            // Requête UPDATE 
            commande.CommandText = "UPDATE clients " +
                                   "SET nom = @Nom, prenom = @Prenom, adresse = @Adresse, date_naissance = @DateNaissance " +
                                   "WHERE Id = @Id"; 

            // Ajout des paramètres pour la mise à jour
            commande.Parameters.AddWithValue("@Nom", UpdatedUser.Nom);
            commande.Parameters.AddWithValue("@Prenom", UpdatedUser.Prenom);
            commande.Parameters.AddWithValue("@Adresse", UpdatedUser.Adresse);
            commande.Parameters.AddWithValue("@DateNaissance", UpdatedUser.DateNaissance);
            commande.Parameters.AddWithValue("@Id", UpdatedUser.Id);  

            // Exécution de la commande
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
        }

        public Utilisateur GetUser(string id)
        {
            Utilisateur resultas = new Utilisateur();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM clients WHERE Id='{id}' ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if (r.Read())
            {
                var date = r.GetDateTime("date_naissance");

                resultas.Id = (r["Id"].ToString());
                resultas.Nom = r["nom"].ToString();
                resultas.Prenom = (r["prenom"].ToString());
                resultas.Adresse = (r["adresse"].ToString());
                resultas.DateNaissance = date;
                resultas.Age = int.Parse(r["age"].ToString());
            }

            var result = r.ToString();

            r.Close();
            con.Close();
            return resultas;
        }

        public void DeleteActiviter(string activiterNom)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"DELETE FROM activites WHERE nom='{activiterNom}' ";
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
        }

        public void AjoutActiviter(Activiter NewActiviter)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO activites ( nom, type, prix_organisation, prix_clients) " +
                           "VALUES (@Nom, @Type, @Prix_organisation, @Prix_clients)";
            commande.Parameters.AddWithValue("@Nom", NewActiviter.Nom);
            commande.Parameters.AddWithValue("@Type", NewActiviter.Type);
            commande.Parameters.AddWithValue("@Prix_organisation", NewActiviter.CoutUnitaire);
            commande.Parameters.AddWithValue("@Prix_clients", NewActiviter.CoutClient);
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
        }

        public void ModifierActiviter(Activiter UpdatedActiviter)
        {
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;

            // Requête UPDATE 
            commande.CommandText = "UPDATE activites " +
                                   "SET  type = @Type, prix_organisation = @PrixUnitaire, prix_clients = @Prixclients " +
                                   "WHERE nom = @Nom";

            // Ajout des paramètres pour la mise à jour
            commande.Parameters.AddWithValue("@Type", UpdatedActiviter.Type);
            commande.Parameters.AddWithValue("@PrixUnitaire", UpdatedActiviter.CoutUnitaire);
            commande.Parameters.AddWithValue("@Prixclients", UpdatedActiviter.CoutClient);
            commande.Parameters.AddWithValue("@Nom", UpdatedActiviter.Nom);

            // Exécution de la commande
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
        }

        public Activiter GetActiviter(string nom)
        {
            Activiter resultas = new Activiter();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM activites WHERE nom='{nom}'";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if (r.Read())
            {
                float.TryParse(r["prix_organisation"].ToString(), out float prix);
                float.TryParse(r["prix_clients"].ToString(), out float prixClients);

                resultas.Nom = r["nom"].ToString();
                resultas.Type = r["type"].ToString();
                resultas.CoutUnitaire = prix;
                resultas.CoutClient = prixClients;
            }

            var result = r.ToString();

            r.Close();
            con.Close();
            return resultas;
        }

    }
}
