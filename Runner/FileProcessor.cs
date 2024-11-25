using System.Globalization;
using Converter;
using CsvHelper;
using CsvHelper.Configuration;

namespace DirectLink_Converter;

public class FileProcessor {
	private readonly List<string[]> _lines;
	private readonly LinkConverter _linkConverter;
	private readonly string _path;
	private readonly List<string> _header;

	public FileProcessor(LinkConverter converter, string path) {
		_linkConverter = converter;
		_path = path;

		using StreamReader reader = new(path);
		using CsvReader csv = new(reader, CultureInfo.InvariantCulture);
		csv.Read();
		csv.ReadHeader();
		_header = csv.HeaderRecord != null ? csv.HeaderRecord.ToList() : [];

		_lines = [];
		while (csv.Read()) {
			_lines.Add(csv.Context.Parser!.Record!);
		}
	}

	public void ConvertLinks() {
		int column = SelectColumn();
		foreach (string[] row in _lines) {
			row[column] = _linkConverter.Convert(row[column]);
			Console.WriteLine(row[column]);
		}
	}

	private int SelectColumn() {
		Console.WriteLine("Which column do you want to convert?");
		for (int i = 0; i < _header.Count; i++) {
			string s = _header[i];
			Console.WriteLine($"{i + 1}. {s}");
		}

		string? selectedColumn = Console.ReadLine();
		if (selectedColumn == null)
			throw new Exception("Invalid converter selected.");

		int column = int.Parse(selectedColumn) - 1;
		if (column >= _header.Count || column < 0)
			throw new Exception("Invalid converter selected.");

		return column;
	}

	public void ExportUpdated() {
		string file = Path.GetFileNameWithoutExtension(_path);
		string exportPath = _path.Replace(file, $"{file}-converted");

		ExportCsv(exportPath);
	}

	private void ExportCsv(string exportPath) {
		using StreamWriter writer = new(exportPath);
		using CsvWriter csv = new(writer, CultureInfo.InvariantCulture);
		foreach (string[] row in _lines) {
			foreach (string field in row) {
				csv.WriteField(field);
			}
			csv.NextRecord();
		}
	}
}