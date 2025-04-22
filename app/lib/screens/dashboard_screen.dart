import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/screens/bill_screen.dart';
import 'package:eat_somewhere/service/server_loader.dart';
import 'package:eat_somewhere/widgets/bill_widget.dart';
import 'package:eat_somewhere/widgets/chips/user_price_chip.dart';
import 'package:eat_somewhere/widgets/constrained_container.dart';
import 'package:eat_somewhere/widgets/error_dialog.dart';
import 'package:eat_somewhere/widgets/chips/price_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:eat_somewhere/widgets/heading.dart';
import 'package:eat_somewhere/widgets/loading_widget.dart';
import 'package:flutter/material.dart';

class DashboardScreen extends StatefulWidget {
  const DashboardScreen({Key? key}) : super(key: key);

  @override
  State<DashboardScreen> createState() => _DashboardScreenState();
}

class _DashboardScreenState extends State<DashboardScreen> {
  List<Bill>? bills = null;

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    ServerLoader.LoadTotals().then((value) {
      if (!mounted) return;
      setState(() {
        bills = value;
      });
    });
  }

  @override
  Widget build(BuildContext context) {
    return ConstrainedContainer(
        child: Scaffold(
      appBar: AppBar(
        title: const Text("Dashboard"),
      ),
      body: Center(
        child: ListView(
          children: [
            FilledButton(
                onPressed: () {
                  Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => BillScreen(),
                    ),
                  );
                },
                child: Text("See bill history")),
            Heading(text: "Assembly balance"),
            if (bills == null)
              LoadingWidget()
            else
              ...bills!
                  .map((x) => BillWidget(
                        bill: x,
                      ))
                  .toList(),
          ],
        ),
      ),
    ));
  }
}
