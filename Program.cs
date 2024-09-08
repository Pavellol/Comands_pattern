// Интерфейс команды
using System.Windows.Input;

public interface ICalculator
{
    void Execute(double a, double b);
}
// Класс "Получатель"
public class Light
{
    public void PlusVelue(double firstValue, double secondValue)
    {
        double result = firstValue + secondValue;
        Console.WriteLine("Ваш ответ:\n");        
        Console.WriteLine(result);
    }
    public void MinusVelue(double firstValue, double secondValue)
    {
        double result = firstValue - secondValue;
        Console.WriteLine("Ваш ответ:\n");        
        Console.WriteLine(result);
    }
    public void SplitVelue(double firstValue, double secondValue)
    {
        double result = firstValue / secondValue;
        Console.WriteLine("Ваш ответ:\n");
        Console.WriteLine(result);
    }
    public void MultiplyVelue(double firstValue, double secondValue)
    {
        double result = firstValue * secondValue;
        Console.WriteLine("Ваш ответ:\n");
        Console.WriteLine(result);
    }
}
// Конкретная команда для деления чисел
public class SplitOnCommand : ICalculator
{
    private readonly Light _light;

    public SplitOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute(double a, double b)
    {
        _light.SplitVelue(a, b);
    }
}
// Конкретная команда для деления чисел
public class MultiplyOnCommand : ICalculator
{
    private readonly Light _light;

    public MultiplyOnCommand(Light light)
    {
        _light = light;
    }

    public void Execute(double a, double b)
    {
        _light.MultiplyVelue(a, b);
    }
}
// Конкретная команда для разности чисел
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
// Класс инициатора
public class RemoteControl
{
    private ICalculator plus;
    private ICalculator minus;
    private ICalculator multiply;
    private ICalculator split;

    private double valueOne;
    private double valueTwo;
    private string valueForDo;
    
    public void SetCommandPlus(ICalculator command)
    {
        plus = command;
    }
    public void SetCommandMinus(ICalculator command)
    {
        minus = command;
    }
    public void SetCommandMultiply(ICalculator command)
    {
        multiply = command;
    }
    public void SetCommandSplit(ICalculator command)
    {
        split = command;
    }

    private void sortForDo(string valueForDo)
    {
        if (valueForDo == "+")
            plus.Execute(valueOne, valueTwo);
        else if (valueForDo == "-")
            minus.Execute(valueOne, valueTwo);
        else if (valueForDo == "/")
            split.Execute(valueOne, valueTwo);
        else if (valueForDo == "*")
            multiply.Execute(valueOne, valueTwo);
        else
            Console.WriteLine("Произошла ошибка, перезапустите приложение");       
    }
    public void PressButton()
    {
        Console.WriteLine("Введите первое число:\n");
        valueOne = double.Parse(Console.ReadLine());

        Console.WriteLine("Введите действие(+|-|/|*):\n");
        valueForDo = Console.ReadLine();

        Console.WriteLine("Введите второе число:\n");
        valueTwo = double.Parse(Console.ReadLine());

        sortForDo(valueForDo);        
    }
}
class Program
{
    static void Main(string[] args)
    {
        // Создаем получателя
        Light livingRoomLight = new Light();
        //Создадим команды
        var plusCommand = new PlusOnCommand(livingRoomLight);
        var minusCommand = new MinusOnCommand(livingRoomLight);
        var splitCommand = new SplitOnCommand(livingRoomLight);
        var multiplyCommand = new MultiplyOnCommand(livingRoomLight);
        //Инциаторы
        var remote = new RemoteControl();
        //Привязка команд
        remote.SetCommandPlus(plusCommand);
        remote.SetCommandMinus(minusCommand);
        remote.SetCommandSplit(splitCommand);
        remote.SetCommandMultiply(multiplyCommand);

        while (true)
        {
            remote.PressButton();
        }
    }
}
