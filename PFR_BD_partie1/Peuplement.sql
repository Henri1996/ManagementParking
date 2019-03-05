
-- -----------------------------------------------------
-- Data for table `Client`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C655', 'Daniel', 0662150650, 'daniel@gmail.com');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C656', 'Gabin', 0673150650, 'gabin@gmail.com');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C657', 'Reno', 0662150870, 'reno@gmail.com');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C658', 'Delon', 0663440650, 'delon@gmail.com');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C659', 'Depardieu', 0600150650, 'depardieu1@gmail.com');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C660', 'Depardieu', 0648883468, 'depardieu2@gmail.com');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C661', 'Dujardin', 0643334680, 'dujardin@gmail.com');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C662', 'Boon', 0648987468, 'boon@orange.fr');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C663', 'Willis', 0648883999, 'willis@orange.fr');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C664', 'Cassel', 0634564689, 'cassel@orange.fr');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C665', 'Charlot', 0698763468, 'charlot@orange.fr');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C666', 'Canet', 0683664468, 'canet@wanadoo.fr');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C667', 'Richard', 0611113468, 'richard@wanadoo.fr');
INSERT INTO `Client` (`NumClient`, `Nom`, `NumTel`, `Email`) VALUES ('C668', 'Canet', 0648345686, 'canet2@gmail.fr');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Séjour`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Séjour` (`NumSejour`, `Date`, `Theme`, `Hebergement`) VALUES ('S001', '2018-04-05', 'A16', '132KJFSKJF');
INSERT INTO `Séjour` (`NumSejour`, `Date`, `Theme`, `Hebergement`) VALUES ('S002', '2018-03-19', 'A13', 'SDNKJ2E23');
INSERT INTO `Séjour` (`NumSejour`, `Date`, `Theme`, `Hebergement`) VALUES ('S003', '2018-02-01', 'ORY', '233NJK22N');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Voiture`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('13AZE21', 'PEUGEOT ', '508', 'BERLINE', 'NULL', 0, 0, 0);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('43BDI09', 'AUDI', 'A4', 'BERLINE', 'A9', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('98TGH32', 'CITROEN', 'C4', 'BERLINE', 'A8', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('90VBC78', 'CITROEN', 'CACTUS', 'BERLINE', 'A7', 0, 0, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('32BNV67', 'MERCEDES', 'AMG', 'CABRIOLET', 'A4', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('78DGS89', 'MERCEDES', 'CLASSEC', 'CABRIOLET', 'NULL', 0, 0, 0);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('65GDT90', 'MERCEDES', 'CLASSEE', 'CABRIOLET', 'A2', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('90TRE12', 'AUDI', 'A5', 'BERLINE', 'NULL', 0, 0, 0);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('72TRE56', 'RENAULT', 'CAPTURE', 'BERLINE', 'A3', 1, 1, 1);
INSERT INTO `Voiture` (`Immatriculation`, `Marque`, `Modele`, `Type`, `Place`, `estVerif`, `estDispo`, `estRendu`) VALUES ('18NVA00', 'RENAULT', 'TALISMAN', 'BERLINE', 'A1', 1, 1, 1);

COMMIT;


-- -----------------------------------------------------
-- Data for table `Reservation`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Reservation` (`NumResa`, `Séjour_NumSejour`, `Voiture_Immatriculation`, `Client_NumClient`) VALUES (1, 'S001', '13AZE21', 'C655');
INSERT INTO `Reservation` (`NumResa`, `Séjour_NumSejour`, `Voiture_Immatriculation`, `Client_NumClient`) VALUES (2, 'S003', '90VBC78', 'C665');
INSERT INTO `Reservation` (`NumResa`, `Séjour_NumSejour`, `Voiture_Immatriculation`, `Client_NumClient`) VALUES (3, 'S001', '78DGS89', 'C657');
INSERT INTO `Reservation` (`NumResa`, `Séjour_NumSejour`, `Voiture_Immatriculation`, `Client_NumClient`) VALUES (4, 'S002', '13AZE21', 'C660');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Parking`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('CodeParking', 'Nom', 'Adresse', 'CodePostal', 'Ville');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A01', 'RIVOLI', '2rueBoucher', '75001', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A03', 'BEAUBOURG', '31rueBeaubourg', '75003', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A04', 'LOBAU', '4rueLobau', '75004', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A05', 'SOUFFLOT', '22rueSoufflot', '75005', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A06', 'JARDIN DES PLANTES', '25rueGeoffroySaintHilaire', '75006', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A07', 'MAUBOURG', '45quaiD\'Orsay', '75007', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A08', 'CHAMPS-ELYSEES', '77avenueMarceau', '75008', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A09', 'PIGALLE', '10rueJean-BaptistePigalle', '75009', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A10', 'LARIBOISIERE', '1bisrueAmbroisePare', '75010', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A11', 'OBERKAMPF', '11rueTernaux', '75011', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A12', 'GARE DE LYON', '6ruedeRambouillet', '75012', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A13', 'ITALIE', '23rueStephenPichon', '75013', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A14', 'RASPAIL', '120boulevardduMontparnasse', '75014', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A15', 'BEAUGRENELLE', '5quaiAndreCitroen', '75015', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A16', 'VICTOR HUGO', '74avenueVictorHugo', '75016', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A17', 'TERNES', '38avenuedesTernes', '75017', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A18', 'STALINGRAD', '13rueD\'Aubervillier', '75018', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A19', 'PHILARMONIE', '185boulevardSerurier', '75019', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('A20', 'ROSA PARKS', '157boulevardMacDonald', '75020', 'PARIS');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('ORY', 'ORLY', 'OrlyAirport', '94310', 'ORLY');
INSERT INTO `Parking` (`CodeParking`, `Nom`, `Adresse`, `CodePostal`, `Ville`) VALUES ('CDG', 'ROISSY', 'RoissyAirport', '95700', 'ROISSY EN FRANCE');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Controleur`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Controleur` (`NumControleur`, `Nom`, `Prenom`) VALUES (001, 'Romano', 'Pierre');
INSERT INTO `Controleur` (`NumControleur`, `Nom`, `Prenom`) VALUES (002, 'Lebas', 'Paul');
INSERT INTO `Controleur` (`NumControleur`, `Nom`, `Prenom`) VALUES (003, 'Dupain', 'Jacques');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Intervention`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Intervention` (`NumIntervention`, `Motif`, `Date`, `Controleur_NumControleur`, `Voiture_Immatriculation`) VALUES (001, 'Vidange', '2018-03-20', 003, '43BDI09');
INSERT INTO `Intervention` (`NumIntervention`, `Motif`, `Date`, `Controleur_NumControleur`, `Voiture_Immatriculation`) VALUES (002, 'Fuite', '2018-02-12', 002, '78DGS89');
INSERT INTO `Intervention` (`NumIntervention`, `Motif`, `Date`, `Controleur_NumControleur`, `Voiture_Immatriculation`) VALUES (003, 'Netoyage', '2018-01-24', 001, '90TRE12');

COMMIT;


-- -----------------------------------------------------
-- Data for table `Stationnement`
-- -----------------------------------------------------
START TRANSACTION;
INSERT INTO `Stationnement` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('98TGH32', 'A01');
INSERT INTO `Stationnement` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('43BDI09', 'A03');
INSERT INTO `Stationnement` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('90VBC78', 'A06');
INSERT INTO `Stationnement` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('32BNV67', 'A05');
INSERT INTO `Stationnement` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('65GDT90', 'A07');
INSERT INTO `Stationnement` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('72TRE56', 'A09');
INSERT INTO `Stationnement` (`Voiture_Immatriculation`, `Parking_CodeParking`) VALUES ('18NVA00', 'A03');

COMMIT;

