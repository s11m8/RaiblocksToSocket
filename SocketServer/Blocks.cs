using System;
using System.Threading;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace Raiblocks
{
    public class Blocks : WebSocketBehavior
    {
        private string _name;
        private static int _number = 0;
        private string _prefix;

        public Blocks()
          : this(null)
        {
        }

        public Blocks(string prefix)
        {
            _prefix = !prefix.IsNullOrEmpty() ? prefix : "anon#";
        }

        private string getName()
        {
            var name = Context.QueryString["name"];
            return !name.IsNullOrEmpty() ? name : _prefix + getNumber();
        }

        private static int getNumber()
        {
            return Interlocked.Increment(ref _number);
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Sessions.Broadcast(String.Format("{0} got logged off...", _name));
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            if (_name == "PosetServer_8e9qqqx6637728111")
            {
                Sessions.Broadcast(String.Format("{0}", e.Data));
            }

        }

        protected override void OnOpen()
        {
            //var pipeClient = new NamedPipeClientForWebsockets(Context);

            _name = getName();
        }
    }
}
