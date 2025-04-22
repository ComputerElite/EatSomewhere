import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/widgets/chips/combinable_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:flutter/material.dart';

class ArrowChip extends CombinableChip {
  final Function()? onTap;

  ArrowChip(
      {Key? key, this.onTap, shape})
      : super(key: key, shape: shape);

  @override
  Widget build(BuildContext context) {
    return Chip(
      label: Text(
        "→",
        style: const TextStyle(color: Colors.white),
      ),
      shape: this.shape,
      padding: const EdgeInsets.all(0),
    );
  }
}
