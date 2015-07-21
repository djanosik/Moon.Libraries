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
            [".3dm"] = "x-world/x-3dmf",
            [".3ds"] = "image/x-3ds",
            [".3gp"] = "video/3gpp",
            [".7z"] = "application/x-7z-compressed",
            [".aac"] = "audio/aac",
            [".ai"] = "application/postscrip",
            [".aif"] = "audio/aiff",
            [".asf"] = "audio/asf",
            [".asp"] = "text/html",
            [".aspx"] = "text/html",
            [".asx"] = "video/x-ms-asf",
            [".atom"] = "application/atom+xml",
            [".avi"] = "video/avi",
            [".bat"] = "application/bat",
            [".bmp"] = "image/bmp",
            [".calx"] = "application/vnd.ms-office.calx",
            [".cda"] = "audio/wav",
            [".cdr"] = "application/cdr",
            [".cer"] = "application/x-x509-ca-cert",
            [".class"] = "application/x-java-applet",
            [".config"] = "text/xml",
            [".cpp"] = "text/plain",
            [".crt"] = "application/x-x509-ca-cert",
            [".cs"] = "text/plain",
            [".css"] = "text/css",
            [".csv"] = "text/csv",
            [".cur"] = "image/x-win-bitmap",
            [".dbf"] = "application/dbf",
            [".der"] = "application/x-x509-ca-cert",
            [".dmg"] = "application/x-apple-diskimage",
            [".doc"] = "application/msword",
            [".docx"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            [".dot"] = "application/msword",
            [".dotx"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            [".dtd"] = "text/xml",
            [".dwg"] = "application/acad",
            [".dxf"] = "application/dxf",
            [".eml"] = "message/rfc822",
            [".eps"] = "application/postscrip",
            [".fla"] = "video/x-flv",
            [".flv"] = "video/x-flv",
            [".fnt"] = "video/x-flv",
            [".gbr"] = "application/gbr",
            [".gif"] = "image/gif",
            [".hta"] = "application/hta",
            [".htm"] = "text/html",
            [".html"] = "text/html",
            [".ico"] = "image/x-icon",
            [".ics"] = "text/calendar",
            [".iff"] = "application/iff",
            [".inf"] = "text/inf",
            [".jar"] = "application/java-archive",
            [".jbst"] = "text/javascript",
            [".jfif"] = "image/jpeg",
            [".jp2"] = "image/jpeg",
            [".jpe"] = "image/jpeg",
            [".jpeg"] = "image/jpeg",
            [".jpg"] = "image/jpeg",
            [".jrpc"] = "text/javascript",
            [".js"] = "application/javascript",
            [".json"] = "application/json",
            [".jsonrpc"] = "text/javascript",
            [".jsx"] = "application/javascript",
            [".kml"] = "application/earthviewer",
            [".lnk"] = "application/x-ms-shortcut",
            [".m3u"] = "audio/x-mpegurl",
            [".m4a"] = "audio/mp4",
            [".m4p"] = "audio/mp4",
            [".m4v"] = "video/mp4",
            [".manifest"] = "application/x-ms-manifest",
            [".markdown"] = "text/plain",
            [".md"] = "text/plain",
            [".mdb"] = "application/msaccess",
            [".mdown"] = "text/plain",
            [".mht"] = "message/rfc822",
            [".mhtml"] = "message/rfc822",
            [".mid"] = "audio/mid",
            [".mim"] = "message/rfc822",
            [".mov"] = "video/quicktime",
            [".mp3"] = "audio/mpeg",
            [".mp4"] = "video/mp4",
            [".mpeg"] = "video/mpeg",
            [".mpg"] = "video/mpeg",
            [".nws"] = "message/rfc822",
            [".odb"] = "application/vnd.oasis.opendocument.database",
            [".odc"] = "application/vnd.oasis.opendocument.chart",
            [".odf"] = "application/vnd.oasis.opendocument.formula",
            [".odg"] = "application/vnd.oasis.opendocument.graphics",
            [".odi"] = "application/vnd.oasis.opendocument.image",
            [".odp"] = "application/vnd.oasis.opendocument.presentation",
            [".ods"] = "application/vnd.oasis.opendocument.spreadsheet",
            [".odt"] = "application/vnd.oasis.opendocument.text",
            [".ogg"] = "audio/ogg",
            [".otf"] = "font/opentype",
            [".p10"] = "application/pkcs10",
            [".p12"] = "application/x-pkcs12",
            [".p7b"] = "application/x-pkcs7-certificates",
            [".p7c"] = "application/pkcs7-mime",
            [".p7m"] = "application/pkcs7-mime",
            [".p7r"] = "application/x-pkcs7-certreqresp",
            [".p7s"] = "application/pkcs7-signature",
            [".pdf"] = "application/pdf",
            [".pfx"] = "application/x-pkcs12",
            [".pl"] = "text/plain",
            [".png"] = "image/png",
            [".pot"] = "application/mspowerpoint",
            [".potx"] = "application/vnd.openxmlformats-officedocument.presentationml.template",
            [".pps"] = "application/vnd.ms-pps",
            [".ppsx"] = "application/vnd.openxmlformats-officedocument.presentationml.slideshow",
            [".ppt"] = "application/mspowerpoint",
            [".pptx"] = "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            [".ps"] = "application/postscript",
            [".psd"] = "image/psd",
            [".pub"] = "application/x-mspublisher",
            [".ra"] = "audio/x-pn-realaudio",
            [".rar"] = "application/x-rar-compressed",
            [".rdf"] = "application/rdf+xml",
            [".resx"] = "application/xml",
            [".rm"] = "audio/x-pn-realaudio",
            [".rss"] = "application/rss+xml",
            [".rtf"] = "application/rtf",
            [".sgml"] = "text/sgml",
            [".sql"] = "text/plain",
            [".srt"] = "text/plain",
            [".svg"] = "image/svg+xml",
            [".swf"] = "application/x-shockwave-flash",
            [".tar"] = "application/tar",
            [".tga"] = "application/tga",
            [".tif"] = "image/tiff",
            [".tiff"] = "image/tiff",
            [".torrent"] = "application/x-bittorrent",
            [".ts"] = "application/javascript",
            [".tsx"] = "application/javascript",
            [".txt"] = "text/plain",
            [".vb"] = "text/plain",
            [".vcf"] = "text/x-vcard",
            [".vcs"] = "text/calendar",
            [".vob"] = "video/dvd",
            [".wav"] = "audio/wav",
            [".wave"] = "audio/wav",
            [".webp"] = "image/webp",
            [".wma"] = "audio/x-ms-wma",
            [".wmv"] = "video/x-ms-wmv",
            [".wps"] = "application/vnd.ms-works",
            [".wsf"] = "text/plain",
            [".xaml"] = "application/xaml+xml",
            [".xap"] = "application/x-silverlight-app",
            [".xbap"] = "application/x-ms-xbap",
            [".xls"] = "application/vnd.ms-excel",
            [".xlsx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            [".xlt"] = "application/vnd.ms-excel",
            [".xltx"] = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            [".xml"] = "text/xml",
            [".xps"] = "application/vnd.ms-xpsdocument",
            [".xsd"] = "text/xml",
            [".xsl"] = "application/xml",
            [".xslt"] = "application/xml",
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