namespace ExamenFinalEnunciadoA
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Formulario();
        }
        public static void Formulario()
        {
            Console.WriteLine ("HOLA! ESTE ES EL EJERCICIO A");
            Console.WriteLine("Escribir el Numero 3 o el Numero 5");
            var valor = Console.ReadLine();
            int number;
            bool success = int.TryParse(valor, out number);
            if(success)
            {
                if (number == 3 || number == 5)
                {
                    for (int i = 0; i < number; i++)
                    {
                        Console.WriteLine($"PROGRAMACION NRO: {i}");
                    }
                }
                else
                {
                    Console.WriteLine("NO SE PUEDE REALIZAR LA OPERACION");
                }
            }
            else
            {
                Console.WriteLine("INGRESE UN NUMERO PORFAVOR");

            }
           
        }
    }
}