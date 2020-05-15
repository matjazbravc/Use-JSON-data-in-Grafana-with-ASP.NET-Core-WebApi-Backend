using GrafanaJsonWebApiDemo.Attributes;
using GrafanaJsonWebApiDemo.Contracts;
using GrafanaJsonWebApiDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System;

namespace GrafanaJsonWebApiDemo.Services.Converters
{
    /// <summary>
    /// Convert List of WeatherForecast Models to GrafanaTable Model
    /// </summary>
    public class WeatherForecastToGrafanaTableConverter : IConverter<IList<WeatherForecast>, GrafanaTable>
    {
        private readonly IGrafanaHelper _grafanaHelper;

        public WeatherForecastToGrafanaTableConverter(IGrafanaHelper grafanaHelper)
        {
            _grafanaHelper = grafanaHelper;
        }

        public GrafanaTable Convert(IList<WeatherForecast> forecasts)
        {
            // Create Properties
            var properties = GetProperties(typeof(WeatherForecast));

            // Create Columns
            var columns = GetColumns(properties);

            // Create Rows
            var rows = new List<IList<object>>();
            foreach (var forecast in forecasts)
            {
                var rowValues = GetRowValues(properties, forecast);
                rows.Add(rowValues);
            }

            // Create result
            var result = new GrafanaTable
            {
                Columns = columns,
                Rows = rows
            };

            return result;
        }

        /// <summary>
        /// Get list of columns converted to Grafana types
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        private List<Column> GetColumns(List<PropertyInfo> properties)
        {
            var result = new List<Column>();
            foreach (var prop in properties)
            {
                var colName = LocalizeColumnName(prop.Name);
                var colType = _grafanaHelper.ConvertType(prop.PropertyType);
                var col = new Column(colName, colType);
                result.Add(col);
            }
            return result;
        }

        /// <summary>
        /// Get Model Properties
        /// </summary>
        /// <param name="model"></param>
        /// <returns>List of PropertyInfo</returns>
        private List<PropertyInfo> GetProperties(Type model)
        {
            // Get model properties Ordered by Order[x] attribute
            var result = (from property in model.GetProperties()
                          let orderAttribute = (property.GetCustomAttributes(typeof(OrderAttribute), false).SingleOrDefault() ?? new OrderAttribute(0)) as OrderAttribute
                          orderby orderAttribute.Order
                          select property).ToList();
            // Remove ignored properties (columns)
            result.RemoveAll(p => IgnoreColumn(p.Name));
            return result;
        }

        /// <summary>
        /// Get single row values
        /// </summary>
        /// <param name="properties"></param>
        /// <param name="forecast"></param>
        /// <returns>List of objects</returns>
        private List<object> GetRowValues(List<PropertyInfo> properties, WeatherForecast forecast)
        {
            var result = new List<object>();
            foreach (var prop in properties)
            {
                // Format to short Date format
                if (prop.PropertyType == typeof(DateTime))
                {
                    var rowValue = (DateTime)prop.GetValue(forecast, null);
                    result.Add(rowValue.ToShortDateString());
                }
                else
                {
                    result.Add(prop.GetValue(forecast, null));
                }
            }
            return result;
        }

        /// <summary>
        /// Check if column should be ignored
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private bool IgnoreColumn(string columnName)
        {
            switch (columnName)
            {
                case "Id":
                    return true;
                default:
                    return false;
            }
        }

        /// <summary>
        /// Localize column name
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        private string LocalizeColumnName(string columnName)
        {
            switch (columnName)
            {
                case "TemperatureC":
                    return "Temperature Celsius";
                case "TemperatureF":
                    return "Temperature Fahrenheit";
                default:
                    return columnName;
            }
        }
    }
}
