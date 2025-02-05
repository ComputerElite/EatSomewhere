import '../data/user.dart';

class LoginManager {
  String server;
  String username;
  String password;

  LoginManager({required this.server, required this.username, required this.password});

  Future<LoginResult> register() {
    
  }

  Future<LoginResult> login() {

  }

  
}

class LoginResult {
  String? error;
  User? user;

  LoginResult({this.error, this.user});
}