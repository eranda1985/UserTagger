﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniSA.UserTagger.ApiClientWorker.Deserializers
{
    public class NamedUserDeserializer
    {
        public NamedUser NamedUser { get; set; }

        public NamedUserDeserializer()
        {
            NamedUser = new NamedUser();
        }
    }
}
