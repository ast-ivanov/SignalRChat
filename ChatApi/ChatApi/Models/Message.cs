﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApi.Models
{
    public class Message
    {
        public string Text { get; set; }

        public DateTime Time { get; set; }

        public string Name { get; set; }
    }
}
