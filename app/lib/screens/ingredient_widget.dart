import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:flutter/material.dart';

class IngredientWidget extends StatefulWidget {
  Ingredient ingredient;
  Function() onTap;

  IngredientWidget({required this.ingredient, required this.onTap, Key? key})
      : super(key: key);

  @override
  State<IngredientWidget> createState() => _IngredientWidgetState();
}

class _IngredientWidgetState extends State<IngredientWidget> {
  @override
  Widget build(BuildContext context) {
    return ListTile(
      title: Text(widget.ingredient.name ?? "Unknown"),
      subtitle: Text("${widget.ingredient.amount} ${widget.ingredient.unit.name} - ${PriceHelper.formatPriceWithUnit(widget.ingredient.cost)}"),
      onTap: widget.onTap,
    );
  }
}
