import 'dart:math';

import 'package:eat_somewhere/screens/dashboard_screen.dart';
import 'package:eat_somewhere/screens/food_entry_screen.dart';
import 'package:eat_somewhere/screens/food_screen.dart';
import 'package:eat_somewhere/service/keyboard_callbacks.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'settings/settings_screen.dart';

class ScreenSelector extends StatefulWidget {
  const ScreenSelector({Key? key}) : super(key: key);

  @override
  State<ScreenSelector> createState() => _ScreenSelectorState();
}

class _ScreenSelectorState extends State<ScreenSelector> {
  int _selectedIndex = 0;
  PageController pageController = PageController();
  List<Widget> screens = [];
  List<BottomNavigationBarItem> navigationBarItems = [];
  List<Widget?> floatingActionButtons = [];

  @override
  void initState() {
    screens = [
      DashboardScreen(),
      FoodEntryScreen(),
      FoodScreen(),
      SettingsScreen(),
    ];
    navigationBarItems = [
      BottomNavigationBarItem(icon: Icon(Icons.dashboard), label: 'Dashboard'),
      BottomNavigationBarItem(icon: Icon(Icons.food_bank), label: 'FoodEntries'),
      BottomNavigationBarItem(icon: Icon(Icons.local_pizza), label: 'Food'),
      BottomNavigationBarItem(icon: Icon(Icons.settings), label: 'Settings'),
    ];
    floatingActionButtons = <Widget?>[
      null,
      FloatingActionButton(onPressed: () {
        FoodEntryScreen.CreateFoodEntry();
      }, child: Icon(Icons.add), tooltip: "Create food entry"),
      FloatingActionButton(onPressed: () {
        FoodScreen.CreateFood();
      }, child: Icon(Icons.add), tooltip: "Create food", heroTag: "create_food"),
      null,
    ];

    Storage.getPageIndex().then((index) {
      if (index != -1) {
        _selectedIndex = min(index, screens.length);
      } else {
        if (Storage.getUser() == null) _selectedIndex = 0;
      }
      setState(() {});
      _tap(_selectedIndex, true);
    });
    super.initState();
  }

  void _tap(int index, bool switchPage) {
    index = max(0, min(index, screens.length - 1));
    setState(() {
      if(switchPage) pageController.jumpToPage(index);
      _selectedIndex = index;
    });
    Storage.savePageIndex(index);
  }

  final FocusNode focusNode = FocusNode();

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: KeyboardListener(
            autofocus: true,
            onKeyEvent: (KeyEvent event) {
              if (event is KeyDownEvent) {
                switch (event.logicalKey) { 
                  case LogicalKeyboardKey.f5:
                    if (KeyboardCallbacks.onRefresh != null) KeyboardCallbacks.onRefresh!();
                    break;
                }
              }
            },
            focusNode: focusNode,
            child: PageView(
                children: screens.map((e) => Padding(padding: const EdgeInsets.all(15), child: e)).toList(),
                onPageChanged: (index) {
                  _tap(index, false);
                },
                controller: pageController,
              ),
            ),
        appBar: null,
        floatingActionButton: floatingActionButtons.elementAt(_selectedIndex),
        bottomNavigationBar: BottomNavigationBar(
          items: navigationBarItems,
          type: BottomNavigationBarType.fixed,
          currentIndex: _selectedIndex,
          onTap: (index)=> _tap(index, true),
        ));
  }
}