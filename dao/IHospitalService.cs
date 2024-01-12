using System;
using HospitalManagementSystem.entity;

namespace HospitalManagementSystem.dao
{
    interface IHospitalService
    {
        /// <summary>
        /// Retrieves the appointment associated with the given ID
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>An Appointment object</returns>
        public Appointment GetAppointmentById(int appointmentId);

        /// <summary>
        /// Retrieves all the appointments associated with the given Patient ID
        /// </summary>
        /// <param name="patientId"></param>
        /// <returns>A list of Appointment objects</returns>
        public List<Appointment> GetAppointmentsForPatient(int patientId);
        
        /// <summary>
        /// Retrieves all the appointments associated with the given Doctor ID
        /// </summary>
        /// <param name="doctorId"></param>
        /// <returns>A list of Appointment objects</returns>
        public List<Appointment> GetAppointmentsForDoctor(int doctorId);
        
        /// <summary>
        /// Schedules an appointment with the given details
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns>A boolean value, true if the appointment was successfully registerd, false otherwise.</returns>
        public bool ScheduleAppointment(Appointment appointment);

        /// <summary>
        /// Updates a specific appointment
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns>A boolean value, true if the appointment was successfully updated, false otherwise.</returns>
        public bool UpdateAppointment(Appointment appointment);

        /// <summary>
        /// Cancels the appointment associated with the given Appointment ID
        /// </summary>
        /// <param name="appointmentId"></param>
        /// <returns>A boolean value, true if the appointment was successfully canceled, false otherwise.</returns>
        public bool CancelAppointment(int appointmentId);
    }
}
