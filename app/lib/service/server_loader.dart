import '../backend_data/assembly.dart';
import 'package:http/http.dart' as http;

import '../data/user.dart';
import 'Server_com.dart';

class ServerLoader {
  static Future<List<Assembly>> LoadAssemblies() async {
    var response = await ServerCom.get("/api/v1/assemblies");
    if (response.statusCode == 200) {
      return (response.body as List<dynamic>).map((e) => Assembly.fromJson(e)).toList();
    }
    return [];
  }
}