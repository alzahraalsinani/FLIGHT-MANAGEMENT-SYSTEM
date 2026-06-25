        using Flight_Management_System.Models;
        using Microsoft.Win32;
        using System.Numerics;
        using System.Threading.Channels;

        namespace Flight_Management_System
        {
        public class Program
        {


        //system storage :
        public static FlightContext context = new FlightContext
        {

        Passengers = new List<Passenger>(),
        Pilots = new List<Pilot>(),
        Aircrafts = new List<Aircraft>(),
        Flights = new List<Flight>(),
        Bookings = new List<Booking>(),
        };


        // Menu :
        static void Main(string[] args)
        {
        bool exit = false;
        while (exit == false)
        {
        Console.WriteLine("\n========================================");
        Console.WriteLine("   Flight Management System");
        Console.WriteLine("========================================");
        Console.WriteLine(" 1  -  Register a Passenger");
        Console.WriteLine(" 2  -  Add an Aircraft");
        Console.WriteLine(" 3  -  Register a Pilot");
        Console.WriteLine(" 4  -  View All Flights");
        Console.WriteLine(" 5  -  Schedule a Flight");
        Console.WriteLine(" 6  -  Book a Flight");
        Console.WriteLine(" 7  -  Cancel a Booking");
        Console.WriteLine(" 8  -  Depart a Flight");
        Console.WriteLine(" 9  -  Cancel a Flight");
        Console.WriteLine(" 10 -  Passenger Booking History");
        Console.WriteLine(" 11  - Flight Revenue & Load Factor Report");
        Console.WriteLine(" 0  - Exit");
        Console.WriteLine("========================================");
        Console.Write("Select option: ");

        int option = int.Parse(Console.ReadLine());

        switch (option)
        {
        case 1: RegisteraPassenger(context); break;
        case 2: AddanAircraft(context); break;
        case 3: RegisteraPilot(context); break;
        case 4: ViewAllFlights(context); break;
        case 5: ScheduleaFlight(context); break;
        case 6: BookaFlight(context); break;
        case 7: CancelaBooking(context); break;
        case 8: DepartaFlight(context); break;
        case 9: CancelaFlight(context); break;
        case 10: PassengerBookingHistory(context); break;
        case 11: FlightRevenueLoadFactorReport(context); break;
        case 0: exit = true; break;
        default: Console.WriteLine("Invalid option. Please try again."); break;
        }
        if (!exit)
        {
        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
        Console.Clear();
        }
        }
        Console.WriteLine("Goodbye!");
        }


        // 1-
        public static void RegisteraPassenger(FlightContext context)
        {
        Console.WriteLine("\n== Register New Passenger ==");

        Console.Write("Enter Full Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Email: ");
        string email = Console.ReadLine();

        Console.Write("Enter Phone: ");
        string phone = Console.ReadLine();

        Console.Write("Enter Passport Number: ");
        string passport = Console.ReadLine();

        Console.Write("Enter Nationality: ");
        string nationality = Console.ReadLine();

        int passengerId = context.Passengers.Count + 1;

        context.Passengers.Add(new Passenger
        {
        passengerId = passengerId,
        passengerName = name,
        passengerEmail = email,
        passengerPhone = phone,
        passportNumber = passport,
        nationality = nationality
        });

        Console.WriteLine($"Passenger registered successfully. Assigned ID: {passengerId}");
        }


        // 2-
        public static void AddanAircraft(FlightContext context)
        {

        Console.WriteLine("\n== Add New Aircraft ==");

        Console.Write("Enter Aircraft Model (e.g. Boeing 737, Airbus A320):");
        string model = Console.ReadLine();

        Console.Write("Enter Total Seats: ");
        int seats = int.Parse(Console.ReadLine());

        int aircraftId = context.Aircrafts.Count + 1;

        context.Aircrafts.Add(new Aircraft
        {
        aircraftId = aircraftId,
        model = model,
        totalSeats = seats,
        isOperational = true
        });

        Console.WriteLine($"Aircraft added successfully. Assigned ID: {aircraftId}");
        }


        // 3-
        public static void RegisteraPilot(FlightContext context)
        {
        Console.WriteLine("\n== Register New Pilot ==");

        Console.Write("Enter Pilot Full Name: ");
        string name = Console.ReadLine();

        Console.Write("Enter Phone: ");
        string phone = Console.ReadLine();

        Console.Write("Enter License Number: ");
        string license = Console.ReadLine();

        int pilotId = context.Pilots.Count + 1;
        context.Pilots.Add(new Pilot
        {
        pilotId = pilotId,
        pilotName = name,
        pilotPhone = phone,
        licenseNumber = license,
        flightHours = 0,
        isAvailable = true
        });

        Console.WriteLine($"Pilot registered successfully. Assigned ID: {pilotId}");
        }


        // 4-
        public static void ViewAllFlights(FlightContext context)
        {
        Console.WriteLine("\n=== All Scheduled Flights ===");

        foreach (Flight f in context.Flights)
        {
        Console.WriteLine($"Code: {f.flightCode}  |  Origin: {f.origin}  |  Destination: {f.destination}" +
        $"  |  Departure Date: {f.departureDate}  |  Time: {f.departureTime}" +
        $"  |  Available Seats: {f.availableSeats}  |  Ticket Price: {f.ticketPrice:C}  |  Current Status: {f.status}");
        }
        }


        // 5-
        public static void ScheduleaFlight(FlightContext context)
        {
        Console.WriteLine("\n=== Schedule a New Flight ===");

        //Aircraft :
        Console.WriteLine("Enter Aircraft ID:");
        int aircraftId = int.Parse(Console.ReadLine());

        Aircraft aircraft = context.Aircrafts.FirstOrDefault(a => a.aircraftId == aircraftId);

        if (aircraft == null)
        {
        Console.WriteLine("Aircraft not found");
        return;
        }

        if (aircraft.isOperational == false)
        {
        Console.WriteLine("Aircraft is not operational");
        return;
        }

        //Pilot :
        Console.WriteLine("Enter Pilot ID:");
        int pilotId = int.Parse(Console.ReadLine());

        Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == pilotId);

        if (pilot == null)
        {
        Console.WriteLine("Pilot not found");
        return;
        }

        if (pilot.isAvailable == false)
        {
        Console.WriteLine("Pilot is not available");
        return;
        }
        //----
        Console.WriteLine("Enter Origin City:");
        string origin = Console.ReadLine();

        Console.WriteLine("Enter Destination City:");
        string destination = Console.ReadLine();

        Console.WriteLine("Enter Departure Date (YYYY-MM-DD):");
        string date = Console.ReadLine();

        Console.WriteLine("Enter Departure Time:");
        string time = Console.ReadLine();

        Console.WriteLine("Enter Ticket Price:");
        decimal price = decimal.Parse(Console.ReadLine());

        int flightId = context.Flights.Count + 1;

        context.Flights.Add(new Flight
        {
        flightId = flightId,
        flightCode = "FL-" + (100 + flightId), 
        aircraftId = aircraftId,
        pilotId = pilotId,
        origin = origin,
        destination = destination,
        departureDate = date,
        departureTime = time,
        ticketPrice = price,
        availableSeats = aircraft.totalSeats, 
        status = "Scheduled" 
        });

            
        pilot.isAvailable = false;

        Console.WriteLine("Flight scheduled successfully!");
        }


        // 6-
        public static void BookaFlight(FlightContext context)
        {
        Console.WriteLine("\n=== Book a Flight ===");

        Console.WriteLine("Enter Passenger ID:");
        int passengerId = int.Parse(Console.ReadLine());

        Passenger passenger = context.Passengers.FirstOrDefault(p => p.passengerId == passengerId);
        if (passenger == null)
        {
        Console.WriteLine("Passenger not found");
        return;
        }

        Console.WriteLine("Enter Flight ID:");
        int flightId = int.Parse(Console.ReadLine());

        Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightId);
        if (flight == null)
        {
        Console.WriteLine("Flight not found");
        return;
        }

        Console.WriteLine("Enter Seat Number:");
        string seat = Console.ReadLine();

        int bookingId = context.Bookings.Count + 1;

        context.Bookings.Add(new Booking
        {
        bookingId = bookingId,
        passengerId = passengerId,
        flightId = flightId,
        seatNumber = seat,
        bookingDate = DateTime.Now.ToString("yyyy-MM-dd"),
        totalPrice = flight.ticketPrice, 
        status = "Confirmed"
        });

        flight.availableSeats--;

        Console.WriteLine("Booking successful!");
        }


        // 7-
        public static void CancelaBooking(FlightContext context)
        {
        Console.WriteLine("\n=== Cancel a Booking ===");

        Console.WriteLine("Enter Booking ID to cancel:");
        int bookingId = int.Parse(Console.ReadLine());

            
        Booking booking = context.Bookings.FirstOrDefault(b => b.bookingId == bookingId);
        if (booking == null)
        {
        Console.WriteLine("Booking not found");
        return;
        }

        booking.status = "Cancelled";

        Flight flight = context.Flights.FirstOrDefault(f => f.flightId == booking.flightId);
        if (flight != null)
        {
        flight.availableSeats++;
        }

        Console.WriteLine("Booking cancelled successfully!");
        }


        // 8-
        public static void DepartaFlight(FlightContext context)
        {
          
        Console.WriteLine("\n=== Depart a Flight ===");

        Console.WriteLine("Enter Flight ID:");
        int flightId = int.Parse(Console.ReadLine());

            
        Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightId);

        if (flight == null)
        {
        Console.WriteLine("Flight not found");
        return;
        }

        flight.status = "Departed";

        Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);

        if (pilot != null)
        {
        pilot.flightHours = pilot.flightHours + 3; 
        pilot.isAvailable = true;                 
        }

        Console.WriteLine("Flight departed successfully!");
        }


        // 9-
        public static void CancelaFlight(FlightContext context)
        {
        Console.WriteLine("\n=== Cancel a Flight ===");

        Console.WriteLine("Enter Flight ID to cancel:");
        int flightId = int.Parse(Console.ReadLine());

        Flight flight = context.Flights.FirstOrDefault(f => f.flightId == flightId);

        if (flight == null)
        {
        Console.WriteLine("Flight not found");
        return;
        }

        flight.status = "Cancelled";
        Pilot pilot = context.Pilots.FirstOrDefault(p => p.pilotId == flight.pilotId);
        if (pilot != null)
        {
        pilot.isAvailable = true;
        }

        foreach (Booking b in context.Bookings)
        {
        if (b.flightId == flightId)
        {
        b.status = "Cancelled";
        }
        }

        Console.WriteLine("Flight and all its bookings cancelled successfully!");
        }



        // 10-
        public static void PassengerBookingHistory(FlightContext context)
        {
            Console.WriteLine("\n=== Passenger Booking History ===");

            Console.WriteLine("Enter Passenger ID:");
            int passengerId = int.Parse(Console.ReadLine());

            Passenger passenger = context.Passengers.FirstOrDefault(p => p.passengerId == passengerId);

            if (passenger == null)
            {
                Console.WriteLine("Passenger not found");
                return;
            }

            decimal totalSpent = 0; 

            foreach (Booking b in context.Bookings)
            {
                if (b.passengerId == passengerId)
                {
                    Console.WriteLine($"Booking ID: {b.bookingId} | Flight ID: {b.flightId} | Seat: {b.seatNumber} | Price: {b.totalPrice} | Status: {b.status}");

                    
                    if (b.status == "Confirmed")
                    {
                        totalSpent = totalSpent + b.totalPrice;
                    }
                }
            }

            Console.WriteLine($"Total Amount Spent: {totalSpent}");
        }


        // 11-
        public static void FlightRevenueLoadFactorReport(FlightContext context)
        {

        }














        }
        }
































