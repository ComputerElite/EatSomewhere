import 'package:eat_somewhere/data/food.dart';
class BackendUser {
  String Id;
  String Username;
  List<Ingredient> Intolerances;

  BackendUser({
    required this.Id,
    required this.Username,
    required this.Intolerances,
  });

  factory BackendUser.fromJson(Map<String, dynamic> json) {
    return BackendUser(
      Id: json['Id'] as String,
      Username: json['Username'] as String,
      Intolerances: (json['Intolerances'] as List<dynamic>)
          .map((e) => Ingredient.fromJson(e as Map<String, dynamic>))
          .toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'Id': Id,
      'Username': Username,
      'Intolerances': Intolerances.map((e) => e.toJson()).toList(),
    };
  }
}
