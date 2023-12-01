using System;

public class PropertyEventArgs : EventArgs
{
    public string PropertyName { get; set; }

    public PropertyEventArgs(string propertyName)
    {
        PropertyName = propertyName;
    }
}

public delegate void PropertyEventHandler(object sender, PropertyEventArgs e);

public interface IPropertyChanged
{
    event PropertyEventHandler PropertyChanged;
}

public class MyClass : IPropertyChanged
{
    private int myProperty;

    public int MyProperty
    {
        get { return myProperty; }
        set
        {
            if (myProperty != value)
            {
                myProperty = value;
                OnMyPropertyChanged(nameof(MyProperty));
            }
        }
    }

    public event PropertyEventHandler PropertyChanged;

    protected virtual void OnMyPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyEventArgs(propertyName));
    }
}

class Program
{
    static void Main()
    {
        MyClass obj = new MyClass();
        obj.PropertyChanged += (sender, e) =>
        {
            Console.WriteLine($"Property {e.PropertyName} changed");
        };
        obj.MyProperty = 42;

        Console.ReadLine();
    }
}

