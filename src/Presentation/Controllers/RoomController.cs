using Application.Common.Dto.Reservation;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers;

public class RoomController(IRoomService service): ApiControllerBase
{

    [HttpPost("{id:guid}/reservation")]
    public async Task<IActionResult> CreateReservation(Guid id, [FromBody] CreateReservationDto reservationDto)
    {
        await service.CreateReservation(id, reservationDto);
        return Ok();
    }

}
