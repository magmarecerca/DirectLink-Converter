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

		LinkConverter linkConverter = SelectConverter();
		FileProcessor fileProcessor = new(linkConverter, path: args[0]);
		fileProcessor.ConvertLinks();
		fileProcessor.ExportUpdated();
	}

	private static LinkConverter SelectConverter() {
		Console.WriteLine("Please select the converter to use:");
		Array values = Enum.GetValues(typeof(ConverterType));
		for (int i = 0; i < values.Length - 1; i++) {
			Console.WriteLine($"{i + 1}. {values.GetValue(i)}");
		}

		string? selectedConverter = Console.ReadLine();
		if (selectedConverter == null)
			throw new Exception("Invalid converter selected.");

		int converterIndex = int.Parse(selectedConverter) - 1;
		if (converterIndex is >= (int)ConverterType.Length or < 0)
			throw new Exception("Invalid converter selected.");

		return new LinkConverter((ConverterType)converterIndex);
	}
}