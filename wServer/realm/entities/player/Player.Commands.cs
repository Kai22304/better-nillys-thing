using System;
using System.Text.RegularExpressions;
using wServer.networking.packets.outgoing;

namespace wServer.realm.entities
{
    partial class Player
    {
        public void SetMusic(string name, bool world = false)
        {
            SendInfo((world ? "World music" : "Music") + $" changed to {name}.");
            Client.SendPacket(new SwitchMusic()
            {
                Music = name
            });
        }
    }
}
