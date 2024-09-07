// Интерфейс команды
using System.Windows.Input;

public interface ICalculator
{
    void Execute(double a, double b);
}
// Класс "Получатель"
public class Light
{
    public void PlusVelue(double a, double b)
    {
        Console.WriteLine("Ваш ответ:\n");
        double result = a + b;
        Console.WriteLine(result);
    }
    public void MinusVelue(double a, double b)
    {
        Console.WriteLine("Ваш ответ:\n");
        double result = a - b;
        Console.WriteLine(result);
    }
}
public class MinusOnCommand : ICalculator
{
    private readonly Light _light;

    public MinusOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute(double a, double b)
    {
        _light.MinusVelue(a, b);
    }
}
// Конкретная команда для сложения чисел
public class PlusOnCommand : ICalculator
{
    private readonly Light _light;

    public PlusOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute(double a, double b)
    {
        _light.PlusVelue(a, b);
    }
}
// Конкретная команда для выключения света
/*public class TurnOffCommand : ICalculator
{
    private readonly Light _light;

    public TurnOffCommand(Light light)
    {
        _light = light;
    }

    public void Execute()
    {
        _light.TurnOff();
    }

    public void Execute(double a, double b)
    {
        throw new NotImplementedException();
    }
}*/
// Класс инициатора
public class RemoteControl
{
    private ICalculator _command;
    private string valueForDo;

    public void SetCommand(ICalculator command)
    {
        _command = command;
    }

    public void PressButton()
    {
        Console.WriteLine("Введите первое число:\n");
        double valueOne = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите действие:\n");
        string valueForDo = Console.ReadLine();

        Console.WriteLine("Введите второе число:\n");
        double valueTwo = double.Parse(Console.ReadLine());

        _command.Execute(valueOne, valueTwo);
    }
}
class Program
{
    static void Main(string[] args)
    {       
        // Создаем получателя
        Light livingRoomLight = new Light();

        // Создаем команды
        ICalculator turnOnCommand = new PlusOnCommand(livingRoomLight);
       
        // Создаем инициатора
        RemoteControl remote = new RemoteControl();
      
        // Включаем свет
        remote.SetCommand(turnOnCommand);
        remote.PressButton();
    }
}