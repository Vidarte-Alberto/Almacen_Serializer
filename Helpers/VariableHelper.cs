class VariableHelper
{
    public static string StringNotEmpty(string value)
    {
        while (string.IsNullOrEmpty(value))
        {
            Console.WriteLine("Este Campo No Puede Estar Vacio");
            value = Console.ReadLine() ?? string.Empty;
        }
        return value;
    }

    public static int IntNotEmpty(int value)
    {
        while (value == 0)
        {
            Console.WriteLine("Este Campo No Puede Estar Vacio");
            value = Convert.ToInt32(Console.ReadLine());
        }
        return value;
    }
}