using System;

namespace GrafanaJsonWebApiDemo.Services
{
    public class GrafanaHelper : IGrafanaHelper
    {
        /// <summary>
        /// Converts .Net System type to Grafana type
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string ConvertType(Type type)
        {
            switch (type)
            {
                case Type _ when type == typeof(long):
                case Type _ when type == typeof(int):
                case Type _ when type == typeof(decimal):
                case Type _ when type == typeof(float):
                    return "number";
                case Type _ when type == typeof(DateTime):
                    return "date";
                default:
                    return "string";
            }
        }
    }
}
