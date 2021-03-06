﻿using System;
using System.Xml.Linq;
using AD.IO;
using AD.OpenXml.Documents;
using AD.Xml;
using JetBrains.Annotations;

namespace AD.OpenXml.Elements
{
    [PublicAPI]
    public static class AddAltChunkExtensions
    {
        private static readonly XNamespace R = XNamespaces.OpenXmlOfficeDocumentRelationships;

        private static readonly XNamespace W = XNamespaces.OpenXmlWordprocessingmlMain;

        private static int _index;
        
        public static XElement AddAltChunk(this XElement element, DocxFilePath toFilePath, DocxFilePath altChunkFile)
        {
            return CreateAltChunk(element, element.Add, toFilePath, altChunkFile);
        }

        public static XElement AddAltChunkAfterSelf(this XElement element, DocxFilePath toFilePath, DocxFilePath altChunkFile)
        {
            return CreateAltChunk(element, element.AddAfterSelf, toFilePath, altChunkFile);
        }

        public static XElement AddAltChunkBeforeSelf(this XElement element, DocxFilePath toFilePath, DocxFilePath altChunkFile)
        {
            return CreateAltChunk(element, element.AddBeforeSelf, toFilePath, altChunkFile);
        }

        public static XElement AddAltChunkFirst(this XElement element, DocxFilePath toFilePath, DocxFilePath altChunkFile)
        {
            return CreateAltChunk(element, element.AddFirst, toFilePath, altChunkFile);
        }

        private static XElement CreateAltChunk(XElement element, Action<object> addMethod, DocxFilePath toFilePath, DocxFilePath altChunkFile)
        {
            string name = $"altChunk{_index++}";
            addMethod(new XElement(W + "altChunk", new XAttribute(R + "id", name)));
            toFilePath.AddDefaultExtensionsToContentTypes();
            toFilePath.AddAltChunkToDocumentRelationships(altChunkFile, name);
            altChunkFile.WriteInto(toFilePath);
            return element;
        }
    }
}
