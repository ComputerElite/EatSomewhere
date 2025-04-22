import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/widgets/chips/additional_persons_chip.dart';
import 'package:eat_somewhere/widgets/chips/arrow_chip.dart';
import 'package:eat_somewhere/widgets/chips/arrow_price_chip.dart';
import 'package:eat_somewhere/widgets/chips/chip_combiner.dart';
import 'package:eat_somewhere/widgets/chips/price_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_additional_person_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_price_chip.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:flutter/material.dart';

class BillWidget extends StatelessWidget {
  final Bill bill;

  const BillWidget({
    Key? key,
    required this.bill,
  }) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return PaddedCard(
      child: Row(
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          BillContent(bill: bill),
          Text(DateHelper.formatDateTime(bill.date))
        ],
      )
    );
  }
}

class BillContent extends StatelessWidget {
  final Bill bill;

  const BillContent({
    Key? key,
    required this.bill,
  }) : super(key: key);
  @override
  Widget build(BuildContext context) {
    // TODO: implement build
    return ChipCombiner(
            chips: [
              UserChip(user: bill.user),
              if(bill.persons > 1) AdditionalPersonsChip(additionalPersons: bill.persons - 1, user: bill.user,),
              ArrowPriceChip(amount: bill.amount),
              UserChip(user: bill.recipient),
            ],
          );
  }
}