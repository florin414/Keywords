namespace Keywords;
internal class EventDemo
{
    public TrainSignal trainSignal = new();

    public EventDemo()
    {
         new Car(trainSignal);
         new Car(trainSignal);
         new Car(trainSignal);
         new Car(trainSignal);

         trainSignal.TrainsAComing = null;
         trainSignal.TrainsAComing.Invoke();
    }
}


class TrainSignal
{
    public event Action TrainsAComing;

    // event este o referinta de delegate cu 2 restrictii
    // 1. nu putem invoca referinta delegatului direct
    // 2. nu putem atrebui direct

    public void HereComesATrain()
    {
        // business logic
        TrainsAComing.Invoke();
    }
}

class Car
{
    public Car(TrainSignal trainSignal)
    {
        trainSignal.TrainsAComing += StopTheCar;
    }

    void StopTheCar()
    {
        Console.WriteLine("Screeeeetch");
    }
}
