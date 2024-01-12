using System;


namespace HospitalManagementSystem.exception
{
    internal class PatientNumberNotFoundException : Exception
    {
        public PatientNumberNotFoundException(string message) : base(message) { }
        public PatientNumberNotFoundException()
        {
            Console.WriteLine("\nPatient not found!");
        }
    }
}
