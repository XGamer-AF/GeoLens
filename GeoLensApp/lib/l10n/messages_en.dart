// DO NOT EDIT. This is code generated via package:intl/generate_localized.dart
// This is a library that provides messages for a en locale. All the
// messages from the main program should be duplicated here with the same
// function name.

import 'package:intl/intl.dart';
import 'package:intl/message_lookup_by_library.dart';

// ignore: unnecessary_new
final messages = new MessageLookup();

// ignore: unused_element
final _keepAnalysisHappy = Intl.defaultLocale;

// ignore: non_constant_identifier_names
typedef MessageIfAbsent(String message_str, List args);

class MessageLookup extends MessageLookupByLibrary {
  get localeName => 'en';

  final messages = _notInlinedMessages(_notInlinedMessages);

  static _notInlinedMessages(_) => <String, Function>{
        // *NAV BAR* ------------------------------------------------------------------------
        "home_title": MessageLookupByLibrary.simpleMessage("Home"),
        "categories_title": MessageLookupByLibrary.simpleMessage("Categories"),
        "events_title": MessageLookupByLibrary.simpleMessage("Events"),
        "remainder_title": MessageLookupByLibrary.simpleMessage("Remainder"),
        "map_title": MessageLookupByLibrary.simpleMessage("Map"),
        "more_title": MessageLookupByLibrary.simpleMessage("More"),

        // *MORE PAGE* -----------------------------------------------------------------------

        "favorite_events":
            MessageLookupByLibrary.simpleMessage("Favorite Events"),
        "change_lang": MessageLookupByLibrary.simpleMessage("Change Language"),
        "about_us": MessageLookupByLibrary.simpleMessage("About Us"),
        "team_work": MessageLookupByLibrary.simpleMessage("Team Work"),
        "help": MessageLookupByLibrary.simpleMessage("Help"),
        "change": MessageLookupByLibrary.simpleMessage("Change"),

        // OnBoarding Screen
        "getStarted_label":
            MessageLookupByLibrary.simpleMessage("GET STARTED NOW"),
        "next_label": MessageLookupByLibrary.simpleMessage("NEXT"),
        "skip_label": MessageLookupByLibrary.simpleMessage("SKIP"),

        // Language & Countries Page

        "btn_change": MessageLookupByLibrary.simpleMessage("Change"),

        // *Events PAGE* ----------------------------------------------
        "search_label": MessageLookupByLibrary.simpleMessage("Search"),
        "more_label": MessageLookupByLibrary.simpleMessage("Read More"),
        "refrences_label":
            MessageLookupByLibrary.simpleMessage("Sources & Refrences"),
        "allEvents": MessageLookupByLibrary.simpleMessage("All Events"),
        "latestEvents": MessageLookupByLibrary.simpleMessage("Latest Events"),

        // Home
        "guide": MessageLookupByLibrary.simpleMessage("Application Guide"),
        "link":
            MessageLookupByLibrary.simpleMessage("To Watch Video On Youtube"),

        // Error Messages

        "errorConnection":
            MessageLookupByLibrary.simpleMessage("Internet connection error"),
        "error_no_data":
            MessageLookupByLibrary.simpleMessage("There is no data"),
      };
}
