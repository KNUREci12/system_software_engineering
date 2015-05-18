using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace audio_recorder.Note_Analyzer
{
    public class Notes
    {
        public Octave OctaveValue { get; set; }

        public Note NoteValue { get; set; }

        public Notes()
        {
            this.OctaveValue = Octave.octave1;
            this.NoteValue = Note.c;
        }

        public Notes( Octave _octave, Note _note )
        {
            this.OctaveValue = _octave;
            this.NoteValue = _note;
        }

        public Double GetFreq()
        {
            return NotesTable[ ( int )OctaveValue, ( int )NoteValue ];
        }

        public static Double[,] NotesTable =
        {
            { 16.352,  18.353,  20.602,  21.827,  24.5,    27.5,  30.868 },
            { 32.703,  36.708,  41.203,  43.654,  48.999,  55,    61.735 },
            { 65.406,  73.416,  82.407,  87.307,  97.999,  110,   123.47 },
            { 130.81,  146.83,  164.81,  174.61,  196,     220,   246.94 },
            { 261.64,  293.66,  329.63,  349.23,  392,     440,   493.88 },
            { 523.25,  587.33,  659.26,  698.46,  789.99,  880,   987.77 },
            { 1046.5,  1174.7,  1318.5,  1396.9,  1568,    1760,  1975.5 },
            { 2093,    2349.3,  2637,    2793.8,  3126,    3520,  3951.1 },
            { 4186,    4698.6,  5274,    5587.7,  6271.9,  7040,  7902.1 }
        };

        public enum Octave
        {
                SubContr_octave
            ,   Contr_octave
            ,   Big_octave
            ,   Small_octave
            ,   octave1
            ,   octave2
            ,   octave3
            ,   octave4
            ,   octave5
        }

        public static Octave OctaveFromString( String _string )
        {
            switch( _string )
            {
                case @"SubContr octave":    return Octave.SubContr_octave;
                case @"Contr octave":       return Octave.Contr_octave;
                case @"Big octave":         return Octave.Big_octave;
                case @"Small octave":       return Octave.Small_octave;
                case @"1 octave":           return Octave.octave1;
                case @"2 octave":           return Octave.octave2;
                case @"3 octave":           return Octave.octave3;
                case @"4 octave":           return Octave.octave4;
                case @"5 octave":           return Octave.octave5;
            }
            throw new Exception( @"uncorrect convert string to octave" );
        }

        public enum Note
        {
                c
            ,   d
            ,   e
            ,   f
            ,   g
            ,   a
            ,   h
        }

        public static Note NoteFromString( String _string )
        {
            switch( _string )
            {
                case @"c":      return Note.c;
                case @"d":      return Note.d;
                case @"e":      return Note.e;
                case @"f":      return Note.f;
                case @"g":      return Note.g;
                case @"a":      return Note.a;
                case @"h":      return Note.h;
            }
            throw new Exception( @"uncorrect convert string to note" );
        }

    }
}
