import 'package:eat_somewhere/service/storage.dart';
import 'package:http/http.dart' as http;

class ServerCom {
  static Future<http.Response> get(String url) async {
    return await http.get(Uri.parse((Storage.getUser()?.server ?? "") + url), headers: {
      "Authorization": "Bearer ${Storage.getUser()?.session}",
    });
  }
  static Future<http.Response> post(String url, String body) async {
    return await http.post(Uri.parse((Storage.getUser()?.server ?? "") + url), body: body, headers: {
      "Authorization": "Bearer ${Storage.getUser()?.session}",
    });
  }
}