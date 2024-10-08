namespace MonsterTradingCardsGame.Core.Networking.Http;

public static class HttpHeader
{
    // General headers
    public const string Accept = "Accept";
    public const string AcceptCharset = "Accept-Charset";
    public const string AcceptEncoding = "Accept-Encoding";
    public const string AcceptLanguage = "Accept-Language";
    public const string Authorization = "Authorization";
    public const string CacheControl = "Cache-Control";
    public const string Connection = "Connection";
    public const string ContentLength = "Content-Length";
    public const string ContentType = "Content-Type";
    public const string Cookie = "Cookie";
    public const string Host = "Host";
    public const string UserAgent = "User-Agent";
    
    // Request headers
    public const string IfModifiedSince = "If-Modified-Since";
    public const string IfNoneMatch = "If-None-Match";
    public const string Range = "Range";
    public const string Referer = "Referer";
    public const string Upgrade = "Upgrade";

    // Response headers
    public const string Location = "Location";
    public const string ContentDisposition = "Content-Disposition";
    public const string ETag = "ETag";
    public const string Expires = "Expires";
    public const string LastModified = "Last-Modified";
    public const string SetCookie = "Set-Cookie";
    public const string WWWAuthenticate = "WWW-Authenticate";
    
    // CORS headers
    public const string AccessControlAllowOrigin = "Access-Control-Allow-Origin";
    public const string AccessControlAllowMethods = "Access-Control-Allow-Methods";
    public const string AccessControlAllowHeaders = "Access-Control-Allow-Headers";
    
    // Security headers
    public const string StrictTransportSecurity = "Strict-Transport-Security";
    public const string ContentSecurityPolicy = "Content-Security-Policy";
    public const string XContentTypeOptions = "X-Content-Type-Options";
    public const string XFrameOptions = "X-Frame-Options";
    public const string XXSSProtection = "X-XSS-Protection";

    // Misc headers
    public const string ContentEncoding = "Content-Encoding";
    public const string TransferEncoding = "Transfer-Encoding";
    public const string Trailer = "Trailer";
    public const string UpgradeInsecureRequests = "Upgrade-Insecure-Requests";
}
