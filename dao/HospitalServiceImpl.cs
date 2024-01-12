using System;
using HospitalManagementSystem.entity;
using HospitalManagementSystem.exception;
using HospitalManagementSystem.main;
using Microsoft.Data.SqlClient;
using System.Linq;

namespace HospitalManagementSystem.dao
{
    public class HospitalServiceImpl : IHospitalService
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader reader;
        public Appointment GetAppointmentById(int appointmentId)
        {
            Appointment appointment = new Appointment();
            try
            {
                using (conn = DBConnection.GetConnection())
                {
                    string query = $"SELECT * FROM Appointments WHERE appointmentID = {appointmentId}";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    //var columns = Enumerable.Range(0, reader.FieldCount).Select(reader.GetName).ToList();
                    if (!reader.HasRows) { Console.WriteLine("No appointments found!"); }
                    else
                    {
                        reader.Read();

                        var data = Enumerable.Range(0, reader.FieldCount).Select(reader.GetValue).ToList();
                        appointment.AppointmentId = (int)data[0];
                        appointment.PatientId = (int)data[1];
                        appointment.DoctorId = (int)data[2];
                        appointment.AppointmentDate = (DateTime)data[3];
                        appointment.Description = (string)data[4];
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return appointment;
        }

        public List<Appointment> GetAppointmentsForPatient(int patientId)
        {
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                using (conn = DBConnection.GetConnection())
                {
                    bool patientIdIsValid = PatientIdValidator(patientId, conn);
                    if (!patientIdIsValid) { throw new PatientNumberNotFoundException("Invalid patient id!"); }
                    string query = $"SELECT * FROM Appointments";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    if (!reader.HasRows) { Console.WriteLine("No appointments found!"); }
                    else
                    {
                        while (reader.Read())
                        {
                            var data = Enumerable.Range(0, reader.FieldCount).Select(reader.GetValue).ToList();

                            Appointment appointment = new Appointment((int)data[0], (int)data[1], (int)data[2], (DateTime)data[3], (string)data[4]);
                            appointments.Add(appointment);
                        }
                    }
                    var appointmentsForPatient = from Appointment appointment in appointments
                                                 where appointment.PatientId == patientId
                                                 select appointment;
                    appointments = appointmentsForPatient.ToList();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return appointments;
        }

        public List<Appointment> GetAppointmentsForDoctor(int doctorId)
        {
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                using (conn = DBConnection.GetConnection())
                {
                    bool doctorIdIsValid = DoctorIdValidator(doctorId, conn);
                    if (!doctorIdIsValid) { throw new Exception("Invalid doctor id!"); }
                    string query = $"SELECT * FROM Appointments";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    if (!reader.HasRows) { Console.WriteLine("No appointments found!"); }
                    else
                    {
                        while (reader.Read())
                        {
                            var data = Enumerable.Range(0, reader.FieldCount).Select(reader.GetValue).ToList();

                            Appointment appointment = new Appointment((int)data[0], (int)data[1], (int)data[2], (DateTime)data[3], (string)data[4]);
                            appointments.Add(appointment);
                        }
                    }
                    var appointmentsForDoctor = from Appointment appointment in appointments
                                                where appointment.DoctorId == doctorId
                                                select appointment;
                    appointments = appointmentsForDoctor.ToList();
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return appointments;
        }

        public bool ScheduleAppointment(Appointment appointment)
        {
            try
            {
                using (conn = DBConnection.GetConnection())
                {
                    bool patientIdIsValid = PatientIdValidator(appointment.PatientId, conn);
                    bool doctorIdIsValid = DoctorIdValidator(appointment.DoctorId, conn);
                    if (!patientIdIsValid) { throw new PatientNumberNotFoundException("Could not find patient id!"); }
                    if (!doctorIdIsValid) { throw new Exception("Could not find doctor ID!"); }
                    string query = $"INSERT INTO Appointments VALUES({appointment.PatientId}, {appointment.DoctorId}, \'{appointment.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss.ff")}\', \'{appointment.Description}\')";
                    cmd = new SqlCommand(query, conn);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) { Console.WriteLine("Your appointment was scheduled successfully!"); return true; }
                    else { Console.WriteLine("Error scheduling appointment!"); return false; }
                }
            }
            catch (PatientNumberNotFoundException pnnfx) { Console.WriteLine(pnnfx.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { conn.Close(); }

            return false;
        }
        
        public bool UpdateAppointment(Appointment appointment)
        {
            try
            {
                using (conn = DBConnection.GetConnection())
                {
                    bool patientIdIsValid = PatientIdValidator(appointment.PatientId, conn);
                    bool doctorIdIsValid = DoctorIdValidator(appointment.DoctorId, conn);
                    if (!patientIdIsValid) { throw new PatientNumberNotFoundException("Invalid patient id!"); }
                    if (!doctorIdIsValid) { throw new Exception("Invalid doctor ID!"); }
                    string query = $"UPDATE Appointments SET patientId = {appointment.PatientId}, doctorId = {appointment.DoctorId}, appointmentDate = \'{appointment.AppointmentDate.ToString("yyyy-MM-dd HH:mm:ss.ff")}\', description = \'{appointment.Description}\' WHERE appointmentId = {appointment.AppointmentId}";
                    cmd = new SqlCommand(query, conn);
                    int rows = cmd.ExecuteNonQuery();
                    if (rows > 0) { Console.WriteLine("Your appointment was updated successfully!"); return true; }
                    else { Console.WriteLine("Error updating appointment!"); return false; }
                }
            }
            catch (PatientNumberNotFoundException pnnfx) { Console.WriteLine(pnnfx.Message); }
            catch (Exception ex) { Console.WriteLine(ex.Message); }

            return false;
        }

        public bool CancelAppointment(int appointmentId)
        {
            try
            {
                Appointment appointment = GetAppointmentById(appointmentId);
                if (appointment == null) { Console.WriteLine("Could not find the appointment!"); return false; }
                else
                {
                    using (conn = DBConnection.GetConnection())
                    {
                        string query = $"DELETE FROM Appointments WHERE appointmentID = {appointmentId}";
                        cmd = new SqlCommand(query, conn);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0) { Console.WriteLine("Your appointment was cancelled successfully!"); return true; }
                        else { Console.WriteLine("Error cancelling appointment!"); return false; }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return false;
        }

        public List<Doctor> DisplayAllDoctors()
        {
            List<Doctor> doctors = new List<Doctor>();
            try
            {
                using (conn = DBConnection.GetConnection())
                {
                    string query = $"SELECT * FROM Doctors";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    if (!reader.HasRows) { Console.WriteLine("No doctors found!"); }
                    else
                    {
                        while (reader.Read())
                        {
                            Doctor doctor = new Doctor();
                            var data = Enumerable.Range(0, reader.FieldCount).Select(reader.GetValue).ToList();
                            doctor.DoctorId = (int)data[0];
                            doctor.FirstName = (string)data[1];
                            doctor.LastName = (string)data[2];
                            doctor.Specialization = (string)data[3];
                            doctor.ContactNumber = (string)data[4];
                            doctors.Add(doctor);
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return doctors;
        }

        public List<Patient> DisplayAllPatients()
        {
            List<Patient> patients = new List<Patient>();
            try
            {
                using (conn = DBConnection.GetConnection())
                {
                    string query = $"SELECT * FROM Patients";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    if (!reader.HasRows) { Console.WriteLine("No patients found!"); }
                    else
                    {
                        while (reader.Read())
                        {
                            Patient patient = new Patient();
                            var data = Enumerable.Range(0, reader.FieldCount).Select(reader.GetValue).ToList();
                            patient.PatientID = (int)data[0];
                            patient.FirstName = (string)data[1];
                            patient.LastName = (string)data[2];
                            patient.DateOfBirth = (DateTime)data[3];
                            patient.Gender = (string)data[4];
                            patient.ContactNumber = (string)data[5];
                            patients.Add(patient);
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return patients;
        }

        public List<Appointment> DisplayAllAppointments()
        {
            List<Appointment> appointments = new List<Appointment>();
            try
            {
                using (conn = DBConnection.GetConnection())
                {
                    string query = $"SELECT * FROM Appointments";
                    cmd = new SqlCommand(query, conn);
                    reader = cmd.ExecuteReader();
                    if (!reader.HasRows) { Console.WriteLine("No appointments found!"); }
                    else
                    {
                        while (reader.Read())
                        {
                            Appointment appointment = new Appointment();
                            var data = Enumerable.Range(0, reader.FieldCount).Select(reader.GetValue).ToList();
                            appointment.AppointmentId = (int)data[0];
                            appointment.PatientId = (int)data[1];
                            appointment.DoctorId = (int)data[2];
                            appointment.AppointmentDate= (DateTime)data[3];
                            appointment.Description = (string)data[4];
                            appointments.Add(appointment);
                        }
                    }
                }
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            return appointments;
        }

        /// <summary>
        /// Checks if the Patient ID is valid
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="conn"></param>
        /// <returns>A boolean value, true if the ID exists, false otherwise.</returns>
        public bool PatientIdValidator(int patientId, SqlConnection conn)
        {
            try
            {
                string query = $"SELECT * FROM Patients WHERE patientId = {patientId}";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                if (reader.HasRows) { return true; }
                return false;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { reader.Close(); }
            return false;
        }

        /// <summary>
        /// Checks if the Doctor ID is valid
        /// </summary>
        /// <param name="doctorId"></param>
        /// <param name="conn"></param>
        /// <returns>A boolean value, true if the ID exists, false otherwise.</returns>
        public bool DoctorIdValidator(int doctorId, SqlConnection conn)
        {
            try
            {
                string query = $"SELECT * FROM Doctors WHERE doctorId = {doctorId}";
                cmd = new SqlCommand(query, conn);
                reader = cmd.ExecuteReader();
                if (reader.HasRows) { return true; }
                return false;
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
            finally { reader.Close(); }
            return false;
        }
    }
}
