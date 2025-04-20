import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/service/server_loader.dart';
import 'package:eat_somewhere/widgets/error_dialog.dart';
import 'package:flutter/material.dart';

class DashboardScreen extends StatefulWidget{
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
        child: ListView(children: bills.map((x) => 
          ListTile(
            title: Text("${x.user?.Username} owes ${x.recipient?.Username} ${PriceHelper.formatPriceWithUnit(x.amount)}"),
          )
        ).toList(),),
      ),
    );
  }
}