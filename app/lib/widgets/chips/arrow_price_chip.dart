import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/widgets/chips/combinable_chip.dart';
import 'package:flutter/material.dart';

class ArrowPriceChip extends CombinableChip {
  final int? amount;
  final Function()? onTap;

  ArrowPriceChip({Key? key, required this.amount, shape, this.onTap})
      : super(key: key, shape: shape);

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Chip(
        label: Text(
          "→ ${PriceHelper.formatPriceWithUnit(amount)} →"
        ),
        shape: shape,
        padding: const EdgeInsets.all(0),
      ),
    );
  }
}
