import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:flutter/material.dart';

import '../data/user.dart';

class UserWidget extends StatelessWidget {
  User user;

  UserWidget({Key? key, required this.user}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    ThemeData t = Theme.of(context);
    return PaddedCard(
      child: Column(
        crossAxisAlignment: CrossAxisAlignment.start,
        children: <Widget>[
          Text(
            "Logged in as ",
          ),
          Text(
            user.name,
            style: t.textTheme.headlineSmall
                ?.copyWith(fontWeight: FontWeight.bold),
          )
        ],
      ),
    );
  }
}
