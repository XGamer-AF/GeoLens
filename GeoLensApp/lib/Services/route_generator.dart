import 'package:data_discovery_for_earth/Models/event_details_model.dart';
import 'package:data_discovery_for_earth/Screens/Intro.dart';
import 'package:data_discovery_for_earth/Screens/about_us.dart';
import 'package:data_discovery_for_earth/Screens/data_change_lang.dart';
import 'package:data_discovery_for_earth/Screens/event_details.dart';
import 'package:data_discovery_for_earth/Screens/event_fav.dart';
import 'package:data_discovery_for_earth/Screens/lang.dart';
import 'package:data_discovery_for_earth/Screens/main_screen.dart';
import 'package:data_discovery_for_earth/Screens/team_work.dart';
import 'package:data_discovery_for_earth/Screens/walkthrough_screen.dart';

import 'package:flutter/material.dart';

class RouteGenerator {
  static Route<dynamic> generateRoute(RouteSettings settings) {
    final args = settings.arguments;
    final  eventInfo = settings.arguments;
    final lang = settings.arguments;
    switch (settings.name) {
      case '/':
        return MaterialPageRoute(builder: (_) => MainScreen());
      case '/lang':
        return MaterialPageRoute(builder: (_) => Language());
      case '/fav':
        return MaterialPageRoute(builder: (_) => EventFav());
      case '/aboutUs':
        return MaterialPageRoute(builder: (_)=> AboutUs());
      case '/slider':
        return MaterialPageRoute(builder: (_)=> SliderHome());
      case '/team':
        return MaterialPageRoute(builder: (_)=> TeamWork());

    /*         case '/condition':
          return MaterialPageRoute(builder: (_)=> Condition());
        case'/countries':
          return MaterialPageRoute(builder: (_)=> Countries());*/
      case '/eventDetails':
        if (args is EventDetailsModel)
        {
          return MaterialPageRoute(builder: (_) => EventDetails(eventInfo,lang));
        }
        break;
      case '/intro':
        if (args is Data) {
          return MaterialPageRoute(builder: (_) => Intro(args));
        }

        return _errorRoute();
      default:
        return _errorRoute();
    }
  }

  static Route<dynamic> _errorRoute() {
    return MaterialPageRoute(builder: (_) {
      return Scaffold(
        appBar: AppBar(
          title: Text('Error '),
        ),
        body: Center(
          child: Text('Error'),
        ),
      );
    });
  }
}
