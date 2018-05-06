﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ImageHuntTelegramBot.Services;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot.Types;

namespace ImageHuntTelegramBot.Controllers
{
  [Route("api/[controller]")]
    public class UpdateController : Controller
    {
      private readonly IUpdateHub _updateService;

      public UpdateController(IUpdateHub updateService)
      {
        _updateService = updateService;
      }

    [HttpPost]
      public async Task<IActionResult> Post([FromBody] Update update)
      {
        await _updateService.Switch(update);
        return Ok();
      }
    }
}
