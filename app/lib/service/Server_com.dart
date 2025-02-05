import 'package:eat_somewhere/service/storage.dart';
import 'package:http/http.dart' as http;

class ServerCom {
  static Future<http.Response> get(String url) async {
    return await http.get(Uri.parse(url), headers: {
      "Authorization": "Bearer ${Storage.getUser()?.session}",
    });
  }
}