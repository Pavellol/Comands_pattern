// Интерфейс команды
using System.ComponentModel.Design;
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
    private double valueOne;
    private double valueTwo;
    
    private ICalculator _command;

    public void SetCommand(ICalculator command)
    {
        _command = command;
    }

    public void PressButton(double valueOne, double valueTwo)
    {
        _command.Execute(valueOne, valueTwo);
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Создаем получателя
        Light livingRoomLight = new Light();
        ICalculator command;
        //Инциатор
        var remote = new RemoteControl();

        Console.WriteLine("Введите первое число:\n");
        double valueOne = double.Parse(Console.ReadLine());

        Console.WriteLine("Введите действие(+|-|/|*):\n");
        string valueForDo = Console.ReadLine();

        Console.WriteLine("Введите второе число:\n");
        double valueTwo = double.Parse(Console.ReadLine());

        /*// Определяем команду через объект
        ICalculator command = valueForDo switch
        {
            "+" => plusCommand,
            "-" => minusCommand,
            "*" => multiplyCommand,
            "/" => splitCommand,
            _ => throw new ArgumentException("Неизвестная операция.")
        };*/

        switch (valueForDo)
        {
            case "+":
                command = new PlusOnCommand(livingRoomLight);
                remote.SetCommand(command);
                break;
            case "-":
                command = new MinusOnCommand(livingRoomLight);
                remote.SetCommand(command);
                break;              
            case "*":
                command = new MultiplyOnCommand(livingRoomLight);
                remote.SetCommand(command);
                break;
            case "/":
                command = new SplitOnCommand(livingRoomLight);
                remote.SetCommand(command);
                break;
            default:
                Console.WriteLine("Неизвестная команда");
                break;            
        }
  
        //поехали      
        remote.PressButton(valueOne, valueTwo);      
    }
}

/*using System.Windows.Input;

class Program
{
    static void Main(string[] args)
    {
        TV tv = new TV();
        Volume volume = new Volume();
        MultiPult mPult = new MultiPult();
        mPult.SetCommand(0, new TVOnCommand(tv));
        mPult.SetCommand(1, new VolumeCommand(volume));
        // включаем телевизор
        mPult.PressButton(0);
        // увеличиваем громкость
        mPult.PressButton(1);
        mPult.PressButton(1);
        mPult.PressButton(1);
        // действия отмены
        mPult.PressUndoButton();
        mPult.PressUndoButton();
        mPult.PressUndoButton();
        mPult.PressUndoButton();

        Console.Read();
    }
}
interface ICommand
{
    void Execute();
    void Undo();
}

class TV
{
    public void On()
    {
        Console.WriteLine("Телевизор включен!");
    }

    public void Off()
    {
        Console.WriteLine("Телевизор выключен...");
    }
}

class TVOnCommand : ICommand
{
    TV tv;
    public TVOnCommand(TV tvSet)
    {
        tv = tvSet;
    }
    public void Execute()
    {
        tv.On();
    }
    public void Undo()
    {
        tv.Off();
    }
}
class Volume
{
    public const int OFF = 0;
    public const int HIGH = 20;
    private int level;

    public Volume()
    {
        level = OFF;
    }

    public void RaiseLevel()
    {
        if (level < HIGH)
            level++;
        Console.WriteLine("Уровень звука {0}", level);
    }
    public void DropLevel()
    {
        if (level > OFF)
            level--;
        Console.WriteLine("Уровень звука {0}", level);
    }
}

class VolumeCommand : ICommand
{
    Volume volume;
    public VolumeCommand(Volume v)
    {
        volume = v;
    }
    public void Execute()
    {
        volume.RaiseLevel();
    }

    public void Undo()
    {
        volume.DropLevel();
    }
}

class NoCommand : ICommand
{
    public void Execute()
    {
    }
    public void Undo()
    {
    }
}

class MultiPult
{
    ICommand[] buttons;
    Stack<ICommand> commandsHistory;

    public MultiPult()
    {
        buttons = new ICommand[2];
        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i] = new NoCommand();
        }
        commandsHistory = new Stack<ICommand>();
    }

    public void SetCommand(int number, ICommand com)
    {
        buttons[number] = com;
    }

    public void PressButton(int number)
    {
        buttons[number].Execute();
        // добавляем выполненную команду в историю команд
        commandsHistory.Push(buttons[number]);
    }
    public void PressUndoButton()
    {
        if (commandsHistory.Count > 0)
        {
            ICommand undoCommand = commandsHistory.Pop();
            undoCommand.Undo();
        }
    }
}*/