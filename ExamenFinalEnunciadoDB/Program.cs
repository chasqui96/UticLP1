namespace ExamenFinalEnunciadoDB
{
    internal class Program
    {
        static void Main(string[] args)
        {
     
                Console.WriteLine("Calculadora de Sueldo Neto");
                Console.WriteLine("--------------------------");

                // Solicitar el número de empleado
                Console.Write("Ingrese el número de empleado: ");
                int numeroEmpleado = int.Parse(Console.ReadLine());

                // Solicitar las horas trabajadas
                Console.Write("Ingrese las horas trabajadas: ");
                int horasTrabajadas = int.Parse(Console.ReadLine());

                // Solicitar el sueldo por hora
                Console.Write("Ingrese el sueldo por hora: ");
                decimal sueldoHora = decimal.Parse(Console.ReadLine());
                decimal sueldoNormal = 0;
                decimal sueldoExtra = 0;
                decimal sueldoExtra2 = 0;
                // Calcular el sueldo neto
                decimal sueldoNeto = 0;
                if (horasTrabajadas <= 40)
                {
                 sueldoNormal = horasTrabajadas * sueldoHora;
                  sueldoNeto = sueldoNormal;
                }
                else
                {
                    int horasNormales = 40;
                     sueldoNormal = horasNormales * sueldoHora;
                    int horasExtra = horasTrabajadas - horasNormales;
                    sueldoExtra = sueldoHora * 2;
                    sueldoExtra2 =horasExtra * sueldoExtra;

                   sueldoNeto = (horasNormales * sueldoHora) + (horasExtra * sueldoExtra);
                }


                // Mostrar el sueldo neto
                Console.WriteLine("--------------------------");
                Console.WriteLine("Sueldo Normal del empleado {0}: {1}", numeroEmpleado, sueldoNormal);
                Console.WriteLine("Sueldo Extra del empleado {0}: {1}", numeroEmpleado, sueldoExtra2);
                Console.WriteLine("Sueldo neto del empleado {0}: {1}", numeroEmpleado, sueldoNeto);

                Console.ReadLine();
            }
    }
}