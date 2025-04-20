class User {
  String name;
  String session;
  String server;

  String? userId;

  User({required this.name, required this.session, required this.server, this.userId});

  factory User.fromJson(Map<String, dynamic> json) {
    return User(
      name: json['name'],
      session: json['session'],
      server: json['server'],
      userId: json['userId'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'name': name,
      'session': session,
      'server': server,
      'userId': userId
    };
  }
}