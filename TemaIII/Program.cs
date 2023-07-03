namespace TemaIII
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int numeroAlumnos = 10;
            const int numeroExamenes = 5;

            double[,] notas = new double[numeroAlumnos, numeroExamenes];

            Console.WriteLine("Registro de Notas de Alumnos");
            Console.WriteLine("---------------------------");

            // Solicitar las notas de cada alumno y examen
            for (int i = 0; i < numeroAlumnos; i++)
            {
                Console.WriteLine("Alumno {0}:", i + 1);

                for (int j = 0; j < numeroExamenes; j++)
                {
                    Console.Write("Ingrese la nota del examen {0}: ", j + 1);
                    notas[i, j] = double.Parse(Console.ReadLine());
                }

                Console.WriteLine();
            }

            Console.WriteLine("---------------------------");
            Console.WriteLine("Promedio de Notas Finales");
            Console.WriteLine("---------------------------");

            // Calcular y mostrar el promedio de cada alumno
            for (int i = 0; i < numeroAlumnos; i++)
            {
                double sumaNotas = 0;
                for (int j = 0; j < numeroExamenes; j++)
                {
                    sumaNotas += notas[i, j];
                }

                double promedio = sumaNotas / numeroExamenes;

                Console.WriteLine("Alumno {0}: Promedio = {1}", i + 1, promedio);
            }

            Console.ReadLine();
        }
    }
}