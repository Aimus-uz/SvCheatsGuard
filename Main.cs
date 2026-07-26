using CounterStrikeSharp.API;
using CounterStrikeSharp.API.Core;
using CounterStrikeSharp.API.Modules.Cvars;

namespace SvCheatsGuard;

public class SvCheatsGuardPlugin : BasePlugin
{
    public override string ModuleName => "SvCheatsGuard";
    public override string ModuleVersion => "1.0.0";
    public override string ModuleAuthor => "zaadrot";
    public override string ModuleDescription => "Принудительно держит sv_cheats 0";

    private ConVar? _cheatsCvar;

    public override void Load(bool hotReload)
    {
        _cheatsCvar = ConVar.Find("sv_cheats");

        _cheatsCvar?.AddOnChangeListener((cvar, oldVal, newVal) =>
        {
            if (newVal == "1")
            {
                Server.PrintToConsole($"[SvCheatsGuard] Обнаружена попытка включить sv_cheats (было: {oldVal}). Возвращаю в 0.");
                cvar.SetValue(false); // sv_cheats 0
            }
        });

        RegisterListener<Listeners.OnMapStart>(mapName =>
        {
            Server.ExecuteCommand("sv_cheats 0");
        });
    }
}
