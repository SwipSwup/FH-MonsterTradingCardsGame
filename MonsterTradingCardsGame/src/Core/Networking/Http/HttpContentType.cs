namespace MonsterTradingCardsGame.Core.Networking.Http;

public enum HttpContentType
{
    // Application types
    Application_EDI_X12,
    Application_EDIFACT,
    Application_Javascript,
    Application_Octet_Stream,
    Application_Ogg,
    Application_PDF,
    Application_XHTML_XML,
    Application_X_Shoudwave_Flash,
    Application_JSON,
    Application_LD_JSON,
    Application_XML,
    Application_Zip,
    Application_X_WWW_Urlencoded,

    // Audio types
    Audio_MPEG,
    Audio_X_MS_WMA,
    Audio_VND_RN_Realaudio,
    Audio_X_WAV,

    // Image types
    Image_GIF,
    Image_JPEG,
    Image_PNG,
    Image_TIFF,
    Image_VND_Microsoft_Icon,
    Image_X_Icon,
    Image_VND_Djvu,
    Image_SVG_XML,

    // Multipart types
    Multipart_Mixed,
    Multipart_Alternative,
    Multipart_Related,
    Multipart_Form_Data,

    // Text types
    Text_CSS,
    Text_CSV,
    Text_HTML,
    Text_JavaScript_Obsolete,
    Text_Plain,
    Text_XML,

    // Video types
    Video_MPEG,
    Video_MP4,
    Video_Quicktime,
    Video_X_MS_WMV,
    Video_X_MSVideo,
    Video_X_FLV,
    Video_WebM,

    // VND types
    VND_Application_OpenDocument_Text,
    VND_Application_OpenDocument_Spreadsheet,
    VND_Application_OpenDocument_Presentation,
    VND_Application_OpenDocument_Graphics,
    VND_Application_VND_MS_Excel,
    VND_Application_VND_OpenXML_Spreadsheet,
    VND_Application_VND_MS_PowerPoint,
    VND_Application_VND_OpenXML_Presentation,
    VND_Application_MSWord,
    VND_Application_VND_OpenXML_WordProcessing,
    VND_Application_VND_Mozilla_XUL_XML,
}