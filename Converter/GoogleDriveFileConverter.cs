using System.Text.RegularExpressions;

namespace Converter;

internal partial class GoogleDriveFileConverter : ILinkConverter {
	private readonly Regex _regex = FileId();

	public string Convert(string source) {
		string id = _regex.Match(source).Groups[0].Value;
		return $"https://drive.google.com/uc?export=download&id={id}";
	}

    [GeneratedRegex("/[-\\w]{25,}/", RegexOptions.IgnoreCase, "en-GB")]
    private static partial Regex FileId();
}