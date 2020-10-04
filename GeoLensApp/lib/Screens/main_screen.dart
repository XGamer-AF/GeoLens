import 'package:data_discovery_for_earth/Screens/home.dart';
import 'package:data_discovery_for_earth/Screens/map.dart';
import 'package:data_discovery_for_earth/Screens/splash_screen.dart';
import 'package:data_discovery_for_earth/Screens/walkthrough_screen.dart';
import 'package:data_discovery_for_earth/theme.dart';
import 'package:eva_icons_flutter/eva_icons_flutter.dart';
import 'package:flutter/material.dart';
import '../localizations.dart';
import 'CookiesClass.dart';
import 'categories.dart';
import 'events.dart';
import 'more.dart';

class MainScreen extends StatefulWidget {
  @override
  _MainScreenState createState() => _MainScreenState();
}

class _MainScreenState extends State<MainScreen> {
  double _sizeIcon = 20.0;
  double _fontSize = 10 ;
  PageController _pageController = PageController();
  int _selectPage = 0;
  final _pages = [Home(), Events(), EventsMap(), More()];

  @override
  void dispose() {
    // TODO: implement dispose
    super.dispose();
    _pageController ?? _pageController.dispose();
  }
  @override
  Widget build(BuildContext context) {
    return Scaffold(
        body: PageView(
          physics:new NeverScrollableScrollPhysics(),
          controller: _pageController,
          onPageChanged: (index) {
            setState(() {
              _selectPage = index;
            });
          },
          children: _pages,
        ),
        bottomNavigationBar: BottomNavigationBar(
          backgroundColor: OwnColors.color2[500],
          selectedItemColor: OwnColors.color2[400],
          unselectedItemColor: OwnColors.color1[500],
          type: BottomNavigationBarType.fixed,
          currentIndex: _selectPage,
          onTap: (index) {
            setState(() {
              _selectPage = index;
              _pageController.animateToPage(_selectPage,
                  duration: Duration(milliseconds: 10), curve: Curves.ease);
            });
          },
          // color: _selectPage == 0
          //     ? OwnColors.color1[500]
          //     : OwnColors.color2[500],
          items: [
            BottomNavigationBarItem(
                icon: Icon(
                  EvaIcons.homeOutline,size: _sizeIcon,
                ),
                title: Text(
                  AppLocalizations.of(context).home_title,style: TextStyle(fontSize: _fontSize),
                  // style: _selectPage == 0
                  //     ? OwnColors.normalColor()
                  //     : OwnColors.normalBlack(),
                )),
            BottomNavigationBarItem(
                icon: Icon(EvaIcons.bookmarkOutline,size: _sizeIcon,),
                title: Text(AppLocalizations.of(context).events_title,style: TextStyle(fontSize: _fontSize),

                )),
            BottomNavigationBarItem(
                icon: Icon(EvaIcons.mapOutline,size: _sizeIcon,),
                title: Text(
                  AppLocalizations.of(context).map_title,style: TextStyle(fontSize: _fontSize),
                )),

            BottomNavigationBarItem(
                icon: Icon(EvaIcons.menuOutline,size: _sizeIcon,),
                title: Text(
                  AppLocalizations.of(context).more_title,style: TextStyle(fontSize: _fontSize),
                )),
          ],
        ));
  }
}
