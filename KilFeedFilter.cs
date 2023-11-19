using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Core.Attributes.Registration;
using CounterStrikeSharp.API.Modules.Events;

namespace KillFeedFilter
{
    public class KilFeedFilter : BasePlugin
    {
        public override string ModuleName => "AbNeR Killfeed Filter";
        public override string ModuleVersion => "1.0.0";
        public override string ModuleAuthor => "AbNeR_CSS";
        public override string ModuleDescription => "Shows only your kills/deaths/assists in the killfeed";

        public override void Load(bool hotReload)
        {
            Console.WriteLine("AbNeR Killfeed filter loaded");
        }

        void FireIfValidPlayer(GameEvent @event, CCSPlayerController? player)
        {
            if (player is not null && player.IsValid && !player.IsBot)
                @event.FireEventToClient(player);
        }

        [GameEventHandler(HookMode.Pre)]
        public HookResult OnPlayerDeath(EventPlayerDeath @event, GameEventInfo info)
        {
            if (!@event.Attacker.IsValid || @event.Attacker.UserId == @event.Userid.UserId)
                return HookResult.Continue;

            FireIfValidPlayer(@event, @event.Attacker);
            FireIfValidPlayer(@event, @event.Assister);
            FireIfValidPlayer(@event, @event.Userid);
            info.DontBroadcast = true;
            return HookResult.Continue;
        }

    }
}