import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/service/storage.dart';
class BackendUser {
  String Id;
  String username;
  List<Ingredient> Intolerances = [];

  BackendUser({
    required this.Id,
    required this.username,
    required this.Intolerances,
  });

  factory BackendUser.fromJson(Map<String, dynamic> json) {
    return BackendUser(
      Id: json['Id'] as String,
      username: json['Username'] as String,
      Intolerances: ((json['Intolerances'] ?? []) as List<dynamic>)
          .map((e) => Ingredient.fromJson(e as Map<String, dynamic>))
          .toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'Id': Id,
      'Username': username,
      'Intolerances': Intolerances.map((e) => e.toJson()).toList(),
    };
  }

  bool isSelf() {
    return Id == Storage.getUser()?.userId;
  }
}
