using System.Diagnostics.CodeAnalysis;
using System.Numerics;

using Dalamud.Bindings.ImGui;

using MoonSharp.Interpreter;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui;

[MoonSharpUserData]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Documentation generation only reflects instance members")]
public class Dimensions { // TODO: luadocs

	public float Width { get; set; }
	public void SetWidth(float width) => this.Width = width;

	public float Height { get; set; }
	public void SetHeight(float height) => this.Height = height;

	public bool Valid => this.Width > 0 && this.Height > 0;

	public void Set(float width, float height) {
		this.SetWidth(width);
		this.SetHeight(height);
	}

	[MoonSharpHidden]
	public Vector2 Native => new(this.Width, this.Height);

	public static implicit operator Vector2(Dimensions dim) => dim.Native;

}
