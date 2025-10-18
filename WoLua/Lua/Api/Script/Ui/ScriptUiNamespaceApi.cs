using MoonSharp.Interpreter;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui;

[MoonSharpUserData]
public class ScriptUiNamespaceApi(ScriptContainer source): ApiBase(source) { // TODO: luadocs

	public ScriptWindowApi Window { get; private set; } = null!;

	public ScriptWidgetApi Widget { get; private set; } = null!;

}
