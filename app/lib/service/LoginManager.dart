import 'dart:convert';

import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/backend_data/backend_login_response.dart';

import '../data/user.dart';
import 'package:http/http.dart' as http;

class LoginManager {
  String server;
  String username;
  String password;

  LoginManager({required this.server, required this.username, required this.password});

  Future<LoginResult> loginOrRegister(bool register) async {
    http.Response response = await http.post(Uri.parse("$server/api/v1/user/${register ? "register" : "login"}"), body: jsonEncode({
      "Username": username,
      "Password": password
    }));
    
    try {
      BackendLoginResponse loginResponse = BackendLoginResponse.fromJson(jsonDecode(response.body));
      if(!loginResponse.Success) {
        return LoginResult(error: loginResponse.Error);
      }
      http.Response userResponse = await http.get(Uri.parse("$server/api/v1/user/me"), headers: {"Authorization": "Bearer ${loginResponse.SessionId}"});
      if(userResponse.statusCode != 200) {
        return LoginResult(error: "Server returned ${userResponse.statusCode}");
      }
      BackendUser user = BackendUser.fromJson(jsonDecode(userResponse.body));
      return LoginResult(user: User(name: username, userId: user.Id, session: loginResponse.SessionId!, server: server));
    } catch (e) {
      return LoginResult(error: "Server returned ${response.statusCode}");
    }
  }
}

class LoginResult {
  String? error;
  User? user;

  LoginResult({this.error, this.user});
}