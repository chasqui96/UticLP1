namespace ExamenFinalEnunciadoB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Formulario();
        }

        public static void Formulario()
        {
            Console.WriteLine("HOLA! ESTE ES EL EJERCICIO B");
            int cantidadNotas = 4;
            double sumaNotas = 0;

            for (int i = 1; i <= cantidadNotas; i++)
            {
                Console.WriteLine("Ingrese la nota {0}:", i);
                string input = Console.ReadLine();
                double nota;

                if (double.TryParse(input, out nota))
                {
                    sumaNotas += nota;
                }
                else
                {
                    Console.WriteLine("El valor ingresado no es una nota válida. Inténtelo nuevamente.");
                    i--; // Restamos 1 al contador para repetir la iteración actual
                }
            }

            double promedio = sumaNotas / cantidadNotas;

            Console.WriteLine("El promedio del alumno es: " + promedio);


        }
    }
}