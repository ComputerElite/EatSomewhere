import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/widgets/chips/combinable_chip.dart';
import 'package:flutter/material.dart';

class PriceChip extends CombinableChip {
  final int? amount;
  final Function()? onTap;

  PriceChip({Key? key, required this.amount, shape, this.onTap})
      : super(key: key, shape: shape);

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Chip(
        label: Text(
          PriceHelper.formatPriceWithUnit(amount),
          style: const TextStyle(color: Colors.white),
        ),
        shape: shape,
        padding: const EdgeInsets.all(0),
        backgroundColor: (amount ?? 0) <= 0 ? Colors.green : Colors.red,
      ),
    );
  }
}
