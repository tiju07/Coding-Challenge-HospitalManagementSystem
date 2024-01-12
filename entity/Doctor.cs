using System;

namespace HospitalManagementSystem.entity
{
    public class Doctor
    {
        private int doctorId;
        private string? firstName;
        private string? lastName;
        private string? specialization;
        private string? contactNumber;

        public int DoctorId { get {  return doctorId; } set { doctorId = value; } }
        public string? FirstName { get {  return firstName; } set { firstName = value; } }
        public string? LastName { get { return lastName; } set { lastName = value; } }
        public string Specialization { get { return specialization; } set { specialization = value; } }
        public string? ContactNumber { get { return contactNumber; } set { contactNumber = value; } }

        public Doctor() {}

        //Parameterized Constructor without "doctorID"
        public Doctor(string firstName, string lastName, string specialization, string contactNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            ContactNumber = contactNumber;
        }

        //Parameterized Constructor with "doctorID"
        public Doctor(int doctorId, string firstName, string lastName, string specialization, string contactNumber)
        {
            DoctorId = doctorId;
            FirstName = firstName;
            LastName = lastName;
            Specialization = specialization;
            ContactNumber = contactNumber;
        }

        //Override for the ToString function
        public override string ToString()
        {
            return $"{new String('-', 30)}\nDoctor ID: {DoctorId}\nDoctor Name: {FirstName + " " + LastName}\nSpecialization : {Specialization}\nContact Number: {ContactNumber}\n{new String('-', 30)}";
        }
    }
}
