// Arrange

using Microsoft.AspNetCore.WebUtilities;
using System.Text;

byte[] asci = new byte[] { 97, 98, 99, 100, 101, 102 };     // a, b, c, d, e, f
byte[] first = new byte[] { 0, 1, 2, 3, 5, 6, 7, 8, 9 }; // first
byte[] high = new byte[] { 200, 201, 202, 203, 205, 206, 207, 208, 209 }; // first
string text = "abcdef";
byte[] bytes_text = Encoding.UTF8.GetBytes(text);

var string_ascii = Encoding.UTF8.GetString(asci);
var string_first = Encoding.UTF8.GetString(first);
var string_high = Encoding.UTF8.GetString(high);

var base_string_ascii = Convert.ToBase64String(asci);
var base_string_first = Convert.ToBase64String(first);
var base_string_high = Convert.ToBase64String(high);
var base_string_text = Convert.ToBase64String(bytes_text);


var base_base_ascii = Convert.FromBase64String(base_string_ascii);
var base_base_first = Convert.FromBase64String(base_string_first);
var base_base_high = Convert.FromBase64String(base_string_high);
var base_base_text = Convert.FromBase64String(base_string_text);


var c1 = Convert.ToBase64String(Encoding.UTF8.GetBytes("a"));
var c2 = Convert.ToBase64String(Encoding.UTF8.GetBytes(c1));
var c3 = Convert.FromBase64String(c2);
var pre = Encoding.UTF8.GetString(c3);
var c4 = Convert.FromBase64String(pre);
var final = Encoding.UTF8.GetString(c4);


var d1 = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes("a"));

var decode = WebEncoders.Base64UrlDecode(d1);
var decodeString = Encoding.UTF8.GetString(decode);
var decode2 = WebEncoders.Base64UrlDecode(c1);
var decodeString2 = Encoding.UTF8.GetString(decode2);

Console.WriteLine("");