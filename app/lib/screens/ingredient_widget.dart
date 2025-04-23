import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/screens/create_ingredient_dialog.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/error_dialog.dart';
import 'package:eat_somewhere/widgets/info_dialog.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:eat_somewhere/widgets/yes_cancel_dialog.dart';
import 'package:flutter/material.dart';

class IngredientWidget extends StatefulWidget {
  Ingredient ingredient;
  Function() onTap;
  Function()? ingredientRemoved;

  IngredientWidget({required this.ingredient, required this.onTap, required this.ingredientRemoved, Key? key})
      : super(key: key);

  @override
  State<IngredientWidget> createState() => _IngredientWidgetState();
}

class _IngredientWidgetState extends State<IngredientWidget> {
  @override
  Widget build(BuildContext context) {
    return PaddedCard(
      onTap: widget.onTap,
        child: Row(
      spacing: 10,
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
          Text(widget.ingredient.name ?? "Unknown"),
          Text("~ ${PriceHelper.formatPriceWithUnit(widget.ingredient.cost)} per ${widget.ingredient.amount} ${widget.ingredient.unit.name}"),
        ]),
        Row(
          spacing: 10,
          children: [
            if(widget.ingredientRemoved != null) IconButton(
              icon: const Icon(Icons.delete),
              onPressed: () async {
                if(await YesCancelDialog.show("Archive Ingredient", "Do you really want to archive '${widget.ingredient.name}'? This cannot be undone") == true) {
                  String? error = await Storage.deleteIngredient(widget.ingredient);
                  if(error != null) {
                    ErrorDialog.show("Error archiving Ingredient", error);
                    return;
                  }
                  InfoDialog.show("Ingredient archived", "Ingredient '${widget.ingredient.name}' was successfully archived");
                  widget.ingredientRemoved?.call();
                }
              }),
            IconButton(
              icon: const Icon(Icons.edit),
              onPressed: () async {
                Ingredient? newIngredient = await showDialog(
                  context: context,
                  builder: (builder) => CreateIngredientDialog(
                        ingredient: widget.ingredient
                      ));
                widget.ingredientRemoved?.call();
                setState(() {});
              }),
            IconButton(
              icon: const Icon(Icons.add),
              onPressed: widget.onTap),
          ],
        )
      ],
    ));
  }
}
