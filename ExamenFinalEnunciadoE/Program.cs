
using System;
using System.Collections.Generic;

namespace ExamenFinalEnunciadoE
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Socio> socios = new List<Socio>();

            Console.WriteLine("Registro de Socios del Club");
            Console.WriteLine("---------------------------");

            while (true)
            {
                // Solicitar los datos del socio
                Console.Write("Número de socio: ");
                int numeroSocio = int.Parse(Console.ReadLine());

                Console.Write("Apellidos: ");
                string apellidos = Console.ReadLine();

                Console.Write("Nombres: ");
                string nombres = Console.ReadLine();

                Console.Write("Edad: ");
                int edad = int.Parse(Console.ReadLine());

                Console.WriteLine("Tipo de deporte:");
                Console.WriteLine("1. Tenis");
                Console.WriteLine("2. Rugby");
                Console.WriteLine("3. Voley");
                Console.WriteLine("4. Hockey");
                Console.WriteLine("5. Fútbol");
                Console.Write("Seleccione el tipo de deporte: ");
                int tipoDeporte = int.Parse(Console.ReadLine());

                // Crear un objeto Socio con los datos ingresados
                Socio socio = new Socio(numeroSocio, apellidos, nombres, edad, (TipoDeporte)tipoDeporte);

                // Agregar el socio a la lista
                socios.Add(socio);

                Console.WriteLine("Socio registrado exitosamente.");

                // Preguntar si se desea registrar otro socio
                Console.Write("¿Desea registrar otro socio? (S/N): ");
                string respuesta = Console.ReadLine().ToLower();
                if (respuesta != "s")
                {
                    break;
                }

                Console.WriteLine();
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Socios registrados:");
            foreach (Socio socio in socios)
            {
                Console.WriteLine(socio);
            }

            Console.ReadLine();
        }
    }

    class Socio
    {
        public int NumeroSocio { get; set; }
        public string Apellidos { get; set; }
        public string Nombres { get; set; }
        public int Edad { get; set; }
        public TipoDeporte TipoDeporte { get; set; }

        public Socio(int numeroSocio, string apellidos, string nombres, int edad, TipoDeporte tipoDeporte)
        {
            NumeroSocio = numeroSocio;
            Apellidos = apellidos;
            Nombres = nombres;
            Edad = edad;
            TipoDeporte = tipoDeporte;
        }

        public override string ToString()
        {
            return $"Número de socio: {NumeroSocio}, Apellidos: {Apellidos}, Nombres: {Nombres}, Edad: {Edad}, Deporte: {TipoDeporte}";
        }
    }

    enum TipoDeporte
    {
        Tenis = 1,
        Rugby = 2,
        Voley = 3,
        Hockey = 4,
        Futbol = 5
    }
}
