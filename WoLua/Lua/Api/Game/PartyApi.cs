using MoonSharp.Interpreter;

namespace WoLua.Lua.Api.Game;

[MoonSharpUserData]
public class PartyApi: ApiBase { // TODO luadoc all of this
	public PartyApi(ScriptContainer source) : base(source) { }

	public static implicit operator bool(PartyApi? party) => party?.InParty ?? false;

	public int? Size => this.Owner.GameApi.Player.Loaded ? Service.Party.Length : null;
	public int? Length => this.Size;
	public bool? InAlliance => this.Owner.GameApi.Player.Loaded ? Service.Party.IsAlliance : null;
	public bool? InParty => this.Owner.GameApi.Player.Loaded ? Service.Party.Length > 0 : null;

	public EntityWrapper this[int idx] => new(Service.Party[idx]?.GameObject);
}
