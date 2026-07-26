using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;
using CounterStrikeSharp.API.Modules.Timers;

namespace SvCheatsGuard;

public class SvCheatsGuardPlugin : BasePlugin
{
    public override string ModuleName => "SvCheatsGuard";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "zaadrot";

    public override void Load(bool hotReload)
    {
        RegisterListener<Listeners.OnMapStart>(mapName =>
        {
            Server.ExecuteCommand("sv_cheats 0");
        });

        AddTimer(1.0f, CheckCheats, TimerFlags.REPEAT);
    }

    private void CheckCheats()
    {
        var cheatsCvar = ConVar.Find("sv_cheats");
        if (cheatsCvar == null) return;

        ref bool isCheatsOn = ref cheatsCvar.GetPrimitiveValue<bool>();
        if (isCheatsOn)
        {
            Server.PrintToConsole("[SvCheatsGuard] sv_cheats был включён — возвращаю в 0.");
            Server.ExecuteCommand("sv_cheats 0");
        }
    }
}
