# DirectLink Converter

This is a small tool to convert sharing links into download links in bulk.

## Usage

Create a csv like so:

| Id  | Link  |
|-----|-------|
| id1 | link1 |
| id2 | link2 |
| id3 | link3 |

Drag and drop the csv file on top of the executable, and select the options you need.

## Expandability

This tool is built in such a way to make it easier to expand.

To expand it you must follow these steps.

### Create the extension class

You must add a new class that implements **ILinkConverter** and handles the conversion of the link.

This can also be used for any other kind of converter.

### Add extension to the ConverterType enum

Inside [LinkConverter.cs](Converter/LinkConverter.cs) you have to add the new type and also add the setup method inside
the constructor.
