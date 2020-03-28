using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Server
{
    public class FileLogger : TextWriter
    {
        public const string DateFormat = "[MMMM dd hh:mm:ss.f tt]: ";

        private bool _NewLine;

        public string FileName { get; private set; }

        public FileLogger(string file)
            : this(file, false)
        { }

        public FileLogger(string file, bool append)
        {
            FileName = file;

            using (var writer = new StreamWriter(new FileStream(FileName, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.Read)))
            {
                writer.WriteLine(">>>Logging started on {0:f}.", DateTime.Now);
                //f = Tuesday, April 10, 2001 3:51 PM 
            }

            _NewLine = true;
        }

        public override void Write(char ch)
        {
            using (var writer = new StreamWriter(new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.Read)))
            {
                if (_NewLine)
                {
                    writer.Write(DateTime.UtcNow.ToString(DateFormat));
                    _NewLine = false;
                }

                writer.Write(ch);
            }
        }

        public override void Write(string str)
        {
            using (var writer = new StreamWriter(new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.Read)))
            {
                if (_NewLine)
                {
                    writer.Write(DateTime.UtcNow.ToString(DateFormat));
                    _NewLine = false;
                }

                writer.Write(str);
            }
        }

        public override void WriteLine(string line)
        {
            using (var writer = new StreamWriter(new FileStream(FileName, FileMode.Append, FileAccess.Write, FileShare.Read)))
            {
                if (_NewLine)
                {
                    writer.Write(DateTime.UtcNow.ToString(DateFormat));
                }

                writer.WriteLine(line);
                _NewLine = true;
            }
        }

        public override Encoding Encoding { get { return Encoding.Default; } }
    }

    public class MultiTextWriter : TextWriter
    {
        private readonly List<TextWriter> _Streams;

        public MultiTextWriter(params TextWriter[] streams)
        {
            _Streams = new List<TextWriter>(streams);

            if (_Streams.Count < 0)
            {
                throw new ArgumentException("You must specify at least one stream.");
            }
        }

        public void Add(TextWriter tw)
        {
            _Streams.Add(tw);
        }

        public void Remove(TextWriter tw)
        {
            _Streams.Remove(tw);
        }

        public override void Write(char ch)
        {
            foreach (var t in _Streams)
            {
                t.Write(ch);
            }
        }

        public override void WriteLine(string line)
        {
            foreach (var t in _Streams)
            {
                t.WriteLine(line);
            }
        }

        public override void WriteLine(string line, params object[] args)
        {
            WriteLine(string.Format(line, args));
        }

        public override Encoding Encoding { get { return Encoding.Default; } }
    }
}
