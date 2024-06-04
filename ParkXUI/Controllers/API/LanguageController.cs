using System.Globalization;
using System.Net.Mail;
using Line.Login;
using Line.Login.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace ParkXUI.Controllers.API;


[Route("api/[controller]")]
[ApiController]
public class LanguageController : ControllerBase
{
    // GET
    private readonly IStringLocalizer<LanguageController> _localizer;
    private readonly IConfiguration _configuration;
    public LanguageController(IStringLocalizer<LanguageController> localizer, IConfiguration configuration)
    {
        _localizer = localizer;
        _configuration = configuration;
    }
   
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        //get localizer en or zh
      
        
        var guid = Guid.NewGuid();
        return Ok(_localizer["RandomGUID", guid.ToString()].Value);
    }
    
    
}