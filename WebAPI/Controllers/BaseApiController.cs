﻿using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public abstract class BaseApiController : Controller
    {
        private IMediator? _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;
    }
}
