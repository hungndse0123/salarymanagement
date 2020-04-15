using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.Swagger;
using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using System.Linq;
using System.Web;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.OpenApi.Any;
using IOperationFilter = Swashbuckle.Swagger.IOperationFilter;

namespace Api_Salary_Management.Filter
{
    public class AuthorizationHeaderParameterOperationFilter : IOperationFilter
    {
        public void Apply(Operation operation, SchemaRegistry schemaRegistry, System.Web.Http.Description.ApiDescription apiDescription)
        {
            if (operation.parameters == null)
                operation.parameters = new List<Parameter>();

            operation.parameters.Add(new Parameter
            {
                name = "Authorization",
                @in = "header",
                required = false,
                description = "access bearer token",
                type = "String",
                schema = new Schema
                {
                    type = "String",
                    @default = new OpenApiString("Bearer ")
                }
            });
        }
    }
}