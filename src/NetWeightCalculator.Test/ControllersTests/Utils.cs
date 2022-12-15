using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetWeightCalculator.Test.Extensions;
using NetWeightCalculator.WebAPI.Controllers;
using NetWeightCalculator.WebAPI.Models;

namespace NetWeightCalculator.Test.ControllersTests
{
    public static class Utils
    {
        private static readonly string BaseRoute = typeof(CalculatorController).GetRouteTemplate();
        
        public static async Task<CalculateTaxesResponseModel?> Calculate(this HttpClient client,  CalculateTaxesRequestModel? postModel = default)
            => await client.PostContentAsync<CalculateTaxesResponseModel>($"{BaseRoute}/calculate", JsonContent.Create(postModel));
        
        private static string GetRouteTemplate(this Type controllerType)
        {
            return controllerType.GetCustomAttribute<RouteAttribute>()!.Template;
        }
    }
}