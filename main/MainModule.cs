using HospitalManagementSystem.dao;
using HospitalManagementSystem.entity;

namespace HospitalManagementSystem.main
{
    internal class MainModule
    {
        static void Main(string[] args)
        {
            HospitalServiceImpl hospitalServiceImpl = new HospitalServiceImpl();
            bool flag = true;
            Console.WriteLine("Welcome to the Hospital Management System!");
            while (flag)
            {
                Console.WriteLine(new String('-', 30));
                Console.WriteLine("\n1. Get an appointment by it's ID");
                Console.WriteLine("2. Get appointments for a specific patient");
                Console.WriteLine("3. Get appointments for a specific doctor");
                Console.WriteLine("4. Schedule an appointment");
                Console.WriteLine("5. Update an existing appointment");
                Console.WriteLine("6. Cancel an appointment");
                Console.WriteLine("7. Display all doctors");
                Console.WriteLine("8. Display all patients");
                Console.WriteLine("9. Display all appointments");
                Console.WriteLine("0. Exit the application");
                Console.Write("Enter your choice(0-6): ");

                string choice = Console.ReadLine();
                switch (choice)
                {
                    case "0":
                        flag = false;
                        break;
                    case "1":
                        try
                        {
                            Console.Write("\nEnter the ID of the appointment: ");
                            int appointmentId;
                            if (!int.TryParse(Console.ReadLine(), out appointmentId)) { Console.WriteLine("Invalid entry!"); break; }
                            Appointment appointment = hospitalServiceImpl.GetAppointmentById(appointmentId);
                            if (appointment.AppointmentId != 0) Console.WriteLine(appointment.ToString());
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    case "2":
                        try
                        {
                            Console.Write("\nEnter the ID of the patient: ");
                            int patientId;
                            if (!int.TryParse(Console.ReadLine(), out patientId)) { Console.WriteLine("Invalid entry!"); break; }
                            List<Appointment> appointments = hospitalServiceImpl.GetAppointmentsForPatient(patientId);

                            foreach (Appointment appointment in appointments) { Console.WriteLine(appointment.ToString()); }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    case "3":
                        try
                        {
                            Console.Write("\nEnter the ID of the doctor: ");
                            int doctorID;
                            if (!int.TryParse(Console.ReadLine(), out doctorID)) { Console.WriteLine("Invalid entry!"); break; }
                            List<Appointment> appointments = hospitalServiceImpl.GetAppointmentsForDoctor(doctorID);

                            foreach (Appointment appointment in appointments) { Console.WriteLine(appointment.ToString()); }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    case "4":
                        try
                        {
                            Console.Write("\nEnter Patient ID: ");
                            int patientId;
                            if (!int.TryParse(Console.ReadLine(), out patientId)) { Console.WriteLine("Invalid entry!"); break; }
                            Console.Write("\nEnter Doctor ID: ");
                            int doctorId;
                            if (!int.TryParse(Console.ReadLine(), out doctorId)) { Console.WriteLine("Invalid entry!"); break; }
                            Console.Write("\nEnter Appointment Date in YYYY-MM-DD hh:mm format: ");
                            DateTime appointmentDate = DateTime.ParseExact(Console.ReadLine() +":00.00", "yyyy-MM-dd HH:mm:ss.ff", null);
                            if (appointmentDate < DateTime.Now) throw new Exception("Invalid appointment date! Appointment date should not be less the current date!");
                            Console.Write("\nEnter a description: ");
                            string? description = Console.ReadLine();
                            Appointment appointment = new Appointment(patientId, doctorId, appointmentDate, description);
                            hospitalServiceImpl.ScheduleAppointment(appointment);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }

                        break;
                    case "5":
                        try
                        {
                            Console.Write("\nEnter the id of the appointment to update: ");
                            int appointmentId;
                            if (!int.TryParse(Console.ReadLine(), out appointmentId)) { Console.WriteLine("Invalid entry!"); break; }
                            Appointment appointment = hospitalServiceImpl.GetAppointmentById(appointmentId);
                            if (appointment.AppointmentId == 0) { break; }
                            Console.Write("\nEnter Patient ID(Leave the field blank if you do not wish to update): ");
                            var input = Console.ReadLine();
                            if (input != "")
                            {
                                int patientId;
                                if (!int.TryParse(input, out patientId)) { Console.WriteLine("Invalid entry!"); break; }
                                appointment.PatientId = patientId;
                            }
                            Console.Write("\nEnter Doctor ID(Leave the field blank if you do not wish to update): ");
                            input = Console.ReadLine();
                            if (input != "")
                            {
                                int doctorId;
                                if (!int.TryParse(input, out doctorId)) { Console.WriteLine("Invalid entry!"); break; }
                                appointment.DoctorId = doctorId;
                            }
                            Console.Write("\nEnter Appointment Date in YYYY-MM-DD HH:MM format(Leave the field blank if you do not wish to update): ");
                            input = Console.ReadLine();
                            if (input != "")
                            {
                                DateTime appointmentDate = DateTime.ParseExact(input + ":00.00", "yyyy-MM-dd HH:mm:ss.ff", null);
                                appointment.AppointmentDate = appointmentDate;
                            }
                            Console.Write("\nEnter a description(Leave the field blank if you do not wish to update): ");
                            input = Console.ReadLine();
                            if (input != "")
                            {
                                appointment.Description = input;
                            }
                            hospitalServiceImpl.UpdateAppointment(appointment);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    case "6":
                        try
                        {
                            Console.Write("\nEnter the id of the appointment you wish to cancel: ");
                            int appointmentId;
                            if (!int.TryParse(Console.ReadLine(), out appointmentId)) { Console.WriteLine("Invalid entry!"); break; }
                            hospitalServiceImpl.CancelAppointment(appointmentId);
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    case "7":
                        try
                        {
                            List<Doctor> doctors = hospitalServiceImpl.DisplayAllDoctors();
                            foreach(Doctor doctor in doctors)
                            {
                                Console.WriteLine(doctor.ToString());
                            }
                        }catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    case "8":
                        try
                        {
                            List<Patient> patients = hospitalServiceImpl.DisplayAllPatients();
                            foreach (Patient patient in patients)
                            {
                                Console.WriteLine(patient.ToString());
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    case "9":
                        try
                        {
                            List<Appointment> appointments = hospitalServiceImpl.DisplayAllAppointments();
                            foreach (Appointment appointment in appointments)
                            {
                                Console.WriteLine(appointment.ToString());
                            }
                        }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    default:
                        Console.WriteLine("Invalid choice! Please enter a choice between 0 and 6");
                        break;
                }
            }
        }
    }
}
