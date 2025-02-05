class User {
  String name;
  String session;
  String server;

  User({required this.name, required this.session, required this.server});

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      name: json['name'],
      session: json['session'],
      server: json['server']
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'session': session,
      'server': server
    };
  }
}