namespace ExamenFinalEnunciadoC
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Formulario();
        }

        public static void Formulario()
        {
            Console.WriteLine("HOLA! ESTE ES EL EJERCICIO C");
            Console.WriteLine("Ingrese el valor del lado 1 del rectángulo:");
            string lado1Input = Console.ReadLine();
            double lado1;

            Console.WriteLine("Ingrese el valor del lado 2 del rectángulo:");
            string lado2Input = Console.ReadLine();
            double lado2;

            if (double.TryParse(lado1Input, out lado1) && double.TryParse(lado2Input, out lado2))
            {
                double perimetro = 2 * (lado1 + lado2);
                double area = lado1 * lado2;

                Console.WriteLine("El perímetro del rectángulo es: " + perimetro);
                Console.WriteLine("El área del rectángulo es: " + area);
            }
            else
            {
                Console.WriteLine("Los valores ingresados no son lados válidos. Inténtelo nuevamente.");
            }



        }
    }
}