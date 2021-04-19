using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Windows.Infrastructure.EFCore.Extensions
{
    public static  class ModelBuilderExtension
    {
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            var mappings = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces()
            .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();
            foreach (var mapping in mappings)
            {
                dynamic mapper = Activator.CreateInstance(mapping);
                modelBuilder.ApplyConfiguration(mapper);
            }
        }
    }
}
