import 'dart:math';

import 'package:eat_somewhere/service/keyboard_callbacks.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

import 'settings_screen.dart';

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
      SettingsScreen(),
      SettingsScreen(),
    ];
    navigationBarItems = [
      BottomNavigationBarItem(icon: Icon(Icons.settings), label: 'Settings'),
      BottomNavigationBarItem(icon: Icon(Icons.settings), label: 'Fake Settings'),
    ];
    floatingActionButtons = <Widget?>[
      null,
      null,
    ];

    Storage.getPageIndex().then((index) {
      if (index != -1) {
        _selectedIndex = min(index, screens.length);
      } else {
        if (Storage.getUser() == null) _selectedIndex = 0;
      }
      setState(() {});
      _tap(_selectedIndex);
    });
    super.initState();
  }

  void _tap(int index) {
    index = max(0, min(index, screens.length - 1));
    setState(() {
      pageController.jumpToPage(index);
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
                  case LogicalKeyboardKey.arrowLeft:
                    _tap(_selectedIndex - 1);
                    break;
                  case LogicalKeyboardKey.arrowRight:
                    _tap(_selectedIndex + 1);
                    break;
                  case LogicalKeyboardKey.f5:
                    if (KeyboardCallbacks.onRefresh != null) KeyboardCallbacks.onRefresh!();
                    break;
                }
              }
            },
            focusNode: focusNode,
            child: Padding(
              padding: const EdgeInsets.only(
                bottom: 15,
                left: 15,
                right: 15,
                top: 15,
              ),
              child: PageView(
                children: screens,
                onPageChanged: _tap,
                controller: pageController,
              ),
            )),
        appBar: null,
        floatingActionButton: floatingActionButtons.elementAt(_selectedIndex),
        bottomNavigationBar: BottomNavigationBar(
          items: navigationBarItems,
          type: BottomNavigationBarType.fixed,
          currentIndex: _selectedIndex,
          onTap: _tap,
        ));
  }
}
