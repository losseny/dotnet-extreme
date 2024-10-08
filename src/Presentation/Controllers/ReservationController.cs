using Application.Common.Dto.Reservation;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Presentation.Services.Interfaces;

namespace Presentation.Controllers;

public class ReservationController(IReserveringService reserveringService, IApplicationContext applicationContext) : ControllerBase
{
    [HttpGet("roster")]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetRosterOfBookerByWeek([FromQuery] int week)
    {
        var reservations = await reserveringService.FetchAllRosterOfBookerByWeek(applicationContext.UserId, week);
        return Ok(reservations);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(Guid id)
    {
        var reservation = await reserveringService.FindReservationById(id);
        return Ok(reservation);
    }
}
