
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
            try
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
                
            }
            catch (Exception ex) { }
            return null;
        }

        public string? UserName(string userName)
        {


            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM clients WHERE Id='{userName}' ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                return r["prenom"].ToString() + " " + r["nom"].ToString();
            }
            }catch (Exception ex) { }
            return null;
        }

        public string? AdminName(string userName)
        {


            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM admin WHERE Id='{userName}' ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                return r["nom"].ToString();
            }
            }catch(Exception ex) { }    
            return null;
        }

        public string? UserLogIn(string userName)
        {


            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM clients WHERE Id='{userName}' ";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();

            if (r.Read())
            {
                return r["Id"].ToString();
            }
            }catch { }
            return null;
        }

        public List<Activiter> GetAllActiviter()
        {
            try { 
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
            }catch { }
            return new List<Activiter>();
        }

        public List<SeancesItem> GetAllSeance(string activiter)
        {
            try { 
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
            }catch {}
            return new List<SeancesItem>();
        }


       
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

                       
                        using (var command = new MySqlCommand("ajouter_participant_seance", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;

                            
                            command.Parameters.AddWithValue("@id_client", idClient);
                            command.Parameters.AddWithValue("@id_seance", id_seance);

                           
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
            try
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
            }
            catch (Exception ex) { }
            return false;




        }

        public List<Utilisateur> GetAllUser()
        {
            try { 
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
            }catch (Exception ex) { }
            return new List<Utilisateur>();
        }

        public void DeleteUser(string userId)
        {
            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"DELETE FROM clients WHERE Id='{userId}' ";
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
            }catch (Exception ex) { }
        }

        public void AjoutUser(Utilisateur NewUser)
        {
            try { 
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
        }catch { }
        }

        public void ModifierUser(Utilisateur UpdatedUser)
        {
            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;

            
            commande.CommandText = "UPDATE clients " +
                                   "SET nom = @Nom, prenom = @Prenom, adresse = @Adresse, date_naissance = @DateNaissance " +
                                   "WHERE Id = @Id"; 

            
            commande.Parameters.AddWithValue("@Nom", UpdatedUser.Nom);
            commande.Parameters.AddWithValue("@Prenom", UpdatedUser.Prenom);
            commande.Parameters.AddWithValue("@Adresse", UpdatedUser.Adresse);
            commande.Parameters.AddWithValue("@DateNaissance", UpdatedUser.DateNaissance);
            commande.Parameters.AddWithValue("@Id", UpdatedUser.Id);  

            
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
            }catch {}
        }

        public Utilisateur GetUser(string id)
        {
            try { 
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
        }catch { }
            return null;
        }

        public void DeleteActiviter(string activiterNom)
        {
            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"DELETE FROM activites WHERE nom='{activiterNom}' ";
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
        }catch { }  
        }

        public void AjoutActiviter(Activiter NewActiviter)
        {
            try { 
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
        }catch { }
        }

        public void ModifierActiviter(Activiter UpdatedActiviter)
        {
            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;

            
            commande.CommandText = "UPDATE activites " +
                                   "SET  type = @Type, prix_organisation = @PrixUnitaire, prix_clients = @Prixclients " +
                                   "WHERE nom = @Nom";

            
            commande.Parameters.AddWithValue("@Type", UpdatedActiviter.Type);
            commande.Parameters.AddWithValue("@PrixUnitaire", UpdatedActiviter.CoutUnitaire);
            commande.Parameters.AddWithValue("@Prixclients", UpdatedActiviter.CoutClient);
            commande.Parameters.AddWithValue("@Nom", UpdatedActiviter.Nom);

            
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
            }catch {}
        }

        public Activiter GetActiviter(string nom)
        {
            try { 
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
            }catch {}
            return null;
        }

        public List<SeancesItem> GetAllSeance()
        {
            try { 
            List<SeancesItem> resultas = new List<SeancesItem>();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM seances";
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
                    Nom= r["nom_activite"].ToString(),


                });
            }

            var result = r.ToString();

            r.Close();
            con.Close();
            return resultas;
            }catch { }
            return new List<SeancesItem>();
        }

        public void DeleteSeance(int idSeance)
        {
            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"DELETE FROM seances WHERE Id='{idSeance}' ";
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
            }catch { }
        }

        public void AjoutSeance(SeancesItem NewSeance)
        {
            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "INSERT INTO seances ( date, heure, place_disponible, place_maximum, nom_activite) " +
                           "VALUES (@Date, @Heure, @PlaceDispo, @PlaceMax, @Nom)";
            commande.Parameters.AddWithValue("@Date", NewSeance.Date);
            commande.Parameters.AddWithValue("@Heure", NewSeance.Heure);
            commande.Parameters.AddWithValue("@PlaceDispo", NewSeance.PlaceDispo);
            commande.Parameters.AddWithValue("@PlaceMax", NewSeance.PlaceMax);
            commande.Parameters.AddWithValue("@Nom", NewSeance.Nom);
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
            }catch{ }
        }

        public void ModifierSeance(SeancesItem seance)
        {
            try { 
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = "UPDATE seances " +
                                   "SET  date = @Date, heure = @Heure, place_disponible = @PlaceDispo, place_maximum = @PlaceMax, nom_activite = @Nom " +
                                   "WHERE Id = @id";
            commande.Parameters.AddWithValue("@Date", seance.Date);
            commande.Parameters.AddWithValue("@Heure", seance.Heure);
            commande.Parameters.AddWithValue("@PlaceDispo", seance.PlaceDispo);
            commande.Parameters.AddWithValue("@PlaceMax", seance.PlaceMax);
            commande.Parameters.AddWithValue("@Nom", seance.Nom);
            commande.Parameters.AddWithValue("@id", seance.Id);
            con.Open();
            int rowsAffected = commande.ExecuteNonQuery();
            con.Close();
            } catch { }
        }

        public SeancesItem GetSeance(int id)
        {
            try { 
            SeancesItem resultas = new SeancesItem();
            MySqlCommand commande = new MySqlCommand();
            commande.Connection = con;
            commande.CommandText = $"SELECT * FROM seances WHERE Id='{id}'";
            con.Open();
            MySqlDataReader r = commande.ExecuteReader();
            if (r.Read())
            {
                var date = r.GetDateTime("date");



                resultas.Id = int.Parse(r["Id"].ToString());
                resultas.Heure = r["heure"].ToString();
                resultas.PlaceDispo = int.Parse(r["place_disponible"].ToString());
                resultas.PlaceMax = int.Parse(r["place_maximum"].ToString());
                resultas.Date = date;
                resultas.Nom = r["nom_activite"].ToString();


                
            }

            var result = r.ToString();

            r.Close();
            con.Close();
            return resultas;
            }catch { }
            return null;
        }

        public int CountClients()
        {
            int clientCount = 0;

            try
            {          
                con.Open();
                string query = "SELECT COUNT(*) FROM clients";
                MySqlCommand cmd = new MySqlCommand(query, con);
                clientCount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du comptage des clients: " + ex.Message);
            }
            finally
            {
                
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return clientCount;
        }

        public int CountActiviter()
        {
            int activiterCount = 0;

            try
            {
                con.Open();
                string query = "SELECT COUNT(*) FROM activites";
                MySqlCommand cmd = new MySqlCommand(query, con);
                activiterCount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du comptage des activiters: " + ex.Message);
            }
            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return activiterCount;
        }

        public int Countinscription(string activiter)
        {
            int inscriptionCount = 0;

            try
            {
                var seance = GetAllSeance(activiter);
                foreach (var item in seance)
                {
                    con.Open();

                    string query = $"SELECT COUNT(*) FROM inscription WHERE id_seance={item.Id}";
                    MySqlCommand cmd = new MySqlCommand(query, con);
                    inscriptionCount += Convert.ToInt32(cmd.ExecuteScalar());
                    con.Close();    
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du comptage des activiters: " + ex.Message);
            }
            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return inscriptionCount;
        }

        public int Moyenne(string activiter)
        {
            int inscriptionCount = 0;
            int noteTotal = 0;

            try
            {
                var seance = GetAllSeance(activiter);
                foreach (var item in seance)
                {
                    

                    string query = $"SELECT * FROM inscription WHERE id_seance={item.Id}";
                    MySqlCommand commande = new MySqlCommand();
                    commande.Connection = con;
                    commande.CommandText = query;
                    con.Open();
                    MySqlDataReader r = commande.ExecuteReader();
                    while (r.Read())
                    {
                        if (r["rating"].ToString() == null)
                        {
                            continue;
                        }
                        var note = int.Parse(r["rating"].ToString());
                        noteTotal += note;
                        inscriptionCount++;
                        
                    }

               
                    con.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du comptage des activiters: " + ex.Message);
            }
            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            if (inscriptionCount == 0)
            {
                return 0;
            }
            return noteTotal/inscriptionCount;
        }


        public int MoyenneAge()
        {
            int usercount = 0;
            int ageTotal = 0;

            try
            {
                var user = GetAllUser();
                foreach (var item in user)
                {
                    con.Open();


                    usercount++;
                    ageTotal += item.Age;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du comptage des activiters: " + ex.Message);
            }
            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            if (usercount == 0)
            {
                return 0;
            }
            return ageTotal / usercount;
        }

        public int CountPlace()
        {
            int PlaceCount = 0;

            try
            {
                con.Open();
                string query = "SELECT SUM(place_disponible) FROM seances";
                MySqlCommand cmd = new MySqlCommand(query, con);
                PlaceCount = Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du comptage des clients: " + ex.Message);
            }
            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return PlaceCount;
        }

        public float PrixMax()
        {
            float prixmax = 0;

            try
            {
                con.Open();
                string query = "SELECT MAX(prix_organisation) FROM activites";
                MySqlCommand cmd = new MySqlCommand(query, con);
                prixmax = Convert.ToInt64(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erreur lors du comptage des clients: " + ex.Message);
            }
            finally
            {

                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }

            return prixmax;
        }



    }
}
