-- MySQL Script generated by MySQL Workbench
-- Tue Mar 27 11:14:08 2018
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering
use Escapade;

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema Escapade
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Table `Client`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Client` (
  `NumClient` VARCHAR(45) NOT NULL,
  `Nom` VARCHAR(45) NULL,
  `NumTel` INT NULL,
  `Email` VARCHAR(45) NULL,
  PRIMARY KEY (`NumClient`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Séjour`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Séjour` (
  `NumSejour` VARCHAR(45) NOT NULL,
  `Date` DATETIME NULL,
  `Theme` VARCHAR(3) NULL,
  `Hebergement` VARCHAR(45) NULL,
  PRIMARY KEY (`NumSejour`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Voiture`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Voiture` (
  `Immatriculation` VARCHAR(45) NOT NULL,
  `Marque` VARCHAR(45) NULL,
  `Modele` VARCHAR(45) NULL,
  `Type` VARCHAR(45) NULL,
  `Place` VARCHAR(45) NULL,
  `estVerif` TINYINT NULL,
  `estDispo` TINYINT NULL,
  `estRendu` TINYINT NULL,
  PRIMARY KEY (`Immatriculation`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Reservation`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Reservation` (
  `NumResa` INT NOT NULL,
  `Séjour_NumSejour` VARCHAR(45) NOT NULL,
  `Voiture_Immatriculation` VARCHAR(45) NOT NULL,
  `Client_NumClient` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`NumResa`),
  INDEX `fk_Reservation_Séjour1_idx` (`Séjour_NumSejour` ASC),
  INDEX `fk_Reservation_Voiture1_idx` (`Voiture_Immatriculation` ASC),
  INDEX `fk_Reservation_Client1_idx` (`Client_NumClient` ASC),
  CONSTRAINT `fk_Reservation_Séjour1`
    FOREIGN KEY (`Séjour_NumSejour`)
    REFERENCES `Séjour` (`NumSejour`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Reservation_Voiture1`
    FOREIGN KEY (`Voiture_Immatriculation`)
    REFERENCES `Voiture` (`Immatriculation`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Reservation_Client1`
    FOREIGN KEY (`Client_NumClient`)
    REFERENCES `Client` (`NumClient`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Parking`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Parking` (
  `CodeParking` VARCHAR(45) NOT NULL,
  `Nom` VARCHAR(45) NULL,
  `Adresse` VARCHAR(45) NULL,
  `CodePostal` VARCHAR(45) NULL,
  `Ville` VARCHAR(45) NULL,
  PRIMARY KEY (`CodeParking`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Controleur`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Controleur` (
  `NumControleur` INT NOT NULL,
  `Nom` VARCHAR(45) NULL,
  `Prenom` VARCHAR(45) NULL,
  PRIMARY KEY (`NumControleur`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Intervention`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Intervention` (
  `NumIntervention` INT NOT NULL,
  `Motif` VARCHAR(45) NULL,
  `Date` DATETIME NULL,
  `Controleur_NumControleur` INT NOT NULL,
  `Voiture_Immatriculation` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`NumIntervention`),
  INDEX `fk_Intervention_Controleur_idx` (`Controleur_NumControleur` ASC),
  INDEX `fk_Intervention_Voiture1_idx` (`Voiture_Immatriculation` ASC),
  CONSTRAINT `fk_Intervention_Controleur`
    FOREIGN KEY (`Controleur_NumControleur`)
    REFERENCES `Controleur` (`NumControleur`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Intervention_Voiture1`
    FOREIGN KEY (`Voiture_Immatriculation`)
    REFERENCES `Voiture` (`Immatriculation`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `Stationnement`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `Stationnement` (
  `Voiture_Immatriculation` VARCHAR(45) NOT NULL,
  `Parking_CodeParking` VARCHAR(45) NOT NULL,
  INDEX `fk_Stationnement_Voiture1_idx` (`Voiture_Immatriculation` ASC),
  INDEX `fk_Stationnement_Parking1_idx` (`Parking_CodeParking` ASC),
  CONSTRAINT `fk_Stationnement_Voiture1`
    FOREIGN KEY (`Voiture_Immatriculation`)
    REFERENCES `Voiture` (`Immatriculation`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Stationnement_Parking1`
    FOREIGN KEY (`Parking_CodeParking`)
    REFERENCES `Parking` (`CodeParking`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
