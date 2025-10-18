using System.Diagnostics;
using System.Numerics;

using Dalamud.Bindings.ImGui;
using Dalamud.Interface;
using Dalamud.Interface.Windowing;

namespace VariableVixen.WoLua.Ui;

internal abstract class BaseWindow: Window {
	protected BaseWindow(string name, ImGuiWindowFlags flags = ImGuiWindowFlags.None, bool forceMainWindow = false) : base(name, flags, forceMainWindow) {
		this.RespectCloseHotkey = true;
		this.IsOpen = false;
		this.TitleBarButtons = [
			new() {
				Priority = 0,
				Icon = FontAwesomeIcon.Heart,
				IconOffset = new(2, 1),
				Click = _ => Process.Start(new ProcessStartInfo("https://ko-fi.com/V7V7IK9UU") { UseShellExecute = true }),
				ShowTooltip = () => {
					ImGui.BeginTooltip();
					ImGui.TextUnformatted("Support me on ko-fi");
					ImGui.EndTooltip();
				},
			},
			new() {
				Priority = 1,
				Icon = FontAwesomeIcon.Code,
				IconOffset = new(1, 1),
				Click = _ => Process.Start(new ProcessStartInfo("https://github.com/VariableVixen/WoLua") { UseShellExecute = true }),
				ShowTooltip = () => {
					ImGui.BeginTooltip();
					ImGui.TextUnformatted("Browse the github repo");
					ImGui.EndTooltip();
				},
			},
		];
		this.AllowClickthrough = false;
		this.AllowPinning = true;
	}



	protected static void Centred(string text, uint spacing = 1, Vector4? colour = null) => ImUtils.Textline(text, spacing, true, colour);

	protected static void Textline(string? text = null, uint spacing = 1, Vector4? colour = null) => ImUtils.Textline(text, spacing, false, colour);

	protected static void Separator(uint spacing = 1) => ImUtils.Separator(spacing);

	protected static bool Section(string title) => ImUtils.Section(title);
}
