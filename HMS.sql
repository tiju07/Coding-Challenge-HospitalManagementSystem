CREATE DATABASE HospitalManagementSystem;

USE HospitalManagementSystem;

-- creating tables
--Patients
CREATE TABLE Patients ( 
patientId INT PRIMARY KEY IDENTITY(1,1),
firstName VARCHAR(50),
lastName VARCHAR(50),
dateOfBirth DATE,
gender VARCHAR(10),
contactNumber VARCHAR(15),
address VARCHAR(100) );

--Doctors
CREATE TABLE Doctors (
doctorId INT PRIMARY KEY IDENTITY(1,1),
firstName VARCHAR(50),
lastName VARCHAR(50),
specialization VARCHAR(100),
contactNumber VARCHAR(15) );

--Appointments table
CREATE TABLE Appointments (
appointmentId INT PRIMARY KEY IDENTITY(1,1),
patientId INT FOREIGN KEY REFERENCES Patients(patientId),
doctorId INT FOREIGN KEY REFERENCES Doctors(doctorId) ,
appointmentDate DATETIME,
description text,
);

-- inserting data
INSERT INTO Patients (firstName, lastName, dateOfBirth, gender, contactNumber, address)
VALUES
('John', 'Doe', '1990-05-15', 'Male', '123-456-7890', '123 Main St, Cityville, State'),
('Jane', 'Smith', '1985-08-22', 'Female', '987-654-3210', '456 Oak St, Townsville, State'),
('Mike', 'Johnson', '1978-12-10', 'Male', '555-123-4567', '789 Pine St, Villagetown, State'),
('Emily', 'Williams', '1995-04-03', 'Female', '111-222-3333', '321 Elm St, Hamletville, State'),
('Chris', 'Anderson', '1982-09-18', 'Male', '777-888-9999', '555 Birch St, Suburbia, State'),
('Sophia', 'Brown', '1998-07-25', 'Female', '444-555-6666', '987 Maple St, Countryside, State'),
('David', 'Miller', '1970-03-08', 'Male', '999-000-1111', '654 Pineapple St, Beachtown, State'),
('Olivia', 'Jones', '1989-11-30', 'Female', '222-333-4444', '876 Apple St, Mountainville, State'),
('Brian', 'Taylor', '1993-06-12', 'Male', '666-777-8888', '234 Cherry St, Riverside, State'),
('Mia', 'White', '1980-01-20', 'Female', '333-444-5555', '789 Grape St, Hillside, State');


INSERT INTO Doctors (firstName, lastName, specialization, contactNumber)
VALUES
('Dr. Robert', 'Smith', 'Cardiologist', '123-456-7890'),
('Dr. Sarah', 'Johnson', 'Orthopedic Surgeon', '987-654-3210'),
('Dr. Michael', 'Williams', 'Pediatrician', '555-123-4567'),
('Dr. Emily', 'Brown', 'Dermatologist', '111-222-3333'),
('Dr. Christopher', 'Anderson', 'Oncologist', '777-888-9999'),
('Dr. Sophia', 'Miller', 'Neurologist', '444-555-6666'),
('Dr. David', 'Davis', 'Gastroenterologist', '999-000-1111'),
('Dr. Olivia', 'Taylor', 'Endocrinologist', '222-333-4444'),
('Dr. Brian', 'Jones', 'Psychiatrist', '666-777-8888'),
('Dr. Mia', 'White', 'Rheumatologist', '333-444-5555');



INSERT INTO Appointments (patientId, doctorId, appointmentDate, description)
VALUES
(1, 1, '2023-01-10 09:00:00', 'Routine checkup'),
(2, 3, '2023-02-15 14:30:00', 'Follow-up for previous surgery'),
(3, 5, '2023-03-20 11:45:00', 'Vaccination for child'),
(4, 2, '2023-04-05 10:15:00', 'Skin condition consultation'),
(5, 6, '2023-05-12 13:00:00', 'Cancer treatment discussion'),
(6, 4, '2023-06-18 15:30:00', 'Neurological examination'),
(7, 7, '2023-07-25 08:45:00', 'Digestive issues consultation'),
(8, 9, '2023-08-30 16:15:00', 'Hormone level check'),
(9, 8, '2023-09-10 12:30:00', 'Psychiatric evaluation'),
(10, 10, '2023-10-22 09:30:00', 'Arthritis treatment plan');
