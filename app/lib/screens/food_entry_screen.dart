import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/foodentry.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/main.dart';
import 'package:eat_somewhere/screens/create_food_entry_screen.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/bill_widget.dart';
import 'package:eat_somewhere/widgets/chips/additional_persons_chip.dart';
import 'package:eat_somewhere/widgets/constrained_container.dart';
import 'package:eat_somewhere/widgets/desktop_mobile_refresh_indicator.dart';
import 'package:eat_somewhere/widgets/loading_widget.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:eat_somewhere/widgets/chips/price_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_additional_person_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_price_chip.dart';
import 'package:flutter/material.dart';

class FoodEntryScreen extends StatefulWidget {
  const FoodEntryScreen({Key? key}) : super(key: key);

  @override
  State<FoodEntryScreen> createState() => _FoodEntryScreenState();

  static void CreateFoodEntry() {
    Navigator.push(
      navigatorKey.currentContext!,
      MaterialPageRoute(
        builder: (context) => CreateFoodEntryScreen(
          foodEntry: FoodEntry(),
        ),
      ),
    );
  }
}

class _FoodEntryScreenState extends State<FoodEntryScreen> {
  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    Storage.onDataReload = () {
      setState(() {});
    };
  }

  @override
  Widget build(BuildContext context) {
    List<FoodEntry>? foodEntries = Storage.getFoodEntriesForCurrentAssembly();
    return ConstrainedContainer(
        child: Scaffold(
            appBar: AppBar(
              title: const Text('Food Entry'),
            ),
            body: foodEntries == null
                ? LoadingWidget()
                : DesktopMobileRefreshIndicator(
                    onRefresh: () async {
                      await Storage.reloadFoodEntries();
                      setState(() {});
                    },
                    child: ListView(
                      children: foodEntries!
                          .map<Widget>((x) => FoodEntryWidget(foodEntry: x))
                          .toList(),
                    ),
                  )));
  }
}

class FoodEntryWidget extends StatefulWidget {
  FoodEntry foodEntry;
  Function()? onTap;
  FoodEntryWidget({Key? key, required this.foodEntry, this.onTap})
      : super(key: key);

  @override
  State<FoodEntryWidget> createState() => _FoodEntryWidgetState();
}

class _FoodEntryWidgetState extends State<FoodEntryWidget> {
  @override
  Widget build(BuildContext context) {
    String billSummary = "";
    List<Bill?> bill = widget.foodEntry.bills
        .where((x) => x!.user?.Id == Storage.getUser()?.userId)
        .toList();
    ThemeData theme = Theme.of(context);
    return PaddedCard(
        onTap: widget.onTap,
        child: Row(
          spacing: 10,
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Column(crossAxisAlignment: CrossAxisAlignment.start, children: [
              Row(children: [
                Text(
                  widget.foodEntry.food?.name ?? "Unknown",
                  style: theme.textTheme.headlineSmall,
                ),
                if(widget.foodEntry.inProgress) Chip(label: Text("In progress"))
              ],),
              Text(
                DateHelper.formatDateTime(widget.foodEntry.date),
                style: theme.textTheme.labelMedium,
              ),
              if (bill.isNotEmpty)
                BillContent(bill: bill[0]!)
              else
                Text("No bill"),
            ]),
            Row(
              spacing: 10,
              children: [
                IconButton(
                    icon: const Icon(Icons.edit),
                    onPressed: () async {
                      await Navigator.push(
                        context,
                        MaterialPageRoute(
                          builder: (context) => CreateFoodEntryScreen(
                            foodEntry: widget.foodEntry,
                          ),
                        ),
                      );
                      setState(() {});
                    })
              ],
            )
          ],
        ));
  }
}
