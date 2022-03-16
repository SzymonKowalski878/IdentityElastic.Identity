using IdentityElastic.Identity.Application.Commands.CreateWorker;
using IdentityElastic.Identity.Client.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityElastic.Identity.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorker(CreateWorkerRequestDto request)
        {
            var result = await _mediator.Send(
                new CreateWorkerCommand(request.Email, request.Password, request.FirstName, request.LastName,
                request.PhoneNumber, request.Role));



            return Ok(result);
        }
    }
}
