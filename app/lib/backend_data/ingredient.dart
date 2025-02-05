import 'package:eat_somewhere/backend_data/assembly.dart';

import 'unit.dart';

class Ingredient {
  String Id;
  String Name;
  double Cost;
  double Amount;
  IngredientUnit Unit;
  DummyAssembly Assembly;

  Ingredient({
    required this.Id,
    required this.Name,
    required this.Cost,
    required this.Amount,
    required this.Unit,
    required this.Assembly,
  });

  factory Ingredient.fromJson(Map<String, dynamic> json) {
    return Ingredient(
      Id: json['Id'],
      Name: json['Name'],
      Cost: json['Cost'],
      Amount: json['Amount'],
      Unit: IngredientUnit.values[json['Unit']],
      Assembly: DummyAssembly.fromJson(json['Assembly']),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'Id': Id,
      'Name': Name,
      'Cost': Cost,
      'Amount': Amount,
      'Unit': Unit.index,
      'Assembly': Assembly.toJson(),
    };
  }
}
