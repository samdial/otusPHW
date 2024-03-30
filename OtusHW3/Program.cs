using OtusHW3;
using System.Diagnostics;
using System.Reflection;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        const int iterations = 1000;

        Stopwatch reflectionSerializationTimer = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            string serialized = SerializeWithReflection(F.Get());
        }
        reflectionSerializationTimer.Stop();
        TimeSpan reflectionSerializationTime = reflectionSerializationTimer.Elapsed;

        Stopwatch jsonSerializationTimer = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            string serializedJson = Newtonsoft.Json.JsonConvert.SerializeObject(F.Get());
        }
        jsonSerializationTimer.Stop();
        TimeSpan jsonSerializationTime = jsonSerializationTimer.Elapsed;

        Console.WriteLine($"Рефлексионная сериализация: {reflectionSerializationTime.TotalMilliseconds} мс");
        Console.WriteLine($"JSON сериализация: {jsonSerializationTime.TotalMilliseconds} мс");

        string serializedObject = SerializeWithReflection(F.Get());
        Stopwatch reflectionDeserializationTimer = Stopwatch.StartNew();
        for (int i = 0; i < iterations; i++)
        {
            F deserializedObject = DeserializeWithReflection<F>(serializedObject);
        }
        reflectionDeserializationTimer.Stop();
        TimeSpan reflectionDeserializationTime = reflectionDeserializationTimer.Elapsed;

        Console.WriteLine($"Рефлексионная десериализация: {reflectionDeserializationTime.TotalMilliseconds} мс");
    }

    static string SerializeWithReflection(object obj)
    {
        StringBuilder sb = new StringBuilder();
        Type type = obj.GetType();
        PropertyInfo[] properties = type.GetProperties();
        foreach (PropertyInfo property in properties)
        {
            sb.Append($"{property.Name}={property.GetValue(obj)};");
        }
        return sb.ToString();
    }

    static T DeserializeWithReflection<T>(string serializedString) where T : new()
    {
        T obj = new T();
        Type type = typeof(T);
        string[] pairs = serializedString.Split(';');
        foreach (string pair in pairs)
        {
            string[] keyValue = pair.Split('=');
            PropertyInfo property = type.GetProperty(keyValue[0]);
            if (property != null)
            {
                object value = Convert.ChangeType(keyValue[1], property.PropertyType);
                property.SetValue(obj, value);
            }
        }
        return obj;
    }
}