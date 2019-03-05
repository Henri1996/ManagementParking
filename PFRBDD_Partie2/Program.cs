using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;
using MySql.Data.MySqlClient;
using System.Diagnostics;
using Newtonsoft.Json;
using Json;
//using AspNet.ScriptManager.jQuery;


namespace PFRBDD
{
    public class Appartement
    {
        public int host_id = 2626;
        public int room_id { get; set; }
        public string room_type { get; set; }
        public int borough { get; set; }
        public string neighborhood { get; set; }
        public double overall_satisfaction { get; set; }
        public int bedrooms { get; set; }
        public int price { get; set; }
        public string week { get; set; }
        public string availability { get; set; }


        public override string ToString()
        {
            return string.Format("[Appartement: host_id={0}\nroom_id={1}\nroom_type={2}\nborough={3}\nneighborhood={4}\noverall_satisfaction={5}\nbedrooms={6}\nprice={7}\nweek={8}\navailability={9}]", host_id, room_id, room_type, borough, neighborhood, overall_satisfaction, bedrooms, price, week, availability);
        }
    }
    class MainClass
    {
        
        static void messageUnXml()
        {
            XmlDocument docXml = new XmlDocument();

            //création de l'en-tête XML (no <=> pas de DTD associée)
            docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.CreateComment("xml version='1.0' encoding='UTF-8'");

            XmlElement racine = docXml.CreateElement("Client");
            docXml.AppendChild(racine);
            racine.SetAttribute("sexe", "masculin");

            XmlElement Balise1 = docXml.CreateElement("Nom");
            Balise1.InnerText = "Monta";

            racine.AppendChild(Balise1);

            XmlElement Balise2 = docXml.CreateElement("Adresse");
            Balise2.InnerText = "ESILV, la defense";
            Balise2.SetAttribute("arrondissement", "16eme");
            racine.AppendChild(Balise2);

            XmlElement Balise3 = docXml.CreateElement("Date");
            Balise3.InnerText = "semaine 14";
            racine.AppendChild(Balise3);

            XmlElement Balise4 = docXml.CreateElement("Sejour");
            Balise3.InnerText = "visite de la Défense";
            racine.AppendChild(Balise3);

            string nom = "Message1";
            // enregistrement du document XML   ==> à retrouver dans le dossier bin\Debug de Visual Studio
            docXml.Save(nom+".xml");

            ProcessStartInfo Sortir = new ProcessStartInfo(@nom + ".xml", "");
            Process.Start(Sortir);



        }

        static string lectureXML(string chemin)
        {
            //créer l'arborescence des chemins XPath du document
            //--------------------------------------------------
            XPathDocument doc = new XPathDocument("Message1.xml");
            XPathNavigator nav = doc.CreateNavigator();

            //créer une requete XPath

            string maRequeteXPath = "/Client/Nom";
            XPathExpression expr = nav.Compile(maRequeteXPath);

            //exécution de la requete

            XPathNodeIterator nodes = nav.Select(expr);// exécution de la requête XPath
            string nom = "";
            while (nodes.MoveNext())
            {

                nom = nodes.Current.Value;
            }
            return nom;
        }

        static string VerificationClient(string maRequete, string mon_nom)
        //liste des marques
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            //Console.WriteLine(maRequete + " : ");
            
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = maRequete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            string nom = "";
            string num_client = "";
            while (reader.Read())// parcours ligne par ligne et renvoie un false quand la ligne est vide 
            {
                nom = reader.GetString(0);  // récupération de la 1ère colonne 
                if (nom == mon_nom)
                {
                    num_client = reader.GetString(1); // on stock le num client
                    Console.WriteLine("Le client existe deja dans la base de donnée, son numero client est : "+num_client);
                }
            }
            reader.Close();
            if (num_client == "")
            {
                MySqlCommand nouvellecommmande = connection.CreateCommand();
                num_client = "C669";
                nouvellecommmande.CommandText = "INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('"+num_client+"', 'Monta', 0699887766, 'monnom@gmail.fr');";
                try { reader = nouvellecommmande.ExecuteReader(); } catch (Exception e) { Console.WriteLine(e.Message); }
                reader.Close();
                Console.WriteLine("Insertion réussi!\nSon numero client est le : "+num_client);
            }


            connection.Close();
            Console.WriteLine();
            return num_client;
        }

        static string VerificationVoiture(string maRequete)
        //liste des marques
        {
            //Console.WriteLine(maRequete + " : ");
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = maRequete;

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            //reader.Close();



            string ma_voiture = "";
            //reader.Read();
            while(reader.Read() && reader.GetString(1)=="1")
            {
                ma_voiture = reader.GetString(0);
                
            }


            reader.Close();

            if (ma_voiture == "")
            {
                MySqlCommand nouvellecommmande = connection.CreateCommand();
                nouvellecommmande.CommandText = "select immatriculation from voiture where estDispo='1';";
                reader = nouvellecommmande.ExecuteReader();
                reader.Read();
                ma_voiture = reader.GetString(0);
                reader.Close();

                MySqlCommand nouvellecommmande2 = connection.CreateCommand();
                nouvellecommmande2.CommandText = "update Stationnement set Parking_CodeParking='A16' where Voiture_Immatriculation='"+ma_voiture+"';";
                reader = nouvellecommmande2.ExecuteReader();
                Console.WriteLine("La voiture " + ma_voiture + " a ete deplace dans le parking du 16 ieme arrondissement");
                reader.Close();
            }

            connection.Close();

            return ma_voiture;
        }

        static bool verifCond(bool [] mesCond)
        {
            bool res = false;
            if(mesCond[0]==true && mesCond[1] == true && mesCond[2] == true && mesCond[3] == true)
            {
                res = true;
            }
            return res;
        }
        //E5
        static List<Appartement> Deserialisation()
        {
            List<string> mesinfos = new List<string>();

            StreamReader reader = new StreamReader("ReponseRBNP.Json");
            string message =reader.ReadToEnd();
            List<Appartement> mesAppart = JsonConvert.DeserializeObject<List<Appartement>>(message);



            return mesAppart;

           
        }

        static string redactionmessage2Xml(List<Appartement> maliste,string immatriculation, string nom_client ="Monta" , string num_client = "C669")
        {
            
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "select p.Nom , v.Place from voiture v , parking p, stationnement where v.immatriculation ='"+immatriculation+"' and voiture_immatriculation = immatriculation and Parking_CodeParking = CodeParking";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            string nom_parking = reader.GetString(0);
            string Num_place = reader.GetString(1);
            Console.WriteLine("\nLa voiture ("+immatriculation+") est garé dans le parking "+nom_parking+" a la place "+Num_place+"\n");

            reader.Close();
            XmlDocument docXml = new XmlDocument();

            // création de l'en-tête XML (no <=> pas de DTD associée)
            docXml.CreateXmlDeclaration("1.0", "UTF-8", "no");
            docXml.CreateComment("xml version='1.0' encoding='UTF-8'");

            XmlElement Racine_Racine = docXml.CreateElement("Message_M2");
            docXml.AppendChild(Racine_Racine);

            XmlElement balise_detail = docXml.CreateElement("Details_Reservation");
            Racine_Racine.AppendChild(balise_detail);

            XmlElement Balise_detail1 = docXml.CreateElement("Numero_reservation");
            Balise_detail1.InnerText = "050";
            balise_detail.AppendChild(Balise_detail1);

            XmlElement Balise_detail2 = docXml.CreateElement("Adherent");
            balise_detail.AppendChild(Balise_detail2);

            XmlElement Balise_detail_adherant = docXml.CreateElement("Nom");
            Balise_detail_adherant.InnerText = nom_client;
            Balise_detail2.AppendChild(Balise_detail_adherant);
            XmlElement Balise_detail_adherant2 = docXml.CreateElement("Numero_adherent");
            Balise_detail_adherant2.InnerText = num_client;
            Balise_detail2.AppendChild(Balise_detail_adherant2);

            XmlElement Balise_detail3 = docXml.CreateElement("Detail_sejour");
            balise_detail.AppendChild(Balise_detail3);

            XmlElement Balise_detail_Sejour = docXml.CreateElement("Nom_theme");
            Balise_detail_Sejour.InnerText = "16_ieme_arrondissement";
            Balise_detail3.AppendChild(Balise_detail_Sejour);

            XmlElement Balise_detail_Sejour2 = docXml.CreateElement("Date");
            Balise_detail_Sejour2.InnerText = "Semaine 14";
            Balise_detail3.AppendChild(Balise_detail_Sejour2);

            XmlElement Balise_detail_Sejour3 = docXml.CreateElement("Etat");
            Balise_detail_Sejour3.InnerText = "Reserve";
            Balise_detail3.AppendChild(Balise_detail_Sejour3);

            XmlElement Balise_detail4 = docXml.CreateElement("Detail_Voiture");
            balise_detail.AppendChild(Balise_detail4);

            XmlElement Balise_detail_Voiture = docXml.CreateElement("Nom_parking");
            Balise_detail_Voiture.InnerText = nom_parking;
            Balise_detail4.AppendChild(Balise_detail_Voiture);

            XmlElement Balise_detail_Voiture1 = docXml.CreateElement("Numero_place");
            Balise_detail_Voiture1.InnerText = Num_place;
            Balise_detail4.AppendChild(Balise_detail_Voiture1);

            XmlElement Balise_detail_Voiture2 = docXml.CreateElement("Immatriculation");
            Balise_detail_Voiture2.InnerText = immatriculation;
            Balise_detail4.AppendChild(Balise_detail_Voiture2);

            for (int i = 0; i < 3; i++)
            {
                
                XmlElement racine = docXml.CreateElement("Appartement_Selectionne");
                Racine_Racine.AppendChild(racine);


                XmlElement BaliseA = docXml.CreateElement("Numero_de_chambre");
                BaliseA.InnerText = Convert.ToString(maliste.ElementAt(i).room_id);
                racine.AppendChild(BaliseA);

                XmlElement Balise3 = docXml.CreateElement("Type_de_chambre");
                Balise3.InnerText = maliste.ElementAt(i).room_type;
                racine.AppendChild(Balise3);

                XmlElement Balise4 = docXml.CreateElement("Arrondissement");
                Balise4.InnerText = Convert.ToString(maliste.ElementAt(i).borough);
                racine.AppendChild(Balise4);

                XmlElement Balise5 = docXml.CreateElement("Station_proche");
                Balise5.InnerText = maliste.ElementAt(i).neighborhood;
                racine.AppendChild(Balise5);



                XmlElement Balise7 = docXml.CreateElement("Note_satisfaction");
                Balise7.InnerText = Convert.ToString(maliste.ElementAt(i).overall_satisfaction);
                racine.AppendChild(Balise7);



                XmlElement Balise9 = docXml.CreateElement("Nombre_chambre");
                Balise9.InnerText = Convert.ToString(maliste.ElementAt(i).bedrooms);
                racine.AppendChild(Balise9);

                XmlElement Balise10 = docXml.CreateElement("Prix");
                Balise10.InnerText = Convert.ToString(maliste.ElementAt(i).price);
                racine.AppendChild(Balise10);



                XmlElement Balise15 = docXml.CreateElement("Disponible");
                Balise15.InnerText = maliste.ElementAt(0).availability;
                racine.AppendChild(Balise15);
            }

            string nom_fichier = "Message2.xml";
            // enregistrement du document XML   ==> à retrouver dans le dossier bin\Debug de Visual Studio
            docXml.Save(nom_fichier);

            return nom_fichier;
        }
        static List<Appartement> ChoisirAppartement(List<Appartement>mesAppart)
        {
            List<Appartement> Selection = new List<Appartement>();
            foreach(Appartement i in mesAppart)
            {
                if(i.borough == 16 && i.overall_satisfaction >= 4.5 && i.bedrooms == 1 && i.availability=="yes")
                {
                    Selection.Add(i);
                }
            }
            return Selection;
        }
        static void AfficherList(List<Appartement>maList)
        {
            foreach(Appartement i in maList)
            {
                Console.WriteLine(i);
            }
        }
        static void AfficherPrettyJson(string nomFichier)
        {
            StreamReader reader = new StreamReader(nomFichier);
            JsonTextReader jreader = new JsonTextReader(reader);
            while (jreader.Read())
            {
                if (jreader.Value != null)
                {
                    if (jreader.TokenType.ToString() == "PropertyName")
                    {
                        Console.Write(jreader.Value + " : ");
                    }
                    else
                    {
                        Console.WriteLine(jreader.Value);
                    }
                }
                else
                {
                    if (jreader.TokenType.ToString() == "StartObject") Console.WriteLine("Nouvel objet\n--------------");
                    if (jreader.TokenType.ToString() == "EndObject") Console.WriteLine("-------------\n");
                    if (jreader.TokenType.ToString() == "StartArray") Console.WriteLine("Liste\n");
                }
            }
            jreader.Close();
            reader.Close();
        }
        static void redactionMessageJson(Appartement monAppart)
        {
            string monFichier = "NotreReponse.json";

            //informations sur notre reponse

            string host_id = Convert.ToString(monAppart.host_id);
            string room_id = Convert.ToString(monAppart.room_id);
            string week = monAppart.week;
            monAppart.availability = "no";
            string availability = monAppart.availability;



            //instanciation des "writer"
            StreamWriter writer = new StreamWriter(monFichier);
            JsonTextWriter jwriter = new JsonTextWriter(writer);

            //debut du fichier Json
            jwriter.WriteStartObject();

            //debut du tableau Json
            jwriter.WritePropertyName("ReponseEscapade");
            jwriter.WriteStartArray();
           
                
            jwriter.WriteStartObject();
            jwriter.WritePropertyName("Host");
            jwriter.WriteValue(host_id);
            jwriter.WritePropertyName("room_id");
            jwriter.WriteValue(room_id);
            jwriter.WritePropertyName("week");
            jwriter.WriteValue(week);
            jwriter.WritePropertyName("Availability");
            jwriter.WriteValue(availability);
            jwriter.WriteEndObject();

            jwriter.WriteEndArray();
            jwriter.WriteEndObject();

            //femeture de "writer"
            jwriter.Close();
            writer.Close();

            Process.Start(monFichier);
            //relecture du fichier créé
            //-----------------------------

        }
        static string lecturemessage3Xml(string immat)
        {

            //créer l'arborescence des chemins XPath du document
            //--------------------------------------------------
            XPathDocument doc = new XPathDocument("Message3.xml");
            XPathNavigator nav = doc.CreateNavigator();

            //créer une requete XPath

            string maRequeteXPath = "/Confirmation/*";
            XPathExpression expr = nav.Compile(maRequeteXPath);

            //exécution de la requete

            XPathNodeIterator nodes = nav.Select(expr);// exécution de la requête XPath
            string text = "Confirmation du client\n\n";
            Dictionary<string, string> monDico = new Dictionary<string, string>();
            while (nodes.MoveNext())
            {

                text += nodes.Current.Name + " : " + nodes.Current.Value + "\n";
                monDico.Add(nodes.Current.Name,nodes.Current.Value);
            }

            Console.WriteLine(text);

            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "INSERT INTO `Reservation` (`NumResa`, `Séjour_NumSejour`, `Voiture_Immatriculation`, `Client_NumClient`) VALUES (1, '"+monDico["Numero_sejour"]+"', '"+immat+"', '"+monDico["Numero_client"]+"');";

            MySqlDataReader reader;
            try {reader = command.ExecuteReader();reader.Close();} catch(Exception ){Console.WriteLine("La reservation est deja dans la base de donnée");}

            MySqlCommand nouvellecommande = connection.CreateCommand();
            nouvellecommande.CommandText = "update Séjour set estConfirme='1' where NumSejour='" + monDico["Numero_sejour"] + "';";
            reader = nouvellecommande.ExecuteReader();
            reader.Close();
            Console.WriteLine("\nVotre reservation a ete crée et le sejour a ete confirme!");

            return text;
        }
        static string enregistrementposition(string numC)
        {
            string position = "";

            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "select Place from Voiture natural join Reservation natural join Client where NumClient='" + numC + "' and estRendu='1';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            reader.Read();
            position = reader.GetString(0);
            reader.Close();

            connection.Close();

            return position;
        }

        static void verificationcontroleur(string immat)
        {


            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command1 = connection.CreateCommand();
            command1.CommandText = "update Voiture set estVerif='0',estDispo='0' where   Immatriculation='" + immat + "';";
            MySqlDataReader reader1;
            reader1 = command1.ExecuteReader();
            reader1.Close();


            MySqlCommand command2 = connection.CreateCommand();
            MySqlDataReader reader2;
            command2.CommandText = "INSERT INTO `Intervention` (`NumIntervention`, `Motif`, `Date`, `Controleur_NumControleur`, `Voiture_Immatriculation`) VALUES (004, 'Netoyage', '2018-01-24', 001, '" + immat + "');"; 

            try {
                reader2 = command2.ExecuteReader();
                reader2.Close();
            }
            catch(Exception){Console.WriteLine("L'intervention a deja ete rajoute a la base de donnee");}



            connection.Close();
        }

        static void remiseneservice(string immat)
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();

            command.CommandText = "update Voiture set estVerif='1',estDispo='1' where estVerif=false and estDispo=false and Immatriculation='" + immat + "';";


            MySqlDataReader reader;
            reader = command.ExecuteReader();




            reader.Close();

            connection.Close();



        }

        static void TadBord1(string immat)
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select NumIntervention , Motif , Date , Controleur_NumControleur  from Intervention where Voiture_Immatriculation = '"+immat+"';";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while(reader.Read())
            {
                int numInter = Convert.ToInt32(reader.GetString(0));
                Console.WriteLine("Intervention numero :"+numInter);


                string motif = reader.GetString(1);
                string date = Convert.ToString(reader.GetString(2));
                int numCon = Convert.ToInt32(reader.GetString(3));

                Console.WriteLine("");
                Console.WriteLine("motif : " + motif);
                Console.WriteLine("date : " + date);
                Console.WriteLine("numero du controleur : " + numCon);
                Console.WriteLine("");
                Console.WriteLine("----------------------------------------");
            }
            reader.Close();
            connection.Close();

        }

        static void TadBord2(string numC)
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select NumSejour , Date , Theme , Hebergement  from Séjour a, Client b, Reservation c where b.NumClient = '" + numC + "' and b.NumClient=c.Client_NumClient and c.Séjour_NumSejour=a.NumSejour;";

            MySqlDataReader reader;
            reader = command.ExecuteReader();

            while (reader.Read())
            {
                string numSejour = reader.GetString(0);
                Console.WriteLine("Sejour numero :" + numSejour);


                string date = reader.GetString(1);
                string theme = Convert.ToString(reader.GetString(2));
                string hebergement = reader.GetString(3);


                Console.WriteLine("");
                Console.WriteLine("date : " + date);
                Console.WriteLine("theme : " + theme);
                Console.WriteLine("hebergement : " + hebergement);
                Console.WriteLine("");
                Console.WriteLine("----------------------------------------");
            }
            reader.Close();
            connection.Close();

        }
        static int max(int a,int b,int c)
        {
            if (a >= b && a >= c) return 1;
            if (b >= a && b >= c) return 2;
            else return 3;

        }
        static void TadBord3()
        {
            string connectionString = "SERVER=fboisson.ddns.net;PORT=3306;DATABASE=demo_henr;UID=S6-DEMO-HENR;PASSWORD=8336;SslMode=none";
            MySqlConnection connection = new MySqlConnection(connectionString);
            connection.Open();

            MySqlCommand command = connection.CreateCommand();
            command.CommandText = "select Séjour_NumSejour from reservation";

            MySqlDataReader reader;
            reader = command.ExecuteReader();
            int S1 = 0;
            int S2 = 0;
            int S3 = 0;

            while (reader.Read())
            {

                if(reader.GetString(0)== "S001") S1++;
                if (reader.GetString(0) == "S002") S2++;
                if (reader.GetString(0) == "S003")S3++;




            }
            int monMax = max(S1,S2,S3);
            if (monMax == 1) Console.WriteLine("Le sejour dans le 16 ieme arrodissement est le sejour le plus rentable.\nEn effet, il a ete choisi a " + S1 + " reprises.\nLes autres ont ete choisi moins que ca!");
            if (monMax == 2) Console.WriteLine("Le sejour dans le 13 ieme arrondissement est le sejour le plus rentable.\nEn effet, il a ete choisi a " + S2 + " reprises.\nLes autres ont ete choisi moins que ca!");
            if (monMax == 3) Console.WriteLine("Le sejour dans le 15 ieme arrondissement est le sejour le plus rentable.\nEn effet, il a ete choisi a " + S3 + " reprises.\nLes autres ont ete choisi moins que ca!");

            reader.Close();
            connection.Close();

        }
        public static void rouge(string aff)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(aff);
            Console.ResetColor();
        }
        public static void Demonstration()
        {
            rouge("\t\t\tDemonstration du logiciel Escapade\n\n");
            rouge("\t\tPartie 1\n\n");
            rouge("\n\tEtape 1 : Generation du fichier xml");
            Console.WriteLine("Appuyez pour continuer ...");
            Console.ReadKey();
            messageUnXml();
            Console.WriteLine("Votre fichier xml vient de s'ouvrir sur votre ordinateur et est enregistré sous le nom de 'Message1.xml' dans votre debug!");
           
            Console.ReadKey();
            rouge("\n\tEtape 2 : Verification de l'existance client ");
            Console.ReadKey();
            string nomC = lectureXML("Message1.xml");
            Console.WriteLine("D'apres le premier message Xml et grace a une requette xpath, le programme a recuperé le nom du client : "+nomC+" ");
            Console.ReadKey();
            string numC = VerificationClient("select nom , NumClient from client;", lectureXML("Message1.xml"));
            Console.WriteLine();
            Console.ReadKey();
            rouge("\n\tEtape 3 : Selection d'une voiture dans l'arrondissement souhaité \n");
            string immat = VerificationVoiture("select immatriculation , estDispo from voiture v, stationnement s , parking p where p.CodePostal = '75016' and p.CodeParking = s.Parking_CodeParking and s.Voiture_immatriculation = v.immatriculation;");
            Console.ReadKey();
            Console.WriteLine("Le programme vous a selectionné la voiture dont l'immatriculation est la suivante  : " + immat+"\n");
            Console.ReadKey();
            rouge("\n\tEtape 4 : La programme a bien recu la reponse RBNP qui vient de s'ouvrir sur votre ordinateur\n");
            Process.Start("ReponseRBNP.json");
            Console.ReadKey();
            rouge("\n\tEtape 5 : Selection de trois appartements repondant aux critères données\n");
            List<Appartement> maList = Deserialisation();
            List<Appartement> maSelection = ChoisirAppartement(maList);
            Console.WriteLine("D'apres les criteres suivant :\nArrondissement = 16 ieme\nNombre de chambre = 1\nEvalution >= 4.5\nDisponibilité : yes\n");
            Console.WriteLine("\nLe programme a choisi les trois appartements suivant : ");

            Console.ReadKey();
            for(int i = 0; i < 3; i++)
            {
                Console.WriteLine(maSelection.ElementAt(i));
                Console.WriteLine();
            }
            Console.ReadKey();
            Console.WriteLine("Pour la suite de la démonstration, nous utiliserons le premier appartement des trois choisis comme celui désiré par le client\n");
            Console.WriteLine("Voici le message json qui informe l'API de l'appartement choisi!");
            redactionMessageJson(maSelection.ElementAt(0));

            Process.Start("NotreReponse.json");

            Console.ReadKey();
            rouge("\n\tEtape 6 : Le programme effectue une reservation (en mode non confirme) et genere un fichier xml pour informer le client :");
            Console.ReadKey();
            string Fichier_Xml = redactionmessage2Xml(maSelection, immat);
            Process.Start(Fichier_Xml);
            Console.ReadKey();
            rouge("\n\tEtape 7 : Validation du séjour par le client ");
            Console.ReadKey();
            Console.WriteLine("\nLe message de confirmation vient de s'ouvrir sur votre ordinateur !");
            Process.Start("Message3.xml");
            Console.ReadKey();
            Console.WriteLine("\nLa reservation va etre crée et le sejour va etre confirmé ...");
            Console.ReadKey();
            lecturemessage3Xml(immat);
            Console.ReadKey();
            // CHECK OUT
            rouge("\n\tCheck out : \n\tE1 : Enregistrement du rendu de la voiture  ");
            //Enregistrement du rendu de la voiture
            Console.ReadKey();
            string position = enregistrementposition(numC);
            Console.WriteLine("La voiture du client " + numC + " a bien ete rendue et se trouve a la place :" + position);
            Console.ReadKey();
            rouge("\n\tE2 : Verification du controleur sur la voiture");
            Console.ReadKey();
            //verification du controleur
            verificationcontroleur(immat);
            Console.WriteLine("La voiture " + immat + " a ete affecte a un controleur qui reporte qu'un nettoyage est necessaire avant sa remise en service.\n");
            Console.ReadKey();
            //Nettoyage voiture
            rouge("\n\tE3 : Nettoyage de la voiture");
            Console.WriteLine("");
            Console.ReadKey();
            Console.WriteLine("Nettoyage en cours....");
            Console.ReadKey();
            Console.WriteLine("Fin du nettoyage");
            Console.ReadKey();
            rouge("\n\tE4 : Remise en service de la voiture");
            Console.ReadKey();
            remiseneservice(immat);
            Console.WriteLine("La voiture " + immat + " est a nouveau disponible et a ete remise en service.");
            Console.ReadKey();
            rouge("\n\t\tTABLEAU DE BORD");
            rouge("\n\tRequete 1 :\n");
            Console.WriteLine("\nRequete pour afficher tous les interventions effectuées sur la voiture " +immat+ "\n");
            Console.ReadKey();
            TadBord1(immat);
            Console.ReadKey();
            rouge("\n\tRequete 2 :\n");
            Console.WriteLine("\nRequete pour afficher tous les sejours reservés par le client C655 puisque le client C669 n'a pas encore effectué de séjour\n");
            Console.ReadKey();

            TadBord2("C655");
            Console.ReadKey();

            rouge("\n\tRequete 3 :\n");
            Console.WriteLine("\nRequete qui recherche dans la base de donnée le séjour le plus rentable\n");

            Console.ReadKey();
            Console.WriteLine("\n");
            TadBord3();


            Console.ReadKey();



        }
        public static void Main(string[] args)
        {

         
            Demonstration();


        }
    }
}
