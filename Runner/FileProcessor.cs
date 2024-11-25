using Converter;

namespace DirectLink_Converter;

public class FileProcessor {
	private readonly List<string> _lines;
	private readonly LinkConverter _linkConverter;
	private readonly string _path;

	public FileProcessor(LinkConverter converter, string path) {
		_linkConverter = converter;
		_path = path;

		using StreamReader reader = new(path);
		_lines = reader.ReadToEnd().Split('\n').ToList();
	}

	public List<string> ConvertLinks() {
		for (int i = 0; i < _lines.Count; i++) {
			_lines[i] = _linkConverter.Convert(_lines[i]);
			Console.WriteLine(_lines[i]);
		}
		return _lines;
	}

	public void ExportUpdated() {
		string file = Path.GetFileNameWithoutExtension(_path);
		string exportPath = _path.Replace(file, $"{file}-converted");
		File.WriteAllLines(exportPath, _lines);
	}
}