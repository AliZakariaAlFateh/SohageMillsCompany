using System.Reflection;

namespace Sohag__Mills_Company.Services.map
{
    public class MapObject
    {
        public TTo MapProperties<TFrom, TTo>(TFrom source, TTo destination)
        {
            PropertyInfo[] sourceProperties = typeof(TFrom).GetProperties();
            PropertyInfo[] destinationProperties = typeof(TTo).GetProperties();

            foreach (var sourceProperty in sourceProperties)
            {
                var destinationProperty = Array.Find(destinationProperties, p => p.Name == sourceProperty.Name);

                if (destinationProperty != null && destinationProperty.PropertyType == sourceProperty.PropertyType)
                {
                    object value = sourceProperty.GetValue(source);
                    destinationProperty.SetValue(destination, value);
                }
            }
            return destination;
        }
    }
}
