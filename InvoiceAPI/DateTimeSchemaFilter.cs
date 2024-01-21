using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using DataType = System.ComponentModel.DataAnnotations.DataType;

namespace InvoiceAPI
{
    public class DateTimeSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.MemberInfo?.GetCustomAttributes(true) is Attribute[] attrs && attrs.Length > 0)
            {
                foreach (var attr in attrs)
                {
                    if (attr is DataTypeAttribute dataTypeAttribute && dataTypeAttribute.DataType == DataType.DateTime)
                    {
                        schema.Format = "date-time";
                    }
                }
            }
        }
    }
}
