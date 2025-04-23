import 'package:eat_somewhere/data/foodentry.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/widgets/chips/price_chip.dart';
import 'package:eat_somewhere/widgets/constrained_container.dart';
import 'package:flutter/material.dart';

class ShoppingListScreen extends StatefulWidget {
  final FoodEntry foodEntry;

  const ShoppingListScreen({Key? key, required this.foodEntry})
      : super(key: key);

  @override
  State<ShoppingListScreen> createState() => _ShoppingListScreenState();
}

class _ShoppingListScreenState extends State<ShoppingListScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Shopping List"),
      ),
      body: ConstrainedContainer(child: ListView(
        children: [
          ListTile(
            title: Text(widget.foodEntry.food?.name ?? "Unknown"),
            subtitle: Text(
                "Price: ${PriceHelper.formatPriceWithUnit(widget.foodEntry.getEstimatedCost())}"),
          ),
          // Adjust the table to use the minimum width it needs
          Row(
            children: [
              Expanded(child: Container(child: Table(
                defaultVerticalAlignment: TableCellVerticalAlignment.middle,
                children: [
                  ...widget.foodEntry
                      .getShoppingList()
                      .map((item) => TableRow(
                        children: [
                            Text(item.amount.toString()),
                            Text(item.ingredient?.unit.name ?? "Unknown"),
                            Text(item.ingredient?.name ?? "Unknown"),
                            PriceChip(amount: item.getEstimatedCost()),
                          ])),
                ],
              )))
            ],
          )
        ],
      ),
    ));
  }
}
