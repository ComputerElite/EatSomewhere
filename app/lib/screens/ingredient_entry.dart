import 'package:eat_somewhere/data/food.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class IngredientEntryWidget extends StatefulWidget {
  IngredientEntry ingredient;

  IngredientEntryWidget({
    Key? key,
    required this.ingredient
  }) : super(key: key);

  @override
  State<IngredientEntryWidget> createState() => _IngredientEntryWidgetState();
}

class _IngredientEntryWidgetState extends State<IngredientEntryWidget> {
  @override
  Widget build(BuildContext context) {
    return Row(
      children: [
        new TextField(
          decoration: new InputDecoration(labelText: "Amount"),
          keyboardType: TextInputType.number,
          onChanged: (value) {
            widget.ingredient.amount = double.tryParse(value.replaceAll(",", ".")) ?? 1;
          },
          controller: TextEditingController(
            text: widget.ingredient.amount.toString(),
          ),
          inputFormatters: <TextInputFormatter>[
            FilteringTextInputFormatter.allow(RegExp(r'^[0-9]+((.|,)[0-9]+)?$'))
          ], // Only numbers can be entered
        ),
      ],
    );
  }
}