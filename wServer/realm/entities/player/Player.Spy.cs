using common;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using wServer.networking.packets.outgoing;

namespace wServer.realm.entities
{
    partial class Player
    {
        public class SpyMessage
        {
            public Player from;
            public SpyType type;
            public string text;
            public string to = null;
        }

        public enum SpyType
        {
            Public,
            Local,
            Private,
            Guild,
            Server
        }

        public void SetSpy(int id = 0)
        {
            var acc = Client.Account;
            acc.AccountIdSpy = id;
            acc.FlushAsync();
        }

        public void CheckSpy(SpyType _type, string _text, string _to = null)
        {
            foreach (var i in Manager.Clients.Keys)
                if (i.Player.Client.Account.AccountIdSpy == AccountId)
                {
                    var spymsg = new SpyMessage() { type = _type, from = this, text = _text, to = _to };
                    i.Player.SendSpyMsg(spymsg);
                }
        }

        public void SendSpyMsg(SpyMessage spymsg)
        {
            switch (spymsg.type)
            {
                case SpyType.Public:
                    SendSpy(spymsg.from, spymsg.text, 0xff69b4, 0xffffff);
                    break;
                case SpyType.Local:
                    SendSpy(spymsg.from, spymsg.text, 0xffc3e1, 0xffc3e1);
                    break;
                case SpyType.Private:
                    SendSpy(spymsg.from, spymsg.text, 0x7f345a, 0xffffff, spymsg.to);
                    break;
                case SpyType.Guild:
                    SendSpy(spymsg.from, spymsg.text, 0xff96ca, 0xff96ca, spymsg.to);
                    break;
                case SpyType.Server:
                    SendSpy(spymsg.from, spymsg.text, 0x662a48, 0x662a48);
                    break;
            }
        }
    }
}
