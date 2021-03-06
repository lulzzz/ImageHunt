﻿using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using ImageHunt;
using Xunit;

namespace ImageHuntTest
{
  public class AutomapperFixture : IDisposable
  {
    public AutomapperFixture()
    {
      Startup.ConfigureMappings();
    }
    public void Dispose()
    {
        Mapper.Reset();
    }
  }
  [CollectionDefinition("AutomapperFixture")]
  public class TestCollectionFixture : ICollectionFixture<AutomapperFixture>
    {
    }
}
