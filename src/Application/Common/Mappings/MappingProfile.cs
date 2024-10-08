using System.Reflection;
using AutoMapper;

namespace Application.Common.Mappings;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
	}

	private void ApplyMappingsFromAssembly(Assembly assembly)
	{
		var mapFromType = typeof(IMapToFrom<>);

		const string mappingMethodName = nameof(IMapToFrom<object>.MappingTo);

		var types = assembly.GetExportedTypes().Where(t => t.GetInterfaces().Any(HasInterface)).ToList();

		var argumentTypes = new[] { typeof(Profile) };

		foreach (var type in types)
		{
			var instance = Activator.CreateInstance(type);

			var methodInfo = type.GetMethod(mappingMethodName);

			if (methodInfo != null)
			{
				methodInfo.Invoke(instance, [this]);
			}
			else
			{
				var interfaces = type.GetInterfaces().Where(HasInterface).ToList();

				if (interfaces.Count <= 0)
				{
					continue;
				}

				foreach (var interfaceMethodInfo in interfaces.Select(@interface => @interface.GetMethod(mappingMethodName, argumentTypes)))
				{
					interfaceMethodInfo?.Invoke(instance, [this]);
				}
			}
		}

		return;

		bool HasInterface(Type t) => t.IsGenericType && t.GetGenericTypeDefinition() == mapFromType;
	}
}
