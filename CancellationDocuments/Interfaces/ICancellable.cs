﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CancellationDocuments
{
    interface ICancellable
    {
        bool Cancel(object document);
    }
}
