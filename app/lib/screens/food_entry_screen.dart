import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/foodentry.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/main.dart';
import 'package:eat_somewhere/screens/create_food_entry_screen.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:flutter/material.dart';

class FoodEntryScreen extends StatefulWidget {
  const FoodEntryScreen({Key? key}) : super(key: key);

  @override
  State<FoodEntryScreen> createState() => _FoodEntryScreenState();

  static void CreateFoodEntry() {
    Navigator.push(
      navigatorKey.currentContext!,
      MaterialPageRoute(
        builder: (context) => CreateFoodEntryScreen(foodEntry: FoodEntry(),),
      ),
    );
  }
}

class _FoodEntryScreenState extends State<FoodEntryScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Food Entry'),
      ),
      body: ListView(
        children: Storage.getFoodEntriesForCurrentAssembly()
            .map<Widget>((x) => FoodEntryWidget(foodEntry: x))
            .toList(),
      ),
    );
  }
}

class FoodEntryWidget extends StatefulWidget {
  FoodEntry foodEntry;
  Function()? onTap;
  FoodEntryWidget({Key? key, required this.foodEntry, this.onTap}) : super(key: key);

  @override
  State<FoodEntryWidget> createState() => _FoodEntryWidgetState();
}

class _FoodEntryWidgetState extends State<FoodEntryWidget> {
  @override
  Widget build(BuildContext context) {
    String billSummary = "";
    List<Bill?> bill = widget.foodEntry.bills.where((x) => x!.user?.Id == Storage.getUser()?.userId).toList();
    if (bill.isNotEmpty) {
      billSummary = "You -> ${PriceHelper.formatPriceWithUnit(bill[0]?.amount)} -> ${bill[0]?.recipient?.Username}";
    } else {
      billSummary = "You are not in this bill";
    }
    return PaddedCard(
      onTap: widget.onTap,
        child: Row(
      spacing: 10,
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
          Text(DateHelper.formatDateTime(widget.foodEntry.date)),
          Text((widget.foodEntry.payedBy == null ? "No one" : (widget.foodEntry.payedBy!.isSelf() ? "You paid" : "${widget.foodEntry.payedBy!.Username} paid") + " ${PriceHelper.formatPriceWithUnit(widget.foodEntry.cost)}"),),
          Text(billSummary)
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
                    builder: (context) => CreateFoodEntryScreen(
                      foodEntry: widget.foodEntry,
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