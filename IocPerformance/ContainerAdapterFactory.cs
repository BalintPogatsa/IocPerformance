﻿using System;
using System.Collections.Generic;
using System.Linq;
using IocPerformance.Adapters;

namespace IocPerformance
{
    internal static class ContainerAdapterFactory
    {
        public static IEnumerable<IContainerAdapter> CreateAdapters()
        {
            yield return new NoContainerAdapter();

            // TODO: remove if all IoC adapters should be compared
            var selectedAdapters = new Type[] {
                typeof(VSMefContainerAdapter),
                typeof(Mef2ContainerAdapter)
              };

            var containers = typeof(ContainerAdapterFactory).Assembly.GetTypes()
                 .Where(t => t.IsClass && !t.IsAbstract && t.GetInterfaces().Contains(typeof(IContainerAdapter)))
                 .Where(t => !t.Equals(typeof(NoContainerAdapter)))
                 .Where(t => selectedAdapters.Contains(t))
                 .Select(t => Activator.CreateInstance(t))
                 .Cast<IContainerAdapter>()
                 .OrderBy(c => c.Name);

            if (containers.Count() != containers.Select(b => b.Name).Distinct().Count())
            {
                var duplicateNames = containers
                    .GroupBy(b => b.Name)
                    .Where(g => g.Count() > 1)
                    .Select(g => g.Key);

                throw new InvalidOperationException(string.Format(
                    "ContainerAdapters must have unique names, the following names are used several times: {0}",
                    string.Join(", ", duplicateNames)));
            }

            foreach (var container in containers)
            {
                yield return container;
            }
        }
    }
}
