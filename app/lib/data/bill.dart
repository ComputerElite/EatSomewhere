import 'package:eat_somewhere/backend_data/Backend_user.dart';

class Bill {
  BackendUser? user;
  BackendUser? recipient;
  int amount = 0;
  int persons = 1;

  Bill.fromJson(Map<String, dynamic> json) {
    user = BackendUser.fromJson(json["User"]);
    recipient = BackendUser.fromJson(json["Recipient"]);
    amount = json["Amount"];
    persons = json["Persons"] ?? 1;
  }
  
  toJson() {
    return {
      "User": user?.toJson(),
      "Recipient": recipient?.toJson(),
      "Amount": amount,
      "Persons": persons,
    };
  }
}