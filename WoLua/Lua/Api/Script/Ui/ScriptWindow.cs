using System;
using System.Numerics;

using Dalamud.Bindings.ImGui;

using VariableVixen.WoLua.Ui;

namespace VariableVixen.WoLua.Lua.Api.Script.Ui;

internal class ScriptWindow: BaseWindow, IDisposable {
	private ScriptContainer owner;
	internal IDisplayWidget[] content;
	private bool disposed;

	public ScriptWindow(ScriptContainer script) : base(Plugin.Name + "__ScriptWindow__" + script.InternalName) {
		this.content = [];
		this.owner = script;
		this.SizeCondition = ImGuiCond.FirstUseEver;
		this.ShowCloseButton = true;
		this.AllowClickthrough = true;
	}

	public Dimensions MinSize { get; } = new();
	public Dimensions DefaultSize { get; } = new();
	public Dimensions MaxSize { get; } = new();

	public bool AutoResize {
		get => this.Flags.HasFlag(ImGuiWindowFlags.AlwaysAutoResize);
		set {
			if (value)
				this.Flags |= ImGuiWindowFlags.AlwaysAutoResize;
			else
				this.Flags &= ~ImGuiWindowFlags.AlwaysAutoResize;
		}
	}

	public bool Empty => this.content.Length == 0;

	public override bool DrawConditions() => !this.disposed && !this.Empty;

	public override void OnOpen() {
		if (this.DefaultSize.Valid)
			this.Size = this.DefaultSize;
	}

	public override void PreDraw() {
		base.PreDraw();

		Vector2 titleSize = ImGui.CalcTextSize(this.WindowName);
		float minWidth = titleSize.X + (this.TitleBarButtons.Count + 3) * 50;
		ImGuiViewportPtr vp = ImGui.GetWindowViewport();
		if (vp.IsNull)
			vp = ImGui.GetMainViewport();

		Vector2 minSize = new(
			MathF.Min(vp.WorkSize.X, MathF.Max(this.MinSize.Width, minWidth)),
			MathF.Min(vp.WorkSize.Y,  MathF.Max(this.MinSize.Height, titleSize.Y))
		);
		Vector2 maxSize = new(
			MathF.Min(vp.WorkSize.X, MathF.Max(this.MaxSize.Width, minWidth)),
			this.MaxSize.Height
		);
		if (maxSize.Y <= 0)
			maxSize.Y = vp.WorkSize.Y;
		maxSize.Y = MathF.Min(vp.WorkSize.Y, MathF.Max(maxSize.Y, titleSize.Y));

		this.SizeConstraints = new() {
			MinimumSize = minSize,
			MaximumSize = maxSize,
		};
	}

	public override void Draw() {
		if (this.AutoResize)
			ImGui.PushTextWrapPos(this.SizeConstraints!.Value.MaximumSize.X - ImGui.GetStyle().WindowPadding.X);
		else
			ImGui.PushTextWrapPos(ImGui.GetContentRegionMax().X);

		foreach (IDisplayWidget widget in this.content)
			widget.Render(this.owner);

		ImGui.PopTextWrapPos();
	}

	protected virtual void Dispose(bool disposing) {
		if (this.disposed)
			return;
		this.disposed = true;

		this.content = [];
		this.owner = null!;
	}

	public void Dispose() {
		this.Dispose(true);
		GC.SuppressFinalize(this);
	}
}
