using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DotNetCore_OpeningHoursQuestion.Domain.Models
{
    public class OpeningHours
    {
        public IEnumerable<Monday> monday { get; set; }
        public IEnumerable<Tuesday> tuesday { get; set; }
        public IEnumerable<Wednesday> wednesday { get; set; }
        public IEnumerable<Thursday> thursday { get; set; }
        public IEnumerable<Friday> friday { get; set; }
        public IEnumerable<Saturday> saturday { get; set; }
        public IEnumerable<Sunday> sunday { get; set; }
    }
}