using System;

namespace Keywords;
internal class DelegateDemo
{
    private delegate void ConsoleMessage(string message);

    private delegate bool CustomNumbers(int n);

    private ConsoleLog consoleLog = new();
    private int[] numbers { get; set; } = new[] { 2, 4, 10, 28, 100, 323, 442 };

    private Func<int, int, int> sum;

    private Action<int> printActionDel;

    private Predicate<string> isUpper;


    public DelegateDemo()
    {
        //ConsoleMessage consoleMessage1 = consoleLog.PrintCustomMessage;
        //ConsoleMessage consoleMessage2 = new(consoleLog.PrintCustomMessage);

        //InvokeConsoleMessageDelegate("hello1", consoleMessage1);
        //InvokeConsoleMessageDelegate("hello2", new ConsoleMessage(consoleLog.PrintCustomMessage));

        //TypesToInitializeADelegate();

        //Funcs();

        //Actions();

        Predicates();
    }

    private void TypesToInitializeADelegate()
    {
        CustomNumbers customnNumbers = new CustomNumbers(GreaterThanThirteen) + LessThanThen + LessThanThen;
        customnNumbers = Delegate.Combine(customnNumbers, new CustomNumbers(LessThanFive)) as CustomNumbers;
        customnNumbers += (int a) => a < 10; // lambda expression 
        customnNumbers += delegate(int val) { // annonymous method
            Console.WriteLine("Anonymous method: {0}", val);
            return val < 10;
        };
        customnNumbers += LessThanThen;

        var result = GetAllNumbers(numbers, customnNumbers);

        foreach (int nr in result)
            Console.WriteLine(nr);

        Console.WriteLine("\n\nCall delegates:\n");
        foreach (var deleg in customnNumbers.GetInvocationList())
            Console.WriteLine(deleg.Method);
    }

    private void Funcs()
    {
        // It has zero or more input parameters and one out parameter.
        sum = Sum;
        Console.WriteLine(sum.Invoke(2, 3));
    }

    private void Actions()
    {
        // Action delegate doesn't return a value.
        printActionDel = ConsoleLog.PrintNumber;
        printActionDel.Invoke(12);
    }

    private void Predicates()
    {
        // A predicate delegate methods must take one input parameter and return a boolean - true or false.
        isUpper = IsUpperCase;
        Console.WriteLine(isUpper.Invoke("SALUT"));
    }
    private static void InvokeConsoleMessageDelegate(string message, ConsoleMessage consoleMessage)
    {
        consoleMessage.Invoke(message);
        Console.WriteLine(consoleMessage.Method);
        Console.WriteLine(consoleMessage.Target);
    }

    private static bool LessThanFive(int n) { Console.WriteLine("LessThanFive"); return n < 5; }
    private static bool LessThanThen(int n) { Console.WriteLine("LessThanThen"); return n < 10; }
    private static bool GreaterThanThirteen(int n) { Console.WriteLine("GreaterThanThirteen"); return n > 13; }

    private static bool IsUpperCase(string str) { return str.Equals(str.ToUpper()); }

    private static int Sum(int x, int y) { return x + y; }

    private static IEnumerable<int> GetAllNumbers(
        IEnumerable<int> numbers, CustomNumbers customnNumbers)
    {
        return numbers.Where(x => customnNumbers.Invoke(x));
    }
}


class ConsoleLog
{
    public static void PrintCustomMessage(string message) => Console.WriteLine(message);

    public static void PrintNumber(int n) => Console.WriteLine(n);
}
