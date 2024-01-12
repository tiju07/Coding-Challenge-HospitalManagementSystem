using System;

namespace HospitalManagementSystem.entity
{
    public class Patient
    {
        private int patientID;
        private string? firstName;
        private string? lastName;
        private DateTime dateOfBirth;
        private string? gender;
        private string? contactNumber;

        public int PatientID { get { return patientID; } set {  patientID = value; } }
        public string FirstName { get { return firstName; } set { firstName = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public DateTime DateOfBirth { get {  return dateOfBirth; } set {  dateOfBirth = value; } }
        public string Gender { get {  return gender; } set {  gender = value; } }
        public string ContactNumber { get {  return contactNumber; } set {  contactNumber = value; } }

        public Patient() {}

        //Parameterized Constructor without "patientID"
        public Patient(string firstName, string lastName, DateTime dateOfBirth, string gender, string contactNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            ContactNumber = contactNumber;
        }

        //Parameterized Constructor with "patientID"
        public Patient(int patientId, string firstName, string lastName, DateTime dateOfBirth, string gender, string contactNumber)
        {
            PatientID = patientId;
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            Gender = gender;
            ContactNumber = contactNumber;
        }

        //Override for the ToString function
        public override string ToString()
        {
            return $"{new String('-', 30)}\nPatient ID: {PatientID}\nPatient Name: {FirstName + " " + LastName}\nDate of Birth: {DateOfBirth.ToString("yyyy-MM-dd")}\nGender: {Gender}\nContact Number: {ContactNumber}\n{new String('-', 30)}";
        }
    }
}
