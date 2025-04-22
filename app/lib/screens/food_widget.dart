import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/data/foodentry.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/screens/create_food.dart';
import 'package:eat_somewhere/screens/create_food_entry_screen.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/error_dialog.dart';
import 'package:eat_somewhere/widgets/info_dialog.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:eat_somewhere/widgets/yes_cancel_dialog.dart';
import 'package:flutter/material.dart';

class FoodWidget extends StatefulWidget {
  Food food;
  Function()? onTap;
  Function()? foodRemoved;

  FoodWidget({
    Key? key,
    required this.food,
    this.foodRemoved,
    this.onTap,
  }) : super(key: key);

  @override
  State<FoodWidget> createState() => _FoodWidgetState();
}

class _FoodWidgetState extends State<FoodWidget> {
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
          Text(widget.food.name ?? "Unknown"),
          Text("~ ${PriceHelper.formatPriceWithUnit(widget.food.estimatedCost)}"),
          Text("~ ${PriceHelper.formatPriceWithUnit(widget.food.getCostPerPerson())} per person"),
        ]),
        Row(
          spacing: 10,
          children: [
            if(widget.foodRemoved != null) IconButton(
              icon: const Icon(Icons.delete),
              onPressed: () async {
                if(await YesCancelDialog.show("Archive Food", "Do you really want to archive '${widget.food.name}'? This cannot be undone") == true) {
                  String? error = await Storage.deleteFood(widget.food);
                  if(error != null) {
                    ErrorDialog.show("Error archiving Food", error);
                    return;
                  }
                  InfoDialog.show("Food archived", "Food '${widget.food.name}' was successfully archived");
                  widget.foodRemoved?.call();
                }
              }),
            IconButton(
              icon: const Icon(Icons.edit),
              onPressed: () async {
                await Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => CreateFoodScreen(
                      food: widget.food,
                    ),
                  ),
                );
                widget.foodRemoved?.call();
                setState(() {});
              }),
            IconButton(
              icon: const Icon(Icons.add),
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => CreateFoodEntryScreen(
                      foodEntry: FoodEntry.fromFood(widget.food),
                    ),
                  ),
                );
              }),
          ],
        )
      ],
    ));
  }
}
