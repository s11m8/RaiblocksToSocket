using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Collections;
using WebSocketSharp;

namespace StreamService
{
    //[ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple,
    //    InstanceContextMode = InstanceContextMode.PerCall)]
    public class RpcStream : IRpcStreamImpl
    {

        private static WebSocket _ws = new WebSocket("ws://localhost:49009/Blocks?name=PosetServer_8e9qqqx6637728111");
        private static HashSet<String> _hs = new HashSet<String>();
        private static object _lock = new object();

        public void GetBlocks(Stream data)
        {
            try {
                if (!_ws.IsAlive) { _ws.Connect(); } //only connect at first message

                StreamReader reader = new StreamReader(data);
                // Work with hashsets to prevent blocks that are sent 2 times (bug in RPC callback) to be broadcasted twice.
                lock (_lock) { // add lock to be sure that the hashset is accessed in a threadsafe manner
                    if (_hs.Add(reader.ReadToEnd())) // HashSets automatically handles double inserts (elements in a hashset are unique!)
                    {
                        _ws.Send(_hs.Last()); //Send the element that has just been added.
                        _hs = new HashSet<string>(_hs.Skip(Math.Max(0, _hs.Count() - 2))); //only keep last 2 records in hashset.
                    }
                }
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }


        }

    }



}