using Keywords;
using System.Reflection.Metadata;

EventDemo _ = new();

Console.ReadKey(true);



class Observable
{
    public delegate void Notification(User user);

    public event Notification NotificationMessage;

    public event Action<User> NotificationUser;


    public void InvokeEvent(User user)
    {
        NotificationMessage.Invoke(user);
        NotificationUser.Invoke(user);
    }
}



class Observator
{
    private Observable Observable = new();
    public Observator()
    {
        Observable.NotificationMessage += Handle;
    }

    private void Handle(User user){}
}





public class User {}
