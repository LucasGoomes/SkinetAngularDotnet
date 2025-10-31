using System;
using System.Security.Claims;
using API.DTOs;
using Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

public class BuggyController : BaseApiController
{
    [HttpGet("unauthorized")]
    public ActionResult GetUnauthorized()
    {
        return Unauthorized();
    }

    [HttpGet("badrequest")]
    public ActionResult GetBadRequest()
    {
        return BadRequest("This is a bad request");
    }

    [HttpGet("notfound")]
    public ActionResult GetNotFound()
    {
        return NotFound();
    }

    [HttpGet("internalerror")]
    public ActionResult GetInternalError()
    {
        throw new Exception("This is a server error");
    }

    [HttpPost("validationerror")]
    public ActionResult GetValidationError(CreateProductDto product)
    {
        return Ok();
    }   

    [Authorize]
    [HttpGet("secret")]
    public ActionResult GetSecret()
    {
        var name = User.FindFirst(ClaimTypes.Name)?.Value;
        var id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Ok("Hello " + name + " with ID " + id);
    }
}
