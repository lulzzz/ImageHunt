﻿using System;

namespace ImageHuntWebServiceClient.Responses
{
  public class GameResponse
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public bool IsActive { get; set; }
  }
}