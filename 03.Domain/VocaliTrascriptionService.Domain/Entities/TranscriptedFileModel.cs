﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocaliTranscriptionService.Domain.Entities
{
    public class TranscriptedFileModel
    {
        public TranscriptedFileModel(string file)
        {
            File = file;
        }

        public string File { get; set; }
    }
}
