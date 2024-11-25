namespace Converter;

public enum ConverterType {
	GoogleDriveImage,
	GoogleDriveFile,

	Length,
}

public class LinkConverter(ConverterType type) : ILinkConverter {
	private readonly ILinkConverter _linkConverter = type switch {
		ConverterType.GoogleDriveImage => new GoogleDriveImageConverter(),
		ConverterType.GoogleDriveFile => new GoogleDriveFileConverter(),

		ConverterType.Length => throw new ArgumentOutOfRangeException(nameof(type), type, null),
		_ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
	};

	public string Convert(string input) => _linkConverter.Convert(input);
}