using System;
using System.Collections.Generic;
using System.IO;

namespace Moon
{
    /// <summary>
    /// Utility used to map file names / extensions to mime types.
    /// </summary>
    public static class MimeType
    {
        static readonly Dictionary<string, string> mimeMap = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase)
        {
            [".aac"] = "audio/aac",
            [".mp3"] = "audio/mpeg",
            [".asf"] = "audio/asf",
            [".m3u"] = "audio/x-mpegurl",
            [".m4a"] = "audio/mp4",
            [".m4p"] = "audio/mp4",
            [".cda"] = "audio/wav",
            [".pdf"] = "application/pdf",
            [".p10"] = "application/pkcs10",
            [".pfx"] = "application/x-pkcs12",
            [".p12"] = "application/x-pkcs12",
            [".p7b"] = "application/x-pkcs7-certificates",
            [".p7c"] = "application/pkcs7-mime",
            [".p7m"] = "application/pkcs7-mime",
            [".p7r"] = "application/x-pkcs7-certreqresp",
            [".p7s"] = "application/pkcs7-signature",
            [".doc"] = "application/msword",
            [".dot"] = "application/msword",
            [".docx"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            [".dotx"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            [".pot"] = "application/mspowerpoint",
            [".ppt"] = "application/mspowerpoint",
            [".pptx"] = "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            [".potx"] = "application/vnd.openxmlformats-officedocument.presentationml.template",
            [".pps"] = "application/vnd.ms-pps",
            [".ppsx"] = "application/vnd.openxmlformats-officedocument.presentationml.slideshow",
            [".ai"] = "application/postscrip",
            [".eps"] = "application/postscrip",
            [".atom"] = "application/atom+xml",
            [".calx"] = "application/vnd.ms-office.calx",
            [".class"] = "application/x-java-applet",
            [".crt"] = "application/x-x509-ca-cert",
            [".der"] = "application/x-x509-ca-cert",
            [".jar"] = "application/java-archive",
            [".js"] = "application/javascript",
            [".jsx"] = "application/javascript",
            [".json"] = "application/json",
            [".manifest"] = "application/x-ms-manifest",
            [".hta"] = "application/hta",
            [".gif"] = "image/gif",
            [".bmp"] = "image/bmp",
            [".ico"] = "image/x-icon",
            [".psd"] = "image/psd",
            [".jpg"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".jpe"] = "image/jpeg",
            [".jp2"] = "image/jpeg",
            [".jfif"] = "image/jpeg",
            [".png"] = "image/png",
            [".eml"] = "message/rfc822",
            [".mhtml"] = "message/rfc822",
            [".mht"] = "message/rfc822",
            [".nws"] = "message/rfc822",
            [".jbst"] = "text/javascript",
            [".jrpc"] = "text/javascript",
            [".jsonrpc"] = "text/javascript",
            [".md"] = "text/plain",
            [".inf"] = "text/inf",
            [".ics"] = "text/calendar",
            [".config"] = "text/xml",
            [".dtd"] = "text/xml",
            [".htm"] = "text/html",
            [".html"] = "text/html",
            [".css"] = "text/css",
            [".avi"] = "video/avi",
            [".mp4"] = "video/mp4",
            [".flv"] = "video/x-flv",
            [".mpg"] = "video/mpeg",
            [".mpeg"] = "video/mpeg",
            [".rdf"] = "application/rdf+xml",
            [".resx"] = "application/xml",
            [".rss"] = "application/rss+xml",
            [".rtf"] = "application/rtf",
            [".sgml"] = "text/sgml",
            [".swf"] = "application/x-shockwave-flash",
            [".tif"] = "image/tiff",
            [".tiff"] = "image/tiff",
            [".torrent"] = "application/x-bittorrent",
            [".txt"] = "text/plain",
            [".vcf"] = "text/x-vcard",
            [".vcs"] = "text/calendar",
            [".wav"] = "audio/wav",
            [".wave"] = "audio/wav",
            [".wma"] = "audio/x-ms-wma",
            [".wmv"] = "video/x-ms-wmv",
            [".xaml"] = "application/xaml+xml",
            [".xap"] = "application/x-silverlight-app",
            [".xbap"] = "application/x-ms-xbap",
            [".xlt"] = "application/vnd.ms-excel",
            [".xls"] = "application/vnd.ms-excel",
            [".xltx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            [".xlsx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            [".xml"] = "text/xml",
            [".xps"] = "application/vnd.ms-xpsdocument",
            [".xsd"] = "text/xml",
            [".xslt"] = "application/xml",
            [".xsl"] = "application/xml",
            [".zip"] = "application/zip"
        };

        /// <summary>
        /// Returns a mime type for the specified file extension.
        /// </summary>
        /// <param name="fileExtensionOrName">
        /// The file extension (including the initial period) or file name (including extension).
        /// </param>
        public static string Get(string fileExtensionOrName)
        {
            Requires.NotNullOrWhiteSpace(fileExtensionOrName, nameof(fileExtensionOrName));

            if (!fileExtensionOrName.StartsWith(".", StringComparison.Ordinal))
            {
                fileExtensionOrName = Path.GetExtension(fileExtensionOrName);
            }

            if (mimeMap.ContainsKey(fileExtensionOrName))
            {
                return mimeMap[fileExtensionOrName];
            }

            return "application/octet-stream";
        }
    }
}