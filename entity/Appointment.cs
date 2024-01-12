﻿using System;

namespace HospitalManagementSystem.entity
{
    public class Appointment
    {
        private int appointmentId;
        private int patientId;
        private int doctorId;
        private DateTime appointmentDate;
        private string? description;

        public int AppointmentId { get { return appointmentId; } set { appointmentId = value; } }
        public int PatientId { get { return patientId; } set {  patientId = value; } }
        public int DoctorId { get { return doctorId; } set {  doctorId = value; } }
        public DateTime AppointmentDate { get {  return appointmentDate; } set { appointmentDate = value; } }
        public string? Description { get { return description; } set { description = value; } }

        public Appointment() { }
        public Appointment(int patientId, int doctorId, DateTime appointmentDate, string description)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            AppointmentDate = appointmentDate;
            Description = description;
        }
        public Appointment(int appointmentId, int patientId, int doctorId, DateTime appointmentDate, string description)
        {
            AppointmentId = appointmentId;
            PatientId = patientId;
            DoctorId = doctorId;
            AppointmentDate = appointmentDate;
            Description = description;
        }

        public override string ToString()
        {
            return $"Appointment ID: {AppointmentId}\nPatient ID: {PatientId}\nDoctor ID: {DoctorId}\nAppointment Date: {AppointmentDate}\nDescription: {Description}\n{new String('-', 30)}";
        }
    }
}
