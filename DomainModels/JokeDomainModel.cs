﻿using System;

namespace Laughy.Models.DomainModels
{
    public class JokeDomainModel
    {
        //Properties
        public Guid DbId { get; set; }
        public string FirstPart { get; set; }
        public string SecondPart { get; set; }
        public string Category { get; set; }
        public bool Favourite { get; set; }
        public bool Selfcreated { get; set; }
    }
}
