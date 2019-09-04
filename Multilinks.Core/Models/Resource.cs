﻿using Newtonsoft.Json;

namespace Multilinks.Core.Models
{
   public abstract class Resource : Link
   {
      [JsonIgnore]
      public Link Self { get; set; }
   }
}
