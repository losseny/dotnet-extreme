using System.Diagnostics.CodeAnalysis;
using Application.Common.Behaviours;
using Application.Common.Dto.Common;
using Application.Common.Dto.Reservation;
using Application.Common.Extensions;
using Application.Common.Mappings;
using Application.Services.Interfaces;
using AutoMapper;
using Data.Agents;
using Data.Agents.Dto;
using Data.Repository;
using Domain.Common.Designs.ReservationFactory;
using Domain.Entities;
using Domain.ValueObjects;

namespace Application.Services;

public class ReserveringService(IReserveringRepository repository, IReservationFactory factory, IMapper mapper) : IReserveringService
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    private readonly IAgent agent = new DummyStudyGroupAgent();
    public async Task<IEnumerable<ReservationDto>> FetchAllRosterOfBookerByWeek(Guid userId, int week = default)
    {
        var dates = ChronosBehaviour.GetInstance()
            .ConvertWeekToDate(week)
            .GenerateDaysInWeek();

        var studyGroup = await agent.GetResource<StudyGroupAgentDto>($"http://dummyhost/api/studygroup/{userId}");

        var reservations =
            await repository.FetchAllRosterOfUserByWeek(dates, userId, studyGroup.Id);
        return reservations.MapToListDataTransferObjects<Reservation, ReservationDto>(mapper.ConfigurationProvider);
    }

    public async Task<ReservationDto> FindReservationById(Guid id)
    {
        var reservation = await repository.FindReservationById(id);
        return  reservation.Map<Reservation, ReservationDto>(mapper.ConfigurationProvider);
    }


    public async Task<Reservation> CreateReservation(CreateReservationDto dto)
    {
        var reservation = factory.CreateReservation(
            dto.ReservationDate.MapTo<ReservationDateDto, ReservationDate>(mapper.ConfigurationProvider),
            dto.Period.MapTo<ReservationPeriodDto, ReservationPeriod>(mapper.ConfigurationProvider),
            dto.StudyGroupId,
            dto.BookerId
        );

        var existingReservation = await repository.FindReservation(reservation);
        if (existingReservation is not null) throw new Exception("Reservation already exists");

        return reservation;
    }
}
