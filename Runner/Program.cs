using Converter;

namespace DirectLink_Converter;

internal static class Program {
	private static void Usage() {
		Console.WriteLine("Usage:");
		Console.WriteLine("Please drag and drop a TXT file to convert its contents.");
		Console.WriteLine("Press enter to exit...");
		Console.ReadLine();
	}

	private static void Main(string[] args) {
		if (args.Length == 0) {
			Usage();
			return;
		}

		Console.WriteLine("Please select the converter to use:");
		Array values = Enum.GetValues(typeof(ConverterType));
		for (int i = 0; i < values.Length - 1; i++) {
			Console.WriteLine($"{i + 1}. {values.GetValue(i)}");
		}

		Converter.Converter converter = SelectConverter();

		string linkPath = args[0];
		using StreamReader reader = new(linkPath);
		List<string> lines = reader.ReadToEnd().Split('\n').ToList();
		for (int i = 0; i < lines.Count; i++) {
			lines[i] = converter.Convert(lines[i]);
			Console.WriteLine(lines[i]);
		}

		// File.WriteAllLines(linkPath, lines);
	}

	private static Converter.Converter SelectConverter() {
		string? selectedConverter = Console.ReadLine();
		if (selectedConverter == null)
			throw new Exception("Invalid converter selected.");

		int converterIndex = int.Parse(selectedConverter) - 1;
		if (converterIndex is >= (int)ConverterType.Length or < 0)
			throw new Exception("Invalid converter selected.");

		return new Converter.Converter((ConverterType)converterIndex);
	}
}