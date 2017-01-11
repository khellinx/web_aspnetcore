using System;
using System.Collections.Generic;

namespace Digipolis.Web.Versioning.Models
{
        public class VersionError
        {
            public VersionError()
            {
                _messages = new List<string>();
            }

            public VersionError(string message, params object[] args) : this()
            {
                AddMessage(message, args);
            }

            public VersionError(IEnumerable<string> messages) : this()
            {
                _messages.AddRange(messages);
            }

            private readonly List<string> _messages;
            public IEnumerable<string> Messages
            {
                get { return _messages; }
            }

            public void AddMessage(string message, params object[] args)
            {
                if (!String.IsNullOrWhiteSpace(message))
                    _messages.Add(String.Format(message, args));
            }
        }
    }