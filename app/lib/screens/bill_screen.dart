import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/bill_widget.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_price_chip.dart';
import 'package:eat_somewhere/widgets/constrained_container.dart';
import 'package:eat_somewhere/widgets/loading_widget.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:flutter/material.dart';

class BillScreen extends StatefulWidget {
  const BillScreen({Key? key}) : super(key: key);

  @override
  State<BillScreen> createState() => _BillScreenState();
}

class _BillScreenState extends State<BillScreen> {
  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    Storage.onDataReload = () {
      setState(() {});
    };
    Storage.reloadBills().then((value) {
      setState(() {});
    });
  }

  @override
  Widget build(BuildContext context) {
    List<Bill>? bills = Storage.getBillsForCurrentAssembly();
    return Scaffold(
        appBar: AppBar(
          title: const Text("Bill history"),
        ),
        body: ConstrainedContainer(
            child: bills == null
                ? LoadingWidget()
                : ListView(
                    children: [
                      ...bills!
                          .map((x) => BillWidget(
                                bill: x,
                              ))
                          .toList(),
                    ],
                  )));
  }
}
