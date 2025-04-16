import 'dart:convert';

import 'package:eat_somewhere/backend_data/ingredient.dart';

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
    print(response.body);
    if (response.statusCode == 200) {
      return (jsonDecode(response.body) as List<dynamic>).map((e) => Assembly.fromJson(e)).toList();
    }
    return [];
  }

  static Future<List<Ingredient>> LoadIngredients() async {
    var response = await ServerCom.get("/api/v1/ingredients");
    if (response.statusCode == 200) {
      return (jsonDecode(response.body) as List<dynamic>).map((e) => Ingredient.fromJson(e)).toList();
    }
    return [];
  }

  static Future<String?> createAssembly(Assembly a) async {

    var response = await ServerCom.post("/api/v1/assembly", jsonEncode(a.toJson()));
    return extractError(response);
  }
}