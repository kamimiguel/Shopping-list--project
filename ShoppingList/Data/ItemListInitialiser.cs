using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using ShoppingList.Models;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ShoppingList.Data
{
    public class ItemListInitialiser
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {


            }
        }
    }
}
 