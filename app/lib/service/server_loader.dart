import 'dart:convert';

import 'package:eat_somewhere/data/food.dart';

import '../backend_data/assembly.dart';
import 'package:http/http.dart' as http;

import '../data/user.dart';
import 'Server_com.dart';


class ErrorContainer<T> {
  T? value;
  String? error;
  ErrorContainer(this.value, this.error);
}

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
  static Future<List<Food>> LoadFoods() async {
    var response = await ServerCom.get("/api/v1/foods");
    print(response.body);
    if (response.statusCode == 200) {
      return (jsonDecode(response.body) as List<dynamic>).map((e) => Food.fromJson(e)).toList();
    }
    return [];
  }


  static Future<String?> createAssembly(Assembly a) async {

    var response = await ServerCom.post("/api/v1/assembly", jsonEncode(a.toJson()));
    return extractError(response);
  }

  static Future<ErrorContainer<CreatedResponse>> postIngredient(Ingredient ingredient) async {
    var response = await ServerCom.post("/api/v1/ingredient", jsonEncode(ingredient.toJson()));
    print(response.body);
    if (response.statusCode == 200) {
      return ErrorContainer(CreatedResponse.fromJson(jsonDecode(response.body)), null);
    } else {
      return ErrorContainer(null, "Error: ${response.statusCode} ${response.body}");
    }
  }

  static Future<ErrorContainer<CreatedResponse>> postFood(Food food) async {
    var response = await ServerCom.post("/api/v1/food", jsonEncode(food.toJson()));
    print(response.body);
    if (response.statusCode == 200) {
      return ErrorContainer(CreatedResponse.fromJson(jsonDecode(response.body)), null);
    } else {
      return ErrorContainer(null, "Error: ${response.statusCode} ${response.body}");
    }
  }

  static Future<ErrorContainer<CreatedResponse>> joinAssembly(String name) async {
    var response = await ServerCom.post("/api/v1/joinassembly", jsonEncode({"Name": name}));
    print(response.body);
    if (response.statusCode == 200) {
      return ErrorContainer(CreatedResponse.fromJson(jsonDecode(response.body)), null);
    } else {
      return ErrorContainer(null, "Error: ${response.statusCode} ${response.body}");
    }
  }

  static Future<ErrorContainer<CreatedResponse>> promoteToAdmin(String s, String t) async{
    var response = await ServerCom.post("/api/v1/promote", jsonEncode({"AssemblyId": s, "UserId": t}));
    print(response.body);
    if (response.statusCode == 200) {
      return ErrorContainer(CreatedResponse.fromJson(jsonDecode(response.body)), null);
    } else {
      return ErrorContainer(null, "Error: ${response.statusCode} ${response.body}");
    }

  }

  static Future<ErrorContainer<CreatedResponse>> removeUserFromAssembly(String s, String t) async {
    var response = await ServerCom.post("/api/v1/removeuser", jsonEncode({"AssemblyId": s, "UserId": t}));
    print(response.body);
    if (response.statusCode == 200) {
      return ErrorContainer(CreatedResponse.fromJson(jsonDecode(response.body)), null);
    } else {
      return ErrorContainer(null, "Error: ${response.statusCode} ${response.body}");
    }
  }

  static Future<ErrorContainer<CreatedResponse>> addToAssembly(String s, String t) async {
    var response = await ServerCom.post("/api/v1/adduser", jsonEncode({"AssemblyId": s, "UserId": t}));
    print(response.body);
    if (response.statusCode == 200) {
      return ErrorContainer(CreatedResponse.fromJson(jsonDecode(response.body)), null);
    } else {
      return ErrorContainer(null, "Error: ${response.statusCode} ${response.body}");
    }
  }
}

class CreatedResponse {
  String? createdId;
  String? error;
  bool? success;
  dynamic? data;

  CreatedResponse({this.createdId, this.error, this.success = false});

  CreatedResponse.fromJson(Map<String, dynamic> json) {
    createdId = json["CreatedId"];
    error = json["Error"];
    success = json["Success"];
    data = json["Data"];
  }
  Map<String, dynamic> toJson() {
    return {
      "CreatedId": createdId,
      "Error": error,
      "Success": success,
      "Data": data,
    };
  }
}