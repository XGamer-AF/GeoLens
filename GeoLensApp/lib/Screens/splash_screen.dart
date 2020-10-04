import 'package:data_discovery_for_earth/Screens/main_screen.dart';
import 'package:data_discovery_for_earth/Screens/walkthrough_screen.dart';
import 'package:flutter/material.dart';
import 'dart:async';

import '../theme.dart';
import 'data_change_lang.dart';
import 'lang.dart';

class SplashScreen extends StatefulWidget {
  final Data data;
  SplashScreen(this.data);
  @override
  _SplashScreenState createState() => _SplashScreenState(this.data);
}

class _SplashScreenState extends State<SplashScreen> {
  final Data data;
  _SplashScreenState(this.data);

  @override
  void initState() {
    // TODO: implement initState
    super.initState();
    Timer(
        Duration(seconds: data.sec2),
        () => {
              if (data.screen == "home")
                {
                  Navigator.pushReplacement(context,
                      new MaterialPageRoute(builder: (context) => MainScreen()))
                }
              else if (data.screen == "lang")
                {
                  Navigator.pushReplacement(context,
                      new MaterialPageRoute(builder: (context) => Language()))
                }
              else if (data.screen == "slider")
                {
                  Navigator.pushReplacement(context,
                      new MaterialPageRoute(builder: (context) => SliderHome()))
                }
              else
                {
                  Navigator.pushReplacement(context,
                      new MaterialPageRoute(builder: (context) => MainScreen()))
                }
            });
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      // key: introKey,
      body: Stack(
        fit: StackFit.expand,
        children: [
          Container(
            color: OwnColors.color2[500],
          ),
          Column(
            mainAxisAlignment: MainAxisAlignment.start,
            children: [
              Expanded(
                flex: 3,
                child: Container(
                  child: Column(
                    mainAxisAlignment: MainAxisAlignment.center,
                    children: [
                      CircleAvatar(
                        radius: 45,
                        backgroundColor: OwnColors.color2[200],
                        child: Image.asset(
                          'assets/Images/logo.png',
                          width: 120,
                          height: 120,
                        ),
                      ),
                      // Image.asset("assets/Images/Triangles.gif",width: 100,height: 100,),
                      Padding(padding: EdgeInsets.only(top: 10)),
                      Text(
                        "GeoLens",
                        style: TextStyle(
                            color: OwnColors.color1[100],
                            fontWeight: FontWeight.bold,
                            fontSize: 25),
                      )
                    ],
                  ),
                ),
              ),
              Expanded(
                flex: 1,
                child: Column(
                  mainAxisAlignment: MainAxisAlignment.center,
                  children: [
                    CircularProgressIndicator(
                      backgroundColor: OwnColors.color2[50],
                    ),
                    Padding(
                      padding: EdgeInsets.only(top: 20),
                    ),
                    Text(
                      "Discover The Secret Of The planet \n BY ONE Click",
                      textAlign: TextAlign.center,
                      style: TextStyle(
                          color: OwnColors.color1[100],
                          fontSize: 15,
                          height: 1.5,
                          fontWeight: FontWeight.bold),
                    )
                  ],
                ),
              )
            ],
          )
        ],
      ),
    );
  }
}
