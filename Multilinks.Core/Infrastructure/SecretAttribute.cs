﻿using System;

namespace Multilinks.Core.Infrastructure
{
   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
   public class SecretAttribute : Attribute
   {
   }
}
