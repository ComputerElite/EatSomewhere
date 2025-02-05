import 'package:flutter/material.dart';

class ConstrainedContainer extends StatelessWidget {
  final Widget child;

  const ConstrainedContainer({Key? key, required this.child}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Center(
      child: Container(
        width: 1000,
        child: child,
      ),
    );
  }
}
