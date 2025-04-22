import 'dart:convert';

import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/data/foodentry.dart';
import 'package:eat_somewhere/data/settings.dart';
import 'package:eat_somewhere/service/storage.dart';

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
  static Future<List<FoodEntry>> LoadFoodEntries(int skip, int count) async {
    var response = await ServerCom.get("/api/v1/foodentries/${Storage.getSettings().chosenAssembly}?skip=$skip&count=$count");
    print(response.body);
    if (response.statusCode == 200) {
      return (jsonDecode(response.body) as List<dynamic>).map((e) => FoodEntry.fromJson(e)).toList();
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

  static Future<ErrorContainer<CreatedResponse>> postFoodEntry(FoodEntry foodEntry) async {
    var response = await ServerCom.post("/api/v1/foodentry", jsonEncode(foodEntry.toJson()));
    print(response.body);
    if (response.statusCode == 200) {
      return ErrorContainer(CreatedResponse.fromJson(jsonDecode(response.body)), null);
    } else {
      return ErrorContainer(null, "Error: ${response.statusCode} ${response.body}");
    }
  }

  static Future<List<Bill>> LoadTotals() async {
    var response = await ServerCom.get("/api/v1/totals/${Storage.getSettings().chosenAssembly}");
    print(response.body);
    if (response.statusCode == 200) {
      return (jsonDecode(response.body) as List<dynamic>).map((e) => Bill.fromJson(e)).toList();
    }
    return [];
  }

  static Future<ErrorContainer<bool?>> deleteFood(Food food) async {
    var response = await ServerCom.delete("/api/v1/food/${food.id}");
    print(response.body);
    if (response.statusCode == 200) {
      return ErrorContainer(jsonDecode(response.body)["Success"], null);
    } else {
      return ErrorContainer(null, "Error: ${response.statusCode} ${response.body}");
    }
  }

  static Future<List<Bill>> LoadBills(int skip, int count) async {
    var response = await ServerCom.get("/api/v1/bills/${Storage.getSettings().chosenAssembly}?skip=$skip&count=$count");
    print(response.body);
    if (response.statusCode == 200) {
      return (jsonDecode(response.body) as List<dynamic>).map((e) => Bill.fromJson(e)).toList();
    }
    return [];
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