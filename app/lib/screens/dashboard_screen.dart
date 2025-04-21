import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/service/server_loader.dart';
import 'package:eat_somewhere/widgets/chips/user_price_chip.dart';
import 'package:eat_somewhere/widgets/error_dialog.dart';
import 'package:eat_somewhere/widgets/chips/price_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:flutter/material.dart';

class DashboardScreen extends StatefulWidget {
  const DashboardScreen({Key? key}) : super(key: key);

  @override
  State<DashboardScreen> createState() => _DashboardScreenState();
}

class _DashboardScreenState extends State<DashboardScreen> {
  List<Bill> bills = [];

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    ServerLoader.LoadBills().then((value) {
      if (!mounted) return;
      setState(() {
        bills = value;
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text("Dashboard"),
      ),
      body: Center(
        child: ListView(
          children: bills
              .map((x) => ListTile(
                    title: Row(
                      children: [
                        UserChip(user: x.user),
                        Text("owes"),
                        UserPriceChip(amount: x.amount, user: x.recipient)
                      ],
                    ),
                  ))
              .toList(),
        ),
      ),
    );
  }
}
