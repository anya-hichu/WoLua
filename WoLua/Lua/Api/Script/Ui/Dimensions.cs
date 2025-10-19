using System.Diagnostics.CodeAnalysis;
using System.Numerics;

using MoonSharp.Interpreter;

using VariableVixen.WoLua.Constants;
using VariableVixen.WoLua.Lua.Docs;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui;

[MoonSharpUserData]
[SuppressMessage("Performance", "CA1822:Mark members as static", Justification = "Documentation generation only reflects instance members")]
public class Dimensions {

	public float Width { get; set; }
	[LuaChainSetterDoc(nameof(Width))]
	public Dimensions SetWidth(float width) {
		this.Width = width;
		return this;
	}

	public float Height { get; set; }
	[LuaChainSetterDoc(nameof(Height))]
	public Dimensions SetHeight(float height) {
		this.Height = height;
		return this;
	}

	[LuaDoc("This method sets both the width and height of this object, then returns the object for chaining.")]
	[MoonSharpUserDataMetamethod(Metamethod.FunctionCall)]
	public Dimensions Set(float width, float height) {
		this.SetWidth(width);
		this.SetHeight(height);
		return this;
	}

	[MoonSharpHidden]
	public Vector2 Native => new(this.Width, this.Height);

	public static implicit operator Vector2(Dimensions dim) => dim.Native;

}
