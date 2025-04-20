import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/screens/create_food.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:flutter/material.dart';

class FoodWidget extends StatefulWidget {
  Food food;
  Function()? onTap;

  FoodWidget({
    Key? key,
    required this.food,
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
            IconButton(
              icon: const Icon(Icons.edit),
              onPressed: () {
                Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => CreateFoodScreen(
                      food: widget.food,
                    ),
                  ),
                );
              })
          ],
        )
      ],
    ));
  }
}
