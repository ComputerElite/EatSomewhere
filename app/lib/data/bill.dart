import 'package:eat_somewhere/backend_data/Backend_user.dart';

class Bill {
  BackendUser? user;
  BackendUser? recipient;
  int amount = 0;
  DateTime date = DateTime.now();
  int persons = 1;

  Bill.fromJson(Map<String, dynamic> json) {
    if(json["User"] != null) user = BackendUser.fromJson(json["User"]);
    if(json["Recipient"] != null) recipient = BackendUser.fromJson(json["Recipient"]);
    amount = json["Amount"];
    persons = json["Persons"] ?? 1;
    if(json["Date"] != null) date = DateTime.parse(json["Date"]);
  }
  
  toJson() {
    return {
      "User": user?.toJson(),
      "Recipient": recipient?.toJson(),
      "Amount": amount,
      "Persons": persons,
      "Date": date.toIso8601String(),
    };
  }
}