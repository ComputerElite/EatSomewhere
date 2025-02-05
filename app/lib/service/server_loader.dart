import 'dart:convert';

import '../backend_data/assembly.dart';
import 'package:http/http.dart' as http;

import '../data/user.dart';
import 'Server_com.dart';

class ServerLoader {
  static Future<String?> extractError(http.Response response) async {
    if(response.statusCode == 200) {
      return null;
    }
    try {
      var json = jsonDecode(response.body);
      return json["error"];
    } catch (e) {
      return "Server returned ${response.statusCode}";
    }
  }

  static Future<List<Assembly>> LoadAssemblies() async {
    var response = await ServerCom.get("/api/v1/assemblies");
    if (response.statusCode == 200) {
      return (response.body as List<dynamic>).map((e) => Assembly.fromJson(e)).toList();
    }
    return [];
  }

  static Future<String?> createAssembly(Assembly a) async {

    var response = await ServerCom.post("/api/v1/assemblies", jsonEncode(a.toJson()));
    return extractError(response);
  }
}