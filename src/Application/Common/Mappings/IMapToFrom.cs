using AutoMapper;

namespace Application.Common.Mappings;

public interface IMapToFrom<T>
{
	void MappingTo(Profile profile)
	{
		profile.CreateMap(GetType(), typeof(T))
			.ReverseMap()
			.ForAllMembers(opt => opt.Condition((_, _, srcMember) => srcMember != null));
	}
}
